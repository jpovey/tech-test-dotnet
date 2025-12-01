namespace ClearBank.DeveloperTest.Data;

public class AccountDataStoreProvider : IAccountDataStoreProvider
{
    private readonly AccountDataStoreOptions _accountDataStoreOptions;

    public AccountDataStoreProvider(AccountDataStoreOptions accountDataStoreOptions)
    {
        _accountDataStoreOptions = accountDataStoreOptions;
    }
    public IAccountDataStore GetAccountDataStore()
    {
        if (_accountDataStoreOptions.DataStoreType == DataStoreType.Backup)
        {
            return new BackupAccountDataStore();
        }

        return new AccountDataStore();
    }
}