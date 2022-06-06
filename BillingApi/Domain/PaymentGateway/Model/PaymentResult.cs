namespace BillingApi.PaymentGateway.Model
{
  public class PaymentResult
  {
    public bool Success { get; }

    public string? TransactionId { get; }

    public string? ErrorMessage { get; }

    private PaymentResult(bool success, string? transactionId = null, string? errorMessage = null)
    {
      Success = success;
      TransactionId = transactionId;
      ErrorMessage = errorMessage;
    }

    public static PaymentResult CreateSuccess(string transactionId) 
      => new PaymentResult(true, transactionId: transactionId);

    public static PaymentResult CreateError(string errorMessage) 
      => new PaymentResult(false, errorMessage: errorMessage);

    public T Handle<T>(Func<string, T> onSuccess, Func<string, T> onError)
    {
      return Success
        ? onSuccess(TransactionId!)
        : onError(ErrorMessage!);
    }
  }
}
