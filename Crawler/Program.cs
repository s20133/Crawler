using System;
using System.Collections;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Crawler
{
    public class Program
    {

        public void Dispose(bool v)
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        public static async Task Main(string[] args)
        {
            Program obj = new Program();
            try
            {
                string websiteUrl = args[0];
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.GetAsync(websiteUrl);
                string content = await response.Content.ReadAsStringAsync();

                Regex regex = new Regex(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])");
                MatchCollection matchCollection = regex.Matches(content);

                Hashtable hash = new Hashtable();
                foreach (var match in matchCollection)
                {
                    String foundMatch = match.ToString();
                    if (hash.Contains(foundMatch) == false)
                        hash.Add(foundMatch, string.Empty);
                    if (foundMatch == "")
                    {
                        Console.WriteLine("Nie znaleziono adresów email");
                    }
                }
                foreach (DictionaryEntry element in hash)
                {
                    Console.WriteLine(element.Key);
                }
                response.Dispose();
                httpClient.Dispose();
            }
            catch(IndexOutOfRangeException)
            {
                Console.WriteLine("index out of bound");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine(" nie ma adresow email");
                throw;
            }
            catch(ArgumentException)
            {
                throw;
            }
            catch(HttpRequestException)
            {
                Console.WriteLine("Błąd w czasie pobierania strony");
            }
        }
    }
}
