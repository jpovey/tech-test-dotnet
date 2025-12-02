namespace ClearBank.DeveloperTest.Domain.PaymentSchemes;

using Strategies;
using System.Collections.Generic;
using Types;

public class PaymentSchemeStrategyFactory : IPaymentSchemeStrategyFactory
{
    private readonly IReadOnlyDictionary<PaymentScheme, IPaymentSchemeStrategy> _strategies = new Dictionary<PaymentScheme, IPaymentSchemeStrategy>
    {
        { PaymentScheme.Bacs, new BacsPaymentSchemeStrategy() },
        { PaymentScheme.FasterPayments, new FasterPaymentsSchemeStrategy() },
        { PaymentScheme.Chaps, new ChapsPaymentSchemeStrategy() }
    };

    public IPaymentSchemeStrategy GetPaymentSchemeStrategy(PaymentScheme scheme)
    {
        return _strategies[scheme];
    }
}