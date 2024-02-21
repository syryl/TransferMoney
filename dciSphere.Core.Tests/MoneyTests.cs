using Xunit;
using FluentAssertions;
using dciSphere.Core.ValueObject;
using dciSphere.Core.Constants;
namespace dciSphere.Core.Tests;
public class MoneyTests
{
    [Fact]
    public void MoneyShouldAcceptDecimalAsAmount()
    {
        //Arrange
        decimal amount = 11.11M;
        int amountAsInt = (int)(amount*100);
        var money = new Money(amount, Constants.Currency.USD);
        //Assert
        money.Amount.Should().Be(amountAsInt);
    }

    [Theory]
    [InlineData(1, Currency.USD, "1,00 USD")]
    [InlineData(11.2, Currency.PLN, "11,20 PLN")]
    [InlineData(-511.99, Currency.PLN, "-511,99 PLN")]
    [InlineData(-1, Currency.EUR, "-1,00 EUR")]
    [InlineData(-1.2, Currency.EUR, "-1,20 EUR")]
    public void MoneyShouldDisplayReadableString(decimal amount, Currency currency, string expected)
    {
        //Arrange
        var money = new Money(amount, currency);
        //Act
        var text = money.ToString();
        //Assert
        text.Should().Be(expected);
    }

    [Theory]
    [InlineData(10,5)]
    [InlineData(2,5)]
    public void MoneyShouldBeAddedOrSubtractedToAnotherMoney(int firstAmount, int secondAmount)
    {
        //Arrange
        var firstMoney = new Money(firstAmount, Currency.USD);
        var secondMoney = new Money(secondAmount, Currency.USD);
        //Act
        var newAddedMoney = firstMoney + secondMoney;
        var newAddedAmount = firstAmount + secondAmount;
        var newSubtractedMoney = firstMoney - secondMoney;
        var newSubtractedAmount = firstAmount - secondAmount;
        //Assert
        newAddedMoney.Amount.Should().Be(newAddedAmount);
        newSubtractedMoney.Amount.Should().Be(newSubtractedAmount);
    }
}
