namespace BillingApi.Dto
{
  public class ProcessOrderRequest
  {
    public string OrderNumber { get; set; } = string.Empty;

    public string UserId { get; set; } = string.Empty;

    public decimal PayableAmount { get; set; }

    public string PaymentGatewayId { get; set; } = string.Empty;

    public string? Description { get; set; }
  }
}
