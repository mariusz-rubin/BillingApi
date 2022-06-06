namespace BillingApi.PaymentGateway.Model
{
  public class PaymentOrder
  {
    public string OrderNumber { get; }

    public string UserId { get; }

    public decimal PayableAmount { get; }

    public string? Description { get; }

    public PaymentOrder(
      string orderNumber,
      string userId,
      decimal payableAmount,
      string? description)
    {
      OrderNumber = orderNumber;
      UserId = userId;
      PayableAmount = payableAmount;
      Description = description;
    }
  }
}
