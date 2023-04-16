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
    public class RequestManager : IRequestService
    {
        IRequestDal _requestDal;

        public RequestManager(IRequestDal requestDal)
        {
            _requestDal = requestDal;
        }

        //public List<RequestListByCustomerDto> GetRequestByEmail(string email)
        //{
        //    var result = _requestDal.GetRequestByEmail(email);

        //    if (result == null)
        //    {
        //        return new ErrorDataResult<List<ProductListDto>>(Messages.EmailInvalid);
        //    }
        //    return new SuccessDataResult<List<ProductListDto>>(result, Messages.ProductListDetail);
        //}
    }
}
