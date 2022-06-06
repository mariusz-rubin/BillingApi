using NUnit.Framework;
using FluentAssertions;
using FluentValidation.TestHelper;
using BillingApi.Api.Validators;
using BillingApi.Domain.Model;
using BillingApi.Dto;

namespace BillingApi.Tests.API.Validators
{
  [TestFixture]
  internal class ProcessOrderRequestValidatorTests
  {
    private ProcessOrderRequestValidator _validator = new ProcessOrderRequestValidator();

    [Test]
    public void Valid_process_order_request()
    {
      // arrange
      var request = CreateValidRequest();

      // act
      var result = _validator.Validate(request);

      // assert
      result.IsValid.Should().BeTrue();        
    }

    [Test]
    public void OrderNumber_cannot_be_empty()
    {
      // arrange
      var request = CreateValidRequest();
      request.OrderNumber = string.Empty;

      // act assert
      _validator.TestValidate(request)
        .ShouldHaveValidationErrorFor(x => x.OrderNumber);
    }

    [Test]
    public void UserId_cannot_be_empty()
    {
      // arrange
      var request = CreateValidRequest();
      request.UserId = string.Empty;

      // act assert
      _validator.TestValidate(request)
        .ShouldHaveValidationErrorFor(x => x.UserId);
    }

    [Test]
    public void PaymentGatewayId_should_be_valid()
    {
      // arrange
      var request = CreateValidRequest();
      request.PaymentGatewayId = "invalid gateway";

      // act assert
      _validator.TestValidate(request)
        .ShouldHaveValidationErrorFor(x => x.PaymentGatewayId);
    }

    [Test]
    public void PayableAmount_should_be_greather_than_zero()
    {
      // arrange
      var request = CreateValidRequest();
      request.PayableAmount = 0;

      // act assert
      _validator.TestValidate(request)
        .ShouldHaveValidationErrorFor(x => x.PayableAmount);
    }

    private static ProcessOrderRequest CreateValidRequest()
    {
      return new ProcessOrderRequest
      {
        OrderNumber = "order number",
        PayableAmount = 1,
        Description = "description",
        PaymentGatewayId = PaymentGatewayId.Transfer.ToString(),
        UserId = "userId"
      };
    }
  }
}
