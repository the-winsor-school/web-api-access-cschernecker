using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Net.Http;
using System.Xml;


namespace BlockChainModel
{
    public class BlockChain
    {

        public static void Main(string[] args)
        {
            Console.WriteLine("Which currency do you want to see?");
            Console.WriteLine("USD, AUD, EUR, GBP, INR, or HKD");
            string userValue = Console.ReadLine();

            if (userValue == "USD")
            {
                ExchangeRates currency = BlockChain.GetExchangeRates();
                Console.WriteLine(currency.USD.buy);
                Console.WriteLine(currency.USD.sell);
                Console.WriteLine(currency.USD.symbol);
            }

            else if (userValue == "AUD")
            {
                ExchangeRates currency = BlockChain.GetExchangeRates();
                Console.WriteLine(currency.AUD.buy);
                Console.WriteLine(currency.AUD.sell);
                Console.WriteLine(currency.AUD.symbol);
            }

            else if (userValue == "EUR")
            {
                ExchangeRates currency = BlockChain.GetExchangeRates();
                Console.WriteLine(currency.EUR.buy);
                Console.WriteLine(currency.EUR.sell);
                Console.WriteLine(currency.EUR.symbol);
            }

            else if (userValue == "GBP")
            {
                ExchangeRates currency = BlockChain.GetExchangeRates();
                Console.WriteLine(currency.GBP.buy);
                Console.WriteLine(currency.GBP.sell);
                Console.WriteLine(currency.GBP.symbol);
            }

            else if (userValue == "INR")
            {
                ExchangeRates currency = BlockChain.GetExchangeRates();
                Console.WriteLine(currency.INR.buy);
                Console.WriteLine(currency.INR.sell);
                Console.WriteLine(currency.INR.symbol);
            }

            else if (userValue == "HKD")
            {
                ExchangeRates currency = BlockChain.GetExchangeRates();
                Console.WriteLine(currency.HKD.buy);
                Console.WriteLine(currency.HKD.sell);
                Console.WriteLine(currency.HKD.symbol);
            }
        }

        public static ExchangeRates GetExchangeRates()
        //setting up to call the Http to get the data from the link
        {
            HttpClient client = new HttpClient();

            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Get,
                string.Format("https://blockchain.info/ticker"));

            HttpResponseMessage response = client.SendAsync(request).Result;

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ExchangeRates));

            if (!response.IsSuccessStatusCode)
            {
                return new ExchangeRates();
            }

            return (ExchangeRates)serializer.ReadObject(response.Content.ReadAsStreamAsync().Result);
        }

        //creating class ExchangeRates to retrieve data on each of the named currencies (excluded those which we weren't interested in from the original API)
        [DataContract]
        public class ExchangeRates
        {
            [DataMember]
            public ExchangeRate USD;
            [DataMember]
            public ExchangeRate AUD;
            [DataMember]
            public ExchangeRate EUR;
            [DataMember]
            public ExchangeRate GBP;
            [DataMember]
            public ExchangeRate INR;
            [DataMember]
            public ExchangeRate HKD;

        }

        //creating class ExchangeRate which is the data type within ExchangeRates to name the data that we want to record for each currency
        //the program can only write "$" for USD, AUD, HKD but will not write other symbols for other currencies, it just returns "?" and I do not know how to solve this
        [DataContract]
        public class ExchangeRate
        {
            [DataMember]
            public double buy;
            [DataMember]
            public double sell;
            [DataMember]
            public string symbol;
        }

    }
}
