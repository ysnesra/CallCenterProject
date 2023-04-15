using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        public void AddCustomerDto(CustomerRegisterDto model)
        {
            //Bu mail adresi kullanılıyor mu
            var dbCustomer=_customerDal.Get(x=>x.Email==model.Email);
            if (dbCustomer == null)
            {
                Customer newCustomer = new Customer()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password
                };

                _customerDal.Add(newCustomer);
            }
            else
            {
                throw new Exception("Aynı mail adresinde kullanıcı bulunmaktadır");
            }
        }
    }
}
