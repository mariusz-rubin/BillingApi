# Assumptions

- Orders and Receipts are not persisted
- Each succesfull call to Payment Gateway returns unique transaction ID
- Errors from Payment Gateways are returned by the API with HttpStatusCode 500 (InternalServerError)
- Each created Receipt contains unique ID, transaction ID, creation date and order number.
- Three PaymentGateways stubs are implemented:
    - Blik - always succeed
    - DotPay - always succeed
    - Transfer - always fail
- Payable amount must be greather than 0
- No currency and taxes are specified in Order and Receipt