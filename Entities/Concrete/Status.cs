using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Entities.Concrete
{
    public class Status :IEntity
    {
        public Status()
        {
            Requests = new HashSet<Request>();
        }
        public int StatusId { get; set; }
        public string StatusName { get; set; }

        public ICollection<Request> Requests { get; set; }


    }
}
