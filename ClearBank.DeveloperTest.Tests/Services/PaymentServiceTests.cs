namespace ClearBank.DeveloperTest.Tests.Services
{
    using AutoFixture;
    using AwesomeAssertions;
    using DeveloperTest.Data;
    using DeveloperTest.Domain.PaymentSchemes;
    using DeveloperTest.Services;
    using NSubstitute;
    using Types;
    using Xunit;

    public class PaymentServiceTests
    {
        private static readonly Fixture Fixture = new();     

        private readonly IAccountDataStoreProvider _accountDataStoreProvider;
        private readonly IAccountDataStore _accountDataStore;
        private readonly IPaymentSchemeStrategyFactory _paymentSchemeStrategyFactory;
        private readonly IPaymentSchemeStrategy _paymentSchemeStrategy;
        private readonly PaymentService _sut;
        private readonly MakePaymentRequest _makePaymentRequest;
        private readonly Account _account;
        private readonly bool _paymentIsValid;

        public PaymentServiceTests()
        {
            _makePaymentRequest = Fixture.Create<MakePaymentRequest>();
            _account = Fixture.Create<Account>();
            _paymentIsValid = Fixture.Create<bool>();

            _accountDataStoreProvider = Substitute.For<IAccountDataStoreProvider>();
            _accountDataStore = Substitute.For<IAccountDataStore>();
            _paymentSchemeStrategyFactory = Substitute.For<IPaymentSchemeStrategyFactory>();
            _paymentSchemeStrategy = Substitute.For<IPaymentSchemeStrategy>();

            _accountDataStoreProvider.ProvideAccountDataStore().Returns(_accountDataStore);
            _accountDataStore.GetAccount(_makePaymentRequest.DebtorAccountNumber).Returns(_account);
            _paymentSchemeStrategyFactory.GetPaymentSchemeStrategy(_makePaymentRequest.PaymentScheme).Returns(_paymentSchemeStrategy);
            _paymentSchemeStrategy.Validate(_account, _makePaymentRequest).Returns(_paymentIsValid);

            _sut = new PaymentService(_accountDataStoreProvider, _paymentSchemeStrategyFactory);
        }

        public class MakePayment : PaymentServiceTests
        {
            [Fact]
            public void SelectsAccountDataStore()
            {
                _sut.MakePayment(_makePaymentRequest);

                _accountDataStoreProvider.Received(1).ProvideAccountDataStore();
            }

            [Fact]
            public void GetsAccountFromAccountDataStore()
            {
                _sut.MakePayment(_makePaymentRequest);

                _accountDataStore.Received(1).GetAccount(_makePaymentRequest.DebtorAccountNumber);
            }

            [Fact]
            public void GetPaymentSchemeStrategy()
            {
                _sut.MakePayment(_makePaymentRequest);

                _paymentSchemeStrategyFactory.Received(1).GetPaymentSchemeStrategy(_makePaymentRequest.PaymentScheme);
            }

            [Fact]
            public void ValidatePayment()
            {
                _sut.MakePayment(_makePaymentRequest);

                _paymentSchemeStrategy.Received(1).Validate(_account, _makePaymentRequest);
            }

            [Fact]
            public void SetsTheSuccessOfMakePaymentResult()
            {
                var result = _sut.MakePayment(_makePaymentRequest);

                result.Success.Should().Be(_paymentIsValid);
            }

            [Fact]
            public void DoesUpdateAccount_GivenValidationSuccess()
            {
                _paymentSchemeStrategy.Validate(_account, _makePaymentRequest).Returns(true);

                _sut.MakePayment(_makePaymentRequest);

                _accountDataStore.Received(1).UpdateAccount(_account);
            }

            [Fact]
            public void DoesNotUpdateAccount_GivenValidationFailure()
            {
                _paymentSchemeStrategy.Validate(_account, _makePaymentRequest).Returns(false);

                _sut.MakePayment(_makePaymentRequest);

                _accountDataStore.Received(0).UpdateAccount(_account);
            }
        }
    }
}
