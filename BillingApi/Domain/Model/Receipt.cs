namespace BillingApi.Domain.Model
{
  public class Receipt
  {
    public Guid ReceiptId { get; }

    public string OrderNumber { get; }    
   
    public string TransactionId { get; }
    
    public DateTime CreatedAt { get; }    

    private Receipt(
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

    public static Receipt CreateNew(string orderNumber, string transactionId)
    {
      return new Receipt(
        Guid.NewGuid(),
        orderNumber,
        transactionId,
        DateTime.Now);
    }
  }
}
