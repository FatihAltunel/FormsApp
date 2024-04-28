using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FormsApp.Models;

namespace FormsApp.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index(string searchString)
    {

        var Products = Repository.Products;

        if(!String.IsNullOrEmpty(searchString)){
            ViewBag.searchString = searchString;    
            Products = Products.Where(p => p.Name.ToLower().Contains(searchString.ToLower())).ToList();
        }

        return View(Products);
    }

}
