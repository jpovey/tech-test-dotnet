namespace ClearBank.DeveloperTest.Domain.PaymentSchemes.Strategies;

using Account;
using Types;

public class ChapsPaymentSchemeStrategy : IPaymentSchemeStrategy
{
    //TODO - Add tests for business logic
    public bool Validate(Account account, MakePaymentRequest request)
    {
        if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps))
        {
            return false;
        }

        if (account.Status != AccountStatus.Live)
        {
            return false;
        }

        return true;
    }
}