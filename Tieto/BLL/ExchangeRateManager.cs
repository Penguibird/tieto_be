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
        public IEnumerable<ExchangeRate> FetchCurrencyResource(DateTime dateTime)
        {
            var db = ObjectContainer.GetExchangeRateDbProvider();

            if (db.ContainsToday(dateTime))
            {
                return db.GetToday(dateTime);
            }

            //Formulate date and add to url
            string days = (dateTime.Day < 10 ? "0" : "") + dateTime.Day.ToString();
            string months = (dateTime.Month < 10 ? "0" : "") + dateTime.Month.ToString();
            string years = dateTime.Year.ToString();

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

                double USA = 0;
                double EMU = 0;

                while (!readStream.EndOfStream)
                {
                    string s = readStream.ReadLine();
                    if (s.Contains("USA"))
                    {
                        string top = s.Split("|")[4];
                        string bottom = s.Split("|")[2];
                        top = top.Replace(",", ".");
                        bottom = bottom.Replace(",", ".");
                        USA = Convert.ToDouble(top) / Convert.ToDouble(bottom);
                    }
                    else if (s.Contains("EMU"))
                    {
                        string top = s.Split("|")[4];
                        string bottom = s.Split("|")[2];
                        top = top.Replace(",", ".");
                        bottom = bottom.Replace(",", ".");
                        EMU = Convert.ToDouble(top) / Convert.ToDouble(bottom);
                    }
                }

                response.Close();
                readStream.Close();

                List<ExchangeRate> l = new List<ExchangeRate>
                {
                    new ExchangeRate
                    {
                        CurrencyCode = CurrencyCode.EUR,
                        Rate = EMU
                    },
                    new ExchangeRate
                    {
                        CurrencyCode = CurrencyCode.USD,
                        Rate = USA
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
