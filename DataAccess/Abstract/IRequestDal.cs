using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
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

        List<RequestCompletedListDto> GetRequestCompletedListDto();

    }
}
