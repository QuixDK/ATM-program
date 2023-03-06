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

        public BankCard(BankClient owner, int cardNumber, Card.Types typeOfCard, int CVV, int pinCode)
        {
            this.owner = owner;
            this.cardNumber = cardNumber;
            this.typeOfCard = typeOfCard;
            this.CVV = CVV;
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

    }
}
