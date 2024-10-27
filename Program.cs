using Product_Inventory.Database;
using Product_Inventory.Services;
using System;
using System.Data.SqlClient;
namespace Product_Inventory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var inventoryManager = new InventoryManager();
            bool running = true;

            try
            {
                Console.WriteLine("Testing database connection...");
                if (!DatabaseManager.TestConnection())
                {
                    Console.WriteLine("Failed to connect to database. Press any key to exit...");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine("Initializing database...");
                DatabaseManager.InitializeDatabase();

                while (running)
                {
                    Console.Clear();
                    Console.WriteLine("=== Inventory Management System ===");
                    Console.WriteLine("1. View all products");
                    Console.WriteLine("2. Add new product");
                    Console.WriteLine("3. Update product quantity");
                    Console.WriteLine("4. Delete product");
                    Console.WriteLine("5. Search product");
                    Console.WriteLine("6. Exit");
                    Console.Write("\nSelect an option: ");

                    switch (Console.ReadLine())
                    {
                        case "1":
                            inventoryManager.ViewAllProducts();
                            break;
                        case "2":
                            inventoryManager.AddProduct();
                            break;
                        case "3":
                            inventoryManager.UpdateProductQuantity();
                            break;
                        case "4":
                            inventoryManager.DeleteProduct();
                            break;
                        case "5":
                            inventoryManager.SearchProduct();
                            break;
                        case "6":
                            running = false;
                            break;
                        default:
                            Console.WriteLine("Invalid option. Press any key to continue...");
                            Console.ReadKey();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}