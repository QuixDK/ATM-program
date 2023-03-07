using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LB_2
{

    internal class ATM
    {
        private Bank bank;
        private int Id;
        private Dictionary<int, int> availableBanknots = new Dictionary<int, int>();

        public ATM(Bank bank, int ATMId)
        {
            this.bank = bank;
            this.Id = ATMId;
            Random rnd = new Random();
            availableBanknots.Add(5000, Convert.ToInt32(Math.Round(rnd.NextDouble() * 100)));
            availableBanknots.Add(2000, Convert.ToInt32(Math.Round(rnd.NextDouble() * 100)));
            availableBanknots.Add(1000, Convert.ToInt32(Math.Round(rnd.NextDouble() * 100)));
            availableBanknots.Add(500, Convert.ToInt32(Math.Round(rnd.NextDouble() * 100)));
            availableBanknots.Add(200, Convert.ToInt32(Math.Round(rnd.NextDouble() * 100)));
            availableBanknots.Add(100, Convert.ToInt32(Math.Round(rnd.NextDouble() * 100)));
            availableBanknots.Add(50, Convert.ToInt32(Math.Round(rnd.NextDouble() * 100)));
        }

        public int getID()
        {
            return Id;
        }

        public string getBankName()
        {
            return bank.Name;
        }

        public void GetAvailableBanknots()
        {
            foreach (int key in availableBanknots.Keys)
            {
                Console.WriteLine(key + " " + availableBanknots[key]);
            }
        }

        public Session StartNewSession(int cardNumber, int pinCode)
        {
            Session session = new Session(cardNumber, pinCode);
            if (CheckPin(session.CardNumber, session.CardPinCode).Equals("Верно"))
            {
                session.SetIsAvailable(true);
                session.SetBankCard(bank.GetBankCard(cardNumber));
                return session;
            }
            else if (CheckPin(session.CardNumber, session.CardPinCode).Equals("Неверный pin"))
            {
                Console.WriteLine("Неверный pin");
                StopNewSession(session);
                return session;

            }
            else
            {
                Console.WriteLine(CheckPin(session.CardNumber, session.CardPinCode));
                StopNewSession(session);
                return session;
            }


        }

        public void StopNewSession(Session session)
        {
            session.SetIsAvailable(false);
        }

        public void Remittance(Session session)
        {
            if (session.IsActive())
            {
                Console.WriteLine("Введите сумму для перевода");
                double amountOfMoney = Convert.ToDouble(Console.ReadLine());
                if (bank.GetCardBalance(session.BankCard.CardNumber) >= amountOfMoney)
                {

                    Console.WriteLine("Введите номер карты для перевода");
                    int cardNumberForRemittance = Convert.ToInt32(Console.ReadLine());
                    foreach (BankCard card in bank.Cards)
                    {
                        if (card.CardNumber == cardNumberForRemittance)
                        {

                            bank.AddCardBalance(card.CardNumber, amountOfMoney);
                            Console.WriteLine("Выбранная карта действительна, перевод осуществлен");
                            bank.WithdrawCardBalance(session.BankCard.CardNumber, amountOfMoney);
                        }
                    }
                }
                else Console.WriteLine("Недостаточно денег для перевода");
            }

        }

        public Dictionary<int, int> Withdraw(Session session, int amountOfMoney)
        {
            Dictionary<int, int> r = new Dictionary<int, int>();
            if (amountOfMoney % 50 != 0)
            {
                Console.WriteLine("Введите сумму кратную 50");
                throw new Exception();
            }
            CashWithDraw(availableBanknots, r, amountOfMoney);
            r = r.Where(kvp => kvp.Value != 0).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            bank.WithdrawCardBalance(session.BankCard.CardNumber, amountOfMoney);
            return r;
        }

        void CashWithDraw(Dictionary<int, int> source, Dictionary<int, int> result, int amount)
        {
            /// Оставшаяся для выдачи сумма
            int change;

            int k = 0;

            if (source.Count > result.Count)
                /// Поиск максимального номинала, который ещё не использовался
                k = source.Where(kvp => !result.ContainsKey(kvp.Key)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value).Keys.Max();
            else
                throw new Exception("Требуемую сумму невозможно выдать");
            /// Требуемое количество купюр данного номинала
            KeyValuePair<int, int> sel = new KeyValuePair<int, int>(k, amount / k);
            /// Если требуемое количество купюр больше, чем имеется в банкомате, 
            /// то записываются для выдачи все купюры данного номинала.
            if (sel.Value > source[sel.Key])
            {
                change = amount - sel.Key * source[sel.Key];
                sel = new KeyValuePair<int, int>(sel.Key, source[sel.Key]);
                source[sel.Key] -= sel.Value;
            }
            else
                change = amount - sel.Key * sel.Value;

            /// Если выдана вся сумаа
            if (change == 0)
            {
                result.Add(sel.Key, sel.Value);
                source[sel.Key] -= sel.Value;
                return;
            }
            /// Если оставшаяся сумма меньше минимального номинала купюры
            if (change <= source.Keys.Min())
            {
                /// Количество отобранных купюр уменьшаем на 1, чтобы подобрать сумму из
                /// более мелких купюр
                sel = new KeyValuePair<int, int>(sel.Key, sel.Value - 1);
                result.Add(sel.Key, sel.Value);
                source[sel.Key] -= sel.Value;
                CashWithDraw(source, result, amount - sel.Key * sel.Value);
                return;
            }

            source[sel.Key] -= sel.Value;
            result.Add(sel.Key, sel.Value);

            CashWithDraw(source, result, change);
        }

        public void PutMoney(Session session)
        {
            if (session.IsActive())
            {
                Console.WriteLine("Введите сумму для пополнения");
                int amountOfMoney = Convert.ToInt32(Console.ReadLine());
                if (amountOfMoney % 50 != 0)
                {
                    Console.WriteLine("Введите сумму кратную 50");
                    throw new Exception();
                }
                bank.AddCardBalance(session.BankCard.CardNumber, amountOfMoney);
                Console.WriteLine("Баланс успешно пополнен");
            }
        }

        public void ViewBalance(Session session)
        {
            if (session.IsActive())
            {
                Console.WriteLine("Ваш баланс равен " + bank.GetCardBalance(session.BankCard.CardNumber));
            }
        }

        public string CheckPin(int cardNumber, int pinCode)
        {
            string bankAnswer = bank.CheckPinCode(cardNumber, pinCode);
            return bankAnswer;
        }

    }


}
