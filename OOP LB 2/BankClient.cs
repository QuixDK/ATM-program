using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LB_2
{
    internal class BankClient
    {
        public string FullName { get; private set; }
        public List<BankCard> Cards { get; private set; } = new List<BankCard>();
        public DateTime DateOfEntry { get; private set; }
        public int PassportNumber { get; private set; }
        public double AmountOfMoney { get; private set; }
        public BankClient(string fullName, List<BankCard> cards, DateTime dateOfEntry, int passportNumber, double amountOfMoney)
        {
            FullName = fullName;
            Cards = cards;
            DateOfEntry = dateOfEntry;
            PassportNumber = passportNumber;
            AmountOfMoney = amountOfMoney;
        }

        public void AddBankCard(BankCard bankCard)
        {
            Cards.Add(bankCard);
        }
    }

}
