namespace OOP_LB_2
{
    internal class Program
    {
        public static List<Bank> banks = new List<Bank>();
        public static List<ATM> ATMs = new List<ATM>();
        public static Bank currentBank;
        public static BankClient currentClient;
        public static ATM currentATM;
        public static Session currentSession;
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
                            if (ATMs.Count == 0)
                            {
                                throw new ATMCountException("Для начала нужно создать хотя бы один банкомат");
                            }
                            ATMService();
                            clearConsole();
                            setATMMenu();
                            break;
                        case 6:
                            showClientCards();
                            break;
                        case 0:
                            clearConsole();
                            sendMenu();
                            break;
                    }
                }
                catch (ATMCountException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (InvalidSessionException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Неверная команда");
                }

            }
            
        }
        public static void sendMenu()
        {
            Console.WriteLine("Меню банка:");
            Console.WriteLine("Введите 1 чтобы создать новый банк");
            Console.WriteLine("Введите 2 чтобы создать нового клиента");
            Console.WriteLine("Введите 3 чтобы выпустить новую карту");
            Console.WriteLine("Введите 4 чтобы создать новый банкомат");
            Console.WriteLine("Введите 5 чтобы перейти в меню банкомата");
            Console.WriteLine("Введите 6 чтобы посмотреть карты клиента");
            Console.WriteLine("Введите 0 чтобы очистить консоль");
        }
        public static void createNewBank()
        {
            try
            {
                Console.WriteLine("Введите название банка");
                String bankName = Console.ReadLine();
                foreach (Bank bank in banks)
                {
                    if (bank.Name.Equals(bankName))
                    {
                        throw new BankNameException("Банк с таким именем уже существует");
                    }
                }
                Bank newBank = new Bank(bankName);
                banks.Add(newBank);
                Console.WriteLine("Банк успешно создан");
            }
            catch (BankNameException ex)
            {
                Console.WriteLine("Банк с таким именем уже существует");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Не удалось добавить банк");
            }
        }
        public static void createNewClient()
        {
            try
            {
                if (banks.Count == 0)
                {
                    throw new BankCountException("Сначала нужно создать хотя бы один банк");
                }
            
                initBank();
                Console.WriteLine("Введите имя клиента");

                String fullName = Console.ReadLine();

                if (fullName == null | fullName.Equals("") | fullName.Equals(" "))
                {
                    throw new FullNameException("Недопустимое имя пользователя");
                }

                Console.WriteLine("Введите номер паспорта клиента");

                int passportNumber = Convert.ToInt32(Console.ReadLine());

                foreach (BankClient client in currentBank.Clients)
                {
                    if (client.PassportNumber == passportNumber)
                    {
                        throw new PassportNumberException("Клиент с таким паспортом уже существует");
                    }
                }

                List<BankCard> cards = new List<BankCard>();

                currentBank.AddClient(fullName, cards, DateTime.Now, passportNumber, 0);

                Console.WriteLine("Клиент успешно добавлен");
            }
            
            catch (BankCountException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FullNameException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (PassportNumberException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Недопустимый номер паспорта");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public static void createNewBankCard()
        {
            Random rnd = new Random();
            try
            {
                if (banks.Count == 0)
                {
                    throw new BankCountException("Сначала нужно создать хотя бы один банк");
                }

                initBank();
                if (currentBank.Clients.Count == 0)
                {
                    throw new ClientCountException("Чтобы создать карту, нужен хотя бы один клиент банка");
                }

                Console.WriteLine("Выберите будующего владельца карты из списка клиентов банка");
                initClient();
                Console.WriteLine("Введите номер для новой карты");
                int cardNumber = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Введите pinCode для новой карты");
                int pinCode = Convert.ToInt32(Console.ReadLine());
                int CVV = Convert.ToInt32(Math.Round((rnd.NextDouble() * 1000)));
                Double amountOfMoney = Math.Round((rnd.NextDouble() * 1000), 2);
                BankCard bankCard = currentBank.CreateCard(currentClient, cardNumber, Card.Types.Visa, CVV, amountOfMoney, pinCode);
                if (bankCard == null)
                {
                    throw new BankCardNumberException("Карта с таким номером уже существует");
                }
                currentClient.AddBankCard(bankCard);
                Console.WriteLine("Карта успешно добавлена");
                

            }
            catch (BankCountException ex)
            {
                Console.WriteLine(ex.Message);
            }

            catch (ClientCountException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (BankCardNumberException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Неверный формат");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public static void createNewATM()
        {
            try
            {
                if (banks.Count == 0)
                {
                    throw new BankCountException("Сначала нужно создать хотя бы один банк");
                }
                initBank();
                ATM aTM = new ATM(currentBank, atmId);
                ATMs.Add(aTM);
                Console.WriteLine("Банкомат успешно создан");
                atmId++;
            }
            catch (BankCountException ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
        public static Session ATMService()
        {
            try
            {
                if (ATMs.Count == 0)
                {
                    throw new ATMCountException("Для начала нужно создать хотя бы один банкомат");
                }
                
            }
            catch (ATMCountException ex)
            {
                Console.WriteLine(ex.Message);
            }
            initATM();
            Console.WriteLine("Введите номер карты");
            int cardNumber = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите pin");
            int pinCode = Convert.ToInt32(Console.ReadLine());
            currentSession = currentATM.StartNewSession(cardNumber, pinCode);
            if (!currentSession.IsActive())
            {
                throw new InvalidSessionException("Не удалось запустить новую сессию");
            }
            return currentSession;

        }
        public static void showClientCards()
        {
            try
            {
                if (banks.Count == 0)
                {
                    throw new BankCountException("Сначала нужно создать хотя бы один банк");
                }
                initBank();
                if (currentBank.Clients.Count == 0)
                {
                    throw new ClientCountException("Чтобы узнать список карт, нужен хотя бы один клиент банка");
                }
                Console.WriteLine("Выберите клиента из списка клиентов банка");
                initClient();
                int i = 0;
                List<BankCard> cards = currentClient.Cards;
                if (cards.Count != 0)
                {
                    foreach (BankCard card in currentClient.Cards)
                    {
                        Console.WriteLine(i + " Номер карты : " + card.CardNumber);
                        i++;
                    }
                }
                else Console.WriteLine("У клиента не обнаружены банковские карты");
            }
            catch (BankCountException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ClientCountException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void getATMMenu()
        {
            Console.WriteLine("Меню банкомата:");
            Console.WriteLine("Введите 1 чтобы снять деньги");
            Console.WriteLine("Введите 2 чтобы положить деньги");
            Console.WriteLine("Введите 3 чтобы узнать баланс");
            Console.WriteLine("Введите 4 чтобы сделать денежный перевод с карты на карту");
            Console.WriteLine("Введите 5 чтобы узнать количество доступных купюр для выдачи");
            Console.WriteLine("Введите 6 чтобы выйти из банкомата");
        }
        public static void withdrawMoney()
        {
            try
            {
                Session session = currentSession;
                int amount;
                if (session.IsActive())
                {
                    Console.Write("Введите требуемую сумму: ");
                    amount = Convert.ToInt32(Console.ReadLine());
                    Dictionary<int, int> result = currentATM.Withdraw(session, amount);
                    Console.Write("{0} = ", amount);
                    foreach (KeyValuePair<int, int> kvp in result)
                    {
                        Console.Write("{1} купюр по {0}, ", kvp.Key, kvp.Value);
                    }

                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Введена некорректная сумма");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public static void putMoney()
        {
            Session session = currentSession;
            currentATM.PutMoney(session);
        }
        public static void checkBalance()
        {
            Session session = currentSession;
            currentATM.ViewBalance(session);
        }
        public static void remittance()
        {
            Session session = currentSession;
            currentATM.Remittance(session);
        }
        public static void getAvailableBanknots()
        {
            currentATM.GetAvailableBanknots();
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
        public static void initClient()
        {
            int i = 0;
            foreach (BankClient client in currentBank.Clients)
            {
                Console.WriteLine(i + " " + client.FullName);
                i++;
            }
            int value = Convert.ToInt32(Console.ReadLine());
            i = 0;

            foreach (BankClient client in currentBank.Clients)
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
                Console.WriteLine(i + " " + bank.Name);
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
        public static void setATMMenu()
        {
            bool isTrue = true;
            getATMMenu();

                while (isTrue)
                {
                try
                {
                    int userResponse = Convert.ToInt32(Console.ReadLine());
                    switch (userResponse)
                    {
                        case 1:
                            withdrawMoney();
                            break;
                        case 2:
                            putMoney();
                            break;
                        case 3:
                            checkBalance();
                            break;
                        case 4:
                            remittance();
                            break;
                        case 5:
                            getAvailableBanknots();
                            break;
                        case 6:
                            isTrue = false;
                            clearConsole();
                            currentATM.StopNewSession(currentSession);
                            sendMenu();
                            break;
                        case 0:
                            clearConsole();
                            getATMMenu();
                            break;
                    }
                }
                catch (InvaidPutMoneyAmountException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Неверная команда");
                }
                
            }

        }
        public static void clearConsole()
        {
            Console.Clear();
        }
    }
    class BankNameException : Exception
    {
        public BankNameException(string message)
            : base(message) { }
    }
    class PassportNumberException : Exception
    {
        public PassportNumberException(string message)
            : base(message) { }
    }
    class BankCountException : Exception
    {
        public BankCountException(string message)
            : base(message) { }
    }
    class FullNameException : Exception
    {
        public FullNameException(string message)
            : base(message) { }
    }
    class ClientCountException : Exception
    {
        public ClientCountException(string message)
            : base(message) { }
    }
    class BankCardNumberException : Exception
    {
        public BankCardNumberException(string message)
            : base(message) { }
    }
    class ATMCountException : Exception
    {
        public ATMCountException(string message) 
            : base(message) { }
    }
    class InvalidSessionException : Exception
    {
        public InvalidSessionException(string message)
            : base(message) { }
    }
    class InvaidPutMoneyAmountException : Exception
    {
        public InvaidPutMoneyAmountException(string message)
            : base(message) { }
    }

}

