using Newtonsoft.Json;
using ShortLinkManager.Models;
using ShortLinkManager.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ShortLinkManager.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //Create Guid for Visitor and try to add as a cookie
            var guestGuid = CookieManager.TryAddCookie("VisitorNumber");
            ViewBag.guestGuid = guestGuid;
            ViewBag.urlWebApiPost = String.Format("{0}Short", ConfigurationManager.AppSettings["WebAPISite"].ToString());
            ViewBag.urlWebApi = ConfigurationManager.AppSettings["WebAPISite"].ToString();

            return View();
        }

        public async Task<ActionResult> Redirection(string shortUrl)
        {
            ShortLinkResponse slresponse = new ShortLinkResponse();
            slresponse.OperationSucceded = false;

            if (!string.IsNullOrEmpty(shortUrl))
            {
                var guestguid = CookieManager.TryAddCookie("VisitorNumber");
                shortUrl = Server.HtmlEncode(shortUrl);

                WebAPIManager wamgr = new WebAPIManager();
                string webapiaddress = ConfigurationManager.AppSettings["WebAPISite"];
                slresponse = await wamgr.GetAsync(String.Format("{0}{1}/{2}", webapiaddress, shortUrl, guestguid));
            }

            if (slresponse.OperationSucceded)
                return Redirect(slresponse.Link);
            else
                return View("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}