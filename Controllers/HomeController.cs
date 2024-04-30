using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FormsApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FormsApp.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index(string searchString, string category)
    {

        var products = Repository.Products;

        if(!String.IsNullOrEmpty(searchString)){
            ViewBag.searchString = searchString;    
            products = products.Where(p => p.Name.ToLower().Contains(searchString.ToLower() )).ToList();
        }

        if(!String.IsNullOrEmpty(category) && category!="0"){
            products = products.Where(p => p.CategoryId == int.Parse(category)).ToList();
        }


        var model = new ProductViewModel(){
            Products = products,
            Categories = Repository.Categories,
            SelectedCategory = string.IsNullOrEmpty(category) ? "0" : category
        };

        return View(model);
    }
    [HttpGet]
    public IActionResult Create(){
        ViewBag.Categories = new SelectList(Repository.Categories,"CategoryId", "Name");
        return View();
    }

    [HttpPost]
    public IActionResult Create(Product model){
        if(ModelState.IsValid){
            model.ProductId=Repository.Products.Count()+1;
            Repository.CreateProduct(model);
            return RedirectToAction("Index");

        }else{
            ViewBag.Categories = new SelectList(Repository.Categories,"CategoryId", "Name");
            return View(model); 
        }
    }

}
