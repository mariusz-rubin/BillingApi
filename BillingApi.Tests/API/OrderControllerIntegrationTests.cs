using System.Net.Http.Json;
using BillingApi.Dto;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using FluentAssertions;
using BillingApi.Domain.Model;
using System.Net;
using BillingApi.Domain.PaymentGateway.Implementations;

namespace BillingApi.Tests.API
{
  [TestFixture]
  internal class OrderControllerIntegrationTests
  {
    WebApplicationFactory<Program> _webAppFactory;    
    
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
      _webAppFactory = new WebApplicationFactory<Program>();      
    }

    [Test]
    public async Task Succefully_process_payment_order()
    {
      // arrange
      using var apiClient = _webAppFactory.CreateClient();

      var request = CreateValidProcessOrderRequest();

      // act
      var httpResponse = await apiClient.PostAsync("orders", JsonContent.Create(request));
      
      // assert
      httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
      
      var processResponse = await httpResponse.Content.ReadFromJsonAsync<OrderProcessedResponse>();
      processResponse.Should().NotBeNull();
      processResponse!.Receipt.Should().NotBeNull();
      processResponse.Receipt.OrderNumber.Should().Be(request.OrderNumber);
    }

    [Test]
    public async Task Fails_with_BadRequest_on_invalid_payment_gateway_ID()
    {
      // arrange
      using var apiClient = _webAppFactory.CreateClient();

      var request = CreateValidProcessOrderRequest();
      request.PaymentGatewayId = "invalid ID";

      // act
      var httpResponse = await apiClient.PostAsync("orders", JsonContent.Create(request));

      // assert
      httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Test]
    public async Task Fails_with_InternalServerError_when_payment_gateway_fails()
    {
      // arrange
      var apiClient = _webAppFactory.CreateClient();
      var request = CreateValidProcessOrderRequest();
      request.PaymentGatewayId = PaymentGatewayId.Transfer.ToString(); // currently all tranfers are implemented to fail.

      // act
      var httpResponse = await apiClient.PostAsync("orders", JsonContent.Create(request));

      // assert
      httpResponse.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
      
      var errorMessage = await httpResponse.Content.ReadAsStringAsync();
      errorMessage.Should().Contain(TransferPaymentGateway.ErrorMessage);
    }

    private static ProcessOrderRequest CreateValidProcessOrderRequest()
    {
      return new ProcessOrderRequest
      {
        OrderNumber = "order number",
        UserId = "user id",
        Description = "description",
        PayableAmount = 10,
        PaymentGatewayId = PaymentGatewayId.Blik.ToString()
      };
    }
  }
}
