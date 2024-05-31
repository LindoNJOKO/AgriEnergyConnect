AgriEnergyConnect

AgriEnergyConnect is a web application designed to connect farmers and employees in the agricultural sector. It allows farmers to manage their profiles and products, while employees can view and manage farmers and their products. The application uses ASP.NET Core MVC for the backend and a SQL Server database for data storage.

Table of Contents

- [Features](#features)
- [Technologies Used](#technologies-used)
- [Folder Structure](#folder-structure)
- [Database Schema](#database-schema)
- [Setup Instructions](#setup-instructions)
- [Usage](#usage)
- [Contributing](#contributing)

Features

- User authentication and authorization using cookies.
- Separate dashboards for farmers and employees.
- Farmers can view and edit their profiles, add and view products.
- Employees can add farmers, view farmers, and view products.

Technologies Used

- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- Bootstrap (for front-end styling)

Folder Structure

```
/Controllers
    - AccountController.cs
    - FarmerController.cs
    - EmployeeController.cs
    - HomeController.cs
/Models
    - AgriEnergyConnectDbContext.cs
    - Employee.cs
    - ErrorViewModel.cs
    - Farmer.cs
    - Login.cs
    - Post.cs
    - Product.cs
    - Register.cs
    - SeedData.cs
/Views
  /Account
    - Register.cshtml
    - Login.cshtml
  /Farmer
    - Profile.cshtml
    - AddProduct.cshtml
    - ViewProducts.cshtml
  /Employee
    - AddFarmer.cshtml
    - ViewFarmers.cshtml
    - ViewProducts.cshtml
    - Dashboard.cshtml
  /Shared
    - _Layout.cshtml
    - _ValidationScriptsPartial.cshtml
    - Error.cshtml
wwwroot/
  - css/
  - js/
appsettings.json
Program.cs
```

Database Schema

- Employee
  - EmployeeId (Primary Key)
  - FirstName
  - LastName
  - Email
  - Cell
  - City
  - EmployeePassword

- Farmer
  - FarmerId (Primary Key)
  - FirstName
  - LastName
  - Email
  - Cell
  - City
  - FarmerPassword
  - Products (Navigation Property)

- Login
  - UserId (Primary Key)
  - Email
  - UserPassword

- Post
  - PostId (Primary Key)
  - PostDes
  - FarmerId (Foreign Key)
  - Farmer (Navigation Property)

- Product
  - ProductId (Primary Key)
  - ProductName
  - ProductDes
  - Category
  - FarmerId (Foreign Key)
  - Farmer (Navigation Property)

- Register
  - UserId (Primary Key)
  - FirstName
  - LastName
  - Email
  - Cell
  - City
  - UserPassword

Setup Instructions

1. Clone the repository

   ```bash
   git clone https://github.com/yourusername/AgriEnergyConnect.git
   cd AgriEnergyConnect
   ```

2. Set up the database

   Ensure that SQL Server is installed and running on your machine. Update the connection string in `appsettings.json` if necessary.

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Data Source=LAPTOP-II9F0GR5\\MSSQLSERVER01;Initial Catalog=AgriEnergyConnectDb;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"
     }
   }
   ```

3. Run database migrations

   Open the Package Manager Console in Visual Studio and run the following commands:

   ```bash
   Update-Database
   ```

   Alternatively, you can use the .NET CLI:

   ```bash
   dotnet ef database update
   ```

4. Run the application

   In Visual Studio, press F5 to run the application, or use the .NET CLI:

   ```bash
   dotnet run
   ```

5. Seed the database

   The application seeds the database with initial data using the `SeedData` class. This happens automatically when the application runs for the first time.

Usage

- Register and Login

  Users can register new accounts and log in using their credentials. Depending on whether the user is a farmer or an employee, they will be redirected to their respective dashboards.

- Farmer Dashboard

  - View and edit profile.
  - Add and view products.

- Employee Dashboard

  - Add new farmers.
  - View farmers and their products.

Contributing

Contributions are welcome! Please follow these steps to contribute:

1. Fork the repository.
2. Create a new branch for your feature or bugfix.
3. Make your changes.
4. Commit and push your changes to your fork.
5. Open a pull request.
