using Entities.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IRequestService
    {
        List<RequestListByCustomerDto> GetRequestByEmail(string email);
        List<SelectListItem> GetBySelectListRequestTypes();

        //Talep Ekleme
        void AddRequestCreateDto(RequestCreateDto model, int customerId);

        List<RequestAllListDto> GetAllRequestList();

        //Müşteri Temsilcisi için Bütün Talepleri listeleme
        RequestAllListDto RequestDetailByCustomerRep(RequestAllListDto model);
    }
}
