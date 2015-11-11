using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using BadBrokerTest.Models;
using Newtonsoft.Json;

namespace BadBrokerTest.Logic
{
    public static class FixerGrabber
    {
        public static ExternalRate GetRates(DateTime date)
        {
            var client = new WebClient { Proxy = { Credentials = CredentialCache.DefaultCredentials } };
            var jsonString = client.DownloadString(
                string.Format("http://api.fixer.io/{0:yyyy-MM-dd}?base=USD&symbols=RUB,EUR,GBP,JPY", date));
            return JsonConvert.DeserializeObject<ExternalRate>(jsonString);
        }
    }
}