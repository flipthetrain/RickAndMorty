using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace DownloadJSONData
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> fileData = new Dictionary<string, string>()
            {
                {"characters","https://rickandmortyapi.com/api/character" },
                {"locations","https://rickandmortyapi.com/api/location" },
                {"episodes","https://rickandmortyapi.com/api/episode" },
            };

            if (!Directory.Exists($@"..\..\..\..\Data\"))
            {
                Directory.CreateDirectory($@"..\..\..\..\Data\");
            }

            foreach (var item in fileData)
            {
                var nextUrl = item.Value;
                using (StreamWriter sw = new StreamWriter($@"..\..\..\..\Data\{item.Key}.json"))
                {
                    sw.WriteLine("{");
                    sw.WriteLine($"{item.Key} : [");
                    HttpClient client = new HttpClient();
                    while (!string.IsNullOrWhiteSpace(nextUrl))
                    {
                        Console.WriteLine(nextUrl);
                        string response = client.GetStringAsync(nextUrl).Result;
                        var parsedObject = JObject.Parse(response);
                        nextUrl = parsedObject["info"]["next"].ToString();
                        var resultsJson = parsedObject["results"];
                        foreach (var item2 in resultsJson)
                        {
                            sw.WriteLine(item2.ToString() + ",");
                        }
                    }
                    sw.WriteLine("]");
                    sw.WriteLine("}");
                    client.Dispose();
                }
            }
        }
    }
}
