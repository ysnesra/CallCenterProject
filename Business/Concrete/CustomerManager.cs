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

        //Müşterinin Bilgi formunu CustomerId ye göre dolu getirme
        public CustomerInformationDto GetCustomerInformation(int customerId)
        {
            //Böyle bir müşteri temsilcisi sistemde var mı 
            var existCustomer = _customerDal.Get(x => x.CustomerId == customerId);          
            if (existCustomer is null)
            {
                throw new Exception("Müşteri bulunmamaktadır");
            }
            CustomerInformationDto response = new CustomerInformationDto()
            {
                CustomerId = existCustomer.CustomerId,
                FirstName = existCustomer.FirstName,
                LastName = existCustomer.LastName,
                Email = existCustomer.Email,
                Phone = existCustomer.Phone  
            };
            return response;
        }

        //Müşterinin Bilgileri Güncelleme
        public void GetCustomerInformationDto(CustomerInformationDto model)
        {
            //Böyle bir müşteri sistemde var mı 
            Customer existCustomer = _customerDal.Get(x => x.CustomerId == model.CustomerId);
            
            if (existCustomer == null)
            {
                throw new Exception("Müşteri bulunmamaktadır");
            }

            existCustomer.CustomerId = model.CustomerId;
            existCustomer.FirstName = model.FirstName;
            existCustomer.LastName = model.LastName;
            existCustomer.Email = model.Email;
            existCustomer.Password = model.Password;
            existCustomer.Phone = model.Phone;
            existCustomer.Role = "customer";

            _customerDal.Update(existCustomer);
            _customerDal.SaveChanges();
        }

        //Müşterinin AdSoyad Bilgilerini Güncelleme
        public void GetProfileChangeFullName(string firstName,string lastName, int customerId)
        {
            Customer existCustomer=_customerDal.Get(x => x.CustomerId == customerId);
            if(existCustomer == null)
            {
                throw new Exception("Müşteri bulunmamaktadır");
            }
            existCustomer.FirstName = firstName;
            existCustomer.LastName = lastName;

            _customerDal.SaveChanges();
        }

        //Müşterinin Telefon Bilgilerini Güncelleme
        public void GetProfileChangePhone(string phone, int customerId)
        {
            Customer existCustomer = _customerDal.Get(x => x.CustomerId == customerId);
            if (existCustomer == null)
            {
                throw new Exception("Müşteri bulunmamaktadır");
            }
            existCustomer.Phone = phone;

            _customerDal.SaveChanges();
        }

        //Müşterinin Mail Bilgilerini Güncelleme
        public void GetProfileChangeEmail(string email, int customerId)
        {
            Customer existCustomer = _customerDal.Get(x => x.CustomerId == customerId);
            if (existCustomer == null)
            {
                throw new Exception("Müşteri bulunmamaktadır");
            }
            existCustomer.Email = email;

            _customerDal.SaveChanges();
        }

        //Müşterinin Parola Bilgilerini Güncelleme
        public void GetProfileChangePassword(string password, int customerId)
        {
            Customer existCustomer = _customerDal.Get(x => x.CustomerId == customerId);
            if (existCustomer == null)
            {
                throw new Exception("Müşteri bulunmamaktadır");
            }
            existCustomer.Password = password;

            _customerDal.SaveChanges();
        }
    }
}
