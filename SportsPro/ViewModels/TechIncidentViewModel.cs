using SportsPro.Models;
using System.Collections.Generic;

namespace SportsPro.ViewModels
{
    public class TechIncidentViewModel
    {
        public Technician Technician { get; set; }
        public List<Incident> Incidents { get; set; }
    }
}
