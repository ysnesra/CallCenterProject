using Business.Abstract;
using DataAccess.Abstract;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AdminManager:IAdminService
    {
        ICustomerDal _customerDal;
        IRequestDal _requestDal;

        public AdminManager(ICustomerDal customerDal, IRequestDal requestDal)
        {
            _customerDal = customerDal;
            _requestDal = requestDal;
        }

        //Admin Tarafta Bütün müşterileri listeleme 
        public List<CustomerAllListDto> GetAllCustomerList()
        {
            var customerListDb = _customerDal.GetAll(x => x.Role == "customer");

            List<CustomerAllListDto> response = customerListDb.Select(x => new CustomerAllListDto()
            {
                CustomerId = x.CustomerId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Password = x.Password,
                Phone = x.Phone,
                Role = x.Role
            }).ToList();

            return response;
        }

        //Admin tarafta Müşterinin Taleplerini listeleme
        public List<RequestListByCustomerDto> GetRequestsByCustomerId(int customerId)
        {
            var requestListdb = _requestDal.GetRequestsByCustomerId(customerId);
            if (requestListdb == null)
            {
                throw new Exception("Bu müşterinin Talebi bulunmamaktadır.");
            }           
                       
            return requestListdb;                      
        }

        public string GetByIdCustomerName(int customerId)
        {
            var customer=_customerDal.Get(x => x.CustomerId == customerId);
          
            return $"{customer.FirstName} {customer.LastName}";
        }

        public async Task<string> GetByIdCustomerNameAsync(int customerId)
        {
            var customer = await _customerDal.GetAsync(x => x.CustomerId == customerId);

            return $"{customer.FirstName} {customer.LastName}";
        }
    }
}
