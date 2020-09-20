using SportsPro.Models;
using System.Collections.Generic;

namespace SportsPro.ViewModels
{
    public class IncidentAEViewModel
    {
        public Incident Incident { get; set; }
        public List<Product> Products { get; set; }
        public List<Technician> Technicians { get; set; }
        public List<Customer> Customers { get; set; }
        public string Action { get; set; }
    }
}