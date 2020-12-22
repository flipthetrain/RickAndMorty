using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace DownloadImages
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> fileData = new List<string>()
            {
                "characters",
                "locations",
                "episodes"
            };

            if (!Directory.Exists($@"..\..\..\..\Data\"))
            {
                Directory.CreateDirectory($@"..\..\..\..\Data\");
            }


            foreach (var item in fileData)
            {
                string charJson;
                using (StreamReader sr = new StreamReader(@$"..\..\..\..\Data\{item}.json"))
                {
                    charJson = sr.ReadToEnd();
                }


                if (!Directory.Exists($@"..\..\..\..\Data\"))
                {
                    Directory.CreateDirectory($@"..\..\..\..\Data\{item}_images");
                }

                var chars = JObject.Parse(charJson)[item];
                HttpClient client = new HttpClient();
                foreach (var item2 in chars)
                {
                    string url = item2["image"]?.ToString();

                    if (string.IsNullOrWhiteSpace(url))
                        continue;

                    var imageName = url.Split("/").Last();
                    Console.WriteLine(imageName);

                    if (File.Exists(@$"..\..\..\..\Data\{item}_images\{imageName}"))
                        continue;

                    var result = client.GetByteArrayAsync(url).Result;
                    using (BinaryWriter bw = new BinaryWriter(File.OpenWrite(@$"..\..\..\..\Data\{item}_images\{imageName}")))
                    {
                        bw.Write(result);
                    }
                }
            }




        }
    }
}
