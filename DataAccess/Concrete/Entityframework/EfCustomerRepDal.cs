﻿using Core.DataAccess.Entityframework;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DataAccess.Concrete.Entityframework
{
    public class EfCustomerRepDal : EfEntityRepositoryBase<CustomerRep, CallCenterDbContext>, ICustomerRepDal
    {
    }
}