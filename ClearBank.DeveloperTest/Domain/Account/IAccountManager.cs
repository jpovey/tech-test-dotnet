namespace ClearBank.DeveloperTest.Domain.Account;

public interface IAccountManager
{
    void DebitAccount(Account account, decimal amount);
}