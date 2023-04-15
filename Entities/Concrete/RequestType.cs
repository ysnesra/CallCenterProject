using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Entities.Concrete
{
    public class RequestType : IEntity
    {
        public RequestType()
        {
            Requests = new HashSet<Request>();
        }
        public int RequestTypeId { get; set; }
        public string RequestTypeName { get; set; }

        public ICollection<Request> Requests { get; set; }
    }
}
