using BillingApi.Api.Validators;
using BillingApi.Domain.PaymentGateway;
using BillingApi.Domain.Services;
using FluentValidation.AspNetCore;

public class Program
{
  public static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services
      .AddMvcCore()
      .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ProcessOrderRequestValidator>());

    builder.Services.AddTransient<IOrderProcessor, OrderProcessor>();
    builder.Services.AddSingleton<IPaymentGatewayFactory, PaymentGatewayFactory>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
      app.UseSwagger();
      app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
  }
}