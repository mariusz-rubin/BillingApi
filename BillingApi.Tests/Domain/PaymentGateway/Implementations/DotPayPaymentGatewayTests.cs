using BillingApi.Domain.PaymentGateway.Implementations;
using NUnit.Framework;
using FluentAssertions;
using BillingApi.Tests.TestUtils;

namespace BillingApi.Tests.Domain.PaymentGateway.Implementations
{
  [TestFixture]
  public class DotPayPaymentGatewayTests
  {
    private DotPayPaymentGateway _gateway = new DotPayPaymentGateway();

    [Test]
    public void DotPay_payment_always_success()
    {
      // arrange
      var paymentOrder = PaymentOrderGenerator.CreateValidPaymentOrder();

      // act
      var result = _gateway.ProcessOrder(paymentOrder);

      // assert
      result.Success.Should().BeTrue();
      result.TransactionId.Should().NotBeNullOrEmpty();
    }
  }
}
