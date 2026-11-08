namespace FraudDetection.Core.Entities;

public class Transaction
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = string.Empty;
    public string CardNumberMasked { get; set; } = string.Empty;
    public string IpAddress { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty; 
    public DateTime Timestamp { get; set; }
    
    // --- Поля, которые просит TransactionsController ---
    public string Status { get; set; } = string.Empty; 
    public DateTime CreatedAtUtc { get; set; }

    public decimal RiskScore { get; set; }
    public bool IsBlocked { get; set; }
}