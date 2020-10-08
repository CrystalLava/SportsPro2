using SportsPro.Models;

namespace SportsPro.Models.DataLayer
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
