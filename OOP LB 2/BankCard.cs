using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LB_2
{
    internal class BankCard
    {
        private BankClient owner;
        private int cardNumber;
        private Card.Types typeOfCard;
        private int CVV;
        private int pinCode;
        private Double amountOfMoney;

        public BankCard(BankClient owner, int cardNumber, Card.Types typeOfCard, int CVV, double amountOfMoney, int pinCode)
        {
            this.owner = owner;
            this.cardNumber = cardNumber;
            this.typeOfCard = typeOfCard;
            this.CVV = CVV;
            this.amountOfMoney = amountOfMoney;
            this.pinCode = pinCode;
            
        }

        public BankClient getOwner()
        {
            return owner;
        }
        public int getPinCode()
        {
            return pinCode;
        }

        public int getCardNumber()
        {
            return cardNumber;
        }
        public Card.Types getCardType()
        {
            return typeOfCard;
        }

        public int getCVV()
        {
            return CVV;
        }

        public Double getAmountOfMoney()
        {
            return amountOfMoney;
        }

        public void addMoney(Double amountOfMoney)
        {
            this.amountOfMoney += amountOfMoney;
        }
        public void withdrawMoney(Double amountOfMoney)
        {
            this.amountOfMoney -= amountOfMoney;
        }
    }
}
