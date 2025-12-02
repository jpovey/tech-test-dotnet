namespace ClearBank.DeveloperTest.Tests.Services
{
    using AutoFixture;
    using AwesomeAssertions;
    using DeveloperTest.Data;
    using DeveloperTest.Domain.Account;
    using DeveloperTest.Domain.PaymentSchemes;
    using DeveloperTest.Services;
    using NSubstitute;
    using NSubstitute.ReturnsExtensions;
    using Types;
    using Xunit;

    public class PaymentServiceTests
    {
        private static readonly Fixture Fixture = new();     

        private readonly IAccountDataStoreProvider _accountDataStoreProvider;
        private readonly IAccountDataStore _accountDataStore;
        private readonly IPaymentSchemeStrategyFactory _paymentSchemeStrategyFactory;
        private readonly IPaymentSchemeStrategy _paymentSchemeStrategy;
        private readonly IAccountManager _accountManager;
        private readonly PaymentService _sut;
        private readonly MakePaymentRequest _request;
        private readonly Account _account;
        private readonly bool _paymentIsValid;

        public PaymentServiceTests()
        {
            _request = Fixture.Create<MakePaymentRequest>();
            _account = Fixture.Create<Account>();
            _paymentIsValid = Fixture.Create<bool>();

            _accountDataStoreProvider = Substitute.For<IAccountDataStoreProvider>();
            _accountDataStore = Substitute.For<IAccountDataStore>();
            _paymentSchemeStrategyFactory = Substitute.For<IPaymentSchemeStrategyFactory>();
            _paymentSchemeStrategy = Substitute.For<IPaymentSchemeStrategy>();
            _accountManager = Substitute.For<IAccountManager>();

            _accountDataStoreProvider.ProvideAccountDataStore().Returns(_accountDataStore);
            _accountDataStore.GetAccount(_request.DebtorAccountNumber).Returns(_account);
            _paymentSchemeStrategyFactory.GetPaymentSchemeStrategy(_request.PaymentScheme).Returns(_paymentSchemeStrategy);
            _paymentSchemeStrategy.Validate(_account, _request).Returns(_paymentIsValid);

            _sut = new PaymentService(_accountDataStoreProvider, _paymentSchemeStrategyFactory, _accountManager);
        }

        public class MakePayment : PaymentServiceTests
        {
            [Fact]
            public void SelectsAccountDataStore()
            {
                _sut.MakePayment(_request);

                _accountDataStoreProvider.Received(1).ProvideAccountDataStore();
            }

            [Fact]
            public void GetsAccountFromAccountDataStore()
            {
                _sut.MakePayment(_request);

                _accountDataStore.Received(1).GetAccount(_request.DebtorAccountNumber);
            }

            [Fact]
            public void GetPaymentSchemeStrategy()
            {
                _sut.MakePayment(_request);

                _paymentSchemeStrategyFactory.Received(1).GetPaymentSchemeStrategy(_request.PaymentScheme);
            }

            [Fact]
            public void ValidatePayment()
            {
                _sut.MakePayment(_request);

                _paymentSchemeStrategy.Received(1).Validate(_account, _request);
            }

            [Fact]
            public void SetsTheSuccessOfMakePaymentResult()
            {
                var result = _sut.MakePayment(_request);

                result.Success.Should().Be(_paymentIsValid);
            }

            [Fact]
            public void DoesUpdateAccount_GivenValidationSuccess()
            {
                _paymentSchemeStrategy.Validate(_account, _request).Returns(true);

                _sut.MakePayment(_request);

                _accountDataStore.Received(1).UpdateAccount(_account);
            }

            [Fact]
            public void DoesNotUpdateAccount_GivenValidationFailure()
            {
                _paymentSchemeStrategy.Validate(_account, _request).Returns(false);

                _sut.MakePayment(_request);

                _accountDataStore.Received(0).UpdateAccount(Arg.Any<Account>());
            }

            [Fact]
            public void DoesDebitPayment_GivenValidationSuccess()
            {
                _paymentSchemeStrategy.Validate(_account, _request).Returns(true);

                _sut.MakePayment(_request);

                _accountManager.Received(1).DebitAccount(_account, _request.Amount);
            }

            [Fact]
            public void DoesNotDebitPayment_GivenValidationSuccess()
            {
                _paymentSchemeStrategy.Validate(_account, _request).Returns(false);

                _sut.MakePayment(_request);

                _accountManager.Received(0).DebitAccount(Arg.Any<Account>(), Arg.Any<decimal>());
            }

            [Fact]
            public void ReturnFailure_GivenAccountIsNull()
            {
                _accountDataStore.GetAccount(_request.DebtorAccountNumber).ReturnsNull();

                var result = _sut.MakePayment(_request);

                result.Success.Should().BeFalse();
            }
        }
    }
}
