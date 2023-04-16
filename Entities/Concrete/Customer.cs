using Core.Entities;

namespace Entities.Concrete
{
    public class Customer : IEntity
    {
        public Customer()
        {
            Calls = new HashSet<Call>();
            Requests = new HashSet<Request>();
        }
        public int CustomerId { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; } = "customer";

        public ICollection<Call> Calls { get; set; } 
        public ICollection<Request> Requests { get; set; }
    }
}
