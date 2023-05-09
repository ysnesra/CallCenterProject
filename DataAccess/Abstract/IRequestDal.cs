using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DataAccess.Abstract
{
    public interface IRequestDal : IEntityRepository<Request>
    {
        //Müşterinin mail adresine göre Taleplerini görüntüleme
        List<RequestListByCustomerDto> GetRequestByEmail(string email);

        //TalepTürlerini listeleme
        List<RequestTypeDto> GetRequestTypes();

        List<RequestAllListDto> GetAllRequestListDetail();

        //Bir Talebin detaylarını ayrıntılı getirme
        RequestAllListDto GetRequestDetail(int requestId, string emailForClaim,int statusId);

        //Kapanan Talepleri Listeleme
        List<RequestCompletedListDto> GetRequestCompletedListDto();

        //Yeni açılan Talepleri Listeleme
        List<RequestNewListDto> GetRequestNewListDto();

        //Sadece Değerlendirmede olan talepleri Listeleme
        List<RequestContinueListDto> GetRequestContinueListDto();

        //Müşteri Temsilcisi kendi çözdüğü Talepleri listeleme
        List<CustomerRepRequestCompletedListDto> GetCustomerRepRequestCompletedListDto(string emailForClaim);

        //Admin tarafta Müşterinin Taleplerini listeleme
        List<RequestListByCustomerDto> GetRequestsByCustomerId(int customerId);
    }
}
