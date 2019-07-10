using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _3.Models;

namespace _3.Controllers
{
    public class LatihanController : Controller
    {
        [Route(@"latihan/form")]
        public ActionResult Form()
        {
            if (TempData.Peek("ListPerson") == null)
            {
                List<Person> ListPerson = new List<Person>();
                TempData.Add("ListPerson", ListPerson);
            }
            return View();
        }

        [ActionName("store")]
        [Route(@"latihan/store")]
        public ActionResult StorePerson(Person person)
        {
            List<Person> ListPerson = (List<Person>)TempData.Peek("ListPerson");
            ListPerson.Add(person);
            return Redirect("~/latihan/viewdataperson");
        }

        [Route(@"latihan/viewdataperson")]
        public ActionResult ViewDataPerson()
        {
            
            if(TempData.Peek("ListPerson") == null)
            {
                return Redirect("~/latihan/form");
            }
            List<Person> ListPerson = (List<Person>)TempData.Peek("ListPerson");
            return View("ViewData", ListPerson);
        }

        [Route(@"latihan/deleteperson/{id}")]
        public ActionResult DeletePerson(string id = "")
        {
            if(id == "" || TempData.Peek("ListPerson") == null)
            {
                return Redirect("~/latihan/viewdataperson");
            }
            List<Person> ListPerson = (List<Person>)TempData.Peek("ListPerson");
            ListPerson.RemoveAll(p => p.Id.Equals(id));
            return Redirect("~/latihan/viewdataperson");
        }

        [Route(@"latihan/updateperson/{id?}")]
        public ActionResult UpdatePerson(string id)
        {
            if (TempData.Peek("ListPerson") == null) {
                return Redirect("~/latihan/viewdataperson");
            }
            List<Person> ListPerson = (List<Person>)TempData.Peek("ListPerson");
            if (id == null)
            {
                return Redirect("~/latihan/viewdataperson");
            }
            Person person = ListPerson.Find(p => p.Id.Equals(id));
            if (ListPerson.FindAll(p => p.Id.Equals(id)).Count < 1)
            {
                return Redirect("~/latihan/viewdataperson");
            }
            return View("Update", person);
        }

        [HttpPost]
        [Route(@"latihan/updateperson")]
        public ActionResult UpdatePerson(Person person)
        {
            List<Person> ListPerson = (List<Person>)TempData.Peek("ListPerson");
            int i = ListPerson.FindIndex(p => p.Id.Equals(person.Id));
            ListPerson[i] = person;
            return Redirect("~/latihan/viewdataperson");
        }
    }
}





            