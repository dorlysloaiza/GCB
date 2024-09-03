public class GCBAttachment:BaseEntity
{
  
    public int TransactionId { get; set; } // Foreign key to Transaction
    public string FileName { get; set; } // Name of the file
    public byte[] FileContent { get; set; } // Content of the file

}