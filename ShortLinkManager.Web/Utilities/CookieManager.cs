using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShortLinkManager.Web.Utilities
{
    public static class CookieManager
    {
        public static string TryAddCookie(string cookieName)
        {
            if (HttpContext.Current.Request.Cookies[cookieName] == null)
            {
                HttpCookie aCookie = new HttpCookie(cookieName);
                aCookie.Value = Guid.NewGuid().ToString();
                aCookie.Expires = DateTime.Now.AddDays(100);
                HttpContext.Current.Response.Cookies.Add(aCookie);

                return aCookie.Value;
            }
            else
            {
                return HttpContext.Current.Request.Cookies[cookieName].Value.ToString();
            }
        }
    }
}