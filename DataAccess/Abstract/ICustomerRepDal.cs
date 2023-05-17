using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DataAccess.Abstract
{
    public interface ICustomerRepDal : IEntityRepository<CustomerRep>
    {
        List<ReportByDateDto> GetAllWithRequest(DateTime startDate,DateTime endDate);
    }
}
