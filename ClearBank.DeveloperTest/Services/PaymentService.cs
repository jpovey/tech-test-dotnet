using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    using Domain.PaymentSchemes;

    public class PaymentService : IPaymentService
    {
        private readonly IAccountDataStore _accountDataStore;
        private readonly IPaymentSchemeStrategyFactory _paymentSchemeStrategyFactory;

        public PaymentService(IAccountDataStoreProvider accountDataStoreProvider, IPaymentSchemeStrategyFactory paymentSchemeStrategyFactory)
        {
            _accountDataStore = accountDataStoreProvider.ProvideAccountDataStore();
            _paymentSchemeStrategyFactory = paymentSchemeStrategyFactory;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var account = _accountDataStore.GetAccount(request.DebtorAccountNumber);

            var paymentSchemeStrategy = _paymentSchemeStrategyFactory.GetPaymentSchemeStrategy(request.PaymentScheme);
            
            var result = new MakePaymentResult
            {
                Success = paymentSchemeStrategy.Validate(account, request)
            };

            if (result.Success)
            {
                account.Balance -= request.Amount;
                _accountDataStore.UpdateAccount(account);
            }

            return result;
        }
    }
}
