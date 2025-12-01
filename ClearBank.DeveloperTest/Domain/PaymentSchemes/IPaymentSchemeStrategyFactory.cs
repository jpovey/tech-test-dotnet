namespace ClearBank.DeveloperTest.Domain.PaymentSchemes;

using Types;

public interface IPaymentSchemeStrategyFactory
{
    IPaymentSchemeStrategy GetPaymentSchemeStrategy(PaymentScheme paymentScheme);
}

//TODO - Implement
public class PaymentSchemeStrategyFactory : IPaymentSchemeStrategyFactory
{
    public IPaymentSchemeStrategy GetPaymentSchemeStrategy(PaymentScheme paymentScheme)
    {
        throw new System.NotImplementedException();
    }
}