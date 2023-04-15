
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Utilities.Security.JWT
{
    public interface ITokenHelper
    {
        //hangi User parametre olarak verilirse onun için Token üretecek 
        //veritabanında user ve ona ait claimleri bulup JWT Token üretecek
        AccessToken CreateToken(Customer customer);
    }
}
