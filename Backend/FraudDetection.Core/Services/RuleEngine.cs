using FraudDetection.Core.Entities;

namespace FraudDetection.Core.Services;

public class RuleEngine
{
    public string EvaluateTransaction(Transaction transaction, IEnumerable<Rule> activeRules)
    {
        // Проходимся по всем правилам, которые менеджер создал в админке
        foreach (var rule in activeRules)
        {
            if (IsRuleMatch(transaction, rule))
            {
                // Если правило сработало, сразу возвращаем его действие (например, "Block" или "Review")
                return rule.Action;
            }
        }

        // Если ни одно правило не сработало, значит транзакция чистая
        return "Approved";
    }

    private bool IsRuleMatch(Transaction transaction, Rule rule)
    {
        // 1. Достаем значение из транзакции в зависимости от того, какое поле указано в правиле
        object? transactionValue = rule.FieldName switch
        {
            "Amount" => transaction.Amount,
            "Country" => transaction.Country,
            "IpAddress" => transaction.IpAddress,
            "Currency" => transaction.Currency,
            _ => null
        };

        if (transactionValue == null) return false;

        // 2. Логика для ЧИСЕЛ (например, если правило проверяет сумму перевода)
        if (transactionValue is decimal amountVal && decimal.TryParse(rule.Value, out decimal ruleAmount))
        {
            return rule.Operator switch
            {
                ">" => amountVal > ruleAmount,
                "<" => amountVal < ruleAmount,
                "==" => amountVal == ruleAmount,
                ">=" => amountVal >= ruleAmount,
                "<=" => amountVal <= ruleAmount,
                _ => false
            };
        }
        
        // 3. Логика для ТЕКСТА (например, если проверяем страну "CZ" или валюту "USD")
        if (transactionValue is string stringVal)
        {
            return rule.Operator switch
            {
                "==" => stringVal.Equals(rule.Value, StringComparison.OrdinalIgnoreCase),
                "!=" => !stringVal.Equals(rule.Value, StringComparison.OrdinalIgnoreCase),
                _ => false
            };
        }

        return false;
    }
}