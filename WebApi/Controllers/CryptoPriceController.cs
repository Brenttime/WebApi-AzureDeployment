using System;
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
        public async Task<double> GetBitCoinPriceAsync()
        {
            var result = await this.webClient.GetAsync(this.cryptoConfig.Value.IndexReferenceUrl);
            Dictionary<string, JObject> pricesPerCurrency = 
                JsonConvert.DeserializeObject<Dictionary<string, JObject>>(await result.Content.ReadAsStringAsync());
            var price = (double)pricesPerCurrency[this.cryptoConfig.Value.Currency][this.cryptoConfig.Value.TimeSelection];
            return price;
        }

        /// <summary>
        /// Get the price of ethereum
        /// </summary>
        /// <returns>the last price of ethereum</returns>
        [HttpGet]
        [Route("eth")]
        public async Task<double> GetEtheruemPriceAsync()
        {
            var result = await this.webClient.GetAsync($"{this.cryptoConfig.Value.TickerRequestUrl}/eth-{this.cryptoConfig.Value.Currency}/buy");
            var pricesPerCurrency =
               (double)JsonConvert.DeserializeObject<JObject>(await result.Content.ReadAsStringAsync())["data"]["amount"];
            return pricesPerCurrency;
        }

        /// <summary>
        /// Get the price of a ticker
        /// </summary>
        /// <param name="ticker">name of a ticker to get a price</param>
        /// <returns>the last price of a ticker</returns>
        [HttpGet]
        [Route("ticker/{ticker}")]
        public async Task<double> GetTickerPriceAsync(string ticker)
        {
            var result = await this.webClient.GetAsync($"{this.cryptoConfig.Value.TickerRequestUrl}/{ticker}-{this.cryptoConfig.Value.Currency}/buy");
            try
            {
               return (double)JsonConvert.DeserializeObject<JObject>(await result.Content.ReadAsStringAsync())["data"]["amount"];
            }
            catch(Exception e)
            {
                // Add Exception Logging
                return -1;
            }
        }
    }
}