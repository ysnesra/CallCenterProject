using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;
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
        public void AddCallDto(CallDto model, string emailForClaim)
        {            
            _callDal.AddRequestCall(model, emailForClaim);
            _callDal.UpdateRequestCall(model.RequestId);
            _callDal.SaveChanges(); 
        }
    }
}
