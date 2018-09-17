using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PrayTm.Pages
{
    public class ChantModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string LoggedUser { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool Login { get; set; }

        [BindProperty(SupportsGet = true)]
        [Required(ErrorMessage = "Number of rounds is required")]
        public string rounds { get; set; }

        [BindProperty(SupportsGet = true)]
        [Required(ErrorMessage = "Date is required")]
        public DateTime date { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Count { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Message { get; set; }

        [BindProperty(SupportsGet = true)]
        public string TotalUsers { get; set; }


        public List<SelectListItem> RoundsList { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "1", Text = "1" },
            new SelectListItem { Value = "2", Text = "2" },
            new SelectListItem { Value = "3", Text = "3"  },
            new SelectListItem { Value = "4", Text = "4"  },
            new SelectListItem { Value = "5", Text = "5"  },
            new SelectListItem { Value = "6", Text = "6"  },
            new SelectListItem { Value = "7", Text = "7"  },
            new SelectListItem { Value = "8", Text = "8"  },
            new SelectListItem { Value = "9", Text = "9"  },
            new SelectListItem { Value = "10", Text = "10"  },
            new SelectListItem { Value = "11", Text = "11"  },
            new SelectListItem { Value = "12", Text = "12"  },
            new SelectListItem { Value = "13", Text = "13"  },
            new SelectListItem { Value = "14", Text = "14"  },
            new SelectListItem { Value = "15", Text = "15"  },
            new SelectListItem { Value = "16", Text = "16"  },
            new SelectListItem { Value = "17", Text = "17"  },
            new SelectListItem { Value = "18", Text = "18"  },
            new SelectListItem { Value = "19", Text = "19"  },
            new SelectListItem { Value = "20", Text = "20"  },
            new SelectListItem { Value = "21", Text = "21"  },
            new SelectListItem { Value = "22", Text = "22"  },
            new SelectListItem { Value = "23", Text = "23"  },
            new SelectListItem { Value = "24", Text = "24"  },
            new SelectListItem { Value = "25", Text = "25"  },
            new SelectListItem { Value = "26", Text = "26" },
            new SelectListItem { Value = "27", Text = "27" },
            new SelectListItem { Value = "28", Text = "28"  },
            new SelectListItem { Value = "29", Text = "29"  },
            new SelectListItem { Value = "30", Text = "30"  },
            new SelectListItem { Value = "31", Text = "31"  },
            new SelectListItem { Value = "32", Text = "32"  },
            new SelectListItem { Value = "33", Text = "33"  },
            new SelectListItem { Value = "34", Text = "34"  },
            new SelectListItem { Value = "35", Text = "35"  },
            new SelectListItem { Value = "36", Text = "36"  },
            new SelectListItem { Value = "37", Text = "37"  },
            new SelectListItem { Value = "38", Text = "38"  },
            new SelectListItem { Value = "39", Text = "39"  },
            new SelectListItem { Value = "40", Text = "40"  },
            new SelectListItem { Value = "41", Text = "41"  },
            new SelectListItem { Value = "42", Text = "42"  },
            new SelectListItem { Value = "43", Text = "43"  },
            new SelectListItem { Value = "44", Text = "44"  },
            new SelectListItem { Value = "45", Text = "45"  },
            new SelectListItem { Value = "46", Text = "46"  },
            new SelectListItem { Value = "47", Text = "47"  },
            new SelectListItem { Value = "48", Text = "48"  },
            new SelectListItem { Value = "49", Text = "49"  },
            new SelectListItem { Value = "50", Text = "50"  },
            new SelectListItem { Value = "51", Text = "51"  },
            new SelectListItem { Value = "52", Text = "52" },
            new SelectListItem { Value = "53", Text = "53" },      
            new SelectListItem { Value = "54", Text = "54"  },
            new SelectListItem { Value = "55", Text = "55"  },
            new SelectListItem { Value = "56", Text = "56"  },
            new SelectListItem { Value = "57", Text = "57"  },
            new SelectListItem { Value = "58", Text = "58"  },
            new SelectListItem { Value = "59", Text = "59"  },
            new SelectListItem { Value = "60", Text = "60"  },
            new SelectListItem { Value = "61", Text = "61"  },
            new SelectListItem { Value = "62", Text = "62"  },
            new SelectListItem { Value = "63", Text = "63"  },
            new SelectListItem { Value = "64", Text = "64"  },
            new SelectListItem { Value = "65", Text = "65"  },
            new SelectListItem { Value = "66", Text = "66"  },
            new SelectListItem { Value = "67", Text = "67"  },
            new SelectListItem { Value = "68", Text = "68"  },
            new SelectListItem { Value = "69", Text = "69"  },
            new SelectListItem { Value = "70", Text = "70"  },
            new SelectListItem { Value = "71", Text = "71"  },
            new SelectListItem { Value = "72", Text = "72"  },
            new SelectListItem { Value = "73", Text = "73"  },
            new SelectListItem { Value = "74", Text = "74"  },
            new SelectListItem { Value = "75", Text = "75"  },
            new SelectListItem { Value = "76", Text = "76" },
            new SelectListItem { Value = "77", Text = "77" },
            new SelectListItem { Value = "78", Text = "78"  },
            new SelectListItem { Value = "79", Text = "79"  },
            new SelectListItem { Value = "80", Text = "80"  },
            new SelectListItem { Value = "81", Text = "81"  },
            new SelectListItem { Value = "82", Text = "82"  },
            new SelectListItem { Value = "83", Text = "83"  },
            new SelectListItem { Value = "84", Text = "84"  },
            new SelectListItem { Value = "85", Text = "85"  },
            new SelectListItem { Value = "86", Text = "86"  },
            new SelectListItem { Value = "87", Text = "87"  },
            new SelectListItem { Value = "88", Text = "88"  },
            new SelectListItem { Value = "89", Text = "89"  },
            new SelectListItem { Value = "90", Text = "90"  },
            new SelectListItem { Value = "91", Text = "91"  },
            new SelectListItem { Value = "92", Text = "92"  },
            new SelectListItem { Value = "93", Text = "93"  },
            new SelectListItem { Value = "94", Text = "94"  },
            new SelectListItem { Value = "95", Text = "95"  },
            new SelectListItem { Value = "96", Text = "96"  },
            new SelectListItem { Value = "97", Text = "97"  },
            new SelectListItem { Value = "98", Text = "98"  },
            new SelectListItem { Value = "99", Text = "99"  },
            new SelectListItem { Value = "100", Text = "100"  },
            new SelectListItem { Value = "101", Text = "101"  },
            new SelectListItem { Value = "102", Text = "102" },
            new SelectListItem { Value = "103", Text = "103"  },
            new SelectListItem { Value = "104", Text = "104"  },
            new SelectListItem { Value = "105", Text = "105"  },
            new SelectListItem { Value = "106", Text = "106"  },
            new SelectListItem { Value = "107", Text = "107"  },
            new SelectListItem { Value = "108", Text = "108"  },
            new SelectListItem { Value = "109", Text = "109"  },
            new SelectListItem { Value = "110", Text = "110"  },
            new SelectListItem { Value = "111", Text = "111"  },
            new SelectListItem { Value = "112", Text = "112"  },
            new SelectListItem { Value = "113", Text = "113"  },
            new SelectListItem { Value = "114", Text = "114"  },
            new SelectListItem { Value = "115", Text = "115"  },
            new SelectListItem { Value = "116", Text = "116"  },
            new SelectListItem { Value = "117", Text = "117"  },
            new SelectListItem { Value = "118", Text = "118"  },
            new SelectListItem { Value = "119", Text = "119"  },
            new SelectListItem { Value = "120", Text = "120"  },
            new SelectListItem { Value = "121", Text = "121"  },
            new SelectListItem { Value = "122", Text = "122"  },
            new SelectListItem { Value = "123", Text = "123"  },
            new SelectListItem { Value = "124", Text = "124"  },
            new SelectListItem { Value = "125", Text = "125"  },
            new SelectListItem { Value = "126", Text = "126" },
            new SelectListItem { Value = "127", Text = "127" },
            new SelectListItem { Value = "128", Text = "128"  },
            new SelectListItem { Value = "129", Text = "129"  },
            new SelectListItem { Value = "130", Text = "130"  },
            new SelectListItem { Value = "131", Text = "131"  },
            new SelectListItem { Value = "132", Text = "132"  },
            new SelectListItem { Value = "133", Text = "133"  },
            new SelectListItem { Value = "134", Text = "134"  },
            new SelectListItem { Value = "135", Text = "135"  },
            new SelectListItem { Value = "136", Text = "136"  },
            new SelectListItem { Value = "137", Text = "137"  },
            new SelectListItem { Value = "138", Text = "138"  },
            new SelectListItem { Value = "139", Text = "139"  },
            new SelectListItem { Value = "140", Text = "140"  },
            new SelectListItem { Value = "141", Text = "141"  },
            new SelectListItem { Value = "142", Text = "142"  },
            new SelectListItem { Value = "143", Text = "143"  },
            new SelectListItem { Value = "144", Text = "144"  },
            new SelectListItem { Value = "145", Text = "145"  },
            new SelectListItem { Value = "146", Text = "146"  },
            new SelectListItem { Value = "147", Text = "147"  },
            new SelectListItem { Value = "148", Text = "148"  },
            new SelectListItem { Value = "149", Text = "149"  },
            new SelectListItem { Value = "150", Text = "150"  },
            new SelectListItem { Value = "151", Text = "151"  },
            new SelectListItem { Value = "152", Text = "152" },
            new SelectListItem { Value = "153", Text = "153" },
            new SelectListItem { Value = "154", Text = "154"  },
            new SelectListItem { Value = "155", Text = "155"  },
            new SelectListItem { Value = "156", Text = "156"  },
            new SelectListItem { Value = "157", Text = "157"  },
            new SelectListItem { Value = "158", Text = "158"  },
            new SelectListItem { Value = "159", Text = "159"  },
            new SelectListItem { Value = "160", Text = "160"  },
            new SelectListItem { Value = "161", Text = "161"  },
            new SelectListItem { Value = "162", Text = "162"  },
            new SelectListItem { Value = "163", Text = "163"  },
            new SelectListItem { Value = "164", Text = "164"  },
            new SelectListItem { Value = "165", Text = "165"  },
            new SelectListItem { Value = "166", Text = "166"  },
            new SelectListItem { Value = "167", Text = "167"  },
            new SelectListItem { Value = "168", Text = "168"  },
            new SelectListItem { Value = "169", Text = "169"  },
            new SelectListItem { Value = "170", Text = "170"  },
            new SelectListItem { Value = "171", Text = "171"  },
            new SelectListItem { Value = "172", Text = "172"  },
            new SelectListItem { Value = "173", Text = "173"  },
            new SelectListItem { Value = "174", Text = "174"  },
            new SelectListItem { Value = "175", Text = "175"  },
            new SelectListItem { Value = "176", Text = "176" },
            new SelectListItem { Value = "177", Text = "177" },
            new SelectListItem { Value = "178", Text = "178"  },
            new SelectListItem { Value = "179", Text = "179"  },
            new SelectListItem { Value = "180", Text = "180"  },
            new SelectListItem { Value = "181", Text = "181"  },
            new SelectListItem { Value = "182", Text = "182"  },
            new SelectListItem { Value = "183", Text = "183"  },
            new SelectListItem { Value = "184", Text = "184"  },
            new SelectListItem { Value = "185", Text = "185"  },
            new SelectListItem { Value = "186", Text = "186"  },
            new SelectListItem { Value = "187", Text = "187"  },
            new SelectListItem { Value = "188", Text = "188"  },
            new SelectListItem { Value = "189", Text = "189"  },
            new SelectListItem { Value = "190", Text = "190"  },
            new SelectListItem { Value = "191", Text = "191"  },
            new SelectListItem { Value = "192", Text = "192"  },
            new SelectListItem { Value = "193", Text = "193"  },
            new SelectListItem { Value = "194", Text = "194"  },
            new SelectListItem { Value = "195", Text = "195"  },
            new SelectListItem { Value = "196", Text = "196"  },
            new SelectListItem { Value = "197", Text = "197"  },
            new SelectListItem { Value = "198", Text = "198"  },
            new SelectListItem { Value = "199", Text = "199"  },
            new SelectListItem { Value = "200", Text = "200"  },
            new SelectListItem { Value = "201", Text = "201"  },
            new SelectListItem { Value = "202", Text = "202" },
            new SelectListItem { Value = "203", Text = "203"  },
            new SelectListItem { Value = "204", Text = "204"  },
            new SelectListItem { Value = "205", Text = "205"  },
            new SelectListItem { Value = "206", Text = "206"  },
            new SelectListItem { Value = "207", Text = "207"  },
            new SelectListItem { Value = "208", Text = "208"  },
            new SelectListItem { Value = "209", Text = "209"  },
            new SelectListItem { Value = "210", Text = "210"  },
            new SelectListItem { Value = "211", Text = "211"  },
            new SelectListItem { Value = "212", Text = "212"  },
            new SelectListItem { Value = "213", Text = "213"  },
            new SelectListItem { Value = "214", Text = "214"  },
            new SelectListItem { Value = "215", Text = "215"  },
            new SelectListItem { Value = "216", Text = "216"  },
            new SelectListItem { Value = "217", Text = "217"  },
            new SelectListItem { Value = "218", Text = "218"  },
            new SelectListItem { Value = "219", Text = "219"  },
            new SelectListItem { Value = "220", Text = "220"  },
            new SelectListItem { Value = "221", Text = "221"  },
            new SelectListItem { Value = "222", Text = "222"  },
            new SelectListItem { Value = "223", Text = "223"  },
            new SelectListItem { Value = "224", Text = "224"  },
            new SelectListItem { Value = "225", Text = "225"  },
            new SelectListItem { Value = "226", Text = "226" },
            new SelectListItem { Value = "227", Text = "227" },
            new SelectListItem { Value = "228", Text = "228"  },
            new SelectListItem { Value = "229", Text = "229"  },
            new SelectListItem { Value = "230", Text = "230"  },
            new SelectListItem { Value = "231", Text = "231"  },
            new SelectListItem { Value = "232", Text = "232"  },
            new SelectListItem { Value = "233", Text = "233"  },
            new SelectListItem { Value = "234", Text = "234"  },
            new SelectListItem { Value = "235", Text = "235"  },
            new SelectListItem { Value = "236", Text = "236"  },
            new SelectListItem { Value = "237", Text = "237"  },
            new SelectListItem { Value = "238", Text = "238"  },
            new SelectListItem { Value = "239", Text = "239"  },
            new SelectListItem { Value = "240", Text = "240"  },
            new SelectListItem { Value = "241", Text = "241"  },
            new SelectListItem { Value = "242", Text = "242"  },
            new SelectListItem { Value = "243", Text = "243"  },
            new SelectListItem { Value = "244", Text = "244"  },
            new SelectListItem { Value = "245", Text = "245"  },
            new SelectListItem { Value = "246", Text = "246"  },
            new SelectListItem { Value = "247", Text = "247"  },
            new SelectListItem { Value = "248", Text = "248"  },
            new SelectListItem { Value = "249", Text = "249"  },
            new SelectListItem { Value = "250", Text = "250"  },
            new SelectListItem { Value = "251", Text = "251"  }
        };

        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChantModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void OnGet()
        {
            var flag = _httpContextAccessor.HttpContext.Session.GetString("HideLogin");
            Login = Convert.ToBoolean(flag);
            LoggedUser = _httpContextAccessor.HttpContext.Session.GetString("Name");
            Count = getTotalCount();
            Message = HttpContext.Request.Query["handler"].ToString();
            TotalUsers = getTotalUsers();
            if(string.IsNullOrEmpty(LoggedUser))
            {
                RedirectToPage("./Index");
            }
        }

        public void OnPost()
        {
            //ViewData["Test String"] = HttpContext.Session.GetString("Test String");
            //ViewData["Test Int"] = HttpContext.Session.GetInt32("Test Int");
            //ViewData["Test Byte Array"], BitConverter.ToBoolean(HttpContext.Session.Get("Test Byte Array"), 0);
        }

        public IActionResult OnPostAddAsync()
        {
            string dateWithTime = date.ToString("yyyy-MM-ddTHH:mm:ss");
            if (dateWithTime == "0001-01-01T00:00:00")
            {
                Message = "Please select a date";
            }
            else
            {
                HttpWebRequest request = WebRequest.Create("https://104.236.243.12/chantingapprest-0.0.1-SNAPSHOT/rest/user/save_new_chanting_session") as HttpWebRequest;
                request.Method = "POST";
                request.ContentType = "application/json";
                //DateTime dt = dtSelectCD.Value
                string Email = _httpContextAccessor.HttpContext.Session.GetString("Email");
                string Password = _httpContextAccessor.HttpContext.Session.GetString("Password");


                //int numberOfRounds = Convert.ToInt32(cmbRounds.Text.ToString());
                int numberofBeads = Convert.ToInt32(rounds) * 108;//numberOfRounds * 108;
                                                                  //string json = "{ \"userName\": \"" + _db.AppUser.Email + "\", \"password\" : \"" + _db.AppUser.Password + "\", \"chantingSessionDate\": \"" + dateWithTime + "\",\"chantingSessionStartTime\":\"" + dateWithTime + "\",\"chantingSessionEndTime\":\"" + dateWithTime + "\", \"numberOfBeads\":\"" + numberofBeads.ToString() + "\"}";
                string json = "{ \"userName\": \"" + Email + "\", \"password\" : \"" + Password + "\", \"chantingSessionDate\": \"" + dateWithTime + "\",\"chantingSessionStartTime\":\"" + dateWithTime + "\",\"chantingSessionEndTime\":\"" + dateWithTime + "\", \"numberOfBeads\":\"" + numberofBeads.ToString() + "\"}";
                byte[] byteArray = Encoding.UTF8.GetBytes(json);
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                // Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length);
                // Close the Stream object.
                dataStream.Close();

                // Get response  
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                // Display the status.
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string result = reader.ReadToEnd();
                string[] userArr = result.Split(',');
                string[] checkStatus = userArr[1].Split(':');
                if (checkStatus[1] == "\"0\"")
                {
                    //MessageBox.Show("Count submitted succesfully");
                    Count = getTotalCount();
                    //cmbRounds.SelectedIndex = 0;
                    DateTime dtNow = DateTime.Now;
                    //dtSelectCD.Value = dtNow;              
                    Message = "Round added succesfully";
                    //return null;
                }
                else
                {
                    Message = "Unable to process request, please try again";
                }
            }
            return RedirectToPage("./Chant", Message);

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

        public IActionResult OnPostLogoutAsync()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("./Index");


        }
    }
}