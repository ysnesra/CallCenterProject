using Business.Utilities.Security.JWT;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        void AddCustomerDto(CustomerRegisterDto model);
        CustomerLoginDto GetByLoginFilter(CustomerLoginDto model);

        string CreateAccessToken(Customer customer);
    }
}
