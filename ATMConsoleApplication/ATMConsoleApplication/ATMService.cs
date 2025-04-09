using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMConsoleApplication
{
    public class ATMService
    {
        public void Withdraw(User user)
        {
            Console.Write("Enter amount to withdraw: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                if (amount > 0 && amount <= user.Wallet)
                {
                    user.Wallet -= amount;
                    Console.WriteLine($"Withdraw successful. New balance: ${user.Wallet}");
                }
                else
                {
                    Console.WriteLine("Invalid amount or insufficient funds.");
                }
            }
            else 
            {
                Console.WriteLine("Invalid input.");
            }
        }

        public void Deposit(User user)
        {
            Console.Write("Enter amount to deposit: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                if (amount > 0)
                {
                    user.Wallet += amount;
                    Console.WriteLine($"Deposit successful. New balance: ${user.Wallet}");
                }
                else
                {
                    Console.WriteLine("Amount must be greater than zero.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }
        public void CheckBalance(User user)
        {
            Console.WriteLine($"Your current balance: ${user.Wallet}");
        }
        public List<User> AddUsers()
        {
            List<User> users = new List<User>();
            users.Add(new User("user1", "pass1", 1000));
            users.Add(new User("user2", "pass2", 1500));
            users.Add(new User("user3", "pass3", 2000));
            users.Add(new User("user4", "pass4", 2500));
            users.Add(new User("user5", "pass5", 3000));
            return users;
        }

    }
}
