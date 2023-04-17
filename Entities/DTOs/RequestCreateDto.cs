using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class RequestCreateDto : RequestListByCustomerDto
    {
        public int RequestTypeId { get; set; }
        public int StatusId { get; set; }
    }
}
