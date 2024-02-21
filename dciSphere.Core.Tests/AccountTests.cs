using dciSphere.Core.Constants;
using dciSphere.Core.Models;
using dciSphere.Core.ValueObject;
using FluentAssertions;
using Xunit;
namespace dciSphere.Core.Tests;

public class AccountTests
{
    [Fact]
    public void NewAccountSucceeds()
    {
        //Arrange
        var name = "test";
        var account = new Account(name, new Money(), default);
        //Assert
        name.Should().Be(account.Name);
    }

    [Theory]
    [InlineData("Account first", 1, Currency.PLN)]
    [InlineData("Account first", 1.1, Currency.USD)]
    public void UpdateAccountSucceeds(string newName, decimal newBalance, Currency newCurrency)
    {
        //Arrange
        var money = new Money();
        var account = new Account("", money, default);
        var newMoney = new Money(newBalance, newCurrency);
        var newBank = new Bank("name");
        //Act
        account.SetBank(newBank);
        account.SetName(newName);
        account.UpdateBalance(newMoney);
        //Assert
        account.BankId.Should().Be(newBank.Id);
        account.Balance.Should().Be(newMoney);
        account.Name.Should().Be(newName);
    }
}

