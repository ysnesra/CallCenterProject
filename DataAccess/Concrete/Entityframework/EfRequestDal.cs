using Core.DataAccess.Entityframework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DataAccess.Concrete.Entityframework
{
    public class EfRequestDal : EfEntityRepositoryBase<Request, CallCenterDbContext>, IRequestDal
    {

        public List<RequestListByCustomerDto> GetRequestByEmail(string email)
        {
            using (CallCenterDbContext context = new CallCenterDbContext())
            { 
                var result = context.Requests
                                .Include(r => r.Customer)
                                .Include(r => r.Status)
                                .Include(r => r.RequestType)
                                .Where(r => r.Customer.Email == email)
                                .Select(r => new RequestListByCustomerDto
                                {
                                    RequestId = r.RequestId,
                                    CustomerId = r.CustomerId,
                                    CreateDate = r.CreateDate,
                                    Description = r.Description,
                                    RequestTypeName = r.RequestType.RequestTypeName,
                                    StatusName = r.Status.StatusName

                                }).ToList();

                return result;

            }
        }

        public List<RequestTypeDto> GetRequestTypes()
        {
            using (CallCenterDbContext context = new CallCenterDbContext())
            {
                var result = context.RequestTypes.Select(r => new RequestTypeDto
                {
                    RequestTypeId = r.RequestTypeId,                                   
                    RequestTypeName = r.RequestTypeName
                    
                }).ToList();

                return result;
            }
        }
    }
}
