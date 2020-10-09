using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;
using SportsPro.Models.DataLayer;

namespace SportsPro.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private IGRepository<Product> ProductRepo { get; set; }

        private IUnitOfWork UnitOfWork { get; }
        public ProductsController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            ProductRepo = unitOfWork.ProductRepository;
        }

        [TempData]
        public string Message { get; set; }


        [Route("Products")] //Add Route
        [HttpGet]
        public ViewResult Index()
        {
            ViewBag.Action = "Edit";
            var Products = ProductRepo.Get(orderBy: products => products.OrderBy(g => g.Name)).ToList();
            return View(Products);
        }

        [HttpGet]
        public ViewResult Add()
        {
            ViewBag.Action = "Add";
            
            return View("Edit", new Product());
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Products = ProductRepo.Get(orderBy: products => products.OrderBy(g => g.Name)).ToList();
            var product = ProductRepo.Get(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ProductID == 0)
                    ProductRepo.Insert(product);
                else
                    ProductRepo.Update(product);
                UnitOfWork.Save();
                TempData["message"] = $"{product.Name} added to database";
                return RedirectToAction("Index", "Product");
            }
            else
            {
                ViewBag.Action = (product.ProductID == 0) ? "Add" : "Edit";
                ViewBag.Products = ProductRepo.Get(orderBy: products => products.OrderBy(g => g.Name)).ToList();
                return View(product);
            }
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = ProductRepo.Get(id);
            return View(product);
        }


        [HttpPost]
        public IActionResult Delete(Product product)
        {
            ProductRepo.Delete(product);
            TempData["message"] = $"{product.Name} deleted from database";
            return RedirectToAction("List", "Product");
        }


    }
}

