public class Category:BaseEntity
{
    public string Name { get; set; }
    public bool IsIncome { get; set; } // True if income, false if expense
}