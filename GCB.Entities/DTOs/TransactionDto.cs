public class TransactionDto
{
  
      public Guid Id { get; set; }
    public Guid BankAccountId { get; set; }
    public string Reference { get; set; }
    public string Description { get; set; }
    public double Amount { get; set; }
    public string Currency { get; set; }
    public double ExchangeRate { get; set; }
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; } 
    public string BankAccountName { get; set; } 
   
}