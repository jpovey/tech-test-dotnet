namespace ClearBank.DeveloperTest.Domain.PaymentSchemes;

using System;
using Strategies;
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