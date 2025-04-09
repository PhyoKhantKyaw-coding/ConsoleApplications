using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailManagementConsoleApplication;

public class RetailService
{
    private readonly AppDbContext _db = new AppDbContext();
    //List<Product> prods = new List<Product>();
    //List<Sale> sales = new List<Sale>();

    public void Retail()
    {

        //SeedProducts();
        while (true)
        {
            Console.WriteLine("\n--- Retail Management Console ---");
            Console.WriteLine("1. Stock Menu");
            Console.WriteLine("2. Cashier Menu");
            Console.WriteLine("3. Manager Menu");
            Console.WriteLine("4. Exit");
            Console.Write("Select menu: ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    StockMenu();
                    break;
                case "2":
                    CashierMenu();
                    break;
                case "3":
                    ManagerMenu();
                    break;
                case "4":
                    Console.WriteLine("Exiting.....");
                    return;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }
    private void StockMenu()
    {

        while (true)
        {
            Console.WriteLine("\n--- Retail Management Console ---");
            Console.WriteLine("1. Display Products");
            Console.WriteLine("2. Add Product");
            Console.WriteLine("3. Edit Product");
            Console.WriteLine("4. Back");
            Console.Write("Select menu: ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Display();
                    break;
                case "2":
                    AddProduct();
                    break;
                case "3":
                    EditProduct();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }
    private void AddProduct()
    {
        Console.WriteLine("=== Add New Product ===");

        string name = GetValidatedString("Enter product name: ");
        int stock = GetValidatedInt("Enter stock: ");
        decimal price = GetValidatedDecimal("Enter price: ");
        decimal cost = GetValidatedDecimal("Enter cost: ");

        int newId = _db.Products.Any() ? _db.Products.Max(p => p.Id) + 1 : 1; 
        Product product = new Product
        {
            Id = newId,
            Name = name,
            Stock = stock,
            Price = price,
            Cost = cost
        };
        _db.Products.Add(product);
        var result = _db.SaveChanges();
        if (result > 0)
        {
            Console.WriteLine("Product added successfully.");
        }
        else
        {
            Console.WriteLine("Failed to add product.");
        }

        Console.WriteLine("✅ Product added successfully!\n");
    }
    private void EditProduct()
    {
        Console.WriteLine("=== Edit Product ===");
        int editId = GetValidatedInt("Enter product ID to edit: ");
        var prod = _db.Products.FirstOrDefault(p => p.Id == editId);

        if (prod == null)
        {
            Console.WriteLine(" Product not found.\n");
            return;
        }

        Console.WriteLine($"Editing Product: {prod.Name} (ID: {prod.Id})");

        string newName = GetValidatedString($"New name (or press Enter to keep \"{prod.Name}\"): ", allowEmpty: true);
        if (!string.IsNullOrWhiteSpace(newName)) prod.Name = newName;

        int newStock = GetValidatedInt($"New stock (or press Enter to keep current: {prod.Stock}): ", allowSkip: true);
        if (newStock >= 0) prod.Stock = newStock;

        decimal newPrice = GetValidatedDecimal($"New price (or press Enter to keep current: {prod.Price:C}): ", allowSkip: true);
        if (newPrice >= 0) prod.Price = newPrice;

        decimal newCost = GetValidatedDecimal($"New cost (or press Enter to keep current: {prod.Cost:C}): ", allowSkip: true);
        if (newCost >= 0) prod.Cost = newCost;
        _db.Entry(prod).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        var result = _db.SaveChanges();
        if (result > 0)
        {
            Console.WriteLine("Product updated successfully.");
        }
        else
        {
            Console.WriteLine("Failed to update product.");
        }

        Console.WriteLine(" Product updated successfully!\n");
    }
    private string GetValidatedString(string prompt, bool allowEmpty = false)
    {
        string input;
        do
        {
            Console.Write(prompt);
            input = Console.ReadLine();

            if (allowEmpty && string.IsNullOrWhiteSpace(input))
                return input;

            if (!string.IsNullOrWhiteSpace(input) && input.Any(c => !char.IsDigit(c)))
                return input;

            Console.WriteLine("Invalid input.");
        } while (true);
    }

    private int GetValidatedInt(string prompt, bool allowSkip = false)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            if (allowSkip && string.IsNullOrWhiteSpace(input))
                return -1; 
            if (int.TryParse(input, out int value) && value >= 0)
                return value;
            Console.WriteLine(" Invalid input. Please enter a valid non-negative integer");
        }
    }
    private decimal GetValidatedDecimal(string prompt, bool allowSkip = false)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();

            if (allowSkip && string.IsNullOrWhiteSpace(input))
                return -1;

            if (decimal.TryParse(input, out decimal value) && value >= 0)
                return value;

            Console.WriteLine("Invalid input. Please enter a valid non-negative decimal.");
        }
    }
    //private void SeedProducts()
    //{
    //    prods.Add(new Product { Id = 1, Name = "Pen", Stock = 100, Price = 1.50m, Cost = 1.00m });
    //    prods.Add(new Product { Id = 2, Name = "Notebook", Stock = 50, Price = 3.00m, Cost = 2.00m });
    //}
    private void Display()
    {
        Console.WriteLine("{0,-12} | {1,-20} | {2,-14} | {3,-12} | {4,-12}",
            "Product ID", "Product Name", "Stock", "Price Per Item","Profit Per Item");
        Console.WriteLine(new string('-', 70));
        var prods = _db.Products.ToList();
        foreach (var product in prods)
        {
            Console.WriteLine("{0,-12} | {1,-20} | {2,-14} | {3,-12:C} | {4,-12}",
                product.Id,product.Name, product.Stock, product.Price,product.Profit);
        }
    }
    private void CashierMenu()
    {
        Console.WriteLine("\n--- Cashier Menu ---");
        List<Cartitem> cart = new List<Cartitem>();
        string input;
        do
        {
            Console.Write("Enter product ID (or 'done' to finish): ");
            input = Console.ReadLine();
            if (input.ToLower() != "done")
            {
                int id = int.Parse(input);
                var product = _db.Products.FirstOrDefault(p => p.Id == id);
                if (product != null)
                {
                    Console.Write("Enter quantity: ");
                    int qty = int.Parse(Console.ReadLine());
                    if (product.Stock >= qty)
                    {
                        cart.Add(new Cartitem { Product = product, Quantity = qty });
                    }
                    else
                    {
                        Console.WriteLine("Insufficient stock.");
                    }
                }
                else
                {
                    Console.WriteLine("Product not found.");
                }
            }
        } while (input.ToLower() != "done");

        Console.WriteLine("\n--- Order Summary ---");
        decimal total = 0;
        foreach (var item in cart)
        {
            decimal itemTotal = item.Quantity * item.Product.Price;
            total += itemTotal;
            Console.WriteLine($"{item.Product.Name} x{item.Quantity} = {itemTotal:C}");
        }

        Console.WriteLine($"Total: {total:C}");
        Console.Write("Confirm payment? (yes/no): ");
        if (Console.ReadLine().ToLower() == "yes")
        {
            var result = 0;
            foreach (var item in cart)
            {
                item.Product.Stock -= item.Quantity;
                Sale sale = new Sale
                {
                    Id = Guid.NewGuid(),
                    ProductId = item.Product.Id,
                    ProductName = item.Product.Name,
                    Quantity = item.Quantity,
                    Price = item.Product.Price,
                    Profit = (item.Product.Price - item.Product.Cost) * item.Quantity
                };
                _db.Sales.Add(sale);
                result = _db.SaveChanges();

                //salesReport.Add(new Sale
                //{
                //    Id = Guid.NewGuid(),
                //    ProductId = item.Product.Id,
                //    ProductName = item.Product.Name,
                //    Quantity = item.Quantity,
                //    Price = item.Product.Price,
                //    Profit = (item.Product.Price - item.Product.Cost) * item.Quantity
                //});
            }
            if (result > 0)
            {
                Console.WriteLine("Transaction saved successfully.");
            }
            else
            {
                Console.WriteLine("Transaction failed to save.");
            }
            Console.WriteLine("Payment completed. Stock updated.");
        }
        else
        {
            Console.WriteLine("Purchase cancelled.");
        }
    }
    private void ManagerMenu()
    {
        Console.WriteLine("\n--- Manager Menu ---\n");
        var sales = _db.Sales.ToList();

        if (!sales.Any())
        {
            Console.WriteLine("No sales have been made yet.");
            return;
        }

        Console.WriteLine("{0,-12} | {1,-20} | {2,-14} | {3,-12} | {4,-10}",
            "Product ID", "Product Name", "Quantity Sold", "Price Per Item", "Profit");
        Console.WriteLine(new string('-', 70));
        foreach (var sale in sales)
        {
            Console.WriteLine("{0,-12} | {1,-20} | {2,-14} | {3,-12:C} | {4,-10:C}",
                sale.ProductId, sale.ProductName, sale.Quantity, sale.Price, sale.Profit);
        }

        Console.WriteLine(new string('-', 70));
        decimal totalRevenue = sales.Sum(s => s.Price * s.Quantity);
        decimal totalProfit = sales.Sum(s => s.Profit);

        Console.WriteLine("{0,49} | {1,-12:C} | {2,-10:C}", "TOTAL:", totalRevenue, totalProfit);
        Console.WriteLine();
    }

}
