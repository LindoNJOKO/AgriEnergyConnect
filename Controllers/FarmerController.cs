using Microsoft.AspNetCore.Mvc;
using AgriEnergyConnect.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

public class FarmerController : Controller
{
    private readonly AgriEnergyConnectDbContext _context;
    private readonly ILogger<FarmerController> _logger;

    public FarmerController(AgriEnergyConnectDbContext context, ILogger<FarmerController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Profile(string email)
    {
        _logger.LogInformation("Profile action invoked with email: {Email}", email);

        if (string.IsNullOrEmpty(email))
        {
            _logger.LogWarning("Profile email parameter is null or empty.");
            return RedirectToAction("Login", "Account");
        }

        try
        {
            var farmer = await _context.Farmers
                                       .Include(f => f.Products) // Include related products
                                       .FirstOrDefaultAsync(f => f.Email == email);

            if (farmer == null)
            {
                _logger.LogWarning("Farmer not found with email: {Email}", email);
                return RedirectToAction("Login", "Account");
            }

            _logger.LogInformation("Farmer found with email: {Email}", email);
            return View(farmer);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving farmer profile for email: {Email}", email);
            return RedirectToAction("Login", "Account");
        }
    }

    [HttpGet]
    public IActionResult Edit(string email)
    {
        _logger.LogInformation("Edit action invoked with email: {Email}", email);

        if (string.IsNullOrEmpty(email))
        {
            _logger.LogWarning("Edit email parameter is null or empty.");
            return RedirectToAction("Login", "Account");
        }

        var farmer = _context.Farmers.FirstOrDefault(f => f.Email == email);
        if (farmer == null)
        {
            _logger.LogWarning("Farmer not found with email: {Email}", email);
            return RedirectToAction("Login", "Account");
        }

        _logger.LogInformation("Farmer found with email: {Email}", email);
        return View(farmer);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Farmer model)
    {
        _logger.LogInformation("Edit POST action invoked with model email: {Email}", model.Email);

        if (ModelState.IsValid)
        {
            try
            {
                var farmer = _context.Farmers.FirstOrDefault(f => f.Email == model.Email);
                if (farmer == null)
                {
                    _logger.LogWarning("Farmer not found with email: {Email}", model.Email);
                    return RedirectToAction("Login", "Account");
                }

                farmer.FirstName = model.FirstName;
                farmer.LastName = model.LastName;
                farmer.Cell = model.Cell;
                farmer.City = model.City;
                farmer.FarmerPassword = model.FarmerPassword;

                _context.Farmers.Update(farmer);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Farmer profile updated for email: {Email}", model.Email);
                return RedirectToAction("Profile", new { email = farmer.Email });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while editing farmer profile for email: {Email}", model.Email);
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");
            }
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult AddProduct(string email)
    {
        _logger.LogInformation("AddProduct GET action invoked with email: {Email}", email);

        if (string.IsNullOrEmpty(email))
        {
            _logger.LogWarning("AddProduct email parameter is null or empty.");
            return RedirectToAction("Login", "Account");
        }

        var farmer = _context.Farmers.FirstOrDefault(f => f.Email == email);
        if (farmer == null)
        {
            _logger.LogWarning("Farmer not found with email: {Email}", email);
            return RedirectToAction("Login", "Account");
        }

        var product = new Product
        {
            FarmerId = farmer.FarmerId,
            Farmer = farmer
        };

        _logger.LogInformation("Farmer found for AddProduct with email: {Email}", email);
        return View(product);
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct(Product model)
    {
        _logger.LogInformation("AddProduct POST action invoked with model FarmerId: {FarmerId}", model.FarmerId);

        if (ModelState.IsValid)
        {
            try
            {
                _context.Products.Add(model);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Product added successfully for FarmerId: {FarmerId}", model.FarmerId);
                return RedirectToAction("Profile", new { email = model.Farmer?.Email });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding product for FarmerId: {FarmerId}", model.FarmerId);
                ModelState.AddModelError("", "An error occurred while adding the product.");
            }
        }

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> ViewProducts(int farmerId)
    {
        _logger.LogInformation("ViewProducts action invoked with farmerId: {FarmerId}", farmerId);

        if (farmerId <= 0)
        {
            _logger.LogWarning("Invalid farmer ID: {FarmerId}", farmerId);
            return BadRequest("Invalid farmer ID.");
        }

        var products = await _context.Products
                                     .Where(p => p.FarmerId == farmerId)
                                     .ToListAsync();

        _logger.LogInformation("Products retrieved for farmerId: {FarmerId}", farmerId);
        return View(products);
    }
}
