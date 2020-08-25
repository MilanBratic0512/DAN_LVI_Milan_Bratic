using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
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
           

            string urlAddress = null;

            string data = null;
            do
            {
                try
                {
                    Console.WriteLine("If you want to stop typing, enter 'x'");
                    Console.WriteLine("Enter a web address in format https://www.xxx.xxx/  : ");
                    urlAddress = Console.ReadLine();
                    if (urlAddress == "x")
                    {
                        break;
                    }
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

                        data = readStream.ReadToEnd();
                        Console.WriteLine("Enter the name of the txt file in which you want to save the html code");
                        string htmlCode = Console.ReadLine();
                        using (StreamWriter file = new StreamWriter(@"..\\..\\HTML\\" + htmlCode + ".txt"))
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
            } while (urlAddress != "x");
           

            Console.WriteLine("Do you want to zip the html file? Enter a 'zip' if you want");
            string zip = Console.ReadLine();

            if (zip == "zip")
            {

                File.Delete(@"..\..\ZIPPED.zip");

                ZipFile.CreateFromDirectory(@"..\..\HTML\", @"..\..\ZIPPED.zip");
               
                Console.WriteLine("You have successfully zipped the file");
            }
            Console.WriteLine("Press enter to exit the program");
            Console.ReadLine();
        }

    }
}
