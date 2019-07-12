using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Api.Models;
using Api.Filters;
using Api.DTO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Api.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        [HttpGet]
        [Route("apiclient")]
        public string ApiClient()
        {
            
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://localhost:58762/");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "cmluYTpzaWxmaWE=");

            var get = client.GetAsync("getprincipal");
            get.Wait();

            var result = get.Result;

            if (result.IsSuccessStatusCode)
            {
                var read = result.Content.ReadAsAsync<string>();
                read.Wait();
                return read.Result;
            }
            return "fail";
        }
    }
}
