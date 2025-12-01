namespace ClearBank.DeveloperTest.Tests.Services
{
    using AutoFixture;
    using DeveloperTest.Data;
    using DeveloperTest.Services;
    using NSubstitute;
    using Types;
    using Xunit;

    public class PaymentServiceTests
    {
        private static readonly Fixture Fixture = new();     

        private readonly IAccountDataStoreProvider _accountDataStoreProvider;
        private readonly IAccountDataStore _accountDataStore;
        private readonly PaymentService _sut;
        private readonly MakePaymentRequest _makePaymentRequest;

        public PaymentServiceTests()
        {
            _makePaymentRequest = Fixture.Create<MakePaymentRequest>();

            _accountDataStoreProvider = Substitute.For<IAccountDataStoreProvider>();
            _accountDataStore = Substitute.For<IAccountDataStore>();

            _accountDataStoreProvider.GetAccountDataStore().Returns(_accountDataStore);

            _sut = new PaymentService(_accountDataStoreProvider);
        }

        public class MakePayment : PaymentServiceTests
        {
            [Fact]
            public void SelectsAccountDataStore()
            {
                _sut.MakePayment(_makePaymentRequest);

                _accountDataStoreProvider.Received(1).GetAccountDataStore();
            }

            [Fact]
            public void GetsAccountFromAccountDataStore()
            {
                _sut.MakePayment(_makePaymentRequest);

                _accountDataStore.Received(1).GetAccount(_makePaymentRequest.DebtorAccountNumber);
            }
        }
    }
}
