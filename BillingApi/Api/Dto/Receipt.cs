namespace BillingApi.Dto
{
  public class Receipt
  {
    public Guid ReceiptId { get; }

    public string OrderNumber { get; }    

    public string TransactionId { get; }    

    public DateTime CreatedAt { get; }    

    public Receipt(
      Guid receiptId,
      string orderNumber,      
      string transactionId,      
      DateTime createdAt)
    {
      ReceiptId = receiptId;
      OrderNumber = orderNumber;      
      TransactionId = transactionId;      
      CreatedAt = createdAt;
    }
  }  
}
