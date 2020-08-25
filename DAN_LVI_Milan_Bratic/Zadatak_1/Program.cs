using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a web address in format https://www.xxx.xxx/  : ");

            string urlAddress = Console.ReadLine();

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;

                    if (String.IsNullOrWhiteSpace(response.CharacterSet))
                        readStream = new StreamReader(receiveStream);
                    else
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));

                    string data = readStream.ReadToEnd();
                    using (StreamWriter file = new StreamWriter(@"..\\..\\html.txt"))
                    {
                        file.WriteLine(data);
                    }
                    response.Close();
                    readStream.Close();
                }

            }
            catch (UriFormatException)
            {
                Console.WriteLine("Wrong web address");
            }
            catch (Exception)
            {
                Console.WriteLine("Error");
            }

            Console.ReadLine();
        }
    }
}
