using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LB_2
{
    internal class Bank
    {
        private String bankName;
        private List<BankClient> clientList = new List<BankClient>();
        private List<BankCard> cards= new List<BankCard>();
        private Dictionary<int, Double> amountOfMoneyOnCard = new Dictionary<int, Double>();
        public Bank(String bankName) 
        { 
            this.bankName = bankName;
        }

        public String getName()
        {
            return bankName;
        }

        public BankCard createCard(BankClient owner, int cardNumber, Card.Types typeOfCard, int CVV, double amountOfMoney, int pinCode)
        {
            BankCard bankCard = new BankCard(owner, cardNumber, typeOfCard, CVV, pinCode);
            foreach (BankCard bankCard1 in cards) 
            {
                if (bankCard1.getCardNumber() == bankCard.getCardNumber())
                {
                    return null;
                }
            }
            cards.Add(bankCard);
            amountOfMoneyOnCard.Add(bankCard.getCardNumber(), amountOfMoney);
            return bankCard;
        }

        public Double getCardBalance(int cardNumber)
        {
            return amountOfMoneyOnCard[cardNumber];
        }
        public void addCardBalance(int cardNumber, Double amount)
        {
            amountOfMoneyOnCard[cardNumber] = Math.Round((amountOfMoneyOnCard[cardNumber] + amount),2);
        }
        public void withdrawCardBalance(int cardNumber, Double amount)
        {
            amountOfMoneyOnCard[cardNumber] = Math.Round((amountOfMoneyOnCard[cardNumber] - amount),2);
        }

        public void addClient(String fullName, List<BankCard> bankCardNumber, DateTime dateOfEntry, int passportNumber, Double amountOfMoney)
        {
            BankClient bankClient = new BankClient(fullName, bankCardNumber, dateOfEntry, passportNumber, amountOfMoney);
            clientList.Add(bankClient);
        }

        public void addBankCard(BankCard bankCard)
        {
           
            cards.Add(bankCard);
        }

        public BankClient getClient(int cardNumber)
        {
            foreach (BankCard card in cards)
            {
                if (card.getCardNumber() == cardNumber)
                {
                    return card.getOwner();
                    
                }
            }
            return null;
        }

        public List<BankCard> getBankCards()
        {
            return cards;
        }

        public BankCard getBankCard(int cardNumber) 
        {
            BankClient currentClient = getClient(cardNumber);
            foreach (BankClient client in clientList)
            {
                if (client == currentClient)
                {
                    foreach (BankCard card in client.getCards())
                    {
                        if (card.getCardNumber() == cardNumber)
                        {
                            return card;
                        }
                    }
                }
            }
            return null;
        }

        public String checkPinCode(int cardNumber, int pinCode)
        {
            try
            {
                BankClient bankClient = getClient(cardNumber);

                if (bankClient == null)
                {
                    throw new Exception("Пользователя с такой картой в этом банке не нашлось");
                }

                List<BankCard> clientCards = bankClient.getCards();

                foreach (BankCard card in clientCards)
                {
                    if (card.getCardNumber() == cardNumber & card.getPinCode() == pinCode)
                    {
                        return "Верно";
                    }
                }
                return "Неверный pin";
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }

        public List<BankClient> getClientsList()
        {
            return clientList;
        }

    }
}
