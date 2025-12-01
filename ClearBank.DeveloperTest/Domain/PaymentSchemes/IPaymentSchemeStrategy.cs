namespace ClearBank.DeveloperTest.Domain.PaymentSchemes;

using Types;

//TODO - Implement
public interface IPaymentSchemeStrategy
{
    bool Validate(Account account, MakePaymentRequest request);
}

public class BacsPaymentSchemeStrategy : IPaymentSchemeStrategy
{
    public bool Validate(Account account, MakePaymentRequest request)
    {
        throw new System.NotImplementedException();
    }
}

public class FasterPaymentsSchemeStrategy : IPaymentSchemeStrategy
{
    public bool Validate(Account account, MakePaymentRequest request)
    {
        throw new System.NotImplementedException();
    }
}

public class ChapsPaymentSchemeStrategy : IPaymentSchemeStrategy
{
    public bool Validate(Account account, MakePaymentRequest request)
    {
        throw new System.NotImplementedException();
    }
}


//switch (request.PaymentScheme)
//{
//    case PaymentScheme.Bacs:
//        if (account == null)
//        {
//            result.Success = false;
//        }
//        else if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs))
//        {
//            result.Success = false;
//        }
//        break;

//    case PaymentScheme.FasterPayments:
//        if (account == null)
//        {
//            result.Success = false;
//        }
//        else if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments))
//        {
//            result.Success = false;
//        }
//        else if (account.Balance < request.Amount)
//        {
//            result.Success = false;
//        }
//        break;

//    case PaymentScheme.Chaps:
//        if (account == null)
//        {
//            result.Success = false;
//        }
//        else if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps))
//        {
//            result.Success = false;
//        }
//        else if (account.Status != AccountStatus.Live)
//        {
//            result.Success = false;
//        }
//        break;
//}