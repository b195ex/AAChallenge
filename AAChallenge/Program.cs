using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AAChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            RunAsync().Wait();
            Console.ReadKey();
        }

        static async Task RunAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://internal-devchallenge-2-dev.apphb.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string guid;
                for (int i = 0; i < 20; i++)
                {
                    guid = Guid.NewGuid().ToString();
                    HttpResponseMessage response = await client.GetAsync("values/" + guid);
                    string result="";
                    if (response.IsSuccessStatusCode)
                    {
                        AAItem item = await response.Content.ReadAsAsync<AAItem>();
                        switch (item.algorithm)
                        {
                            case "IronMan":
                                Algorithms.SortArray(item, false);//check
                                Algorithms.ShiftVowels(item);//check
                                result = Algorithms.AsciiConcat(item);//check
                                result = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(result));
                                break;
                            case "TheIncredibleHulk":
                                Algorithms.ShiftVowels(item);//check
                                Algorithms.SortArray(item, true);//check
                                result = Algorithms.AsterixConcat(item);//check
                                result = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(result));
                                break;
                            case "Thor":
                                //1
                                Algorithms.crazy(item);
                                Algorithms.SortArray(item, false);//check
                                Algorithms.AltCase(item);
                                Algorithms.Fib(item);
                                result = Algorithms.AsterixConcat(item);//check
                                result = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(result));
                                break;
                            case "CaptainAmerica":
                                Algorithms.ShiftVowels(item);//check
                                Algorithms.SortArray(item, true);//check
                                Algorithms.Fib(item);
                                result = Algorithms.AsciiConcat(item);//check
                                result = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(result));
                                break;
                        }

                        Calculation postval = new Calculation();
                        postval.encodedValue = result;
                        response = await client.PostAsJsonAsync(string.Format("values/{0}/{1}", guid, item.algorithm), postval);
                        Console.WriteLine(i);
                        Console.WriteLine(item.algorithm);
                        AAResponse asdf = await response.Content.ReadAsAsync<AAResponse>();
                        Console.WriteLine(asdf.status);
                        Console.WriteLine(asdf.message);
                    }
                }
            }
        }
    }
}
