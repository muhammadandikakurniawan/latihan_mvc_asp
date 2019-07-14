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
            //set authorization
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


        [HttpGet]
        [Route("apiclient/employee/{id?}")]
        public string ApiClientGet(decimal id)
        {
            //set authorization
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:58762/");
            var get = client.GetAsync("/try/employee");
            if (id != null)
            {
                get = client.GetAsync("/try/employee?id=" + id);
            }
            
            get.Wait();

            var result = get.Result;
            if (result.IsSuccessStatusCode) {
                return result.Content.ReadAsStringAsync().Result;
            }
            return "nul";
        }

        [Route("apiclient/post")]
        public ActionResult ClientPost()
        {
                return View("Post");
            
        }

        [HttpPost]
        [Route("apiclient/post")]
        public ActionResult ClientPost(EmployeeDTO emp)
        {
            if (emp != null)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:58762/");
                var post = client.PostAsJsonAsync("try/employee", emp);
                post.Wait();
                var result = post.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData.Add("message", "success");
                    return View("Post");
                }
                else
                {
                    TempData.Add("message", "fail");
                    return View("Post");
                }
            }
            else
            {
                TempData.Add("message", "fail");
                return Redirect("~/apiclient/post");
            }
        }
    }
}
