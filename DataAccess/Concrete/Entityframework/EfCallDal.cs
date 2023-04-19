using Core.DataAccess.Entityframework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DataAccess.Concrete.Entityframework
{
    public class EfCallDal : EfEntityRepositoryBase<Call, CallCenterDbContext>, ICallDal
    {
        //Müşteri Temsilcisi Görüşme Form Ekranından Ekleme
        public void AddRequestCall(CallDto model, int customerId, string emailForClaim, int statusId)
        {
            using (CallCenterDbContext context = new CallCenterDbContext())
            {
                if (context.CustomerReps.Any(x => x.Email == emailForClaim))
                {
                    var customerRepId = context.CustomerReps.FirstOrDefault(x => x.Email == emailForClaim).CustomerRepId;
                    var dbStatusId = context.Statuses.FirstOrDefault(x => x.StatusId == statusId).StatusId;

                    var resultAdd = context.Calls.Add(new Call
                    {
                        CallRecord = model.CallRecord,
                        CallDate = model.CallDate,
                        CallTime = model.CallTime,
                        CallNote = model.CallNote,
                        CustomerId = customerId,
                        CustomerRepId = customerRepId,
                        RequestId = model.RequestId,

                    });
                    context.SaveChanges();
                }

            }
        }
    }
}
