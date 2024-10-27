# Inventory Management System
A C# console application for managing product inventory using SQL Server Express.

## Features
- View all products
- Add new products
- Update product quantities
- Delete products
- Search products by name or category
- SQL Server database integration

## Prerequisites
- Visual Studio 2019 or later
- SQL Server Express
- .NET 6.0 or later

## Setup
1. Clone the repository
2. Open the solution in Visual Studio
3. Make sure SQL Server Express is installed and running
4. Run the application - it will automatically create the database and required tables

## Usage
The application provides a simple console menu:
1. View all products
2. Add new product
3. Update product quantity
4. Delete product
5. Search product
6. Exit

## Database
The application automatically creates:
- Database: InventoryDB
- Table: Products with columns:
  - Id (INT, Primary Key)
  - Name (NVARCHAR(100))
  - Price (DECIMAL(18,2))
  - Quantity (INT)
  - Category (NVARCHAR(50))
