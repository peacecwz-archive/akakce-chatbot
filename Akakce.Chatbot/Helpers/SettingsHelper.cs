using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Akakce.Chatbot
{
    public class SettingsHelper
    {
        private static string Get(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
        public static string APIEndpoint { get => Get(nameof(APIEndpoint)); }
    }
}
