using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class CallDto : IDto
    {
        public int CallId { get; set; }
        public DateTime CallDate { get; set; }
        public float CallTime { get; set; }
        
        public string? CallNote { get; set; }
        public int CustomerId { get; set; }     
        public int CustomerRepId { get; set; }      
        public int RequestId { get; set; }
        

    }
}
