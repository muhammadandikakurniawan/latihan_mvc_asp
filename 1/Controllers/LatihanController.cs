using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _1.Controllers
{
    public class LatihanController : Controller
    {
        // GET: Latihan
        [Route(@"palindrom/{text?}")]
        public string Palindrom(string text = "")
        {
            return text.Substring(0);
        }
    }
}