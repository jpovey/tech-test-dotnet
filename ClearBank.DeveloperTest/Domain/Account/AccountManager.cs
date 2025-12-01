namespace ClearBank.DeveloperTest.Domain.Account;

public class AccountManager : IAccountManager
{
    public void DebitAccount(Account account, decimal amount)
    {
        account.Balance -= amount;
    }
}