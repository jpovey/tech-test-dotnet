namespace ClearBank.DeveloperTest.Domain.PaymentSchemes.Strategies;

using Types;

public class ChapsPaymentSchemeStrategy : IPaymentSchemeStrategy
{
    //TODO - Add tests for business logic
    public bool Validate(Account account, MakePaymentRequest request)
    {
        if (account == null)
        {
            return false;
        }
        else if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps))
        {
            return false;
        }
        else if (account.Status != AccountStatus.Live)
        {
            return false;
        }

        return true;
    }
}