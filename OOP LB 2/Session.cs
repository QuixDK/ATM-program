using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LB_2
{

    internal class Session
    {
        public int CardNumber { get; private set; }
        public int CardPinCode { get; private set; }
        public BankCard BankCard { get; private set; }
        public bool IsAvailable { get; private set; }

        public Session(int cardNumber, int pinCode)
        {
            CardNumber = cardNumber;
            CardPinCode = pinCode;
        }

        public void SetBankCard(BankCard bankCard)
        {
            BankCard = bankCard;
        }

        public void SetIsAvailable(bool isAvailable)
        {
            IsAvailable = isAvailable;
        }

        public bool IsActive()
        {
            return IsAvailable;
        }


    }
}
