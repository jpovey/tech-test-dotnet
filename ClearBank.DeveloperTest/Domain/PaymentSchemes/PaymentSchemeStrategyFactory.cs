namespace ClearBank.DeveloperTest.Domain.PaymentSchemes;

using System;
using Types;

public class PaymentSchemeStrategyFactory : IPaymentSchemeStrategyFactory
{
    public IPaymentSchemeStrategy GetPaymentSchemeStrategy(PaymentScheme paymentScheme)
    {
        return paymentScheme switch
        {
            PaymentScheme.Bacs => new BacsPaymentSchemeStrategy(),
            PaymentScheme.FasterPayments => new FasterPaymentsSchemeStrategy(),
            PaymentScheme.Chaps => new ChapsPaymentSchemeStrategy(),
            _ => throw new ArgumentOutOfRangeException(nameof(paymentScheme))
        };
    }
}