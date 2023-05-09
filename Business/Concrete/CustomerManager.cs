using Business.Abstract;
using Business.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.Entityframework;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        private ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        public void AddCustomerDto(CustomerRegisterDto model)
        {
            //Bu mail adresi kullanılıyor mu
            var dbCustomer = _customerDal.Get(x => x.Email == model.Email);
            if (dbCustomer == null)
            {
                Customer newCustomer = new Customer()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Phone = model.Phone,
                    Password = model.Password
                };

                _customerDal.Add(newCustomer);
                _customerDal.SaveChanges();
            }
            else
            {
                throw new Exception("Aynı mail adresinde kullanıcı bulunmaktadır");
            }
        }

        public CustomerLoginDto GetByLoginFilter(CustomerLoginDto model)
        {
            var customerDb = _customerDal.Get(x => x.Email == model.Email && x.Password == model.Password);

            if (customerDb is null)
            {
                throw new Exception("Bu Mail ve Parola ya ait kullanıcı bulunamadı");
            }

            CustomerLoginDto response = new()
            {
                CustomerId = customerDb.CustomerId,
                Email = customerDb.Email,
                Password = customerDb.Password,
                Role = customerDb.Role,

            };

            return (response);
        }
      
    }
}
