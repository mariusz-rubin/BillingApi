using BillingApi.PaymentGateway;
using BillingApi.PaymentGateway.Model;

namespace BillingApi.Domain.PaymentGateway.Implementations
{
  public class DotPayPaymentGateway : IPaymentGateway
  {
    public PaymentResult ProcessOrder(PaymentOrder order)
    {
      return PaymentResult.CreateSuccess($"dotPay_{Guid.NewGuid()}");
    }
  }
}
