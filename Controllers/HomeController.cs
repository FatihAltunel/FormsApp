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
    public async Task<IActionResult> Create(Product model, IFormFile ProductImage){

        var allowedExtensions= new[]{".jpg",".jpeg","png"};

    if(ModelState.IsValid && ProductImage!=null){
            var extension = Path.GetExtension(ProductImage.FileName); //ProductImage dosyasının ismindeki uzantıyı alır

            if(!allowedExtensions.Contains(extension)){
                ModelState.AddModelError("","Only '.png,' '.jpg' and '.jpeg' are allowed");
                ViewBag.Categories = new SelectList(Repository.Categories,"CategoryId", "Name");
                return View(model); 
            }

            var randomFileName = string.Format($"{Guid.NewGuid()}{extension}"); //uzantısı extension olan randım bir isim oluşturur

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomFileName); //randomFileName isimli dosyayı wwwroot/img dizinine yapıştırır
            using(var stream = new FileStream(path, FileMode.Create))
                {
                    await ProductImage.CopyToAsync(stream);
                }

            // using ifadesi: using ifadesi, bir kaynağı kullanıp işi bitirdikten sonra kaynağı otomatik olarak serbest bırakmak için kullanılır. Bu durumda, FileStream için kullanılacaktır.
            // FileStream oluşturma: FileStream, bir dosyayı diskte oluşturmak, okumak, yazmak veya üzerine yazmak için kullanılır. path değişkeni, oluşturulacak dosyanın yolunu içerir ve FileMode.Create ile dosyanın oluşturulacağı ve varsa üzerine yazılacağı belirtilir. Bu satırda yeni bir dosya oluşturulur veya mevcut dosyanın üzerine yazılır.
            // await ProductImage.CopyToAsync(stream): Bu, ProductImage adlı bir dosyanın içeriğini oluşturduğumuz stream adlı FileStream'e kopyalamak için kullanılır. ProductImage bir IFormFile nesnesidir ve bir dosyanın HTTP isteği sırasında sunucuya yüklenmesini temsil eder. CopyToAsync() metodu, ProductImage dosyasının içeriğini stream akışına asenkron olarak kopyalar. Dosya kopyalandıktan sonra, bu metot asenkron olarak tamamlanır ve işlem kontrol akışı devam eder.
            // await ifadesi: await, asenkron bir işlemin tamamlanmasını beklemek için kullanılır. Bu durumda, dosya kopyalama işlemi tamamlanana kadar beklenir ve daha sonra işlem devam eder.
            // stream kapatma: using bloğu bitince stream otomatik olarak kapatılır. Dosya kaynağına yapılan erişimlerin kapatılması, kaynağın güvenli bir şekilde serbest bırakılmasını sağlar. Bu da dosya kaynağının diğer işlemler için kullanılabilir hale gelmesini sağlar.

            model.Image=randomFileName;
            model.ProductId=Repository.Products.Count()+1;
            Repository.CreateProduct(model);
            return RedirectToAction("Index");
    }else{
            ViewBag.Categories = new SelectList(Repository.Categories,"CategoryId", "Name");
            return View(model); 
        }

}

}
