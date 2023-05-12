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
        private readonly CallCenterDbContext _context;

        public EfRequestDal(CallCenterDbContext context) : base(context)
        {
            _context = context;
        }

        public List<RequestListByCustomerDto> GetRequestByEmail(string email)
        {
            var result = _context.Requests
                            .Include(r => r.Customer)
                            .Include(r => r.Status)
                            .Include(r => r.RequestType)
                            .Include(r => r.CustomerRep)
                            .Where(r => r.Customer.Email == email)
                            .OrderByDescending(r => r.CreateDate)
                            .Select(r => new RequestListByCustomerDto
                            {
                                RequestId = r.RequestId,
                                CustomerId = r.CustomerId,
                                CustomerRepId = r.CustomerRep == null ? default(int) : r.CustomerRep.CustomerRepId,
                                CreateDate = r.CreateDate,
                                Description = r.Description,
                                RequestTypeName = r.RequestType.RequestTypeName,
                                StatusName = r.Status.StatusName,
                                CustomerName = $"{r.Customer.FirstName} {r.Customer.LastName}",
                                CustomerRepName = $"{r.CustomerRep.FirstName} {r.CustomerRep.LastName}"

                            }).ToList();

            return result;

        }

        public List<RequestTypeDto> GetRequestTypes()
        {
            var result = _context.RequestTypes.Select(r => new RequestTypeDto
            {
                RequestTypeId = r.RequestTypeId,
                RequestTypeName = r.RequestTypeName

            }).ToList();

            return result;
        }


        public List<RequestAllListDto> GetAllRequestListDetail()
        {
            var result = _context.Requests
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

        public RequestAllListDto GetRequestDetail(int requestId, string emailForClaim, int statusId)
        {
            var dbStatus = _context.Statuses.Single(x => x.StatusId == statusId);

            var result = _context.Requests
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
            if (_context.CustomerReps.Any(x => x.Email == emailForClaim))
            {
                var customerRepId = _context.CustomerReps.Single(x => x.Email == emailForClaim).CustomerRepId;

                var resultEdit = _context.Requests.Update(new Request
                {
                    RequestId = result.RequestId,
                    CreateDate = result.CreateDate,
                    Description = result.Description,
                    RequestTypeId = result.RequestTypeId,
                    StatusId = dbStatus.StatusId,
                    CustomerId = result.CustomerId,
                    CustomerRepId = customerRepId
                });
                //Generic tarafta kaydetme işlemini ayırdım.SaveChanges metotunu ayrı çağırırız.
                // _context.SaveChanges(); 
            }
            return result;
        }

        public List<RequestCompletedListDto> GetRequestCompletedListDto()
        {
            var result = _context.Requests
                            .Include(r => r.Customer)
                            .Include(r => r.Status)
                            .Include(r => r.RequestType)
                            .Include(r => r.CustomerRep)
                            .Where(r => r.StatusId == 3)
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

        //Sadece Yeni talepleri Listeleme
        public List<RequestNewListDto> GetRequestNewListDto()
        {
            var result = _context.Requests
                            .Include(r => r.Customer)
                            .Include(r => r.Status)
                            .Include(r => r.RequestType)
                            .Include(r => r.CustomerRep)
                            .Where(r => r.StatusId == 1)
                            .Select(r => new RequestNewListDto
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

        //Sadece Değerlendirmede olan talepleri Listeleme
        public List<RequestContinueListDto> GetRequestContinueListDto()
        {
            var result = _context.Requests
                            .Include(r => r.Customer)
                            .Include(r => r.Status)
                            .Include(r => r.RequestType)
                            .Include(r => r.CustomerRep)
                            .Where(r => r.StatusId == 2)
                            .Select(r => new RequestContinueListDto
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

        //Sadece Kapanan talepleri Listeleme
        public List<CustomerRepRequestCompletedListDto> GetCustomerRepRequestCompletedListDto(string emailForClaim)
        {
            var customerRepId = _context.CustomerReps.First(x => x.Email == emailForClaim).CustomerRepId;
            var result = _context.Requests
                            .Include(r => r.Customer)
                            .Include(r => r.Status)
                            .Include(r => r.RequestType)
                            .Include(r => r.CustomerRep)
                            .Where(r => r.CustomerRepId == customerRepId && r.StatusId==3)
                            .Select(r => new CustomerRepRequestCompletedListDto
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

        //Admin tarafta Müşterinin Taleplerini listeleme
        public List<RequestListByCustomerDto> GetRequestsByCustomerId(int customerId)
        {         
            var result = _context.Requests
                            .Include(r => r.Customer)
                            .Include(r => r.Status)
                            .Include(r => r.RequestType)
                            .Include(r => r.CustomerRep)
                            .Where(r => r.Customer.CustomerId==customerId)
                            .OrderByDescending(r => r.CreateDate)
                            .Select(r => new RequestListByCustomerDto
                            {
                                RequestId = r.RequestId,
                                CustomerId = r.CustomerId,
                                CustomerRepId = r.CustomerRep == null ? default(int) : r.CustomerRep.CustomerRepId,
                                CreateDate = r.CreateDate,
                                Description = r.Description,
                                RequestTypeName = r.RequestType.RequestTypeName,
                                StatusName = r.Status.StatusName,
                                CustomerName = $"{r.Customer.FirstName} {r.Customer.LastName}",
                                CustomerRepName = $"{r.CustomerRep.FirstName} {r.CustomerRep.LastName}"

                            }).ToList();

            return result;
        }

    }
}
