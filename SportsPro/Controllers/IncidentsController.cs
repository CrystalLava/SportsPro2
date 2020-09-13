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
        public IActionResult Edit(Incident t)
        {
            if (t.IncidentID == 0)
                context.Incidents.Add(t);
            else
                context.Incidents.Update(t);

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
        public IActionResult Delete(Incident t)
        {
            ViewBag.Action = "";
            context.Incidents.Remove(t);
            context.SaveChanges();
            return RedirectToAction("Index", "Incidents");
        }

    }
}