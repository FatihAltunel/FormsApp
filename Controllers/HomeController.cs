using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FormsApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FormsApp.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index(string searchString, string category)
    {

        var Products = Repository.Products;

        ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");

        if(!String.IsNullOrEmpty(searchString)){
            ViewBag.searchString = searchString;    
            Products = Products.Where(p => p.Name.ToLower().Contains(searchString.ToLower())).ToList();
        }

        if(!String.IsNullOrEmpty(category) && category!="0"){
            Products = Products.Where(p => p.CategoryId == int.Parse(category)).ToList();
        }

        return View(Products);
    }

}
