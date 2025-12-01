namespace ClearBank.DeveloperTest.Data;

public interface IAccountDataStoreProvider
{
    IAccountDataStore GetAccountDataStore();
}