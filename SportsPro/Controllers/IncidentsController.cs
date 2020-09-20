using System.Linq;
using Microsoft.AspNetCore.Mvc;
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
        [TempData]
        public string Message { get; set; }


        [Route("Incidents")] //Add Route
        public IActionResult Index()
        {
            var Incidents = context.Incidents
                .Include(i => i.Customer)
                .Include(i => i.Product)
                .ToList();

            var viewModel = new IncidentFViewModel
            {
                Incidents = Incidents,
                Filter = ""
            };

            return View(viewModel);
        }


        [HttpGet]
        public IActionResult Add()
        {
            var viewModel = new IncidentAEViewModel
            {
                Incident = new Incident(),
                Customers = context.Customers.ToList(),
                Products = context.Products.ToList(),
                Technicians = context.Technicians.ToList(),
                Action = "Add"
            };

            return View("Edit", viewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var t = context.Incidents.Find(id);
            var viewModel = new IncidentAEViewModel
            {
                Incident = t,
                Customers = context.Customers.ToList(),
                Products = context.Products.ToList(),
                Technicians = context.Technicians.ToList(),
                Action = "Edit"
            };
            return View(viewModel);
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