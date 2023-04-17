using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class RequestTypeDto :IDto
    {
        public int RequestTypeId { get; set; }
        public string RequestTypeName { get; set; }

    }
}
