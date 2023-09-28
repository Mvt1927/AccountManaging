using System.Collections;

namespace AccountManaging
{
    interface IAccount
    {
        int AccountId { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        decimal Balance { get; set; }

        void Query();

    }
    class Account : IAccount
    {
        private int accountId;
        private string firstName;
        private string lastName;
        private decimal balance;

        public Account()
        {
            AccountId = 0;
            FirstName = "first name";
            LastName = "last name";
            Balance = 0;
        }
        public Account(int accountId, string firstName, string lastName, decimal balance)
        {
            AccountId = accountId;
            FirstName = firstName;
            LastName = lastName;
            Balance = balance;
        }

        public int AccountId { get => accountId; set => accountId = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public decimal Balance { get => balance; set => balance = value; }

        public void FillInfo()
        {
            Console.Write("Account ID: ");
            AccountId = int.Parse(Console.ReadLine());
            Console.Write("First Name: ");
            FirstName = Console.ReadLine();
            Console.Write("Last Name: ");
            LastName = Console.ReadLine();
            Console.Write("Balance: ");
            Balance = decimal.Parse(Console.ReadLine());
        }

        public void Query()
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine("Account ID: {0}", AccountId);
            Console.WriteLine("First Name: {0}", FirstName);
            Console.WriteLine("Last Name: {0}", LastName);
            Console.WriteLine("Balance: {0}", Balance);
            Console.WriteLine("------------------------------");

        }

    }
    class AccountList
    {
        private ArrayList Accounts = new ArrayList();
        public void LoadFile()
        {
            Console.Write("Nhap ten file de load: ");
            string fileName = Console.ReadLine();
            try
            {
                FileStream fileIn = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                StreamReader streamReader = new StreamReader(fileIn);
                Accounts.Clear();
                string str;
                while ((str = streamReader.ReadLine()) != null)
                {
                    string[] list = str.Split(",");
                    Account account = new Account(int.Parse(list[0]), list[1], list[2], decimal.Parse(list[3]));
                    Accounts.Add(account);
                }
                streamReader.Close();
                fileIn.Close();
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void SaveFile()
        {

            Console.Write("Nhap ten file de luu: ");
            string fileName = Console.ReadLine();
            try
            {
                FileStream fileOut = new FileStream(fileName, FileMode.CreateNew, FileAccess.Write);
                StreamWriter streamWriter = new StreamWriter(fileOut);

                foreach (Account account in Accounts)
                {
                    streamWriter.WriteLine("{0},{1},{2},{3}", account.AccountId, account.FirstName, account.LastName, account.Balance);
                }
                streamWriter.Close();
                fileOut.Close();
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void NewAccount()
        {
            Account a = new Account();
            a.FillInfo();
            Accounts.Add(a);
        }

        public void Report()
        {
            foreach (Account account in Accounts)
            {
                account.Query();
            }

        }
    }
}
