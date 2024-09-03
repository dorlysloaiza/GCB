public class BankAccount:BaseEntity
{

    public string AccountNumber { get; set; }
    public string BankName { get; set; }
    public string Currency { get; set; }
    public decimal InitialBalance { get; set; }
    public decimal CurrentBalance { get; set; }
    public string AccountHolder { get; set; }
    public DateTime LastReconciliationDate { get; set; }
    // Navigation property
    public List<Transaction> Transactions { get; set; }
}