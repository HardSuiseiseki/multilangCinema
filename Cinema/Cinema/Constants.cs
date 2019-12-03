using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Cinema
{
    public static class Constants
    {
        public static string ReportsDirectory => ConfigurationManager.AppSettings["ReportsDirectory"];
        public static string ExcelTemplatesDirectory => ConfigurationManager.AppSettings["ExcelTemplatesDirectory"];
        public static TimeSpan DefaultCacheTime => GetMinutesTimeSpanSetting("DefaultCacheTime", 30);
        public static NameValueCollection UrlsToWarmUp => ConfigurationManager.GetSection("CacheWarmingUpUrls") as NameValueCollection;
        public static string Domain => ConfigurationManager.AppSettings["Domain"];

        private static TimeSpan GetMinutesTimeSpanSetting(string settingName, int defaultValue)
        {
            var settingValue = ConfigurationManager.AppSettings[settingName];
            int parsedValue;
            if (int.TryParse(settingValue, out parsedValue))
            {
                return TimeSpan.FromMinutes(parsedValue);
            }
            return TimeSpan.FromMinutes(defaultValue);
        }
    }
}