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
    public class LoginModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string LoggedUser { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool Login { get; set; }

        [BindProperty(SupportsGet = true)]
        [Required(ErrorMessage = "Email is required")]
        public string email { get; set; }
        [BindProperty(SupportsGet = true)]
        [Required(ErrorMessage = "Password is required")]
        public string password { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Message { get; set; }


        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }



        public void OnGet()
        {
            _httpContextAccessor.HttpContext.Session.SetString("Email", "yettobeset");
            var flag = HttpContext.Session.GetString("HideLogin");
            Login = Convert.ToBoolean(flag);
            LoggedUser = HttpContext.Session.GetString("Name");
            Message = HttpContext.Request.Query["handler"].ToString();
        }

        public IActionResult OnPostLoginAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // await _db.SaveChangesAsync();
            _httpContextAccessor.HttpContext.Session.SetString("Email", "yettobesetpost");
            HttpWebRequest request = WebRequest.Create("https://104.236.243.12/chantingapprest-0.0.1-SNAPSHOT/rest/user/login") as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/json";
            string json = "{ \"email\": \"" + email + "\", \"password\" : \"" + password + "\"}";
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
            if (checkStatus[1] == "\"0\"")
            {
                string[] userName = userArr[4].Split(':');
                _httpContextAccessor.HttpContext.Session.SetString("Email", email);
                _httpContextAccessor.HttpContext.Session.SetString("Password", password);
                _httpContextAccessor.HttpContext.Session.SetString("Name", userName[1].Replace('\\', ' ').Replace('"', ' ').Trim());
                _httpContextAccessor.HttpContext.Session.SetString("HideLogin", "true");
                return RedirectToPage("./Chant");
            }
            else
            {
                Message = "Invalid email or password";
                return RedirectToPage("./Login", Message);
            }           
        }

        public IActionResult OnPostLogoutAsync()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("./Index");
        }
    }
}