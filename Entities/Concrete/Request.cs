using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Entities.Concrete
{
    public class Request :IEntity
    {
        public Request()
        {
            Calls = new HashSet<Call>();
        }

        public int RequestId { get; set; }
        public DateTime CreateDate { get; set; }
        public string Description { get; set; }
        public int RequestTypeId { get; set; }
        public int StatusId { get; set; }
        public int CustomerId { get; set; }
        public int? CustomerRepId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual CustomerRep CustomerRep { get; set; }
        public virtual RequestType RequestType { get; set; }
        public virtual Status Status { get; set; }

        public ICollection<Call> Calls { get; set; }




    }
}
