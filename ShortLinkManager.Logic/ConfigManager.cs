using System.Configuration;

namespace ShortLinkManager.Logic
{
    public static class ConfigManager
    {
        public static int KeyLength
        {
            get
            {
                int keyLength;
                if (!int.TryParse(ConfigurationManager.AppSettings["KeyLength"], out keyLength))
                {
                    keyLength = 6;
                }
                return keyLength;
            }
        }

        public static int CacheTimeout
        {
            get
            {
                int cacheTimeout;
                if (!int.TryParse(ConfigurationManager.AppSettings["CacheTimeout"], out cacheTimeout))
                {
                    cacheTimeout = 5;
                }
                return cacheTimeout;
            }
        }
    }
}
