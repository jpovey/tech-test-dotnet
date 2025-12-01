namespace ClearBank.DeveloperTest.Domain.PaymentSchemes.Strategies;

using Account;
using Types;

public class FasterPaymentsSchemeStrategy : IPaymentSchemeStrategy
{
    //TODO - Add tests for business logic
    public bool Validate(Account account, MakePaymentRequest request)
    {
        if (account == null)
        {
            return false;
        }

        if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments))
        {
            return false;
        }

        if (account.Balance < request.Amount)
        {
            return false;
        }

        return true;
    }
}