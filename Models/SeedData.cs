using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace AgriEnergyConnect.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AgriEnergyConnectDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AgriEnergyConnectDbContext>>()))
            {
                // Look for any employees.
                if (!context.Employees.Any())
                {
                    context.Employees.AddRange(
                        new Employee
                        {
                            FirstName = "Default",
                            LastName = "Employee",
                            Email = "employee@agrienergyconnect.com",
                            EmployeePassword = "password123", // In a real application, ensure this is hashed
                            City = "CityName",
                            Cell = 1234567890
                        }
                    );
                }

                // Look for any farmers.
                if (!context.Farmers.Any())
                {
                    context.Farmers.AddRange(
                        new Farmer
                        {
                            FirstName = "Default",
                            LastName = "Farmer",
                            Email = "farmer@agrienergyconnect.com",
                            FarmerPassword = "password123", // In a real application, ensure this is hashed
                            City = "CityName",
                            Cell = 1234567890
                        }
                    );
                }

                // Ensure the corresponding login records are added
                if (!context.Logins.Any())
                {
                    context.Logins.AddRange(
                        new Login
                        {
                            Email = "employee@agrienergyconnect.com",
                            UserPassword = "password123" // Ensure this matches the Employee password
                        },
                        new Login
                        {
                            Email = "farmer@agrienergyconnect.com",
                            UserPassword = "password123" // Ensure this matches the Farmer password
                        }
                    );
                }

                context.SaveChanges();
            }
        }
    }
}
