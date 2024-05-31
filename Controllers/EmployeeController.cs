using Microsoft.AspNetCore.Mvc;
using AgriEnergyConnect.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class EmployeeController : Controller
{
    private readonly AgriEnergyConnectDbContext _context;

    public EmployeeController(AgriEnergyConnectDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult AddFarmer()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddFarmer(Farmer model)
    {
        if (ModelState.IsValid)
        {
            _context.Farmers.Add(model);
            _context.SaveChanges();
            return RedirectToAction("ViewFarmers");
        }
        return View(model);
    }

    public IActionResult ViewFarmers()
    {
        var farmers = _context.Farmers.Include(f => f.Products).ToList();
        return View(farmers);
    }

    public IActionResult ViewProducts(string category)
    {
        var products = _context.Products.Where(p => p.Catergry == category).ToList();
        return View(products);
    }

    public IActionResult Dashboard()
    {
        return View();
    }
}
