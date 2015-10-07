using System;
using System.Collections.Generic;
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
                    if (response.IsSuccessStatusCode)
                    {
                        AAItem item = await response.Content.ReadAsAsync<AAItem>();
                        string result;
                        switch (item.algorithm)
                        {
                            case "IronMan":
                                Algorithms.SortArray(item, false);
                                Algorithms.ShiftVowels(item);
                                result = Algorithms.AsciiConcat(item);
                                result = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(result));
                                break;
                            case "TheIncredibleHulk":
                                Algorithms.ShiftVowels(item);
                                Algorithms.SortArray(item, true);
                                result = Algorithms.AsterixConcat(item);
                                result = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(result));
                                break;
                            case "Thor":
                                //1
                                Algorithms.crazy(item);
                                Algorithms.SortArray(item, false);
                                Algorithms.AltCase(item);
                                Algorithms.Fib(item);
                                result = Algorithms.AsciiConcat(item);
                                result = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(result));
                                break;
                            case "CaptainAmerica":
                                Algorithms.ShiftVowels(item);
                                Algorithms.SortArray(item, false);
                                Algorithms.Fib(item);
                                result = Algorithms.AsciiConcat(item);
                                result = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(result));
                                break;
                            default:
                                Console.WriteLine("Unknown Algorithm: " + item.algorithm);
                                break;
                        }//end switch
                    }//end if response success
                    //POST [result] into values/{guid}/{algorithmName}

                }//end for 1-20
            }//end using httpclient
        }//end runasync
    }
}
