namespace ClearBank.DeveloperTest.Data;

using Types;

public interface IAccountDataStore
{
    Account GetAccount(string accountNumber);
    void UpdateAccount(Account account);
}