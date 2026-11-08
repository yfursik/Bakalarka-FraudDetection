namespace FraudDetection.Core.Entities;

public class Transaction
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = string.Empty;
    public string CardNumberMasked { get; set; } = string.Empty;
    public string IpAddress { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    
    // Результаты оценки риска
    public decimal RiskScore { get; set; }
    public bool IsBlocked { get; set; }
}