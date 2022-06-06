using BillingApi.Domain.Model;

namespace BillingApi.Domain.Services
{
  public class OrderProcessingResult
  {
    public bool Success { get; }

    public Receipt? Receipt { get; }

    public string? Error { get; }

    private OrderProcessingResult(
      bool success, 
      Receipt? receipt = null, 
      string? error = null)
    {
      Success = success;
      Receipt = receipt;
      Error = error;
    }

    public static OrderProcessingResult CreateSuccess(Receipt receipt) 
      => new OrderProcessingResult(true, receipt);

    public static OrderProcessingResult CreateError(string error) 
      => new OrderProcessingResult(false, error: error);

    public T Handle<T>(Func<Receipt, T> onSuccess, Func<string, T> onError)
    {
      return Success
        ? onSuccess(Receipt!)
        : onError(Error!);
    }
  }
}
