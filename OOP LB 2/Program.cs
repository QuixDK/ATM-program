namespace OOP_LB_2
{
    internal class Program
    {
        public static List<Bank> banks = new List<Bank>();
        public static List<ATM> ATMs = new List<ATM>();
        public static Bank currentBank;
        public static BankClient currentClient;
        public static ATM currentATM;
        public static int atmId = 0; 
        public static void Main(string[] args)
        {
            sendMenu();
            while (true)
            {
                try
                {
                    int userResponse = Convert.ToInt32(Console.ReadLine());
                    switch (userResponse)
                    {
                        case 1:
                            createNewBank();
                            break;
                        case 2:
                            createNewClient();
                            break;
                        case 3:
                            createNewBankCard();
                            break;
                        case 4:
                            createNewATM();
                            break;
                        case 5:
                            withdrawMoney();
                            break;
                        case 6:
                            putMoney();
                            break;
                        case 7:
                            checkBalance();
                            break;
                        case 8:
                            remittance();
                            break;
                        case 9:
                            repayALoan();
                            break;
                        case 10:
                            showClientCards();
                            break;
                        case 0:
                            clearConsole();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Неправильная команда");
                }
                
                
            }
            
        }

        public static Session ATMService()
        {
            if (ATMs.Count != 0)
            {
                initATM();
                Console.WriteLine("Введите номер карты");
                int cardNumber = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Введите pin");
                int pinCode = Convert.ToInt32(Console.ReadLine());
                Session currentSession = currentATM.startNewSession(cardNumber, pinCode);
                if (currentSession.isActive())
                {
                    return currentSession;
                }
                

            }
            else Console.WriteLine("Для снятия денег нужен хотя бы один банкомат");
            return null;
        }
        
        public static void withdrawMoney()
        {
            if (ATMs.Count != 0)
            {
                currentATM.withdrawMoney(ATMService());
            }
            else Console.WriteLine("Для снятия денег нужен хотя бы один банкомат");
        }
        public static void putMoney()
        {
            if (ATMs.Count != 0)
            {
                Session session = ATMService();
                currentATM.putMoney(session);
            }
            else Console.WriteLine("Для пополнения денег нужен хотя бы один банкомат");
        }
        public static void checkBalance()
        {
            if (ATMs.Count != 0)
            {
                Session session = ATMService();
                currentATM.viewBalance(session);
            }
            else Console.WriteLine("Для проверки баланса нужен хотя бы один банкомат");
        }
        public static void remittance()
        {
            if (ATMs.Count != 0)
            {
                Session session = ATMService();
                currentATM.remittance(session);
            }
            else Console.WriteLine("Для перевода нужен хотя бы один банкомат");
        }
        public static void repayALoan()
        {
            if (ATMs.Count != 0)
            {
                initATM();
                currentATM.getAvailableBanknots();
            }
            else Console.WriteLine("Для снятия денег нужен хотя бы один банкомат");
        }
        

        public static void initATM()
        {
            Console.WriteLine("Выберите банкомат из списка");
            int i = 0;
            foreach (ATM ATM in ATMs)
            {
                Console.WriteLine(i + " Id банкомата: " + ATM.getID() + " банк банкомата: " + ATM.getBankName());
                i++;
            }
            int ATMNumber = Convert.ToInt32(Console.ReadLine());
            i = 0;
            foreach (ATM ATM in ATMs)
            {
                if (i == ATMNumber)
                {
                    currentATM = ATM;
                }
                i++;
            }
        }

        public static void clearConsole()
        {
            Console.Clear();
            sendMenu();
        }
        public static void createNewClient()
        {
            if (banks.Count != 0)
            {
                try
                {
                    Console.WriteLine("Введите полное имя клиента");
                    String fullName = Console.ReadLine();
                    Console.WriteLine("Введите номер паспорта клиента");
                    int passportNumber = Convert.ToInt32(Console.ReadLine());
                    List<BankCard> cards = new List<BankCard>();
                    initBank();
                    currentBank.addClient(fullName, cards, DateTime.Now, passportNumber, 0);
                    Console.WriteLine("Клиент успешно добавлен");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Не удалось добавить клиента");
                }
            }
            else Console.WriteLine("Сначала нужно создать хотя бы один банк");

        }

        public static void createNewATM()
        {
            initBank();
            ATM aTM = new ATM(currentBank, atmId);
            ATMs.Add(aTM);
            Console.WriteLine("Банкомат успешно создан");
            atmId++;
        }

        public static void showClientCards()
        {
            initBank();
            Console.WriteLine("Выберите клиента из списка клиентов банка");
            initClient();
            int i = 0;
            List<BankCard> cards = currentClient.getCards();
            if (cards.Count != 0)
            {
                foreach (BankCard card in currentClient.getCards())
                {
                    Console.WriteLine(i + " Номер карты : " + card.getCardNumber());
                    i++;
                }
            }
            else Console.WriteLine("У клиента не обнаружены банковские карты");
        }

        public static void createNewBankCard()
        {
            
            if (banks.Count != 0)
            {
                
                initBank();
                if (currentBank.getClientsList().Count != 0)
                {
                    Console.WriteLine("Выберите владельца карты из списка клиентов банка");
                    initClient();
                    Console.WriteLine("Введите номер для новой карты");
                    int cardNumber = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Введите pinCode для новой карты");
                    Random rnd = new Random();
                    int pinCode = Convert.ToInt32(Console.ReadLine());
                    int CVV = Convert.ToInt32(Math.Round((rnd.NextDouble() * 1000)));
                    
                    Double amountOfMoney = Math.Round((rnd.NextDouble() * 1000), 2);
                    BankCard bankCard = currentBank.createCard(currentClient, cardNumber, Card.Types.Visa, CVV, amountOfMoney, pinCode);
                    if (bankCard != null)
                    {
                        currentClient.addBankCard(bankCard);
                        Console.WriteLine("Карта успешно добавлена");
                    }
                    else Console.WriteLine("Карта с таким номером уже существует");

                }
                else Console.WriteLine("Чтобы создать карту, нужен хотя бы один клиент банка");

            }
            else Console.WriteLine("Нужно создать хотя бы один банк");
        }
       
        public static void initClient()
        {
            int i = 0;
            foreach (BankClient client in currentBank.getClientsList())
            {
                Console.WriteLine(i + " " + client.getFullName());
                i++;
            }
            int value = Convert.ToInt32(Console.ReadLine());
            i = 0;

            foreach (BankClient client in currentBank.getClientsList())
            {
                if (i == value)
                {
                    currentClient = client;
                }
                i++;
            }
        }
        public static void initBank()
        {
            Console.WriteLine("Выберите банк из списка");
            int i = 0;
            foreach (Bank bank in banks)
            {
                Console.WriteLine(i + " " + bank.getName());
                i++;
            }
            int bankNumber = Convert.ToInt32(Console.ReadLine());
            i = 0;
            foreach (Bank bank in banks)
            {
                if (i == bankNumber)
                {
                    currentBank = bank;
                }
                i++;
            }
        }

        public static void createNewBank()
        {
            Console.WriteLine("Введите название банка");
            Bank newBank = new Bank(Console.ReadLine());
            banks.Add(newBank);
            Console.WriteLine("Банк успешно создан");
        }

        public static void sendMenu()
        {
            Console.WriteLine("Введите 1 чтобы создать новый банк");
            Console.WriteLine("Введите 2 чтобы создать нового клиента");
            Console.WriteLine("Введите 3 чтобы выпустить новую карту");
            Console.WriteLine("Введите 4 чтобы создать новый банкомат");
            Console.WriteLine("Введите 5 чтобы снять деньги");
            Console.WriteLine("Введите 6 чтобы положить деньги");
            Console.WriteLine("Введите 7 чтобы узнать баланс");
            Console.WriteLine("Введите 8 чтобы сделать денежный перевод с карты на карту");
            Console.WriteLine("Введите 9 чтобы узнать количество доступных купюр для выдачи");
            Console.WriteLine("Введите 10 чтобы посмотреть карты клиента");
        }
    
    }
}

