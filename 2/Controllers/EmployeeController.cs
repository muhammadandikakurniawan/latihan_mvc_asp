using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _2.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee

        [Route(@"employee")]
        public ActionResult Index(string data1)
        {
            //data binding bisa diletakkan di argument method 
            //atau di ambil dengan function Request utk post dan Request.QueryString utk get
            ViewBag.Title = "Employee";
            ViewBag.Data = new Dictionary<string, object>()
            {
                {"data1",data1},
                {"data2",Request.QueryString["data2"]},
                {"data3",Request["data3"]}
            };
            return View();
        }

        [Route(@"employee/insert")]
        public ActionResult Insert()
        {
            Session.Add("employees", new List<Dictionary<string, object>>());
            return View();
        }


    }
}