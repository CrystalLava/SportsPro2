using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;


namespace SportsPro.Controllers
{
    [Authorize(Roles = "Admin")]//Only admin has access to this page
    public class CustomersController : Controller
    {
        private SportsProContext context { get; set; }

        public CustomersController(SportsProContext context)
        {
            this.context = context;
        }
        [TempData]//Temp message for top of the page when customer added/edited/deleted
        public string Message { get; set; }

        [Route("Customers")]// Add Route
        public IActionResult Index()//Load customers in order of first name
        {
            var Customers = context.Customers.OrderBy(g => g.FirstName).ToList();
            return View(Customers);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Countries = context.Countries.OrderBy(g => g.Name).ToList();
            return View("Edit", new Customer());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Countries = context.Countries.OrderBy(g => g.Name).ToList();
            var t = context.Customers.Find(id);
            return View(t);
        }

        [HttpPost]//Message when customer Edited or Added, save changes, and return to customer page
        public RedirectToActionResult Edit(Customer customer)
         {
            if (customer.CustomerID == 0)
            {
               Message = $"Added Customer {customer.FullName}";
               context.Customers.Add(customer);
            }
            else
            {
               Message = $"Edited Customer {customer.FullName}";
               context.Customers.Update(customer);
            }

            context.SaveChanges();
            return RedirectToAction("Index", "Customers");
         }
       
        [HttpGet]
        public IActionResult Delete(int id)
        {
            ViewBag.Action = "Delete";
            var t = context.Customers.Find(id);
            return View(t);
        }

        [HttpPost]//Message when customer deleted, save changes, and return to customer page
        public IActionResult Delete(Customer customer)
        {
            Message = $"Deleted Customer {customer.FullName}";
            context.Customers.Remove(customer);
            context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }
    }
}