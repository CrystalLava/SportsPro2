using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;
using SportsPro.ViewModels;

namespace SportsPro.Controllers
{
    public class TechIncidentController : Controller
    {
        private SportsProContext context { get; set; }

        public TechIncidentController(SportsProContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ViewResult Get()
        {
            var technicians = context.Technicians.ToList();
            return View(technicians);
        }

        [HttpGet]
        public ViewResult List(int id)
        {
            var technician = context.Technicians.Find(id);
            var viewModel = new TechIncidentViewModel
            {
                Technician = technician,
                Incidents = context
                    .Incidents
                    .Include(i => i.Customer)
                    .Include(i => i.Product)
                    .Where(i => i.TechnicianID == technician.TechnicianID &&
                                i.DateClosed == null)
                    .ToList()
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var i = context.Incidents
                .Include(i => i.Customer)
                .Include(i => i.Product)
                .Include(i => i.Technician)
                .SingleOrDefault(i => i.IncidentID == id);
            return View(i);
        }


        [HttpPost]
        public IActionResult Edit(Incident incident)
        {
            var i = context.Incidents.Find(incident.IncidentID);
            i.Description = incident.Description;
            i.DateClosed = incident.DateClosed;
            context.Incidents.Update(i);
            context.SaveChanges();

            return RedirectToAction("List", new { id = incident.TechnicianID });
        }

    }
}