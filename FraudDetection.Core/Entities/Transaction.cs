namespace FraudDetection.Core.Entities;

public class Transaction
{
    public Guid Id { get; set; }
    
    // Сумма перевода
    public decimal Amount { get; set; } 
    
    // Валюта перевода (CZK, EUR, USD)
    public string Currency { get; set; } = string.Empty;
    
    // Кто отправляет (например, номер счета или ID юзера)
    public string SenderId { get; set; } = string.Empty;
    
    // Куда отправляют
    public string ReceiverAccount { get; set; } = string.Empty;
    
    // IP-адрес отправителя (очень важно для fraud-систем)
    public string IpAddress { get; set; } = string.Empty;
    
    // Страна, из которой идет запрос
    public string Country { get; set; } = string.Empty;
    
    // Время создания транзакции (в UTC, чтобы избежать проблем с часовыми поясами)
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    
    // Результат проверки (будет заполняться нашим движком: "Approved", "Blocked" и т.д.)
    public string Status { get; set; } = "Pending";
}