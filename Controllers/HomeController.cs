using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductAndCategories_2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ProductAndCategories.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;

          public HomeController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return RedirectToAction("product");
        }

        [HttpGet("product")]
        public IActionResult product()
        {
            List<Product> p = dbContext.products.ToList();
            ViewBag.product = p;
            return View();
        }

         [HttpGet("categories")]
        public IActionResult categories()
        {
            List<Category> c = dbContext.categories.ToList();
            ViewBag.category = c;
            return View();
        }

        [HttpPost("postProduct")]
         public IActionResult postProduct(Product d)
        {
            if(ModelState.IsValid){

                dbContext.Add(d);
                dbContext.SaveChanges();
                return RedirectToAction("product");
            }
            return View("product");
        }

         [HttpPost("postCategory")]
         public IActionResult postCategory(Category c)
        {
            if(ModelState.IsValid){

                dbContext.Add(c);
                dbContext.SaveChanges();
                return RedirectToAction("categories");
            }
            return View("categories");
        }

         [HttpGet("products/{ProductId}")]
         public IActionResult categoryNew(int ProductId){
            Product oneproduct = dbContext.products.Include(p=>p.Categories).ThenInclude(p=>p.Category).FirstOrDefault(p=>p.ProductId == ProductId);

          

            List<Category> allcategory = dbContext.categories.ToList();
            List<Category> usedCategories = new List<Category>();

            foreach(Association a in oneproduct.Categories)
            {
                usedCategories.Add(a.Category);
            }

             List<Category> NotUsedCategories = new List<Category>();
             foreach(Category c in allcategory){
                 if(!usedCategories.Contains(c)){
                    NotUsedCategories.Add(c);
                 }
             }

            ViewBag.usedCategories = usedCategories;
            ViewBag.NOTusedCategories = NotUsedCategories;
            ViewBag.product = oneproduct;

            return View();
         }
        [HttpGet("categories/{CategoryId}")]
        public IActionResult productNew(int CategoryId){
            Category oneCategory = dbContext.categories.Include(p=>p.Products).ThenInclude(p=>p.Product).FirstOrDefault(p=>p.CategoryId==CategoryId);

            List<Product> allProduct = dbContext.products.ToList();
            List<Product> usedProduct = new List<Product>();

            foreach(Association a in oneCategory.Products){
                usedProduct.Add(a.Product);
            }

            List<Product> notUsedProduct = new List<Product>();
            foreach(Product p in allProduct){
                if(!usedProduct.Contains(p)){
                    notUsedProduct.Add(p);
                }
            }

            ViewBag.usedProduct = usedProduct;
            ViewBag.notUsedProduct = notUsedProduct;
            ViewBag.category = oneCategory;

            return View();
        }

          [HttpPost("addCategory")]
          public IActionResult addCategory(Association ass){
              int id = ass.ProductId;
              dbContext.associations.Add(ass);
              dbContext.SaveChanges();
              //This uses the route
              return Redirect($"products/{id}");

          }

           [HttpPost("addProduct")]
          public IActionResult addProduct(Association ass){
              int id = ass.CategoryId;
              dbContext.associations.Add(ass);
              dbContext.SaveChanges();
              //This uses the route
              return Redirect($"categories/{id}");

          }
        
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
