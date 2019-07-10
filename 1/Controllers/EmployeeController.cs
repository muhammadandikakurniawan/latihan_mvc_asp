using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _1.Controllers
{
    public class EmployeeController : Controller
    {
        [Route(@"employee/")]
        public ViewResult Index()
        {
            ViewBag.Title = "Employee";
            List<Dictionary<string, object>> persons = new List<Dictionary<string, object>>()
            {
                {
                    new Dictionary<string, object>()
                    {
                        {"id",1},
                        {"name","andika"},
                        {"age",18},
                        {"division","programmer"}
                    }
                },
                {
                    new Dictionary<string, object>()
                    {
                        {"id",2},
                        {"name","widodo"},
                        {"age",23},
                        {"division","designer"}
                    }
                },
                {
                    new Dictionary<string, object>()
                    {
                        {"id",3},
                        {"name","andri"},
                        {"age",22},
                        {"division","marketing"}
                    }
                },
                {
                    new Dictionary<string, object>()
                    {
                        {"id",4},
                        {"name","anggi"},
                        {"age",19},
                        {"division","hrd"}
                    }
                }
            };
            ViewBag.Employee = persons;
            return View();
        }

        [Route(@"employee/{id:int?}/edit")]
        public string Edit(int? id)
        {
            return "Edit Employee Id = " + id;
        }

        [Route(@"employee/{id?}/delete")]
        public string Delete(string id)
        {
            return "Delete Employee Id = " + id;
        }

        [Route(@"employee/list/{id:int?}")]
        public string List(int id = 0)
        {
            List<string> persons = new List<string>
            {
                "andika","widodo","andri","prima","ahmad"
            };
            if(id == 0)
            {
                return "gk milih siapa-siapa";
            }
            if(id > persons.Count)
            {
                return "Employe tidak ada";
            }
            else
            {
                return persons[id - 1];
            }
        }

        [Route(@"employee/detail/{id:int?}")]
        public ViewResult Detail(int id = 0)
        {
            List<Dictionary<string, object>> persons = new List<Dictionary<string, object>>()
            {
                {
                    new Dictionary<string, object>()
                    {
                        {"id",1},
                        {"name","andika"},
                        {"age",18},
                        {"division","programmer"}
                    }
                },
                {
                    new Dictionary<string, object>()
                    {
                        {"id",2},
                        {"name","widodo"},
                        {"age",23},
                        {"division","designer"}
                    }
                },
                {
                    new Dictionary<string, object>()
                    {
                        {"id",3},
                        {"name","andri"},
                        {"age",22},
                        {"division","marketing"}
                    }
                },
                {
                    new Dictionary<string, object>()
                    {
                        {"id",4},
                        {"name","anggi"},
                        {"age",19},
                        {"division","hrd"}
                    }
                }
            };
            if(id == 0)
            {
                Dictionary<string, string> data = new Dictionary<string, string>() { { "name", "null" }, { "division", "null" } };
                ViewBag.data = data;
            }
            if(id > persons.Count)
            {
                Dictionary<string, string> data = new Dictionary<string, string>() { { "name", "null" }, { "division", "null" } };
                ViewBag.data = data;
            }
            else
            {
                Dictionary<string, object> data = (Dictionary<string, object>)persons.Find((p) => p["id"].Equals(id));
                ViewBag.data = data;
            }
            ViewBag.Title = "Detail Karyawan";
            return View();
        }

        [Route(@"employee/data")]
        public ViewResult Data()
        {
            Models.Employee emp = new Models.Employee();
            emp.EmployeeId = 1;
            emp.EmployeeName = "Muhammad Andika Kurniawan";
            ViewBag.Title = "Data Employee";
            //ViewBag.Data = emp;
            return View(emp);
        }
    }
}