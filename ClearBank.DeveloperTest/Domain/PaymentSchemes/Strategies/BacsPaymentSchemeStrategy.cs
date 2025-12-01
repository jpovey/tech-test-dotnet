namespace ClearBank.DeveloperTest.Domain.PaymentSchemes.Strategies;

using Account;
using Types;

public class BacsPaymentSchemeStrategy : IPaymentSchemeStrategy
{
    //TODO - Add tests for business logic
    public bool Validate(Account account, MakePaymentRequest request)
    {
        if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs))
        {
            return false;
        }

        return true;
    }
}