using FraudDetection.Core.Entities;
using FraudDetection.Core.Services;
using FraudDetection.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FraudDetection.Api.Controllers;

[ApiController] // Говорит .NET, что это API-контроллер (он сам будет парсить JSON)
[Route("api/[controller]")] // Путь будет: /api/transactions
public class TransactionsController : ControllerBase
{
    private readonly AppDbContext _dbContext;
    private readonly RuleEngine _ruleEngine;

    // Внедрение зависимостей (DI) - .NET сам подсунет сюда базу и движок
    public TransactionsController(AppDbContext dbContext, RuleEngine ruleEngine)
    {
        _dbContext = dbContext;
        _ruleEngine = ruleEngine;
    }

    [HttpPost("evaluate")] // Полный путь: POST /api/transactions/evaluate
    public async Task<IActionResult> EvaluateTransaction([FromBody] Transaction transaction)
    {
        // 1. Достаем все активные правила из нашей БД PostgreSQL
        var activeRules = await _dbContext.Rules.Where(r => r.IsActive).ToListAsync();

        // 2. Отдаем транзакцию в движок на проверку
        transaction.Status = _ruleEngine.EvaluateTransaction(transaction, activeRules);
        
        // Обновляем время на всякий случай
        transaction.CreatedAtUtc = DateTime.UtcNow;

        // 3. Сохраняем саму транзакцию в базу (для истории и отчетов менеджеру)
        _dbContext.Transactions.Add(transaction);
        await _dbContext.SaveChangesAsync();

        // 4. Отдаем красивый JSON ответ обратно на фронтенд
        return Ok(new 
        { 
            TransactionId = transaction.Id, 
            Status = transaction.Status,
            CheckedRulesCount = activeRules.Count
        });
    }
}