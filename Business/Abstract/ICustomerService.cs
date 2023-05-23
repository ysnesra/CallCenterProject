using Business.Utilities.Security.JWT;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        void AddCustomerDto(CustomerRegisterDto model);
        CustomerLoginDto GetByLoginFilter(CustomerLoginDto model);

        //Müşterinin Bilgi formunu CustomerId ye göre dolu getirme
        CustomerInformationDto GetCustomerInformation(int customerId);

        //Müşteri Bilgileri Güncelleme         
        void GetCustomerInformationDto(CustomerInformationDto model);

        //Müşterinin AdSoyad Bilgilerini Güncelleme
        public void GetProfileChangeFullName(string firstName, string lastName, int customerId);

        //Müşterinin Telefon Bilgilerini Güncelleme
        public void GetProfileChangePhone(string phone,int customerId);

        //Müşterinin Mail Bilgilerini Güncelleme
        public void GetProfileChangeEmail(string email, int customerId);

        //Müşterinin Parola Bilgilerini Güncelleme
        public void GetProfileChangePassword(string password, int customerId);
    }
       
}
