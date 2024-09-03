public class Transaction:BaseEntity
{
  
    public Guid BankAccountId { get; set; } // Foreign key to BankAccount
    public string Reference { get; set; }
    public string Description { get; set; }
    public double Amount { get; set; }
    public string Currency { get; set; }
    public double ExchangeRate { get; set; }
    public Guid CategoryId { get; set; } // Foreign key to Category
    public Category Category { get; set; } // Navigation property
    // Navigation property
    public BankAccount BankAccount { get; set; }
    public List<GCBAttachment> Attachments { get; set; } // List of attachments
}