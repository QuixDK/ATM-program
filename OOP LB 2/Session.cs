using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LB_2
{
    
    internal class Session
    {
        private int cardNumber;
        private int cardPinCode;
        private BankCard bankCard;
        private bool isAvailable;

        public Session(int cardNumber, int pinCode)
        {
            this.cardNumber = cardNumber;
            this.cardPinCode = pinCode;
            
        }
        public void setBankCard(BankCard bankCard)
        {
            this.bankCard = bankCard;
            
        }
        public void setIsAvailable(Boolean isAvailable)
        {
            this.isAvailable = isAvailable;
        }
        public bool isActive()
        {
            return this.isAvailable;
        }
        public int getCardNumber() { return cardNumber;}
        public int getPinCode() { return cardPinCode; }

        public BankCard getBankCard() { return bankCard;}

    }
}
