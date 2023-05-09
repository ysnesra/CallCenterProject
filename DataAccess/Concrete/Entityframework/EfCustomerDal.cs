using Core.DataAccess.Entityframework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DataAccess.Concrete.Entityframework
{
    public class EfCustomerDal : EfEntityRepositoryBase<Customer, CallCenterDbContext>, ICustomerDal
    {
        private readonly CallCenterDbContext _context;
        public EfCustomerDal(CallCenterDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
