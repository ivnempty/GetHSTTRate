using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HSTTRate
{
    public class RateItem
    {
        public string CcyCode { get; set; }
        public string CcyDisplayCode { get; set; }
        public string CcyDisplayName { get; set; }
        public string CcyBaseRemark { get; set; }
        public string ChartFlag { get; set; }
        public string TtBuyRate { get; set; }
        public string TtSellRate { get; set; }
        public string CcyNameZh { get; set; }
        public string CcyNameCn { get; set; }
        public string CcyNameEn { get; set; }
    }

    public class RateResponse
    {
        public string LastUpdateTime { get; set; }
        public List<RateItem> FxttExchangeRates { get; set; }
    }

    class Program
    {
        static HttpClient client = new HttpClient();

        static void ShowSellRate(RateItem tRate)
        {
            Console.WriteLine($"Code : {tRate.CcyCode}\tBuy Rate: {tRate.TtBuyRate}\t Sell Rate: {tRate.TtSellRate}");
        }

        static async Task GetSellRateAsync()
        {
            long ts = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;
            string path = $"https://rbwm-api.hsbc.com.hk/pws-hk-hase-rates-papi-prod-proxy/v1/fxtt-exchange-rates?date={ts}";

            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var responseMsg = JsonConvert.DeserializeObject<RateResponse>(content);
                var jpySellRate = responseMsg.FxttExchangeRates.First(t => t.CcyCode == "JPY");
                ShowSellRate(jpySellRate);
            }
        }

        static void Main(string[] args)
        {
            /*
            using (StreamReader sr = new StreamReader("D:\\Download\\fxtt-exchange-rates.json"))
            {
                string json = sr.ReadToEnd();
                var response = JsonConvert.DeserializeObject<RateResponse>(json);
                var jpySellRate = response.FxttExchangeRates.First(t => t.ccyCode == "JPY");         
                ShowSellRate(jpySellRate);
            }
            */
            GetSellRateAsync().Wait();
            Console.ReadLine();
        }
    }
}
