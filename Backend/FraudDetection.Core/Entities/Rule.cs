namespace FraudDetection.Core.Entities;

public class Rule
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    // Насколько сильно срабатывание правила повышает общий риск транзакции
    public decimal RiskWeight { get; set; } 
    public bool IsActive { get; set; }
}