using BillingApi.Domain.Model;
using BillingApi.Dto;
using FluentValidation;

namespace BillingApi.Api.Validators
{
  public class ProcessOrderRequestValidator : AbstractValidator<ProcessOrderRequest>
  {
    public ProcessOrderRequestValidator()
    {
      RuleFor(x => x.OrderNumber)
        .NotEmpty();

      RuleFor(x => x.UserId)
        .NotEmpty();      

      RuleFor(x => x.PayableAmount)
        .GreaterThan(0);

      RuleFor(x => x.PaymentGatewayId)
        .IsEnumName(typeof(PaymentGatewayId))
        .WithMessage(req => $"Invalid paymentGatewayId: '{req.PaymentGatewayId}'. Available values: {GetAvailablePaymentGatewayIds()}" );        
    }

    private static string GetAvailablePaymentGatewayIds()
    {
      var ids = Enum.GetValues<PaymentGatewayId>().Select(x => x.ToString());

      return string.Join(", ", ids);
    }
  }
}
