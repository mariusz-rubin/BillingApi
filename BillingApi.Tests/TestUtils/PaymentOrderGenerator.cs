using BillingApi.PaymentGateway.Model;

namespace BillingApi.Tests.TestUtils
{
  internal class PaymentOrderGenerator
  {
    public static PaymentOrder CreateValidPaymentOrder()
    {
      return new PaymentOrder(
        "number",
        "userId",
        payableAmount: 1,
        "description");
    }
  }
}
