namespace FraudDetection.Core.Entities;

public class Rule
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    // Новые поля, которые требует RuleEngine
    public string FieldName { get; set; } = string.Empty; // Например: "Amount", "Country"
    public string Operator { get; set; } = string.Empty;  // Например: ">", "==", "Contains"
    public string Value { get; set; } = string.Empty;     // Например: "10000", "CZ"
    public string Action { get; set; } = string.Empty;    // Например: "Block", "Flag"
    
    public decimal RiskWeight { get; set; } 
    public bool IsActive { get; set; }
}