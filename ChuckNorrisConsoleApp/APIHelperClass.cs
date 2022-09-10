using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuckNorrisConsoleApp
{
    internal class APIHelperClass
    {
        public static string[] GetRandomJokes(string jokeUrl)
        {
            StringBuilder joke = new StringBuilder("");
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(jokeUrl);
                StringBuilder url = new StringBuilder("jokes/random");
                joke.Append(Task.FromResult(client.GetStringAsync(url.ToString()).Result).Result);
            }
            catch (Exception ex) {
            
            }
            return new string[] { JsonConvert.DeserializeObject<dynamic>(joke.ToString()).value };
        }
    }
}
