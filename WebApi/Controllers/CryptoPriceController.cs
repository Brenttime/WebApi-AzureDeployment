using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebApi.Config;

namespace WebApi.Controllers
{
    public class CryptoPriceController : Controller
    {
        private readonly HttpClient webClient;

        // Configuration options for crypto 
        private readonly IOptions<CryptoPriceOptions> cryptoConfig;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cryptoConfig">configuration for crypto</param>
        /// <param name="webClient">client config</param>
        public CryptoPriceController(IOptions<CryptoPriceOptions> cryptoConfig, HttpClient webClient)
        {
            this.cryptoConfig = cryptoConfig;
            this.webClient = webClient;
        }

        /// <summary>
        /// Get the price of bitcoin
        /// </summary>
        /// <returns>the last price of bitcoin</returns>
        [HttpGet]
        [Route("btc")]
        public async Task<JToken> GetBitCoinPriceAsync()
        {
            var result = await this.webClient.GetAsync(this.cryptoConfig.Value.IndexReferenceUrl);
            Dictionary<string, JObject> pricesPerCurrency = 
                JsonConvert.DeserializeObject<Dictionary<string, JObject>>(await result.Content.ReadAsStringAsync());
            var price = pricesPerCurrency[this.cryptoConfig.Value.Currency][this.cryptoConfig.Value.TimeSelection];
            return price;
        }

        /// <summary>
        /// Get the price of bitcoin
        /// </summary>
        /// <returns>the last price of bitcoin</returns>
        [HttpGet]
        [Route("eth")]
        public async Task<dynamic> GetEtheruemPriceAsync()
        {
            var result = await this.webClient.GetAsync($"{this.cryptoConfig.Value.TickerRequestUrl}/eth-{this.cryptoConfig.Value.Currency}/buy");
            var pricesPerCurrency =
               JsonConvert.DeserializeObject<JObject>(await result.Content.ReadAsStringAsync())["data"]["amount"];
            return pricesPerCurrency;
            
        }
    }
}