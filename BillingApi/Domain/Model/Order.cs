namespace BillingApi.Domain.Model
{
  public class Order
  {
    public string OrderNumber { get; }

    public string UserId { get; }
    
    public decimal PayableAmount { get; }

    public PaymentGatewayId PaymentGatewayId { get; }

    public string? Description { get; }   

    public Order(
      string orderNumber,
      string userId,
      decimal payableAmount,
      PaymentGatewayId paymentGatewayId,
      string? description)
    {
      OrderNumber = orderNumber;
      UserId = userId;
      PayableAmount = payableAmount;
      PaymentGatewayId = paymentGatewayId;
      Description = description;
    }
  }
}
