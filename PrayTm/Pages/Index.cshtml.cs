using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PrayTm.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string LoggedUser { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool Login { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Count { get; set; }
        [BindProperty(SupportsGet = true)]
        public string TotalUsers { get; set; }

        public void OnGet()
        {
            var flag = HttpContext.Session.GetString("HideLogin");
            Login = Convert.ToBoolean(flag);
            LoggedUser = HttpContext.Session.GetString("Name");
            Count = getTotalCount();
            TotalUsers = getTotalUsers();
        }

        private string getTotalCount()
        {
            HttpWebRequest request = WebRequest.Create(" https://104.236.243.12/chantingapprest-0.0.1-SNAPSHOT/rest/getTotalBeadsForToday") as HttpWebRequest;
            request.Method = "POST";
            ServicePointManager.ServerCertificateValidationCallback =
                 delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

            // Get response  
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            
            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            int count = Convert.ToInt32(reader.ReadToEnd());
            int countRound = count / 108;
            return countRound.ToString();
        }

        private string getTotalUsers()
        {
            HttpWebRequest request = WebRequest.Create(" https://104.236.243.12/chantingapprest-0.0.1-SNAPSHOT/rest/user/refresh_home") as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/json";
            string json = "{ \"email\": \"" + "test@mayapur.com" + "\", \"password\" : \"" + "mayapur" + "\"}";
            byte[] byteArray = Encoding.UTF8.GetBytes(json);
            request.ContentLength = byteArray.Length;
            ServicePointManager.ServerCertificateValidationCallback =
            delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();

            //Get response
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string user = reader.ReadToEnd();
            string[] userArr = user.Split(',');
            string[] checkStatus = userArr[1].Split(':');
            string totalUsers = "";
            if (checkStatus[1] == "\"0\"")
            {
                totalUsers = userArr[4].Split(':')[1];
            }
            return totalUsers;
        }
    }
}
