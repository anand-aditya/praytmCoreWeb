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

namespace PrayTm.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string LoggedUser { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool Login { get; set; }

        [BindProperty(SupportsGet = true)]
        [Required(ErrorMessage = "Name is required")]
        public string name { get; set; }
        [BindProperty(SupportsGet = true)]
        [Required(ErrorMessage = "Email is required")]
        public string email { get; set; }
        [BindProperty(SupportsGet = true)]
        [Required(ErrorMessage = "Password is required")]
        public string password { get; set; }
        [BindProperty(SupportsGet = true)]
        [Required(ErrorMessage = "Confirm Password is required")]
        public string confirmPassword { get; set; }
        [BindProperty(SupportsGet = true)]
        public string mobile { get; set; }
        [BindProperty(SupportsGet = true)]
        [Required(ErrorMessage = "City is required")]
        public string city { get; set; }
        [BindProperty(SupportsGet = true)]
        [Required(ErrorMessage = "Country is required")]
        public string country { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Message { get; set; }

        public void OnGet()
        {
            var flag = HttpContext.Session.GetString("HideLogin");
            Login = Convert.ToBoolean(flag);
            LoggedUser = HttpContext.Session.GetString("Name");
            Message = HttpContext.Request.Query["handler"].ToString();
        }

        public IActionResult OnPostRegisterAsync()
        {

            if (password != confirmPassword)
            {
                Message = "Passwrords do not match";
            }           
            else
            {
                HttpWebRequest request = WebRequest.Create("https://104.236.243.12/chantingapprest-0.0.1-SNAPSHOT/rest/user/register") as HttpWebRequest;
                request.Method = "POST";
                request.ContentType = "application/json";
                string json = "{ \"name\":\"" + name + "\", \"email\": \"" + email + "\", \"password\" : \"" + password + "\", \"mobile\": \"" + mobile + "\", \"city\": \"" + city + "\", \"country\": \"" + country + "\", \"isRegisteredViaGoogle\": \"false\"}";
                byte[] byteArray = Encoding.UTF8.GetBytes(json);
                request.ContentLength = byteArray.Length;
                ServicePointManager.ServerCertificateValidationCallback =
                delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                // Get response  
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string user = reader.ReadToEnd();
                string[] userArr = user.Split(',');
                string[] checkStatus = userArr[1].Split(':');
                if (checkStatus[1] == "\"0\"")
                {
                    //AppUser.Email = email;
                    //AppUser.Password = password;
                    //AppUser.Name = userName[1].Replace('\\', ' ').Replace('"', ' ').Trim();
                    //_db.AppUser = AppUser;
                    Message = "Account registered succesfully, Login to continue";
                    return RedirectToPage("./Login", Message);

                }
                else if (checkStatus[1] == "\"1\"")
                {
                    //lblMsg.Text = "Email is already resgistered";
                    //lblMsg.Visible = true;
                    Message = "Email is already resgistered";
                }
               
            }

            return null;

        }

        public IActionResult OnPostLogoutAsync()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("./Index");


        }
    }
}