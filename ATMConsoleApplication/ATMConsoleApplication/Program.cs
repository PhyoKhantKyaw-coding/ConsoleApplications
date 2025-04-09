// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");
using ATMConsoleApplication;

ATMService atmService = new ATMService();

List<User> users = atmService.GetAllUsers();

Console.WriteLine("----- Welcome to ATM Console Application -----");
login();
void login()
{
    while (true)
    {
        User loggedInUser = null;
        int attempts = 0;

        while (loggedInUser == null && attempts < 3)
        {
            Console.Write("\nEnter UserName: ");
            string userName = Console.ReadLine();
            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            loggedInUser = users.Find(u => u.UserName == userName && u.Password == password);

            if (loggedInUser == null)
            {
                attempts++;
                Console.WriteLine($"Login Failed! Attempt {attempts}/3");

                if (attempts == 3)
                {
                    Console.WriteLine("Account is locked. Please restart the program.");
                    return;
                }
            }
            else
            {
                Console.WriteLine("\nLogin Success!");
                break;
            }
        }

        if (loggedInUser != null)
        {
            ShowMenu(atmService, loggedInUser);
        }
    }
}
void ShowMenu(ATMService atmService, User user)
{
    while (true)
    {
        Console.WriteLine("\n--- ATM Menu ---");
        Console.WriteLine("1. Withdraw");
        Console.WriteLine("2. Deposit");
        Console.WriteLine("3. Check Balance");
        Console.WriteLine("4. Logout");
        Console.WriteLine("5. History");
        Console.WriteLine("6. End Program");
        Console.Write("Choose an option (1-6): ");
        string option = Console.ReadLine()!;

        switch (option)
        {
            case "1":
                atmService.Withdraw(user);
                break;
            case "2":
                atmService.Deposit(user);
                break;
            case "3":
                atmService.CheckBalance(user);
                break;
            case "4":
                Console.WriteLine("\nDo you want to logout ! y/n:");
                string result = Console.ReadLine()!;
                if (result == "n") 
                {
                    break; 
                }
                else
                {
                    Console.WriteLine("loging out...");
                    Console.WriteLine("\nDo you want to login in again ! y/n:");
                    string result2 = Console.ReadLine()!;
                    if (result2 == "n")
                    {
                        Console.WriteLine("Ending program...");
                        Environment.Exit(0);
                    }
                    else
                    {
                        login();
                        return;
                    }
                    return;
                }
            case "5":
                List<Transaction> trans = atmService.GetTransactionHistory(user);
                Console.WriteLine("Transaction History:\n");
                Console.WriteLine("{0,-5} {1,-20} {2,-10} {3,-20} {4,-15}",
                    "No", "UserName", "Amount", "Date", "Type");
                Console.WriteLine(new string('-', 70));

                User user1 = atmService.GetUserById(user.UserID);
                int transactionNo = 1;
                foreach (Transaction t in trans)
                {
                    Console.WriteLine("{0,-5} {1,-20} {2,-10:C} {3,-20} {4,-15} ",
                        transactionNo++, 
                        user1.UserName,
                        t.Amount,
                        t.TransactionDate.ToString("yyyy-MM-dd HH:mm"),
                        t.TransactionType);                        
                }
                break;
            case "6":
                Console.WriteLine("Ending program...");
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Invalid option. Please try again.");
                break;
        }
    }
}
