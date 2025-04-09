using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMConsoleApplication
{
    public class ATMService
    {
        private readonly AppDbContext _db = new AppDbContext();
        public void Withdraw(User user)
        {
            User user1 = _db.Users.FirstOrDefault(u => u.UserID== user.UserID)!;
            if (user1 == null)
            {
                Console.WriteLine("User not found.");
                return;
            }
            Console.Write("Enter amount to withdraw: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                if (amount > 0 && amount <= user.Wallet)
                {
                    user1.Wallet -= amount;
                    Transaction transaction = new Transaction
                    {
                        UserID = user.UserID,
                        Amount = amount,
                        TransactionDate = DateTime.Now,
                        TransactionType = "Withdraw"
                    };
                    _db.Transactions.Add(transaction);
                    var result = _db.SaveChanges();
                    if (result > 0)
                    {
                        Console.WriteLine("Transaction saved successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Transaction failed to save.");
                    }
                    Console.WriteLine($"Withdraw successful. New balance: ${user1.Wallet}");
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
            User user1 = _db.Users.FirstOrDefault(u => u.UserID == user.UserID)!;
            if (user1 == null)
            {
                Console.WriteLine("User not found.");
                return;
            }
            Console.Write("Enter amount to deposit: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                if (amount > 0)
                {
                    user1.Wallet += amount;
                    Transaction transaction = new Transaction
                    {
                        UserID = user.UserID,
                        Amount = amount,
                        TransactionDate = DateTime.Now,
                        TransactionType = "Deposit"
                    };
                    _db.Transactions.Add(transaction);
                    var result = _db.SaveChanges();
                    if (result > 0)
                    {
                        Console.WriteLine("Transaction saved successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Transaction failed to save.");
                    }
                    Console.WriteLine($"Deposit successful. New balance: ${user1.Wallet}");
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
            User user1 = _db.Users.FirstOrDefault(u => u.UserID == user.UserID)!;
            if (user1 == null)
            {
                Console.WriteLine("User not found.");
                return;
            }
            Console.WriteLine($"Your current balance: ${user1.Wallet}");
        }

        public List<User> GetAllUsers()
        {
            return _db.Users.ToList();
        }
        public User GetUserById(Guid userId)
        {
            return _db.Users.FirstOrDefault(u => u.UserID == userId)!;
        }

        public List<Transaction> GetTransactionHistory(User user)
        {
            User user1 = _db.Users.FirstOrDefault(u => u.UserID == user.UserID)!;
            List<Transaction> transactions = new List<Transaction>();
            transactions = _db.Transactions.Where(t => t.UserID == user1.UserID).ToList();
            return transactions;
        }
        //public List<User> AddUsers()
        //{
        //    List<User> users = new List<User>();
        //    users.Add(new User("user1", "pass1", 1000));
        //    users.Add(new User("user2", "pass2", 1500));
        //    users.Add(new User("user3", "pass3", 2000));
        //    users.Add(new User("user4", "pass4", 2500));
        //    users.Add(new User("user5", "pass5", 3000));
        //    return users;
        //}


    }
}
