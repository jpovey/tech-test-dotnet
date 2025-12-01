namespace ClearBank.DeveloperTest.Domain.PaymentSchemes;

using Types;

public interface IPaymentSchemeStrategyFactory
{
    IPaymentSchemeStrategy GetPaymentSchemeStrategy(PaymentScheme paymentScheme);
}