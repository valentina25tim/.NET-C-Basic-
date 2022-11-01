using ABP_Entity.HalperLibrary;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace ABP_Dal.Parsing
{
    public class Parser
    {
        public List<string> DivNames = new List<string>();
        public List<List<string>> DivCode_n = new List<List<string>>();
        public List<List<DateTime>> DivDateFrom_n = new List<List<DateTime>>();
        public List<List<DateTime>> DivDateTo_n = new List<List<DateTime>>();
        public List<List<string>> DivComplectation_n = new List<List<string>>();

        private List<List<string>> divUrlToComplectation = new List<List<string>>();


        private List<string> divConsist = new List<string>();
        private List<HtmlAgilityPack.HtmlNode> divConsistHtml = new List<HtmlAgilityPack.HtmlNode>();
        private List<List<string>> divDate_n = new List<List<string>>();

        public void Start()
        {
            CarModelParsing("https://www.ilcats.ru/toyota/?function=getModels&market=EU");
        }

        public void PrintAtConsole()
        {
            Start();

            for (var i = 0; i < DivCode_n.Count; i++)
            {
                if (DivNames.ElementAt(i) != null)
                {
                    Console.Write($"{i + 1}   -    ");
                    Console.WriteLine($"{DivNames.ElementAt(i)}");

                }
                for (var j = 0; j < DivCode_n.ElementAt(i).Count; j++)
                {
                    Console.Write($"{DivCode_n.ElementAt(i).ElementAt(j)}   -    ");
                    Console.WriteLine($"{DivComplectation_n.ElementAt(i).ElementAtOrDefault(j)}");
                    Console.WriteLine($"{DivDateFrom_n.ElementAt(i).ElementAtOrDefault(j)}  -   {DivDateTo_n.ElementAt(i).ElementAtOrDefault(j)}");
                    //Console.WriteLine($"{DivDate_n.ElementAt(i).ElementAtOrDefault(j)}");
                    //Console.WriteLine($"{divUrlToComplectation.ElementAt(i).ElementAt(j)}");
                }
                Console.WriteLine("____________________________");
            }
        }
        private void CarModelParsing(string url)
        {
            try
            {
                using (HttpClientHandler hdl = new HttpClientHandler
                {
                    AllowAutoRedirect = false,
                    AutomaticDecompression = System.Net.DecompressionMethods.Deflate |
                    System.Net.DecompressionMethods.GZip |
                    System.Net.DecompressionMethods.None
                })

                using (var client = new HttpClient(hdl))
                {
                    using (HttpResponseMessage response = client.GetAsync(url).Result)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var html = response.Content.ReadAsStringAsync().Result;
                            if (!string.IsNullOrEmpty(html))
                            {
                                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                                doc.LoadHtml(html);

                                divConsistHtml = doc.DocumentNode.SelectNodes("html/body/div/div/div[@class='List']").ToList();// содержание Дива листа 

                                var names = doc.DocumentNode.SelectNodes(".//div[@class = 'List']//div[@class = 'Header']//div[@class = 'name']"); //чистое имя

                                for (var i = 0; divConsistHtml.Count > i; i++)
                                {
                                    if (divConsistHtml.ElementAt(i) != null)
                                    {
                                        divConsist.Add(divConsistHtml.ElementAt(i).InnerText); // содержание Дива листа  в виде текста одной модели 
                                        DivNames.Add(names.ElementAt(i).InnerText); // содержание Дива листа  в виде текста одной модели 
                                    }
                                }
                            }
                        }
                    }
                }
                for (var i = 0; i < divConsist.Count; i++)
                {
                    List<string> _divCode = new List<string>();
                    _divCode = Halper_Parsing.ListParsedContent(divConsistHtml, divConsist, i, "//div[@class ='id']/a");
                    DivCode_n.Add(_divCode);

                    List<string> _divComplectation = new List<string>();
                    _divComplectation = Halper_Parsing.ListParsedContent(divConsistHtml, divConsist, i, "//div[@class ='modelCode']");
                    DivComplectation_n.Add(_divComplectation);

                    List<string> _divDates = new List<string>();
                    _divDates = Halper_Parsing.ListParsedContent(divConsistHtml, divConsist, i, "//div[@class ='dateRange']");
                    divDate_n.Add(_divDates);
                }

                for (var i = 0; i < DivComplectation_n.Count; i++ )
                {
                    List<string> _divUrlToComplectation = new List<string>();

                    for (var j = 0; j < DivComplectation_n.ElementAt(i).Count; j++)
                    {
                       
                        _divUrlToComplectation = Halper_Parsing.ListParsedContent(divConsistHtml, divConsist, i, "//div[@class ='id']/a[@href]");
                    }
                    divUrlToComplectation.Add(_divUrlToComplectation);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            DivDateFrom_n = Halper_FromStr_ToDateTime.ListOnePartDateTime(2, DivCode_n.Count, divDate_n);
            DivDateTo_n = Halper_FromStr_ToDateTime.ListOnePartDateTime(1, DivCode_n.Count, divDate_n);
        }
    }
    }

