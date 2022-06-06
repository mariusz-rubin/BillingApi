using BillingApi.PaymentGateway;
using BillingApi.PaymentGateway.Model;

namespace BillingApi.Domain.PaymentGateway.Implementations
{
  public class BlikPaymentGateway : IPaymentGateway
  {
    public PaymentResult ProcessOrder(PaymentOrder order)
    {
      return PaymentResult.CreateSuccess($"blik_{Guid.NewGuid()}");
    }
  }
}
