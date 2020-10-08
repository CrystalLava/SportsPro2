using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsPro.Models.DataLayer
{
    public interface IUnitOfWork
    {
        public interface IUnitOfWork
        {
            IGRepository<Product> ProductRepository { get; }
            IGRepository<Incident> IncidentRepository { get; }
            IGRepository<Technician> TechnicianRepository { get; }
            IGRepository<Customer> CustomerRepository { get; }

            IGRepository<Registration> RegistrationRepository { get; }
            void Save();
        }
    }
}
