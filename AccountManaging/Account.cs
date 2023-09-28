using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

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
    public class Account : IAccount, IComparable
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


        public int CompareTo(object? obj)
        {
            if (obj is not Account)
            {
                throw new ArgumentException();
            }
            Account account = obj as Account;
            int compare = this.AccountId.CompareTo(account.AccountId);
            if (compare != 0)
            {
                return compare;
            }
            compare = this.FirstName.CompareTo(account.FirstName);
            if (compare != 0)
            {
                return compare;
            }
            compare = this.LastName.CompareTo(account.LastName);
            if (compare != 0)
            {
                return compare;
            }
            return this.Balance.CompareTo(account.Balance);
        }

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

    class AccountIdComparer : IComparer
    {
        public int Compare(object? x, object? y)
        {
            if (x is Account && y is Account)
            {
                return (x as Account).AccountId.CompareTo((y as Account).AccountId);
            }
            return 0;
        }
    }
    class FirstNameComparer : IComparer
    {
        public int Compare(object? x, object? y)
        {
            if (x is Account && y is Account)
            {
                return string.Compare((x as Account).FirstName, (y as Account).FirstName);
            }
            return 0;
        }
    }

    class LastNameComparer : IComparer
    {
        public int Compare(object? x, object? y)
        {
            if (x is Account && y is Account)
            {
                return string.Compare((x as Account).LastName, (y as Account).LastName);
            }
            return 0;
        }
    }

    class BalanceComparer : IComparer
    {
        public int Compare(object? x, object? y)
        {
            if (x is Account && y is Account)
            {
                return (x as Account).Balance.CompareTo((y as Account).Balance);
            }
            return 0;
        }
    }
    class AccountList
    {
        private ArrayList Accounts = new ArrayList();


        public Account this[int index]
        {
            get
            {
                if (index >= 0 && index < Accounts.Count)
                {
                    return (Account)Accounts[index];
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
            set
            {
                if (index >= 0 && index < Accounts.Count)
                {
                    Accounts[index] = value;
                }
                else if (index == Accounts.Count)
                {
                    Accounts.Add(value);
                }
                else throw new IndexOutOfRangeException();
            }
        }
        public void LoadFile()
        {
            Console.Write("Nhap ten file de load (de trong de thoat): ");
            string fileName = Console.ReadLine();
            if (fileName == "")
            {
                return;
            }
            if (!fileName.EndsWith(".xml"))
            {
                fileName += ".xml";
            }
            try
            {
                FileStream fileIn = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(ArrayList), new Type[] { typeof(Account) });
                Accounts.Clear();
                Accounts = xmlSerializer.Deserialize(fileIn) as ArrayList;
                Console.WriteLine("Load du lieu thanh cong");
                fileIn.Close();
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void SaveFile()
        {

            // Code that uses obsolete API.
            // ...

            // Re-enable the warning.
            Console.Write("Nhap ten file de luu (de trong de thoat): ");
            string fileName = Console.ReadLine();
            if (fileName == "")
            {
                return;
            }
            if (!fileName.EndsWith(".xml"))
            {
                fileName += ".xml";
            }
            try
            {
                FileStream fileOut = new FileStream(fileName, FileMode.CreateNew, FileAccess.Write);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(ArrayList), new Type[] { typeof(Account) });
                StreamWriter streamWriter = new StreamWriter(fileOut);
                xmlSerializer.Serialize(streamWriter, Accounts);
                Console.WriteLine("Luu du lieu thanh cong");
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
        public int BinarySearch(IComparer comparer, object key)
        {
            return Accounts.BinarySearch(key, comparer);
        }

        public void Sort()
        {
            Accounts.Sort();
            Console.WriteLine("-----------Da sort----------");
        }

        public void Sort(IComparer comparer)
        {
            Accounts.Sort(comparer);
            Console.WriteLine("-----------Da sort----------");

        }

        public void Remove(int index)
        {
            Accounts.RemoveAt(index);
            Console.WriteLine("-----------Da xoa----------");
        }
    }
}

