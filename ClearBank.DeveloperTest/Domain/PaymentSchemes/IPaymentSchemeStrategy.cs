namespace ClearBank.DeveloperTest.Domain.PaymentSchemes;

using Account;
using Types;

public interface IPaymentSchemeStrategy
{
    bool Validate(Account account, MakePaymentRequest request);
}