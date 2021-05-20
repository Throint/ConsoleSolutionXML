using System;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;

using System.IO;
using System.Linq;

using System.Collections.Generic;


namespace TestTask
{
    class Program
    {
        static void Main(string[] args)
        {
            string prevPath = @"C:\Users\user\Downloads\test.xml";
          

            while (true)
            { 
                //menu
            Console.WriteLine("\t\t\tChoose option");
            Console.WriteLine("1. Enter path of xml file");
            Console.WriteLine($"2. Use default or previous path of xml file ({prevPath})");
            Console.WriteLine("3. Exit");
             string _rl=   Console.ReadLine();
                switch(_rl)
                {
                    case "1":

                        prevPath = Console.ReadLine();
                        if(File.Exists(prevPath))
                        {
                            if (Path.GetExtension(prevPath) == ".xml")
                            {
                                XDocument _xDocument = XDocument.Load(@prevPath);

                                var _tmp = _xDocument.Element("CATALOG").Elements("CD");
                                CD_Analys cDAnalys = new CD_Analys();
                                foreach (var rs in _tmp)
                                {
                                    //increment every cd
                                    cDAnalys.CdsCount++;
                                    //get counntries
                                    cDAnalys.Countries.Add(rs.Element("COUNTRY").Value);
                                    //get price
                                    var t = rs.Element("PRICE").Value;
                                    var q = t.Replace('.', ',');
                                    decimal val ;
                                    if(decimal.TryParse(q, out val))
                                    {
                                        cDAnalys.PricesSum += val;
                                    }


                                    int curYear;
                                    if (int.TryParse(rs.Element("YEAR").Value, out curYear))
                                        {

                                    }

                                    //get years and compare with saved
                                    if ((curYear< cDAnalys.MinYear) || (cDAnalys.MinYear == 0))
                                    {
                                        cDAnalys.MinYear = int.Parse(rs.Element("YEAR").Value);
                                    }

                                    if ((curYear > cDAnalys.MaxYear) || (cDAnalys.MaxYear == 0))
                                    {
                                        cDAnalys.MaxYear = int.Parse(rs.Element("YEAR").Value);
                                    }



                                }
                                //replace repeated countries
                                cDAnalys.Countries = cDAnalys.Countries.Distinct().ToList();
                                string _result = JsonConvert.SerializeObject(cDAnalys, Newtonsoft.Json.Formatting.Indented);

                                Console.WriteLine("\n");
                                Console.WriteLine(_result);
                                Console.WriteLine("\n");
                            }
                        }
                        else
                        {
                            
                            Console.WriteLine("Incorrect path");
                        }

                        break;
                    case "2":


                        XDocument xDocument = XDocument.Load(@prevPath);

                        var tmp = xDocument.Element("CATALOG").Elements("CD");
                        CD_Analys cD_Analys = new CD_Analys();
                        foreach (var rs in tmp)
                        {

                            cD_Analys.CdsCount++;
                            cD_Analys.Countries.Add(rs.Element("COUNTRY").Value);
                            var t = rs.Element("PRICE").Value;
                            var q = t.Replace('.', ',');

                            decimal Sum;
                            if (decimal.TryParse(q, out Sum))
                            {
                                cD_Analys.PricesSum += decimal.Parse(q);
                            }

                            int _Year;
                            if (int.TryParse(rs.Element("YEAR").Value, out _Year))
                            {

                                if ((_Year < cD_Analys.MinYear) || (cD_Analys.MinYear == 0))
                                {
                                    cD_Analys.MinYear = int.Parse(rs.Element("YEAR").Value);
                                }

                                if ((_Year > cD_Analys.MaxYear) || (cD_Analys.MaxYear == 0))
                                {
                                    cD_Analys.MaxYear = int.Parse(rs.Element("YEAR").Value);
                                }
                            }


                        }
                        //replace repeated countries
                        cD_Analys.Countries = cD_Analys.Countries.Distinct().ToList();
                        string result = JsonConvert.SerializeObject(cD_Analys, Newtonsoft.Json.Formatting.Indented);

                        Console.WriteLine("\n");
                        Console.WriteLine(result);
                        Console.WriteLine("\n");



                        break;
                    case "3":
                        Environment.Exit(1);
                        break;

                }

          

        }
           
        }
    }

    public class CD_Analys
    {
        public int CdsCount { get; set; } = 0;
        public decimal PricesSum { get; set; } = 0;
        public List<string> Countries { get; set; } = new List<string>();
        public int MinYear
        {
            get; set;
        } = 0;
        public int MaxYear { get; set; } = 0;
    }


}
