using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class CustomerRepRequestCompletedListDto : IDto
    {
        public int RequestId { get; set; }
        public DateTime CreateDate { get; set; }
        public string Description { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int? CustomerRepId { get; set; }
        public string? CustomerRepName { get; set; }
        public int RequestTypeId { get; set; }
        public string RequestTypeName { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
    }
}
