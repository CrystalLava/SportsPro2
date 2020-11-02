using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SportsPro.Models;

//Page not completed//
namespace SportsPro.Controllers
{
    //[Authorize(Roles = "Admin")]//only admin can view and use this page
    public class RegistrationController : Controller
    {
        private SportsProContext context { get; set; }

        public RegistrationController(SportsProContext ctx)
        {
            context = ctx;
        }

        [HttpGet]
        public ViewResult GetCustomer(string error=null)
        {
            var model = new RegisterViewModel
            {
                //ActiveIncident = activeIncident,
                //ActiveTechnician = activeTechnician,
                //Incidents = context.Incidents.OrderBy(i => i.Title).ToList(),
                //Technicians = context.Technicians.OrderBy(c => c.Name).ToList(),
                Customers = context.Customers.OrderBy(c => c.FirstName).ToList(),
                // Products = context.Products.OrderBy(p => p.Name).ToList(),

            };
            IQueryable<Customer> query = context.Customers;

            if(error == "not_select")
            {
                model.WarningText = "Please select a customer!";
            }

            model.Customers = query.ToList();
           
            return View(model);
        }

        //[HttpGet]
        //public ViewResult Add()
        //{
        //    ViewBag.Action = "Add";
        //    ViewBag.Countries = context.Countries.OrderBy(g => g.Name).ToList();
        //    return View("Edit", new Customer());
        //}

        [HttpGet]
        public ViewResult Registrations(string activeIncident = "All", string activeTechnician = "All")
        {

            var custId = HttpContext.Session.GetInt32("customerId");
            var customerProducts = new List<Product>();

            if (custId.HasValue && custId > 0)
            {
                  customerProducts = (from r in context.Registrations
                                        join p in context.Products on r.ProductID equals p.ProductID
                                        where r.CustomerID == custId
                                        select p).OrderBy(p => p.Name).ToList();
            }

            var model = new RegisterViewModel
            {
                Customers = context.Customers.OrderBy(c => c.FirstName).ToList(),
                Products = context.Products.OrderBy(p => p.Name).ToList(),
                CustomerProducts = customerProducts,
                WarningText = TempData["message"].ToString()
            };
            //IQueryable<Incident> query = context.Incidents;
            //if (activeIncident != "All")
            //    query = query.Where(i => i.IncidentID.ToString() == activeIncident);
            //if (activeTechnician != "All")
            //    query = query.Where(i => i.Technician.TechnicianID.ToString() == activeTechnician);
            //model.Incidents = query.ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Registrations(RegisterViewModel inc)

        {
            var custId = inc.CurrentCustomer.CustomerID;

            if(custId == 0)
            {
                return RedirectToAction("GetCustomer", new { @error = "not_select" });
            }
            HttpContext.Session.SetInt32("customerId", custId);

            var customer = context.Customers.Single(c => c.CustomerID == custId);
            var model = new RegisterViewModel
            {
                //Products = context.Products.Where(g => g.ProductID == custId).OrderBy(a => a.Registrations).ToList(),
                Customers = context.Customers.OrderBy(c => c.FirstName).ToList(),
                Products = context.Products.OrderBy(p => p.Name).ToList(),
                CustomerProducts = (from r in context.Registrations
                           join p in context.Products on r.ProductID equals p.ProductID
                           where r.CustomerID == custId
                           select p).OrderBy(p=> p.Name).ToList(),
                ActiveCustomer = customer.FullName

            };
            return View(model);
        }

        [HttpPost]
        public ActionResult RegisterProduct(RegisterViewModel inc)

        {
            var custId = HttpContext.Session.GetInt32("customerId");

            if (!custId.HasValue || custId == 0)
            {
                return RedirectToAction("GetCustomer", new { @error = "not_select" });
            }
            var customer = context.Customers.Single(c => c.CustomerID == custId);

            context.Registrations.Add(new Registration
            {
                CustomerID = custId.Value,
                ProductID = inc.CurrentProduct.ProductID
            });

            context.SaveChanges();
     
            return RedirectToAction("Registrations");
        }

        [HttpGet]
        public ViewResult Delete(int id)
        {
            var product = context.Products.Find(id);
 
            return View(product);
        }

        [HttpPost]
        public ActionResult DeleteProduct(int productId)
        {
            var product = context.Products.Find(productId);
            context.Products.Remove(product);
            context.SaveChanges();
            TempData["message"] = $"{product.Name} deleted from database";
            return RedirectToAction("Registrations", "Registration");
        }
    }
}
