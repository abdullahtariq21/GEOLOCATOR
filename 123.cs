using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace geolocate
{
    internal class Program
    {
        public class Data
        {
            public string city { get; set; }
            public string region { get; set; }
            public string country { get; set; }
            public string loc { get; set; }
            public string org { get; set; }
            public string postal { get; set; }
            public string timezone { get; set; }
        }

        static async Task Main(string[] args)
        {
            Console.Title = "Geolocator";
            Console.Write("Enter IP Address: ");
            string ip = Console.ReadLine();
            string url = $"http://ipinfo.io/{ip}/json";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    Console.WriteLine("Request succeeded");

                    string responseData = await response.Content.ReadAsStringAsync();
                    Data ipInfo = JsonConvert.DeserializeObject<Data>(responseData);

                    Console.Clear();
                    Console.WriteLine($"City: {ipInfo.city}");
                    Console.WriteLine($"Region: {ipInfo.region}");
                    Console.WriteLine($"Country: {ipInfo.country}");
                    Console.WriteLine($"Location: {ipInfo.loc}");
                    Console.WriteLine($"Organization: {ipInfo.org}");
                    Console.WriteLine($"Postal Code: {ipInfo.postal}");
                    Console.WriteLine($"Timezone: {ipInfo.timezone}");

                    string[] coords = ipInfo.loc.Split(',');
                    Console.WriteLine($"Google Maps: http://google.com/maps/?q={coords[0]},{coords[1]}");
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}