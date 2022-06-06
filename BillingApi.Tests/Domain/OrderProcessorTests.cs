using BillingApi.Domain.Model;
using BillingApi.Domain.Services;
using NUnit.Framework;
using FluentAssertions;
using BillingApi.Domain.PaymentGateway;
using Moq;
using BillingApi.PaymentGateway;
using BillingApi.PaymentGateway.Model;

namespace BillingApi.Tests
{
  [TestFixture]
  public class OrderProcessorTests
  {
    private OrderProcessor _orderProcessor;
    private Mock<IPaymentGatewayFactory> _factoryMock;
    private Mock<IPaymentGateway> _paymentGatewayMock;

    [SetUp]
    public void SetUp()
    {
      _factoryMock = new Mock<IPaymentGatewayFactory>();
      _paymentGatewayMock = new Mock<IPaymentGateway>();
      _orderProcessor = new OrderProcessor(_factoryMock.Object);

      _factoryMock
        .Setup(x => x.Create(It.IsAny<PaymentGatewayId>()))
        .Returns(_paymentGatewayMock.Object);
    }

    [Test]
    public void Calls_payment_gateway_with_order_data()
    {
      // arrange
      var order = CreateOrder();
      
      _paymentGatewayMock
        .Setup(x => x.ProcessOrder(It.IsAny<PaymentOrder>()))
        .Returns(PaymentResult.CreateSuccess(Guid.NewGuid().ToString()));

      // act
      var result = _orderProcessor.Process(order);

      // assert
      _paymentGatewayMock.Verify(
        x => x.ProcessOrder(
          It.Is<PaymentOrder>(paymentOrder =>
            paymentOrder.OrderNumber == order.OrderNumber
            && paymentOrder.PayableAmount == order.PayableAmount
            && paymentOrder.UserId == order.UserId
            && paymentOrder.Description == order.Description)));
    }

    [Test]
    public void On_payment_success_creates_receipt_with_transaction_id()
    {
      // arrange
      var expectedTransactionId = Guid.NewGuid().ToString();
           var order = CreateOrder();

      _paymentGatewayMock
        .Setup(x => x.ProcessOrder(It.IsAny<PaymentOrder>()))
        .Returns(PaymentResult.CreateSuccess(expectedTransactionId));

      // act
      var result = _orderProcessor.Process(order);

      // assert
      result.Success.Should().BeTrue();
      result.Receipt.Should().NotBeNull();
      result.Receipt!.TransactionId.Should().Be(expectedTransactionId);
      result.Receipt.OrderNumber.Should().Be(order.OrderNumber);      
    }

    [Test]
    public void On_payment_error_returns_error_from_gateway()
    {
      // arrange
      var expectedError = "Payment failed";      

      var order = CreateOrder();

      _factoryMock
        .Setup(x => x.Create(order.PaymentGatewayId))
        .Returns(_paymentGatewayMock.Object);

      _paymentGatewayMock
        .Setup(x => x.ProcessOrder(It.IsAny<PaymentOrder>()))
        .Returns(PaymentResult.CreateError(expectedError));

      // act
      var result = _orderProcessor.Process(order);

      // assert
      result.Success.Should().BeFalse();
      result.Error.Should().Contain(expectedError);
    }

    private static Order CreateOrder()
    {
      return new Order(
        "orderNumber", 
        "userId", 
        payableAmount: 100, 
        PaymentGatewayId.Blik, 
        "description");
    }
  }
}