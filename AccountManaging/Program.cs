using System;
using System.ComponentModel.Design;

namespace AccountManaging
{
    class Program
    {
        static AccountList accountList = new AccountList();
        public static int SearchById() {
            Console.Write("Enter Account ID to search: ");
            int searchId = int.Parse(Console.ReadLine());
            Account searchKey = new Account();
            searchKey.AccountId = searchId;

            int index = accountList.BinarySearch(new AccountIdComparer(), searchKey);
            if (index >= 0)
            {
                Console.WriteLine($"Account with Account ID {searchId} found at index {index}: ");
                accountList[index].Query();
            }
            else
            {
                Console.WriteLine($"Account with Account ID {searchId} not found.");
            }

            return index; 
        }
        public static void Menu()
        {
            Console.WriteLine("-------------Menu-------------");
            Console.WriteLine("1.\tAdd ");
            Console.WriteLine("2.\tSave ");
            Console.WriteLine("3.\tLoad ");
            Console.WriteLine("4.\tReport");
            Console.WriteLine("5.\tSort");
            Console.WriteLine("6.\tSearch");
            Console.WriteLine("7.\tRemove");
            Console.WriteLine("e|q.\tExit");           
        }
        public static void MenuSort()
        {
            Console.WriteLine("-------------Sort-------------");
            Console.WriteLine("1.\tAccount Id ");
            Console.WriteLine("2.\tFirst Name ");
            Console.WriteLine("3.\tLast Name ");
            Console.WriteLine("4.\tBalance");
            Console.WriteLine("e|q.\tBack");
        }

        public static void Sort() {
            char choice;
            do
            {
                MenuSort();
                Console.Write("Your choice: ");
                choice = Console.ReadKey().KeyChar;
                Console.WriteLine();
                switch (choice)
                {
                    case '1':
                        accountList.Sort(new AccountIdComparer());
                        return;
                    case '2':
                        accountList.Sort(new FirstNameComparer());
                        return;
                    case '3':
                        accountList.Sort(new LastNameComparer());
                        return;
                    case '4':
                        accountList.Sort(new BalanceComparer());
                        return;
                    case 'q':
                    case 'e':
                        break;
                    default:
                        Console.WriteLine("Error input again ! ");
                        break;

                }
            } while (choice != 'e' && choice != 'q');
        }

        public static void Remove()
        {
            int index = SearchById();
            if (index == -1)
            {
                return;
            }
            char choice;
            do
            {
                Console.Write("Ban co muon xoa Account tren ko (y/n): ");
                choice = Console.ReadKey().KeyChar;
                Console.WriteLine();
                switch (choice)
                {
                    case 'y':
                        accountList.Remove(index);
                        return;
                    case 'n':
                        return;
                    case 'q':
                    case 'e':
                        break;
                    default:
                        Console.WriteLine("Error input again ! ");
                        break;

                }

            } while (choice != 'e' && choice != 'q');
        }

        static void Main(string[] args)
        {
            
            char choice;
            do {
                Menu();
                Console.Write("Your choice: ");
                choice = Console.ReadKey().KeyChar;
                Console.WriteLine();
                switch (choice)
                {
                    case '1':
                        accountList.NewAccount();
                        break;
                    case '2':
                        accountList.SaveFile();
                        break;
                    case '3':
                        accountList.LoadFile();
                        break;
                    case '4':
                        accountList.Report();
                        break;
                    case '5':
                        Sort();
                        break;
                    case '6':
                        SearchById();
                        break;
                    case '7':
                        Remove();
                        break;
                    case 'q':
                    case 'e':
                        break;
                    default:
                        Console.WriteLine("Error input again ! ");
                        break;
                        
                }
            } while (choice != 'e' && choice != 'q');
        }
    }
}