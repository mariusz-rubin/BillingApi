using BillingApi.Domain.Model;
using BillingApi.Domain.PaymentGateway;
using BillingApi.PaymentGateway.Model;

namespace BillingApi.Domain.Services
{
  public interface IOrderProcessor
  {
    OrderProcessingResult Process(Order order);
  }

  public class OrderProcessor : IOrderProcessor
  {
    private readonly IPaymentGatewayFactory _paymentGatewayFactory;
    
    public OrderProcessor(IPaymentGatewayFactory paymentGatewayFactory)
    {
      _paymentGatewayFactory = paymentGatewayFactory;
    }

    public OrderProcessingResult Process(Order order)
    {
      var paymentGateway = _paymentGatewayFactory.Create(order.PaymentGatewayId);

      var paymentOrder = CreatePaymentOrder(order);

      return paymentGateway.ProcessOrder(paymentOrder)
        .Handle(
          onSuccess: transactionId => OnPaymentSuccess(transactionId, order),
          onError: error => OnPaymentFailed(error));
    }

    private OrderProcessingResult OnPaymentFailed(string error)
    {
      return OrderProcessingResult.CreateError($"Payment gateway failed with error: {error}");
    }

    private OrderProcessingResult OnPaymentSuccess(string transactionId, Order order)
    {
      var receipt = Receipt.CreateNew(order.OrderNumber, transactionId);

      return OrderProcessingResult.CreateSuccess(receipt);
    }

    private static PaymentOrder CreatePaymentOrder(Order order)
    {
      return new PaymentOrder(
        order.OrderNumber,
        order.UserId,
        order.PayableAmount,
        order.Description);
    }    
  }
}
