using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LB_2
{
    internal class Bank
    {
        public string Name { get; private set; }
        public List<BankClient> Clients { get; private set; } = new();
        public List<BankCard> Cards { get; private set; } = new();
        public Dictionary<int, double> CardBalances { get; private set; } = new();

        public Bank(string name)
        {
            Name = name;
        }

        public BankCard CreateCard(BankClient owner, int cardNumber, Card.Types typeOfCard, int cvv, double amountOfMoney, int pinCode)
        {
            if (Cards.Any(card => card.CardNumber == cardNumber))
            {
                throw new BankCardExistException("Такая карта уже существует");
            }

            var card = new BankCard(owner, cardNumber, typeOfCard, cvv, pinCode);
            Cards.Add(card);
            CardBalances[cardNumber] = amountOfMoney;
            return card;
        }

        public double GetCardBalance(int cardNumber)
        {
            return CardBalances.GetValueOrDefault(cardNumber);
        }

        public void AddCardBalance(int cardNumber, double amount)
        {
            CardBalances[cardNumber] += Math.Round(amount,2);
        }

        public void WithdrawCardBalance(int cardNumber, double amount)
        {
            CardBalances[cardNumber] -= Math.Round(amount,2);
        }

        public void AddClient(string fullName, List<BankCard> bankCardNumber, DateTime dateOfEntry, int passportNumber, double amountOfMoney)
        {
            var bankClient = new BankClient(fullName, bankCardNumber, dateOfEntry, passportNumber, amountOfMoney);
            Clients.Add(bankClient);
        }

        public BankClient GetClient(int cardNumber)
        {
            return Cards.FirstOrDefault(card => card.CardNumber == cardNumber)?.Owner;
        }

        public BankCard GetBankCard(int cardNumber)
        {
            var client = GetClient(cardNumber);
            return client?.Cards.FirstOrDefault(card => card.CardNumber == cardNumber);
        }

        public string CheckPinCode(int cardNumber, int pinCode)
        {
            try
            {
                var client = GetClient(cardNumber);

                if (client == null)
                {
                    throw new Exception("Пользователя с такой картой в этом банке не нашлось");
                }

                var card = client.Cards.FirstOrDefault(c => c.CardNumber == cardNumber && c.PinCode == pinCode);

                return card != null ? "Верно" : "Неверный pin";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

    }
    class BankCardExistException : Exception
    {
        public BankCardExistException(string message)
            : base(message) { }
    }
}
