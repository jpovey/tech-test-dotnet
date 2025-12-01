using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    using Domain.Account;
    using Domain.PaymentSchemes;

    public class PaymentService(IAccountDataStoreProvider accountDataStoreProvider,
        IPaymentSchemeStrategyFactory paymentSchemeStrategyFactory, 
        IAccountManager accountManager) : IPaymentService
    {
        private readonly IAccountDataStore _accountDataStore = accountDataStoreProvider.ProvideAccountDataStore();

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var account = _accountDataStore.GetAccount(request.DebtorAccountNumber);

            var paymentSchemeStrategy = paymentSchemeStrategyFactory.GetPaymentSchemeStrategy(request.PaymentScheme);

            var result = new MakePaymentResult
            {
                Success = paymentSchemeStrategy.Validate(account, request)
            };

            if (result.Success)
            {
                accountManager.DebitAccount(account, request.Amount);
                _accountDataStore.UpdateAccount(account);
            }

            return result;
        }
    }
}
