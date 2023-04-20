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
                                .OrderByDescending(r => r.CreateDate)
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

        public List<RequestAllListDto> GetAllRequestListDetail()
        {
            using (CallCenterDbContext context = new CallCenterDbContext())
            {
                var result = context.Requests
                                .Include(r => r.Customer)
                                .Include(r => r.Status)
                                .Include(r => r.RequestType)
                                .Include(r => r.CustomerRep)
                                .Select(r => new RequestAllListDto
                                {
                                    RequestId = r.RequestId,
                                    CustomerId = r.CustomerId,
                                    CustomerRepId = r.CustomerRep == null ? default(int) : r.CustomerRep.CustomerRepId,
                                    CreateDate = r.CreateDate,
                                    Description = r.Description,
                                    CustomerName = $"{r.Customer.FirstName} {r.Customer.LastName}",
                                    CustomerRepName = $"{r.CustomerRep.FirstName} {r.CustomerRep.LastName}",
                                    RequestTypeName = r.RequestType.RequestTypeName,
                                    StatusName = r.Status.StatusName

                                }).ToList();

                return result;

            }
        }

        public RequestAllListDto GetRequestDetail(int requestId, string emailForClaim, int statusId)
        {
            using (CallCenterDbContext context = new CallCenterDbContext())
            {
                var dbStatus = context.Statuses.Single(x => x.StatusId == statusId);

                var result = context.Requests
                                .Include(r => r.Customer)
                                .Include(r => r.Status)
                                .Include(r => r.RequestType)
                                .Include(r => r.CustomerRep)
                                .Where(r => r.RequestId == requestId)
                                .Select(r => new RequestAllListDto
                                {
                                    RequestId = r.RequestId,
                                    CustomerId = r.CustomerId,
                                    CustomerRepId = r.CustomerRep == null ? default(int) : r.CustomerRep.CustomerRepId,
                                    CreateDate = r.CreateDate,
                                    Description = r.Description,
                                    CustomerName = $"{r.Customer.FirstName}{r.Customer.LastName}",
                                    CustomerRepName = emailForClaim,/*$"{r.CustomerRep.FirstName}{r.CustomerRep.LastName}",*/
                                    RequestTypeName = r.RequestType.RequestTypeName,
                                    StatusId = dbStatus.StatusId,
                                    StatusName = dbStatus.StatusName,
                                    RequestTypeId = r.RequestTypeId

                                }).FirstOrDefault();

                //Update işlemi
                if (context.CustomerReps.Any(x => x.Email == emailForClaim))
                {
                    var customerRepId = context.CustomerReps.Single(x => x.Email == emailForClaim).CustomerRepId;
                   
                    var resultEdit = context.Requests.Update(new Request
                    {
                        RequestId = result.RequestId,
                        CreateDate = result.CreateDate,
                        Description = result.Description,
                        RequestTypeId = result.RequestTypeId,
                        StatusId = dbStatus.StatusId,
                        CustomerId = result.CustomerId,
                        CustomerRepId = customerRepId
                    });
                    context.SaveChanges();
                }
                return result;

            }
        }

        public List<RequestCompletedListDto> GetRequestCompletedListDto()
        {
            using (CallCenterDbContext context = new CallCenterDbContext())
            {
                var result = context.Requests
                                .Include(r => r.Customer)
                                .Include(r => r.Status)
                                .Include(r => r.RequestType)
                                .Include(r => r.CustomerRep)
                                .Where(r=>r.StatusId==3)
                                .Select(r => new RequestCompletedListDto
                                {
                                    RequestId = r.RequestId,
                                    CustomerId = r.CustomerId,
                                    CustomerRepId = r.CustomerRep == null ? default(int) : r.CustomerRep.CustomerRepId,
                                    CreateDate = r.CreateDate,
                                    Description = r.Description,
                                    CustomerName = $"{r.Customer.FirstName} {r.Customer.LastName}",
                                    CustomerRepName = $"{r.CustomerRep.FirstName} {r.CustomerRep.LastName}",
                                    RequestTypeName = r.RequestType.RequestTypeName,
                                    StatusName = r.Status.StatusName

                                }).ToList();

                return result;

            }
        }
    }
}
