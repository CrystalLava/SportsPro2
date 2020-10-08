using SportsPro.Models;

namespace SportsPro.Models.DataLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SportsProContext _sportsProContext;

        public UnitOfWork(SportsProContext sportsProContext, IGRepository<Product> productRepository, 
            IGRepository<Incident> incidentRepository, IGRepository<Technician> technicianRepository, 
            IGRepository<Customer> customerRepository, IGRepository<Registration> registrationRepository)
        {
            _sportsProContext = sportsProContext;
            ProductRepository = productRepository;
            IncidentRepository = incidentRepository;
            TechnicianRepository = technicianRepository;
            CustomerRepository = customerRepository;
            RegistrationRepository = registrationRepository;
        }

        public IGRepository<Product> ProductRepository { get; }
        public IGRepository<Incident> IncidentRepository { get; }
        public IGRepository<Technician> TechnicianRepository { get; }
        public IGRepository<Customer> CustomerRepository { get; }

        public IGRepository<Registration> RegistrationRepository { get; }

        public void Save()
        {
            _sportsProContext.SaveChanges();
        }
    }
}