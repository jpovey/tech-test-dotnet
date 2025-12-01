namespace ClearBank.DeveloperTest.Data;

public class AccountDataStoreProvider : IAccountDataStoreProvider
{
    private readonly IAccountDataStore _accountDataStore;

    public AccountDataStoreProvider(AccountDataStoreOptions accountDataStoreOptions)
    {
        if (accountDataStoreOptions.DataStoreType == DataStoreType.Backup)
        {
            _accountDataStore = new BackupAccountDataStore();
        }

        _accountDataStore = new AccountDataStore();
    }
    public IAccountDataStore GetAccountDataStore()
    {
        return _accountDataStore;
    }
}