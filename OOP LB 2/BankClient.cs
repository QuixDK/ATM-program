using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LB_2
{
    internal class BankClient
    {
        private String fullName;
        private List<BankCard> cards = new List<BankCard>();
        private DateTime dateOfEntry;
        private int passportNumber;
        private Double amountOfMoney;
        public BankClient(String fullName, List<BankCard> cards, DateTime dateOfEntry, int passportNumber, Double amountOfMoney) 
        {
            this.fullName = fullName;
            this.cards = cards;
            this.dateOfEntry = dateOfEntry;
            this.passportNumber = passportNumber;
            this.amountOfMoney = amountOfMoney;
        }

        public String getFullName()
        {
            return fullName;
        }

        public List<BankCard> getCards()
        {
            
            return cards;
        }

        public DateTime getDateOfEntry()
        {
            return dateOfEntry;
        }

        public int getPassportNumber()
        {
            return passportNumber;
        }

        public Double getAmountOfMoney()
        {
            return amountOfMoney;
        }

        public void setFullName(String fullName)
        {
            this.fullName = fullName;
        }
        public void addBankCard(BankCard bankCard)
        {
            this.cards.Add(bankCard);
        }
        public void setPassportNumber(int passportNumber)
        {
            this.passportNumber = passportNumber;
        }
        public void setDateOfEntry(DateTime dateOfEntry)
        {
            this.dateOfEntry = dateOfEntry;
        }
        public void setAmountOfMoney(Double amountOfMoney)
        {
            this.amountOfMoney = amountOfMoney;
        }
    }
}
