using dciSphere.Core.Constants;
using dciSphere.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Core.ValueObject
{
    public record Money
    {
        public int Amount { get; private set; }
        public Currency Currency { get; private set; }

        public Money() : this(0, Currency.USD) { }
        public Money(decimal amount, Currency currency)
        {
            Currency = currency;
            Amount = decimal.ToInt32(amount * 100);
        }

        public Money(int amount, Currency currency)
        {
            Currency = currency;
            Amount = amount;
        }

        public static Money operator +(Money a, Money b)
        {
            if (a.Currency != b.Currency)
            {
                throw new NonConvertableCurrencies();
            }

            var sum = a.Amount + b.Amount;
            return new(sum, a.Currency);
        }

        public static Money operator -(Money a, Money b)
        {
            if (a.Currency != b.Currency)
            {
                throw new NonConvertableCurrencies();
            }

            var sum = a.Amount - b.Amount;
            return new(sum, a.Currency);
        }

        public override string ToString()
        {
            var amountAsText = Amount.ToString();
            if (amountAsText.Length == 2 || (amountAsText.StartsWith('-') && amountAsText.Length == 3))
            {
                amountAsText = amountAsText.Insert(amountAsText.Length - 2, "0");
            }

            amountAsText = amountAsText.Insert(amountAsText.Length - 2, ",");
            var result = $"{amountAsText} {Currency.ToString()}";
            return result;
        }
    }
}
