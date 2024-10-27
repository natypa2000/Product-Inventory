using System;
using System.Data.SqlClient;
using Product_Inventory.Models;
using Product_Inventory.Database;


namespace Product_Inventory.Services
{
    public class InventoryManager
    {
        public void ViewAllProducts()
        {
            Console.Clear();
            Console.WriteLine("=== All Products ===\n");

            using (var connection = DatabaseManager.GetConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Products";
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            Console.WriteLine("No products found.");
                        }

                        while (reader.Read())
                        {
                            Console.WriteLine(new Product
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Price = reader.GetDecimal(2),
                                Quantity = reader.GetInt32(3),
                                Category = reader.GetString(4)
                            });
                        }
                    }
                }
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        public void AddProduct()
        {
            Console.Clear();
            Console.WriteLine("=== Add New Product ===\n");

            Console.Write("Enter product name: ");
            string name = Console.ReadLine();

            Console.Write("Enter price: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                Console.WriteLine("Invalid price format.");
                return;
            }

            Console.Write("Enter quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity))
            {
                Console.WriteLine("Invalid quantity format.");
                return;
            }

            Console.Write("Enter category: ");
            string category = Console.ReadLine();

            using (var connection = DatabaseManager.GetConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        INSERT INTO Products (Name, Price, Quantity, Category)
                        VALUES (@Name, @Price, @Quantity, @Category)";

                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Price", price);
                    command.Parameters.AddWithValue("@Quantity", quantity);
                    command.Parameters.AddWithValue("@Category", category);

                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine(rowsAffected > 0 ? "\nProduct added successfully!" : "\nFailed to add product.");
                }
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        public void UpdateProductQuantity()
        {
            Console.Clear();
            Console.WriteLine("=== Update Product Quantity ===\n");

            Console.Write("Enter product ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID format.");
                return;
            }

            Console.Write("Enter new quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity))
            {
                Console.WriteLine("Invalid quantity format.");
                return;
            }

            using (var connection = DatabaseManager.GetConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE Products SET Quantity = @Quantity WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Quantity", quantity);

                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine(rowsAffected > 0 ? "\nQuantity updated successfully!" : "\nProduct not found.");
                }
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        public void DeleteProduct()
        {
            Console.Clear();
            Console.WriteLine("=== Delete Product ===\n");

            Console.Write("Enter product ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID format.");
                return;
            }

            using (var connection = DatabaseManager.GetConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM Products WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", id);

                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine(rowsAffected > 0 ? "\nProduct deleted successfully!" : "\nProduct not found.");
                }
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        public void SearchProduct()
        {
            Console.Clear();
            Console.WriteLine("=== Search Product ===\n");

            Console.Write("Enter product name or category to search: ");
            string searchTerm = Console.ReadLine();

            using (var connection = DatabaseManager.GetConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        SELECT * FROM Products 
                        WHERE Name LIKE @SearchTerm 
                        OR Category LIKE @SearchTerm";
                    command.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");

                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            Console.WriteLine("No products found.");
                        }

                        while (reader.Read())
                        {
                            Console.WriteLine(new Product
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Price = reader.GetDecimal(2),
                                Quantity = reader.GetInt32(3),
                                Category = reader.GetString(4)
                            });
                        }
                    }
                }
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
