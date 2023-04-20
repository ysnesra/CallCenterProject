using Core.DataAccess.Entityframework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;


namespace DataAccess.Concrete.Entityframework
{
    public class EfCallDal : EfEntityRepositoryBase<Call, CallCenterDbContext>, ICallDal
    {
        //Müşteri Temsilcisi Görüşme Form Ekranından Ekleme
        public void AddRequestCall(CallDto formdata,string emailForClaim)
        {
            using (CallCenterDbContext context = new CallCenterDbContext())
            {
                var customerRepId = context.CustomerReps.First(x => x.Email == emailForClaim).CustomerRepId;
                
                Call call = new Call
                {
                    CallDate = formdata.CallDate,
                    CallTime = formdata.CallTime,
                    CallNote = formdata.CallNote,
                    RequestId = formdata.RequestId,
                    CustomerId = formdata.CustomerId,
                    CustomerRepId = customerRepId,
                };
                context.Calls.Add(call);
                context.SaveChanges();


                var requestUpdated = context.Requests.Single(x => x.RequestId == call.RequestId);
                requestUpdated.StatusId = 3;
                context.SaveChanges();
            }
        }
    }
}
