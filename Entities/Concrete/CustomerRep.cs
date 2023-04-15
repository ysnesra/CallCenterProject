using Core.Entities;

namespace Entities.Concrete
{
    public class CustomerRep : IEntity
    {
        public CustomerRep()
        {

            Calls = new HashSet<Call>();
            Requests = new HashSet<Request>();
        }
     
        public int CustomerRepId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
       

        public ICollection<Call> Calls { get; set; }
        public ICollection<Request> Requests { get; set; }

     }
}
