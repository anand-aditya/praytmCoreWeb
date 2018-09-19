using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PrayTm.Pages
{
    public class HistoryModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string LoggedUser { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool Login { get; set; }

        public HtmlString history { get; set;}

        [BindProperty(SupportsGet = true)]
        public string date17 { get; set; }
        [BindProperty(SupportsGet = true)]
        public string date18 { get; set; }
        [BindProperty(SupportsGet = true)]
        public string date19 { get; set; }
        [BindProperty(SupportsGet = true)]
        public string date20 { get; set; }
        [BindProperty(SupportsGet = true)]
        public string date21 { get; set; }
        [BindProperty(SupportsGet = true)]
        public string date22 { get; set; }
        [BindProperty(SupportsGet = true)]
        public string date23 { get; set; }
        [BindProperty(SupportsGet = true)]
        public string date24 { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Count { get; set; }

        [BindProperty(SupportsGet = true)]
        public string TotalUsers { get; set; }

        public IActionResult OnGet()
        {
            var flag = HttpContext.Session.GetString("HideLogin");
            Login = Convert.ToBoolean(flag);
            LoggedUser = HttpContext.Session.GetString("Name");
            if (string.IsNullOrEmpty(LoggedUser))
            {
                return RedirectToPage("./Login");
            }           
            getHistory();
            //Count = getTotalCount();
            //TotalUsers = getTotalUsers();
            return null;
        }

        private void getHistory()
        {
            try
            {
                HttpWebRequest request = WebRequest.Create("https://104.236.243.12/chantingapprest-0.0.1-SNAPSHOT/rest/user/get_chanting_history") as HttpWebRequest;
                request.Method = "POST";
                request.ContentType = "application/json";

                string Email = HttpContext.Session.GetString("Email");
                string Password = HttpContext.Session.GetString("Password");

                string json = "{ \"userDto\":{ \"email\": \"" + Email + "\", \"password\" : \"" + Password + "\"}}";
                byte[] byteArray = Encoding.UTF8.GetBytes(json);
                request.ContentLength = byteArray.Length;
                ServicePointManager.ServerCertificateValidationCallback =
                    delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

                Stream dataStream = request.GetRequestStream();
                // Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length);
                // Close the Stream object.
                dataStream.Close();

                // Get response  
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string historyResponse = reader.ReadToEnd();
                string[] historyArr = historyResponse.Split('{');
                string[] historyArrMonth = historyArr[3].Split(',');
                Dictionary<DateTime, int> dicHistory = new Dictionary<DateTime, int>();
                for (int i = 0; i < historyArrMonth.Length; i++)
                {
                    string[] data = historyArrMonth[i].Split(':');
                    DateTime dt = Convert.ToDateTime(data[0].Replace("\"", ""));
                    string date = dt.ToString("dd-MMM-yyyy");
                    int count = Convert.ToInt32(data[1].Replace("}", ""));
                    int countRounds = count / 108;
                    dicHistory.Add(dt, countRounds);
                }
                var list = dicHistory.Keys.ToList();
                list.Sort();
                list.Reverse();
                string test = "<table><tr><td><strong>Date</td><td><td>&nbsp;&nbsp;&nbsp;</td><td>&nbsp;&nbsp;&nbsp;</td><td>&nbsp;&nbsp;&nbsp;</td><td>&nbsp;&nbsp;&nbsp;</td><td>&nbsp;&nbsp;&nbsp;</td></td><td><strong>Rounds</strong></td></tr>";
                foreach (var lt in list)
                {
                    string dateChk = lt.ToString("dd-MMM-yyyy");
                    switch (dateChk)
                    {
                        case "17-Sep-2018":
                            date17 = dicHistory[lt].ToString();
                            break;
                        case "18-Sep-2018":
                            date18 = dicHistory[lt].ToString();
                            break;
                        case "19-Sep-2018":
                            date19 = dicHistory[lt].ToString();
                            break;
                        case "20-Sep-2018":
                            date20 = dicHistory[lt].ToString();
                            break;
                        case "21-Sep-2018":
                            date21 = dicHistory[lt].ToString();
                            break;
                        case "22-Sep-2018":
                            date22 = dicHistory[lt].ToString();
                            break;
                        case "23-Sep-2018":
                            date23 = dicHistory[lt].ToString();
                            break;
                        case "24-Sep-2018":
                            date24 = dicHistory[lt].ToString();
                            break;
                        default:                            
                            break;
                    }
                    test += "<tr><td>"+lt.ToString("dd-MMM-yyyy") + "</td><td><td>&nbsp;&nbsp;&nbsp;</td><td>&nbsp;&nbsp;&nbsp;</td><td>&nbsp;&nbsp;&nbsp;</td><td>&nbsp;&nbsp;&nbsp;</td><td>&nbsp;&nbsp;&nbsp;</td> </td><td>" + dicHistory[lt].ToString() +"</td></tr>";                                    
                }
                test += "</table>";                 
                history = new HtmlString(test);
                //lblHistory.Visible = true;
                //lblHeader.Visible = true;
            }
            catch (Exception)
            {
                //MessageBox.Show("Connection error, please try again");
            }
        }
        public IActionResult OnPostLogoutAsync()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("./Index");

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