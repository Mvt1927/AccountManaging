using System;
using System.ComponentModel.Design;

namespace AccountManaging
{
    class Program
    {
        public static void Menu()
        {
            Console.WriteLine("-------------Menu-------------");
            Console.WriteLine("a.\tAdd ");
            Console.WriteLine("s.\tSave ");
            Console.WriteLine("l.\tLoad ");
            Console.WriteLine("r.\tReport");
            Console.WriteLine("e|q.\tExit");           
        }
        static void Main(string[] args)
        {
            AccountList accountList = new AccountList();
            char choice;
            do {
                Menu();
                Console.Write("Your choice: ");
                choice = Console.ReadKey().KeyChar;
                Console.WriteLine();
                switch (choice)
                {
                    case 'a':
                        accountList.NewAccount();
                        break;
                    case 's':
                        accountList.SaveFile();
                        break;
                    case 'l':
                        accountList.LoadFile();
                        break;
                    case 'r':
                        accountList.Report();
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