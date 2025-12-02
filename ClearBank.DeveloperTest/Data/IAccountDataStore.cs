namespace ClearBank.DeveloperTest.Data;

using Domain.Account;

public interface IAccountDataStore
{
    Account GetAccount(string accountNumber);
    void UpdateAccount(Account account);
}