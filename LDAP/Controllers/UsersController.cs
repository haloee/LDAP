using LDAP.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using LDAP.Models;
using LDAP.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LDAP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
       
        private readonly Interface1 _context;
        public UsersController(Interface1 Context)
        {
            _context = Context;
        }
        [HttpGet("external")]
        //public async Task<IActionResult> YourAction()
        //{
        //    try
        //    {
        //        var apiUrl = "https://auth.hungaria.koerber.de/AllUsers/get-all";

        //        using (var handler = new HttpClientHandler
        //        {
        //            UseDefaultCredentials = true,
        //            Credentials = CredentialCache.DefaultCredentials
        //        })
        //        using (var httpClient = new HttpClient(handler))
        //        {
        //            var response = await httpClient.GetAsync(apiUrl);

        //            if (response.IsSuccessStatusCode)
        //            {
        //                var responseContent = await response.Content.ReadAsStringAsync();
        //                var users = JsonConvert.DeserializeObject<List<ExUsers>>(responseContent);
        //                return Ok(users);
        //            }
        //            else
        //            {
        //                // Return the HTTP status code and a message
        //                return StatusCode((int)response.StatusCode, $"Error occurred during API call. StatusCode: {response.StatusCode}");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Return a 500 Internal Server Error with a message
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}
        public async Task<IActionResult> GetExUs()
        {
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            httpClientHandler.UseDefaultCredentials = true;
            HttpClient httpClient = new HttpClient(httpClientHandler);
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("https://auth.hungaria.koerber.de/AllUsers/get-all");
            var Json= await httpResponseMessage.Content.ReadAsStringAsync();
            ExList exlist=JsonConvert.DeserializeObject<ExList>(Json);
            httpClientHandler.Dispose();
            httpClient.Dispose();
            return Ok(exlist);
        }
        [HttpGet("GetAll")]
        public IActionResult GetAllUsers()
        {
            var getall = _context.GetAllUsers();
            return Ok(getall);
        }
        [HttpGet("GetUser")]
        public IActionResult GetUser(string accountName)
        {
            var getuser = _context.GetUser(accountName);
            return Ok(getuser);

        }
    }

    public class User
    {
        
        public string Name { get; set; }
        public string sAMAccountName { get; set; }
        public string department { get; set; }
        public string mail { get; set; }
        public string extensionAttribute { get; set; }
        public string manager { get; set; }
    }
    

}

