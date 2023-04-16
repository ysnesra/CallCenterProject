using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Entities.DTOs
{
    public class CustomerLoginDto :IDto
    {
        public int CustomerId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }

        public string Role { get; set; }
    }
}
