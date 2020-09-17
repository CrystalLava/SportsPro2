using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;

namespace SportsPro.Controllers
{
    public class ProductsController : Controller
    {
        private SportsProContext context { get; set; }

        public ProductsController(SportsProContext context)
        {
            this.context = context;
        }


        [Route("Products")] //Add Route
        [HttpGet]
        public IActionResult Index()
        {
            return View(context.Products.ToList());
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("Edit", new Product());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var p = context.Products.Find(id);
            return View(p);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (product.ProductID == 0)
                context.Products.Add(product);
            else
                context.Products.Update(product);

            context.SaveChanges();
            return RedirectToAction("Index", "Products");
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            ViewBag.Action = "Delete";
            var p = context.Products.Find(id);
            return View(p);
        }


        [HttpPost]
        public IActionResult Delete(Product product)
        {
            context.Products.Remove(product);
            context.SaveChanges();
            return RedirectToAction("Index", "Products");
        }


    }
}

