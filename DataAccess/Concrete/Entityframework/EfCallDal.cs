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
        private readonly CallCenterDbContext _context;

        public EfCallDal(CallCenterDbContext context) : base(context)
        {
            _context  = context;
        }

        //Müşteri Temsilcisi Görüşme Form Ekranından Ekleme
        public void AddRequestCall(CallDto callModel,string emailForClaim)
        {          
            var customerRepId = _context.CustomerReps.First(x => x.Email == emailForClaim).CustomerRepId;

            Call call = new Call
            {
                CallDate = callModel.CallDate,
                CallTime = callModel.CallTime,
                CallNote = callModel.CallNote,
                RequestId = callModel.RequestId,
                CustomerId = callModel.CustomerId,
                CustomerRepId = customerRepId,
            };
            _context.Calls.Add(call);
                       
        }

        public void UpdateRequestCall(int requestId)
        {
            var requestUpdated = _context.Requests.Single(x => x.RequestId == requestId);
            requestUpdated.StatusId = 3;          
        }

        public string CallwithCustomer(int customerId)
        {
           string email= _context.Customers.Include(x=>x.Calls).Single(x=>x.CustomerId == customerId).Email;
           return email;
        }
    }
}
