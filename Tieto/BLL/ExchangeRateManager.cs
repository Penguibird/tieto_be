using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tieto.Models;

namespace Tieto.BLL
{
    public class ExchangeRateManager : IExchangeRateManager
    {
        public IEnumerable<ExchangeRate> FetchCurrencyResource(long dateTime)
        {
            var db = ObjectContainer.GetExchangeRateDbProvider();

            if (db.ContainsToday(dateTime))
            {
                return db.GetToday(dateTime);
            }

            //Formulate date and add to url
            var date = new DateTime(dateTime * 10000 + new DateTime(1970, 1, 1).Ticks);
            string days = (date.Day < 10 ? "0" : "") + date.Day.ToString();
            string months = (date.Month < 10 ? "0" : "") + date.Month.ToString();
            string years = date.Year.ToString();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.cnb.cz/cs/financni_trhy/devizovy_trh/kurzy_devizoveho_trhu/denni_kurz.txt?date=" + days + "." + months + "." + years);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }

                double USD = 0;
                double EUR = 0;
                double GBP = 0;
                double CHF = 0;

                string validDate = "";

                while (!readStream.EndOfStream)
                {
                    string s = readStream.ReadLine();

                    if (validDate == "")
                    {
                        validDate = s.Split(" ")[0];
                    }

                    if (s.Contains("USD"))
                    {
                        string top = s.Split("|")[4];
                        string bottom = s.Split("|")[2];
                        top = top.Replace(",", ".");
                        bottom = bottom.Replace(",", ".");
                        USD = Convert.ToDouble(top) / Convert.ToDouble(bottom);
                    }
                    else if (s.Contains("EUR"))
                    {
                        string top = s.Split("|")[4];
                        string bottom = s.Split("|")[2];
                        top = top.Replace(",", ".");
                        bottom = bottom.Replace(",", ".");
                        EUR = Convert.ToDouble(top) / Convert.ToDouble(bottom);
                    }
                    else if (s.Contains("GBP"))
                    {
                        string top = s.Split("|")[4];
                        string bottom = s.Split("|")[2];
                        top = top.Replace(",", ".");
                        bottom = bottom.Replace(",", ".");
                        GBP = Convert.ToDouble(top) / Convert.ToDouble(bottom);
                    }
                    else if (s.Contains("CHF"))
                    {
                        string top = s.Split("|")[4];
                        string bottom = s.Split("|")[2];
                        top = top.Replace(",", ".");
                        bottom = bottom.Replace(",", ".");
                        CHF = Convert.ToDouble(top) / Convert.ToDouble(bottom);
                    }
                }

                response.Close();
                readStream.Close();

                List<ExchangeRate> l = new List<ExchangeRate>
                {
                    new ExchangeRate
                    {
                        CurrencyCode = CurrencyCode.EUR,
                        Rate = EUR
                    },
                    new ExchangeRate
                    {
                        CurrencyCode = CurrencyCode.USD,
                        Rate = USD
                    },
                    new ExchangeRate
                    {
                        CurrencyCode = CurrencyCode.GBP,
                        Rate = GBP
                    },
                    new ExchangeRate
                    {
                        CurrencyCode = CurrencyCode.CHF,
                        Rate = CHF
                    }
                };

                db.Create(new DayExchange
                {
                    Date = dateTime,
                    Rates = l
                });

                return l.AsEnumerable();

            }

            return null;

        }
    }
}
