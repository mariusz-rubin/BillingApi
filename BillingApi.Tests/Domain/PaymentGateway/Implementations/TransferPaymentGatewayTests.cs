using BillingApi.Domain.PaymentGateway.Implementations;
using NUnit.Framework;
using FluentAssertions;
using BillingApi.PaymentGateway.Model;
using BillingApi.Tests.TestUtils;

namespace BillingApi.Tests.Domain.PaymentGateway.Implementations
{
  [TestFixture]
  public class TransferPaymentGatewayTests
  {
    private TransferPaymentGateway _gateway = new TransferPaymentGateway();
    [Test]
    public void Transfer_payment_always_fail()
    {
      var paymentOrder = PaymentOrderGenerator.CreateValidPaymentOrder();

      var result = _gateway.ProcessOrder(paymentOrder);

      result.Success.Should().BeFalse();
      result.ErrorMessage.Should().NotBeNullOrEmpty();
    }
  }
}
