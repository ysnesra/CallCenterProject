using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Utilities.Security.JWT
{
    public class AccessToken
    {
        private AccessToken accessToken;
        private string v;

        public AccessToken()
        {

        }

        public AccessToken(AccessToken accessToken, string v)
        {
            this.accessToken = accessToken;
            this.v = v;
        }

        public string Token { get; set; } //json web token değerimiz
        public DateTime Expiration { get; set; }  //token bitiş zamanı
    }
}
