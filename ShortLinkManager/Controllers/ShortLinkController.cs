using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using ShortLinkManager.Logic;
using ShortLinkManager.Models;
using System.Web.Http.Cors;

namespace ShortLinkManager.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ShortLinkController : ApiController
    {       

        [HttpGet]
        [Route("{key}/{guestguid}")]
        public ShortLinkResponse Get(string key, string guestguid)
        {
            ShortLinkResponse ro = new ShortLinkResponse();

            LinkManager urlManager = new LinkManager();
            ro = urlManager.GetUrl(key, guestguid);

            return ro;
        }

        [HttpPost]
        [Route("short")]
        public ShortLinkResponse Post([FromBody] ShortLinkRequest slrequest)
        {
            LinkManager urlManager = new LinkManager();
            ShortLinkResponse ro = new ShortLinkResponse();
            ro = urlManager.AddShortLink(slrequest);

            return ro;
        }


        [HttpPost]
        public ShortLinkResponse AggiungiLink([FromBody] ShortLinkRequest slrequest)
        {
            //ShortLinkRequest slrequest = JsonConvert.DeserializeObject<ShortLinkRequest>(data);
            LinkManager urlManager = new LinkManager();
            ShortLinkResponse ro = new ShortLinkResponse();
            ro = urlManager.AddShortLink(slrequest);

            //return ro;
            var response = new HttpResponseMessage(HttpStatusCode.Created)
            {
                Content = new StringContent(JsonConvert.SerializeObject(ro))
            };

            return ro;
        }
    }
}
