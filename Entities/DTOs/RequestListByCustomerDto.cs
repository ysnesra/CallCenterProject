using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class RequestListByCustomerDto
    {
        public int RequestId { get; set; }
        public DateTime CreateDate { get; set; }
        public string Description { get; set; }
        public int RequestTypeName { get; set; }
        public int StatusName { get; set; }
        public int CustomerId { get; set; }
    }
}
