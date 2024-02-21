using dciSphere.Abstraction.Domain;
using dciSphere.Core.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Core.Models
{
    public class Account(string name, Money balance, int bankId) : Entity
    {
        public string Name { get; private set; } = name;
        public Money Balance { get; private set; } = balance;
        public int BankId { get; private set; } = bankId;
        public virtual Bank Bank { get; }

        protected Account(): this("default", new(), default) { }

        public void SetName(string _name) => Name = _name;
        public void UpdateBalance(Money _balance) => Balance = _balance;
        public void SetBank(Bank _bank) => BankId = _bank.Id;
    }
}
