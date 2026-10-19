namespace FraudDetection.Core.Entities;

public class Rule
{
    public Guid Id { get; set; }
    
    // Название правила в админке (например: "Блокировка крупных переводов")
    public string Name { get; set; } = string.Empty; 
    
    // Какое свойство транзакции проверяем (например: "Amount", "Country", "IpAddress")
    public string FieldName { get; set; } = string.Empty; 
    
    // Математический или логический оператор (">", "<", "==", "!=")
    public string Operator { get; set; } = string.Empty; 
    
    // С чем сравниваем (например: "50000" или "NG")
    public string Value { get; set; } = string.Empty; 
    
    // Действие при срабатывании правила (например: "Block", "RequireSMS", "Review")
    public string Action { get; set; } = string.Empty; 
    
    // Активно ли правило (менеджер может его временно выключить галочкой)
    public bool IsActive { get; set; } = true; 
}