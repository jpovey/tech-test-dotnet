namespace ClearBank.DeveloperTest.Domain.Account;

public interface IAccountManager
{
    void ApplyPayment(Account account, decimal amount);
}