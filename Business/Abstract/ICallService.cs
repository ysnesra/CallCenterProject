using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICallService
    {  
        void AddCallDto(CallDto model, string emailForClaim);
        string CallwithCustomer(int customerId);

    }
}
