namespace ClearBank.DeveloperTest.Tests.Data
{
    using AwesomeAssertions;
    using DeveloperTest.Data;
    using Xunit;

    public class AccountDataStoreProviderTests
    {
        private AccountDataStoreProvider _sut = new(new AccountDataStoreOptions
        {
            DataStoreType = DataStoreType.Default
        });

        public class GetAccountDataStore : AccountDataStoreProviderTests
        {
            [Fact]
            public void GetsAccountDataStore()
            {
                var result = _sut.ProvideAccountDataStore();

                result.Should().BeOfType<AccountDataStore>();
            }

            [Fact]
            public void GetsBackupAccountDataStore_GivenDataStoreTypeIsBackup()
            {
                _sut = new AccountDataStoreProvider(new AccountDataStoreOptions
                {
                    DataStoreType = DataStoreType.Backup
                });

                var result = _sut.ProvideAccountDataStore();

                result.Should().BeOfType<BackupAccountDataStore>();
            }
        }
    }
}
