using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAdminService
    {
        //Admin Tarafta Bütün müşterileri listeleme 
        List<CustomerAllListDto> GetAllCustomerList();

        //Admin tarafta Müşterinin Taleplerini listeleme
        List<RequestListByCustomerDto> GetRequestsByCustomerId(int customerId);

        //Müşterinin Adını dönderen metot
        string GetByIdCustomerName(int customerId);
        Task<string> GetByIdCustomerNameAsync(int customerId);
    }
}

