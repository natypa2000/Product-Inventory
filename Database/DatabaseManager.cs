using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace Product_Inventory.Database
{
    public static class DatabaseManager
    {
        // Using SQL Express specific connection string
        private static string masterConnectionString = @"Server=.\SQLEXPRESS;Database=master;Integrated Security=True";
        private static string inventoryConnectionString = @"Server=.\SQLEXPRESS;Database=InventoryDB;Integrated Security=True";

        public static void InitializeDatabase()
        {
            try
            {
                // First, create the database using master connection
                using (var connection = new SqlConnection(masterConnectionString))
                {
                    connection.Open();
                    Console.WriteLine("Successfully connected to SQL Server!");

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT name FROM sys.databases WHERE name = 'InventoryDB'";
                        var result = command.ExecuteScalar();

                        if (result == null)
                        {
                            command.CommandText = "CREATE DATABASE InventoryDB";
                            command.ExecuteNonQuery();
                            Console.WriteLine("Database created successfully!");
                        }
                    }
                }

                // Now create the table in the new database
                using (var connection = new SqlConnection(inventoryConnectionString))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = @"
                            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Products' and xtype='U')
                            BEGIN
                                CREATE TABLE Products (
                                    Id INT PRIMARY KEY IDENTITY(1,1),
                                    Name NVARCHAR(100) NOT NULL,
                                    Price DECIMAL(18,2) NOT NULL,
                                    Quantity INT NOT NULL,
                                    Category NVARCHAR(50)
                                );
                                PRINT 'Products table created successfully!';
                            END";
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing database: {ex.Message}");
                Console.WriteLine("Stack Trace: " + ex.StackTrace);
                throw;
            }
        }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(inventoryConnectionString);
        }

        // Add this method to test the connection
        public static bool TestConnection()
        {
            try
            {
                using (var connection = new SqlConnection(masterConnectionString))
                {
                    connection.Open();
                    Console.WriteLine("Test connection successful!");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Connection test failed: {ex.Message}");
                return false;
            }
        }
    }
}
