using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SportsPro.Models;
using Microsoft.EntityFrameworkCore;
using SportsPro.ViewModels;

namespace SportsPro.Controllers
{
    public class IncidentsController : Controller
    {
        private SportsProContext context { get; set; }

        public IncidentsController(SportsProContext context)
        {
            this.context = context;
        }
      
        [Route("Incidents")] //Add Route
        [HttpGet]
        public ViewResult Index(string activeIncident = "All", string activeTechnician = "All")
       
        {
            string FilterString = HttpContext.Session.GetString("FilterString");
            var viewModel = new IncidentViewModel
            {
                ActiveIncident = activeIncident,
                ActiveTechnician = activeTechnician,
                Incidents = context.Incidents.OrderBy(i => i.Title).ToList(),
                Technicians = context.Technicians.OrderBy(c => c.Name).ToList(),
                Customers = context.Customers.OrderBy(c => c.FirstName).ToList(),
                Products = context.Products.OrderBy(p => p.Name).ToList(),

            };
            IQueryable<Incident> query = context.Incidents;
            if (FilterString != "null")
            {
                if (FilterString != "unassigned")
                    query = query.Where(i => i.TechnicianID == null);
                if (FilterString != "open")
                    query = query.Where(i => i.DateClosed == null);
            }
            viewModel.Incidents = query.ToList();
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var viewModel = new IncidentViewModel
            {
                Action = "Add",
                Customers = context.Customers.ToList(),
                Products = context.Products.ToList(),
                Technicians = context.Technicians.ToList()
            };

            return View("Edit", viewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var t = context.Incidents.Find(id);
            var viewModel = new IncidentViewModel
            {
                CurrentIncident = t,
                Customers = context.Customers.OrderBy(c => c.FirstName).ToList(),
                Products = context.Products.OrderBy(p => p.Name).ToList(),
                Technicians = context.Technicians.OrderBy(c => c.Name).ToList(),
                Action = "Edit"
            };
            return View("Edit", viewModel);
        }

        [HttpPost]
        public IActionResult Edit(IncidentViewModel viewModel)
        {
            string Action = "Edit";
            if (viewModel.CurrentIncident.IncidentID == 0)
                Action = "Add";

            if (ModelState.IsValid)
            {
                if (viewModel.CurrentIncident.IncidentID == 0)
                    context.Incidents.Add(viewModel.CurrentIncident);
                else
                    context.Incidents.Update(viewModel.CurrentIncident);

                context.SaveChanges();
                TempData["message"] = $"{viewModel.CurrentIncident.Title} {Action}ed.";
                return RedirectToAction("Index", "Incidents");
            }
            else
            {
                viewModel.Action = Action;
                viewModel.Customers = context.Customers.OrderBy(c => c.FirstName).ToList();
                viewModel.Technicians = context.Technicians.OrderBy(c => c.Name).ToList();
                viewModel.Products = context.Products.OrderBy(p => p.Name).ToList();

                return View(viewModel);
            }
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
            TempData["message"] = $"Deleted Incident {t.Title}";
            ViewBag.Action = "";
            context.Incidents.Remove(t);
            context.SaveChanges();
            return RedirectToAction("Index", "Incidents");
        }

        public IActionResult FilterAll()
        {
            HttpContext.Session.SetString("FilterString", "null");
            return RedirectToAction("Index");
        }

        public IActionResult FilterUnassigned()
        {
            HttpContext.Session.SetString("FilterString", "unassigned");
            return RedirectToAction("Index");
        }

        public IActionResult FilterOpen()
        {
            HttpContext.Session.SetString("FilterString", "open");
            return RedirectToAction("Index");
        }
    }
}