using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ef.Controllers
{
    public class ExController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}