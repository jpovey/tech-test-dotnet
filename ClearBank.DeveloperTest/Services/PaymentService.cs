using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    using Domain.PaymentSchemes;

    public class PaymentService : IPaymentService
    {
        private readonly IAccountDataStoreProvider _accountDataStoreProvider;
        private readonly IPaymentSchemeStrategyFactory _paymentSchemeStrategyFactory;

        public PaymentService(IAccountDataStoreProvider accountDataStoreProvider, IPaymentSchemeStrategyFactory paymentSchemeStrategyFactory)
        {
            _accountDataStoreProvider = accountDataStoreProvider;
            _paymentSchemeStrategyFactory = paymentSchemeStrategyFactory;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var accountDataStore = _accountDataStoreProvider.ProvideAccountDataStore();

            var account = accountDataStore.GetAccount(request.DebtorAccountNumber);

            var paymentSchemeStrategy = _paymentSchemeStrategyFactory.GetPaymentSchemeStrategy(request.PaymentScheme);
            
            var result = new MakePaymentResult
            {
                Success = paymentSchemeStrategy.Validate(account, request)
            };

            if (result.Success)
            {
                account.Balance -= request.Amount;
                accountDataStore.UpdateAccount(account);
            }

            return result;
        }
    }
}
