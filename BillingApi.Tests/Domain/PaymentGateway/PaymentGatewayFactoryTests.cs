using BillingApi.Domain.Model;
using BillingApi.Domain.PaymentGateway;
using BillingApi.Domain.PaymentGateway.Implementations;
using NUnit.Framework;
using FluentAssertions;

namespace BillingApi.Tests.Domain.PaymentGateway
{
  [TestFixture]
  public class PaymentGatewayFactoryTests
  {
    private PaymentGatewayFactory _factory = new PaymentGatewayFactory();

    [TestCase(PaymentGatewayId.Blik, typeof(BlikPaymentGateway))]
    [TestCase(PaymentGatewayId.DotPay, typeof(DotPayPaymentGateway))]
    [TestCase(PaymentGatewayId.Transfer, typeof(TransferPaymentGateway))]
    public void Creates_expected_gateway(PaymentGatewayId paymentGatewayId, Type expectedGatewayType)
    {
      // act
      var paymentGateway = _factory.Create(paymentGatewayId);

      // assert
      paymentGateway.Should().BeOfType(expectedGatewayType);
    }

    [Test]
    public void Fails_on_not_supported_payment_gateway_ID()
    {
      // arrange
      var notSupportedPaymentGatewayId = (PaymentGatewayId)100;
            
      Action createGateway = () => _factory.Create(notSupportedPaymentGatewayId);

      // act assert
      createGateway.Should().Throw<NotSupportedException>();
    }
  }
}
