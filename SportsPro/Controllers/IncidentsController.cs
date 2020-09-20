using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;
using Microsoft.EntityFrameworkCore;

namespace SportsPro.Controllers
{
    public class IncidentsController : Controller
    {
        private SportsProContext context { get; set; }

        public IncidentsController(SportsProContext context)
        {
            this.context = context;
        }
        [TempData]
        public string Message { get; set; }


        [Route("Incidents")] //Add Route
        public IActionResult Index()
        {
            var Incidents = context.Incidents
                .Include(i => i.Customer)
                .Include(i => i.Product)
                .ToList();

            return View(Incidents);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Customers = context.Customers.ToList();
            ViewBag.Technicians = context.Technicians.ToList();
            ViewBag.Products = context.Products.ToList();
            return View("Edit", new Incident());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Customers = context.Customers.ToList();
            ViewBag.Technicians = context.Technicians.ToList();
            ViewBag.Products = context.Products.ToList();
            var t = context.Incidents.Find(id);
            return View(t);
        }
        [HttpPost]
                public RedirectToActionResult Edit(Incident incident)
                {
  
                    if (incident.IncidentID == 0)
                    {

                        Message = $"Added Incident";
                        context.Incidents.Add(incident);
                    }
                    else
                    {
                        Message = $"Edited Incident";
                        context.Incidents.Update(incident);
                    }

                    context.SaveChanges();
                    return RedirectToAction("Index", "Incidents");
                }
       

        [HttpGet]
        public IActionResult Delete(int id)
        {
            ViewBag.Action = "Delete";
            var t = context.Incidents
                .Include(i => i.Customer)
                .Include(i => i.Product)
                .Include(i => i.Technician)
                .SingleOrDefault(i => i.IncidentID == id);

            return View(t);
        }


        [HttpPost]
        public IActionResult Delete(Incident incident)
        {
            Message = $"Deleted Incident";
            context.Incidents.Remove(incident);
            context.SaveChanges();
            TempData["Success"] = "Success!";
            return RedirectToAction("Index", "Incidents");
        }

    }
}