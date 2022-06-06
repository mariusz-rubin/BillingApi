using System.Net;
using BillingApi.Domain.Model;
using BillingApi.Domain.Services;
using BillingApi.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BillingApi.Controllers
{
  [ApiController]
  [Route("orders")]
  public partial class OrderController : ControllerBase
  {
    private IOrderProcessor _orderProcessor;

    public OrderController(IOrderProcessor orderProcessor)
    {
      _orderProcessor = orderProcessor;
    }

    [HttpPost]
    public ActionResult<OrderProcessedResponse> ProcessOrder(ProcessOrderRequest processOrderRequest)
    {      
      var order = CreateOrder(processOrderRequest);
      
      var result = _orderProcessor.Process(order);

      return CreateResponse(result);
    }

    private ActionResult<OrderProcessedResponse> CreateResponse(OrderProcessingResult result)
    {
      return result.Handle(
        onSuccess: receipt => Ok(CreateSuccessfullResponse(receipt)),
        onError: error => StatusCode((int)HttpStatusCode.InternalServerError, error));      
    }

    private static Order CreateOrder(ProcessOrderRequest processOrderRequest)
    {
      var paymentGatewayId = Enum.Parse<PaymentGatewayId>(processOrderRequest.PaymentGatewayId);
      
      return new Order(
        processOrderRequest.OrderNumber,
        processOrderRequest.UserId,
        processOrderRequest.PayableAmount,
        paymentGatewayId,
        processOrderRequest.Description);
    }

    private static OrderProcessedResponse CreateSuccessfullResponse(Domain.Model.Receipt receipt)
    {
      return new OrderProcessedResponse(
       new Dto.Receipt(
        receipt.ReceiptId,
        receipt.OrderNumber,
        receipt.TransactionId,
        receipt.CreatedAt));
    }   
  }
}
