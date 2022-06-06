using BillingApi.PaymentGateway;
using BillingApi.PaymentGateway.Model;

namespace BillingApi.Domain.PaymentGateway.Implementations
{
  public class TransferPaymentGateway : IPaymentGateway
  {
    public const string ErrorMessage = "Bank transfer failed.";

    public PaymentResult ProcessOrder(PaymentOrder order)
    {
      return PaymentResult.CreateError(ErrorMessage);
    }
  }
}
