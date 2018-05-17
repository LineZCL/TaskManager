using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;


namespace TaskManager.Helper
{
    public class CookieHelper
    {

        public static string GetInfoUserConnected(ClaimsIdentity identity, string info)
        {
            IEnumerable<Claim> claims = identity.Claims;

            foreach(var claim in claims)
            {
                if (claim.Type.ToLower().Contains(info))
                {
                    return claim.Value;
                }
            }
            return "";
        }
    }
}