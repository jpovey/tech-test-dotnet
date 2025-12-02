namespace ClearBank.DeveloperTest.Tests.Domain.Account
{
    using AutoFixture;
    using AwesomeAssertions;
    using DeveloperTest.Domain.Account;
    using Xunit;

    public class AccountManagerShould
    {
        private static readonly Fixture Fixture = new();
        private readonly Account _account = Fixture.Create<Account>();
        private readonly decimal _debitAmount = Fixture.Create<decimal>();
        private readonly AccountManager _sut = new();

        public class DebitAccount : AccountManagerShould
        {
            [Fact]
            public void MinusesAmountFromBalance()
            {
                var expectedBalance = _account.Balance - _debitAmount;

                _sut.DebitAccount(_account, _debitAmount);

                _account.Balance.Should().Be(expectedBalance);
            }
        }
    }
}
