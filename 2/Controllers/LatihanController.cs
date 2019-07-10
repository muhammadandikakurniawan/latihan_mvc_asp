using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _2.Models;

namespace _2.Controllers
{
    public class LatihanController : Controller
    {
        // GET: Latihan
        [Route(@"latihan/palindrom/{text?}")]
        public ActionResult Palindrom(string text = "")
        {
            if(text == "")
            {
                ViewBag.Result = "-------------";
            }
            else
            {
                int l = text.Length - 1;
                int f = 0;
                List<string> tl = new List<string>();
                List<string> tf = new List<string>();
                //bool status = false;
                while (f < text.Length)
                {
                    tf.Add(text.Substring(f, 1));
                    tl.Add(text.Substring(l, 1));

                    f++;
                    l--;
                }
                if (String.Join(" - ", tf).ToLower() == String.Join(" - ", tl).ToLower())
                {
                    ViewBag.Result = text + " Palindrom";
                }
                else
                {
                    ViewBag.Result = text + " Tidak Palindrom";
                }
            }
            
            return View();
        }

        [Route(@"latihan/form")]
        public ActionResult Form()
        {
            return View();
        }

        [Route(@"latihan/show")]
        //[HttpPostAttribute]
        public ActionResult Show(string id = "",string fn = "", string ln = "")
        {
            //Dictionary<string, object> data = new Dictionary<string, object>();
            //data.Add("id", id);
            //data.Add("fn", fn);
            //data.Add("ln", ln);
            //ViewBag.Data = data;
            
            if(id == "" || fn == "" || ln == "")
            {
                return Redirect("~/latihan/form");
            }
            Person person = new Person();
            person.Id = Convert.ToInt32(id);
            person.FirstName = fn;
            person.LastName = ln;


            return View(person);
        }

        [Route(@"latihan/modelbind")]
        public ActionResult ModelBind()
        {

            return View();
        }

        [Route(@"latihan/showmodelbind")]
        public ActionResult ShowModelBind(Person person)
        {
            if(person == null)
            {
                return Redirect(@"~/latihan/modelbind");
            }
            Person.ListPerson.Add(person);
            return View(person);
        }

        [Route(@"latihan/trytemp")]
        public ActionResult Store(Person person)
        {
            TempData.Add("person", person);

            return Redirect("~/latihan/showtempt");
        }

        [Route(@"latihan/showtempt")]
        public ActionResult ShowTempt()
        {
            //Person person = (Person)TempData["person"];
            Person person = (Person)TempData.Peek("person");
            return View("ShowModelBind",person);
        }
    }
}