using System;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using ShortLinkManager.Data;
using System.Linq;
using ShortLinkManager.Models;

namespace ShortLinkManager.Logic
{
    public class LinkManager
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        public LinkManager()
        {
        }

        public ShortLinkResponse AddShortLink(ShortLinkRequest linkRequest)
        {
            ShortLinkResponse ro = new ShortLinkResponse();
            ro.ErrorMessage = string.Empty;

            string Url = linkRequest.Link;

            if (!string.IsNullOrWhiteSpace(Url))
            {
                Url = Url.Trim().ToLower();
            }

            String cached = null;
            cached = CacheManager.GetCached<String>(Url);
            if (!String.IsNullOrWhiteSpace(cached))
            {
                ro.ShortLink = cached;
                ro.OperationSucceded = true;

                return ro;
            }

            try
            {
                //var url = new Uri(Url);
                UriBuilder uriBuilder = new UriBuilder(Url);

            }
            catch (Exception)
            {
                ro.OperationSucceded = false;
                ro.ErrorMessage = "Invalid Link";
                return ro;
            }

            String newKey = null;
            while (string.IsNullOrEmpty(newKey))
            {
                if (!unitOfWork.ShortLinkRepository.Exists(l => l.Url == Url))
                {
                    newKey = Guid.NewGuid().ToString("N").Substring(0, ConfigManager.KeyLength).ToLower();
                    Data.Models.ShortLink link = new Data.Models.ShortLink();
                    link.Key = newKey;
                    link.Url = Url;
                    link.DateCreated = DateTime.Now;
                    try
                    {
                        unitOfWork.ShortLinkRepository.Insert(link);
                        unitOfWork.Save();
                    }
                    catch (Exception)
                    {
                        ro.OperationSucceded = false;
                        ro.ErrorMessage = "Error insert link in database";
                    }
                }
                else
                {
                    var lnks = unitOfWork.ShortLinkRepository.Get(l => l.Url == Url);
                    newKey = lnks.FirstOrDefault().Key;
                }
            }

            CacheManager.TryAddToCache<String>(Url, newKey);

            AddGuest(linkRequest.GuestGuid);
            AddVisit(newKey, linkRequest.GuestGuid);

            ro.ShortLink = newKey;
            ro.OperationSucceded = true;

            return ro;

            
        }

        public ShortLinkResponse GetUrl(string ShortLinkKey, string guestGuid)
        {
            ShortLinkResponse ro = new ShortLinkResponse();
            ro.ErrorMessage = string.Empty;

            var url = unitOfWork.ShortLinkRepository.FindByProperty(l => l.Key.ToUpper() == ShortLinkKey.ToUpper());

            if (url != null)
            {
                AddGuest(guestGuid);
                AddVisit(url.Key, guestGuid);

                ro.Link = url.Url;
                ro.OperationSucceded = true;
            }
            else
            {
                ro.OperationSucceded = false;
                ro.ErrorMessage ="ShortLink is Missing";
            }

            return ro;
        }

        public async Task<HttpStatusCode> HttpGetStatusCode(string Url)
        {
            try
            {
                var httpclient = new HttpClient();
                httpclient.Timeout = TimeSpan.FromSeconds(ConfigManager.CacheTimeout);
                var response = await httpclient.GetAsync(Url, HttpCompletionOption.ResponseHeadersRead);

                string text = null;

                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    var bytes = new byte[10];
                    var bytesread = stream.Read(bytes, 0, 10);
                    stream.Close();

                    text = Encoding.UTF8.GetString(bytes);

                    Console.WriteLine(text);
                }

                return response.StatusCode;
            }
            catch (Exception)
            {
                return HttpStatusCode.NotFound;
            }
        }
        public void AddGuest(string guid)
        {            
            if (!unitOfWork.GuestRepository.Exists(l => l.GuestGuid.ToUpper() == guid.ToUpper()))
            {
                Data.Models.Guest guest = new Data.Models.Guest();

                guest.GuestGuid = guid;
                guest.DateCreated = DateTime.Now;
                try
                {
                    unitOfWork.GuestRepository.Insert(guest);
                    unitOfWork.Save();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                
            }
        }
        private void AddVisit(string newKey, string guestTestGuid)
        {
            Data.Models.Visits visit = new Data.Models.Visits();

            visit.GuestGuid = guestTestGuid;
            visit.ShortLinkKey = newKey;
            visit.DateVisited = DateTime.Now;
            try
            {
                unitOfWork.VisitsRepository.Insert(visit);
                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
    }
}
