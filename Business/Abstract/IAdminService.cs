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

        //Admin Tarafta Bütün Müşterileri Temsilcilerini listeleme 
        List<CustomerRepAllListDto> GetAllCustomerRepList();

        //Müşteri Temsilcisi Ekleme
        void AddCustomerRepDto(CustomerRepRegisterDto model);

        //Müşteri Temsilcisi Güncelleme formunu CustomerId ye göre dolu getirme
        CustomerRepEditDto GetCustomerRepByCustomerId(int customerId);

        //Müşteri Temsilcisi Güncelleme              
        void EditCustomerRepDto(CustomerRepEditDto model);

        //Müşteri Temsilcisi Silme
        void DeleteCustomerRepDto(int customerId);

        //Müşteri Temsilcisi Raporları
        List<ReportDto> GetReportList();


    }
}

