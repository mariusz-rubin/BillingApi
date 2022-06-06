using BillingApi.Domain.Model;
using BillingApi.Domain.PaymentGateway.Implementations;
using BillingApi.PaymentGateway;

namespace BillingApi.Domain.PaymentGateway
{
  public interface IPaymentGatewayFactory
  {
    IPaymentGateway Create(PaymentGatewayId paymentGatewayId);
  }

  public class PaymentGatewayFactory : IPaymentGatewayFactory
  {
    public IPaymentGateway Create(PaymentGatewayId paymentGatewayId)
    {
      return paymentGatewayId switch
      {
        PaymentGatewayId.Blik => new BlikPaymentGateway(),
        PaymentGatewayId.DotPay => new DotPayPaymentGateway(),
        PaymentGatewayId.Transfer => new TransferPaymentGateway(),
        _ => throw new NotSupportedException($"Payment gateway with ID: '{paymentGatewayId}' is not supported.")
      };
    }
  }
}
