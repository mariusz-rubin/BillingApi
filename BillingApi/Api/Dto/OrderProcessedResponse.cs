namespace BillingApi.Dto
{
  public class OrderProcessedResponse
  {
    public Receipt Receipt { get; }

    public OrderProcessedResponse(Receipt receipt)
    {
      Receipt = receipt;
    }
  }
}
