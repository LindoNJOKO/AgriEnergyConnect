using Microsoft.AspNetCore.Mvc;
using AgriEnergyConnect.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

public class AccountController : Controller
{
    private readonly AgriEnergyConnectDbContext _context;
    private readonly ILogger<AccountController> _logger;

    public AccountController(AgriEnergyConnectDbContext context, ILogger<AccountController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(Login login)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var user = _context.Logins.FirstOrDefault(u => u.Email == login.Email);

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "No account found with the provided email.");
                }
                else if (user.UserPassword != login.UserPassword)
                {
                    ModelState.AddModelError(string.Empty, "Incorrect password. Please try again.");
                }
                else
                {
                    var employee = _context.Employees.FirstOrDefault(e => e.Email == login.Email);
                    var farmer = _context.Farmers.FirstOrDefault(f => f.Email == login.Email);

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Email)
                    };

                    if (employee != null)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, "Employee"));
                    }
                    else if (farmer != null)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, "Farmer"));
                    }

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties { IsPersistent = true };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    if (employee != null)
                    {
                        return RedirectToAction("Dashboard", "Employee");
                    }
                    else if (farmer != null)
                    {
                        return RedirectToAction("Profile", "Farmer", new { email = farmer.Email });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during login.");
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");
            }
        }

        return View(login);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(Register model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                // Check if the email is already registered
                if (_context.Logins.Any(l => l.Email == model.Email))
                {
                    ModelState.AddModelError(string.Empty, "Email is already registered.");
                    return View(model);
                }

                // Add user to the database
                var newUser = new Login
                {
                    Email = model.Email,
                    UserPassword = model.UserPassword
                };

                _context.Logins.Add(newUser);

                if (model.Role == "Employee")
                {
                    var newEmployee = new Employee
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        Cell = model.Cell,
                        City = model.City,
                        EmployeePassword = model.UserPassword
                    };

                    _context.Employees.Add(newEmployee);
                }
                else if (model.Role == "Farmer")
                {
                    var newFarmer = new Farmer
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        Cell = model.Cell,
                        City = model.City,
                        FarmerPassword = model.UserPassword
                    };

                    _context.Farmers.Add(newFarmer);
                }

                await _context.SaveChangesAsync();

                // Redirect to the PendingReg page after successful registration
                return RedirectToAction("PendingReg");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during registration.");
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");
            }
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult PendingReg()
    {
        return View();
    }
}
