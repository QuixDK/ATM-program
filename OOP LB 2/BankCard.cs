using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LB_2
{
    internal class BankCard
    {
        public BankClient Owner { get; private set; }
        public int CardNumber { get; private set; }
        public Card.Types CardType { get; private set; }
        public int CVV { get; private set; }
        public int PinCode { get; private set; }

        public BankCard(BankClient owner, int cardNumber, Card.Types typeOfCard, int CVV, int pinCode)
        {
            Owner = owner;
            CardNumber = cardNumber;
            CardType = typeOfCard;
            this.CVV = CVV;
            PinCode = pinCode;
        }

    }
}
