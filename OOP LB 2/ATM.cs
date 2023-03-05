using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LB_2
{
    
    internal class ATM
    {
        private Bank bankName;
        private int ATMId;
        private Dictionary<int, int> availableBanknots = new Dictionary<int, int>();

        public ATM(Bank bank, int ATMId) 
        {
            this.bankName = bank; 
            this. ATMId = ATMId;
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
            return ATMId;
        }

        public String getBankName()
        {
            return bankName.getName();
        }

        public void getAvailableBanknots()
        {
            foreach (int key in availableBanknots.Keys)
            {
                Console.WriteLine(key + " " + availableBanknots[key]);
            }
        }

        public Session startNewSession(int cardNumber, int pinCode)
        {
            Session session = new Session(cardNumber, pinCode);
            if (checkCVV(session.getCardNumber(), session.getPinCode()).Equals("Верно"))
            {
                session.setIsAvailable(true);
                session.setBankCard(bankName.getBankCard(cardNumber));
                return session;
            }
            else if (checkCVV(session.getCardNumber(), session.getPinCode()).Equals("Неверный pin"))
            {
                Console.WriteLine("Неверный pin");
                stopNewSession(session);

            }
            else
            {
                Console.WriteLine(checkCVV(session.getCardNumber(), session.getPinCode()));
                stopNewSession(session);
            }
            
            return session;
        }

        public void stopNewSession(Session session)
        {
            session.setIsAvailable(false);
        }

        public void remittance(Session session)
        {
            if (session.isActive())
            {
                Console.WriteLine("Введите сумму для перевода");
                Double amountOfMoney = Convert.ToDouble(Console.ReadLine());
                if (session.getBankCard().getAmountOfMoney() >= amountOfMoney)
                {
                    
                    Console.WriteLine("Введите номер карты для перевода");
                    int cardNumberForRemittance = Convert.ToInt32(Console.ReadLine());
                    foreach (BankCard card in bankName.getBankCards())
                    {
                        if (card.getCardNumber() == cardNumberForRemittance)
                        {
                            card.addMoney(amountOfMoney);
                            Console.WriteLine("Выбранная карта действительна, перевод осуществлен");
                            session.getBankCard().withdrawMoney(amountOfMoney);
                            stopNewSession(session);
                        }
                    }
                }
                else Console.WriteLine("Недостаточно денег для перевода");
                stopNewSession(session);
            }

        }

        public void withdrawMoney(Session session)
        {
            if (session.isActive())
            {
                
                
                Console.WriteLine("Введите сумму для снятия");
                int amountOfMoney = Convert.ToInt32(Console.ReadLine());
                try
                {
                    if (session.getBankCard().getAmountOfMoney() >= amountOfMoney)
                    {
      
                        if (amountOfMoney % 50 != 0)
                        {
                            Console.WriteLine("Введите сумму кратную 50");
                            throw new Exception();
                        }
                        

                        int valueOf5K = 0;
                        if (amountOfMoney >= 5000)
                        {

                            valueOf5K = ((int)amountOfMoney / 5000);
                            amountOfMoney -= valueOf5K * 5000;
                        }

                        int valueOf2K = 0;
                        if (amountOfMoney >= 2000)
                        {

                            valueOf2K = ((int)amountOfMoney / 2000);
                            amountOfMoney -= valueOf2K * 2000;
                        }

                        int valueOf1K = 0;
                        if (amountOfMoney >= 1000)
                        {

                            valueOf1K = ((int)amountOfMoney / 1000);
                            amountOfMoney -= valueOf1K * 1000;
                        }

                        int valueOf500 = 0;
                        if (amountOfMoney >= 500)
                        {

                            valueOf500 = ((int)amountOfMoney / 500);
                            amountOfMoney -= valueOf500 * 500;
                        }

                        int valueOf200 = 0;
                        if (amountOfMoney >= 200)
                        {

                            valueOf200 = ((int)amountOfMoney / 200);
                            amountOfMoney -= valueOf200 * 200;
                        }

                        int valueOf100 = 0;
                        if (amountOfMoney >= 100)
                        {

                            valueOf100 = ((int)amountOfMoney / 100);
                            amountOfMoney -= valueOf100 * 100;
                        }

                        int valueOf50 = 0;
                        if (amountOfMoney >= 50)
                        {

                            valueOf50 = ((int)amountOfMoney / 50);
                            amountOfMoney -= valueOf50 * 50;
                        }


                        if (valueOf5K > availableBanknots[5000])
                        {
                            Console.WriteLine("Недостаточно купюр для снятия");
                            throw new Exception();
                        }
                        if (valueOf2K > availableBanknots[2000])
                        {
                            Console.WriteLine("Недостаточно купюр для снятия");
                            throw new Exception();
                        }
                        if (valueOf1K > availableBanknots[1000])
                        {
                            Console.WriteLine("Недостаточно купюр для снятия");
                            throw new Exception();
                        }
                        if (valueOf500 > availableBanknots[500])
                        {
                            Console.WriteLine("Недостаточно купюр для снятия");
                            throw new Exception();
                        }
                        if (valueOf200 > availableBanknots[200])
                        {
                            Console.WriteLine("Недостаточно купюр для снятия");
                            throw new Exception();
                        }
                        if (valueOf100 > availableBanknots[100])
                        {
                            Console.WriteLine("Недостаточно купюр для снятия");
                            throw new Exception();
                        }
                        if (valueOf50 > availableBanknots[50])
                        {
                            Console.WriteLine("Недостаточно купюр для снятия");
                            throw new Exception();
                        }
                        availableBanknots[5000] -= valueOf5K;
                        availableBanknots[2000] -= valueOf2K;
                        availableBanknots[1000] -= valueOf1K;
                        availableBanknots[500] -= valueOf500;
                        availableBanknots[200] -= valueOf200;
                        availableBanknots[100] -= valueOf100;
                        availableBanknots[50] -= valueOf50;

                        session.getBankCard().withdrawMoney(amountOfMoney);
                        Console.WriteLine("Деньги успешно сняты, купюры: 5000 рублей: " + valueOf5K +
                            ", 2000 рублей: " + valueOf2K + ", 1000 рублей: " + valueOf1K +
                            ", 500 рублей: " + valueOf500 + ", 200 рублей: " + valueOf200 +
                            ", 100 рублей: " + valueOf100 + ", 50 рублей: " + valueOf50);
                        stopNewSession(session);
                    }
                    else Console.WriteLine("Недостаточно денег для снятия");
                    stopNewSession(session);
                }
                catch (Exception e)
                {

                }
                
            }
        }

        public void putMoney(Session session)
        {
            if (session.isActive())
            {
                Console.WriteLine("Введите сумму для пополнения");
                Double amountOfMoney = Convert.ToDouble(Console.ReadLine());
                session.getBankCard().addMoney(amountOfMoney);
                Console.WriteLine("Баланс успешно пополнен");
                stopNewSession(session);
            }
        }

        public void viewBalance(Session session)
        {
            if (session.isActive())
            {
                Console.WriteLine("Ваш баланс равен " + session.getBankCard().getAmountOfMoney());
                stopNewSession(session);
            }
        }

        public String checkCVV(int cardNumber, int pinCode)
        {
            String bankAnswer = bankName.checkPinCode(cardNumber, pinCode);
            return bankAnswer;
        }

    }


}
