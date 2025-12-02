namespace ClearBank.DeveloperTest.Tests.Domain.PaymentSchemes
{
    using System;
    using System.Collections.Generic;
    using AwesomeAssertions;
    using Types;
    using DeveloperTest.Domain.PaymentSchemes;
    using DeveloperTest.Domain.PaymentSchemes.Strategies;
    using Xunit;

    public class PaymentSchemeStrategyFactoryTests
    {
        private readonly PaymentSchemeStrategyFactory _sut = new();

        public class GetPaymentSchemeStrategy : PaymentSchemeStrategyFactoryTests
        {
            [Theory]
            [MemberData(nameof(SchemeValidationData))]
            public void GetsPaymentSchemeStrategy(PaymentScheme paymentScheme, Type expectedStrategy)
            {
                var result = _sut.GetPaymentSchemeStrategy(paymentScheme);
                result.Should().BeOfType(expectedStrategy);
            }

            public static IEnumerable<object[]> SchemeValidationData =>
                new List<object[]>
                {
                    new object[] { PaymentScheme.Bacs, typeof(BacsPaymentSchemeStrategy)  },
                    new object[] { PaymentScheme.FasterPayments, typeof(FasterPaymentsSchemeStrategy) },
                    new object[] { PaymentScheme.Chaps, typeof(ChapsPaymentSchemeStrategy) }
                };
        }
    }
}
