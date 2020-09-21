using System.Linq;
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
        public IActionResult Edit(IncidentAEViewModel viewModel)
        {
            if (viewModel.Incident.IncidentID == 0)
                context.Incidents.Add(viewModel.Incident);
            else
                context.Incidents.Update(viewModel.Incident);

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

        public IActionResult FilterUnassigned()
        {

            return RedirectToAction("Index");
        }
        public IActionResult FilterOpen()
        {
            return RedirectToAction("Index");
        }
    }
}