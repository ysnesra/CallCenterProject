using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Business.Concrete
{
    public class RequestManager : IRequestService
    {
        IRequestDal _requestDal;

        public RequestManager(IRequestDal requestDal)
        {
            _requestDal = requestDal;
        }

        public List<RequestListByCustomerDto> GetRequestByEmail(string email)
        {
            var result = _requestDal.GetRequestByEmail(email);

            if (result == null)
            {
                throw new Exception("Talep bulunmamaktadır");
            }
            return result;

        }

        //Talep türünü liste şeklinde getiren method
        public List<SelectListItem> GetBySelectListRequestTypes()
        {
            List<SelectListItem> selectList = new List<SelectListItem>();

            foreach (var item in _requestDal.GetRequestTypes().Select(a => new { a.RequestTypeId, a.RequestTypeName }))
            {
                selectList.Add(new SelectListItem() { Text = item.RequestTypeName, Value = item.RequestTypeId.ToString() });
            }
            return selectList;

        }
        // Talep ekleme
        public void AddRequestCreateDto(RequestCreateDto model, int customerId)
        {
            try
            {
                Request newRequest = new Request()
                {
                    CreateDate = DateTime.Now,
                    Description = model.Description,
                    RequestTypeId = model.RequestTypeId,
                    StatusId = 1,                         //default:yeniTalep
                    CustomerId = customerId,

                };
                _requestDal.Add(newRequest);
                _requestDal.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Talep Ekleme işlemi başarısız");
            }

        }
        //Müşteri Temsilcisi için Bütün Talepleri listeleme
        public List<RequestAllListDto> GetAllRequestList()
        {
            var result = _requestDal.GetAllRequestListDetail();
            return result;
        }
        //Müşteri Temsilcisinin Talebin Detayına gittiği Method
        public RequestAllListDto RequestDetailByCustomerRep(int requestId, string emailForClaim)
        {
            int statusId = 2;
            var requestDto = _requestDal.GetRequestDetail(requestId, emailForClaim, statusId);
            _requestDal.SaveChanges();
            return requestDto;
        }

        //Kapanan Talepler
        public List<RequestCompletedListDto> GetRequestCompletedList()
        {
            var result = _requestDal.GetRequestCompletedListDto();
            return result;
        }

        //Yeni açılan Talepler
        public List<RequestNewListDto> GetRequestNewList()
        {
            var result = _requestDal.GetRequestNewListDto();
            return result;
        }

        //Değerlendirmede olan Talepler
        public List<RequestContinueListDto> GetRequestContinueList()
        {
            var result = _requestDal.GetRequestContinueListDto();
            return result;
        }

        //Müşteri Temsilcisinin çözdüğü Taleplerin Listesi
        public List<CustomerRepRequestCompletedListDto> GetCustomerRepRequestCompletedList(string emailForClaim)
        {
            var result = _requestDal.GetCustomerRepRequestCompletedListDto(emailForClaim);
            return result;
        }
    }
}
