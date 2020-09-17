using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;

namespace SportsPro.Controllers
{
    public class CustomersController : Controller
    {
        private SportsProContext context { get; set; }

        public CustomersController(SportsProContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var Customers = context.Customers.ToList();
            return View(Customers);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Countries = context.Countries.ToList();
            return View("Edit", new Customer());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Countries = context.Countries.ToList();
            var t = context.Customers.Find(id);
            return View(t);
        }

        [HttpPost]
        public IActionResult Edit(Customer t)
        {
            if (t.CustomerID == 0)
                context.Customers.Add(t);
            else
                context.Customers.Update(t);

            context.SaveChanges();
            TempData["Success"] = "Success!";
            return RedirectToAction("Index", "Customers");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            ViewBag.Action = "Delete";
            var t = context.Customers.Find(id);
            return View(t);
        }


        [HttpPost]
        public IActionResult Delete(Customer t)
        {
            ViewBag.Action = "";
            context.Customers.Remove(t);
            context.SaveChanges();
            TempData["Success"] = "Success!";
            return RedirectToAction("Index", "Customers");
        }

    }
}

