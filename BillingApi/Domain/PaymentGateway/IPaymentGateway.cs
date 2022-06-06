using BillingApi.PaymentGateway.Model;

namespace BillingApi.PaymentGateway
{
  public interface IPaymentGateway
  {
    PaymentResult ProcessOrder(PaymentOrder order);
  }
}
