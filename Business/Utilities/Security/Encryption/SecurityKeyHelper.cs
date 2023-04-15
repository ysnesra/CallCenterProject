using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Utilities.Security.Encryption
{
    public class SecurityKeyHelper
    {
        // parametre olarak verilen securityKey -> appsetting de oluşturduğımuz securityKey değeri
        //bunu byte[] array formatına çeviriyoruz    
        public static SecurityKey CreateSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}
