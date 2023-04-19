using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DataAccess.Abstract
{
    public interface ICallDal : IEntityRepository<Call>
    {
        //Müşteri Temsilcisi Görüşme Form Ekranından Ekleme
        void AddRequestCall(CallDto model, int customerId, string emailForClaim, int statusId);
    }
}
