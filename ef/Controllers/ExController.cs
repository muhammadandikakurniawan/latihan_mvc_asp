using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ef.Models;
using System.Text.RegularExpressions;

namespace ef.Controllers
{
    public class ExController : Controller
    {
        [Route("")]
        public ActionResult Index()
        {
            return View("Employee/Index");
        }

        [Route("ex/employee/list")]
        public ActionResult EmployeeList()
        {
            HREntities3 hr = new HREntities3();
            List<COPY_EMP> emp = hr.COPY_EMP.ToList();
            return View("Employee/List", emp);
        }

        [HttpPost]
        [Route("ex/employee/list")]
        public ActionResult EmployeeList(string keyword)
        {
            using (HREntities3 hr = new HREntities3())
            {
                var regex = new Regex(".+(" + keyword + ").+", RegexOptions.IgnoreCase);
                //cara 1 => List<COPY_EMP> emp = hr.COPY_EMP.Where(e => e.FIRST_NAME.Contains(keyword)).ToList();
                List<COPY_EMP> emp = (from e in hr.COPY_EMP where e.FIRST_NAME.Contains(keyword) select e).ToList();
                if (emp.Count < 1 || keyword == "")
                {
                    return Redirect("~/ex/employee/list");
                }
                return View("Employee/List", emp);
            }
        }




        [Route("ex/employee/edit/{id}")]
        public ActionResult EditEmployee(int id)
        {
            if(id == null)
            {
                return Redirect("~/ex/employee/list");
            }
            HREntities3 hr = new HREntities3();
            COPY_EMP emp = hr.COPY_EMP.ToList().Find(e => e.EMPLOYEE_ID.Equals(id));
            return View("Employee/Edit", emp);
        }

        [HttpPost]
        [Route("ex/employee/edit/")]
        public ActionResult EditEmployee(COPY_EMP emp)
        {

            using (HREntities3 hr = new HREntities3())
            {
                COPY_EMP empl = hr.COPY_EMP.Find(emp.EMPLOYEE_ID);
                empl.FIRST_NAME = emp.FIRST_NAME;
                empl.LAST_NAME = emp.LAST_NAME;
                empl.EMAIL = emp.EMAIL;
                hr.SaveChanges();
                return Redirect("~/ex/employee/list");
            }
        }

        [Route("ex/employee/add")]
        public ActionResult AddEmployee()
        {
            return View("Employee/AddForm");
        }

        [HttpPost]
        [Route("ex/employee/add")]
        public ActionResult AddEmployee(COPY_EMP emp)
        {
           using (HREntities3 hr = new HREntities3())
            {
                hr.COPY_EMP.Add(emp);
                hr.SaveChanges();
            }
            return Redirect("~/ex/employee/list");
        }

        [Route("ex/employee/delete/{id}")]
        public ActionResult DeleteEmployee(int id)
        {
            using (HREntities3 hr = new HREntities3()) {
                COPY_EMP employee = hr.COPY_EMP.ToList().Find(e => e.EMPLOYEE_ID.Equals(id));
                hr.COPY_EMP.Remove(employee);
                hr.SaveChanges();
                return Redirect("~/ex/employee/list");
            }
        }






        [Route("ex/barang/list")]
        public ActionResult ListBarang()
        {
            db_adpEntities adp = new db_adpEntities();
            List<barang> ListBarang = adp.barangs.ToList();
            return View("Barang/List",ListBarang);
        }

        [HttpGet]
        [Route("ex/barang/add")]
        public ActionResult AddBarang()
        {
            return View("Barang/Add");
        }

        [HttpPost]
        [Route("ex/barang/add")]
        public ActionResult AddBarang(barang barang)
        {
            db_adpEntities adp = new db_adpEntities();
            adp.barangs.Add(barang);
            adp.SaveChanges();
            return Redirect("~/ex/barang/list");
        }








    }
}