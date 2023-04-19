using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CallManager :ICallService
    {
        ICallDal _callDal;

        public CallManager(ICallDal callDal)
        {
            _callDal = callDal;
        }

        //Müşteri Temsilcisi Görüşme Form Ekranından ekleme
        public void AddCallDto(CallDto model, int customerId, string emailForClaim)
        {
            int statusId = 3;
            _callDal.AddRequestCall(model, customerId, emailForClaim, statusId);

        }
    }
}
