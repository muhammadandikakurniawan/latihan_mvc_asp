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
        public ActionResult Auth()
        {
            if(Session["Login"] != null)
            {
                HREntities3 hr = new HREntities3();
                COPY_EMP empl = hr.COPY_EMP.ToList().Find(e => e.EMAIL == Session["Login"].ToString());
                if (empl == null)
                {
                    TempData.Add("message", "User tidak tersedia");
                    TempData.Add("type", "warning");
                    return Redirect("~/");
                }
                else
                {
                    if (empl.FIRST_NAME == empl.FIRST_NAME)
                    {
                        Session.Add("login", empl.EMAIL);
                        return Redirect("~/ex");
                    }
                    else
                    {
                        TempData.Add("message", "password salah");
                        TempData.Add("type", "warning");
                        return Redirect("~/");
                    }
                }
            }
            return View("Auth");
        }

        [HttpPost]
        [Route("")]
        public ActionResult Auth(COPY_EMP emp)
        {
            HREntities3 hr = new HREntities3();
            COPY_EMP empl = hr.COPY_EMP.ToList().Find(e => e.EMAIL == emp.EMAIL);
            if (empl == null)
            {
                TempData.Add("message", "User tidak tersedia");
                TempData.Add("type", "warning");
                return Redirect("~/");
            }
            else
            {
                if (empl.FIRST_NAME != emp.FIRST_NAME)
                {
                    TempData.Add("message", "password salah");
                    TempData.Add("type", "warning");
                    return Redirect("~/");
                }
                else
                {
                    
                    Session.Add("login", emp.EMAIL);
                    return Redirect("~/ex");
                }
            }
        }

        [Route("ex")]
        public ActionResult Index()
        {
            if(Session["login"] == null)
            {
                return Redirect("~/");
            }
            HREntities3 hr = new HREntities3();
            COPY_EMP emp = hr.COPY_EMP.ToList().Find(e => e.EMAIL == Session["login"].ToString());
            TempData.Add("message", "Login as "+ emp.FIRST_NAME);
            TempData.Add("type", "success");
            return View("Index");
        }

        [Route("ex/employee/list")]
        public ActionResult EmployeeList()
        {
            if (Session["login"] == null)
            {
                return Redirect("~/");
            }
            HREntities3 hr = new HREntities3();
            List<COPY_EMP> emp = hr.COPY_EMP.ToList();
            return View("Employee/List", emp);
        }

        [HttpPost]
        [Route("ex/employee/list")]
        public ActionResult EmployeeList(string keyword)
        {
            if (Session["login"] == null)
            {
                return Redirect("~/");
            }
            using (HREntities3 hr = new HREntities3())
            {
                // List<COPY_EMP> emp = hr.COPY_EMP.Where(e => e.FIRST_NAME.Contains(keyword)).ToList(); //cara 1 
                List<COPY_EMP> emp = (from e in hr.COPY_EMP where e.FIRST_NAME.Contains(keyword) select e).ToList(); //cara 2
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
            if (Session["login"] == null)
            {
                return Redirect("~/");
            }
            if (id == null)
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
            if (Session["login"] == null)
            {
                return Redirect("~/");
            }
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
            if (Session["login"] == null)
            {
                return Redirect("~/");
            }
            return View("Employee/AddForm");
        }

        [HttpPost]
        [Route("ex/employee/add")]
        public ActionResult AddEmployee(COPY_EMP emp)
        {
            if (Session["login"] == null)
            {
                return Redirect("~/");
            }
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
            if (Session["login"] == null)
            {
                return Redirect("~/");
            }
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
            if (Session["login"] == null)
            {
                return Redirect("~/");
            }
            db_adpEntities adp = new db_adpEntities();
            List<barang> ListBarang;
            if (Request["keyword"] != null)
            {
                ListBarang = adp.barangs.ToList().FindAll(p => Regex.IsMatch(p.nama_barang, "^.*["+Request["keyword"]+"].*$"));
                if (ListBarang.Count < 1)
                {
                    ListBarang = adp.barangs.ToList();
                }
            }
            else
            {
                ListBarang = adp.barangs.ToList();
            }
            return View("Barang/List", ListBarang);
        }

        //[HttpPost]
        //[Route("ex/barang/list")]
        //public ActionResult SearchBarang(string keyword = "")
        //{
        //    if (Session["login"] == null)
        //    {
        //        return Redirect("~/");
        //    }
        //    db_adpEntities adp = new db_adpEntities();
        //    List<barang> ListBarang;
        //    if (keyword != null || keyword != "")
        //    {
        //        ListBarang = adp.barangs.Where(p => p.nama_barang.Contains(keyword) || p.id_barang.Contains(keyword)).ToList();
        //        if (ListBarang.Count < 1)
        //        {
        //            TempData.Add("message", "Barang tidak tersedia");
        //            TempData.Add("type", "warning");
        //            return Redirect("~/ex/barang/list");
        //        }
        //        else
        //        {
        //            return View("Barang/List", ListBarang);
        //        }
        //    }
        //    return Redirect("~/ex/barang/list");
        //}

        [HttpGet]
        [Route("ex/barang/add")]
        public ActionResult AddBarang()
        {
            if (Session["login"] == null)
            {
                return Redirect("~/");
            }
            return View("Barang/Add");
        }

        [HttpPost]
        [Route("ex/barang/add")]
        public ActionResult AddBarang(barang barang)
        {
            if (Session["login"] == null)
            {
                return Redirect("~/");
            }
            db_adpEntities adp = new db_adpEntities();

            if (adp.barangs.Where(p => p.id_barang == barang.id_barang).ToList().Count >= 1)
            {
                TempData.Add("message", "barang yang sudah ada tidak bisa ditambahkn kembali");
                TempData.Add("type", "warning");
                return Redirect("~/ex/barang/add");
            }

            adp.barangs.Add(barang);
            adp.SaveChanges();
            TempData.Add("message", "Barang baru berhasil ditambahkan");
            TempData.Add("type", "success");
            return Redirect("~/ex/barang/add");
        }


        [Route("ex/barang/edit/{id?}")]
        public ActionResult EditBarang(string id = null)
        {
            if (Session["login"] == null)
            {
                return Redirect("~/");
            }
            if (id == null)
            {
                return Redirect("~/ex/barang/list");
            }
            db_adpEntities adp = new db_adpEntities();
            if (adp.barangs.Where(p => p.id_barang == id).ToList().Count < 1)
            {
                TempData.Add("message", "barang tidak tersedia !");
                TempData.Add("type", "warning");
                return Redirect("~/ex/barang/list");
            }

            barang barang = adp.barangs.ToList().Find(e => e.id_barang == id);
            return View("Barang/Edit", barang);
        }

        [HttpPost]
        [Route("ex/barang/edit")]
        public ActionResult EditBarang(barang b = null)
        {
            if (Session["login"] == null)
            {
                return Redirect("~/");
            }
            db_adpEntities adp = new db_adpEntities();
            if (adp.barangs.Where(p => p.id_barang == b.id_barang).ToList().Count < 1)
            {
                TempData.Add("message", "barang tidak tersedia !");
                TempData.Add("type", "warning");
                return Redirect("~/ex/barang/list");
            }
            
            barang barang = adp.barangs.ToList().Find(p => p.id_barang == b.id_barang);
            barang.nama_barang = b.nama_barang;
            barang.harga_barang = b.harga_barang;
            barang.stock_barang = b.stock_barang;
            adp.SaveChanges();
            TempData.Add("message", "Barang baru berhasil diedit");
            TempData.Add("type", "primary");
            return Redirect("~/ex/barang/list");
        }


        [Route("ex/barang/delete/{id}")]
        public ActionResult DeleteBarang(string id = "")
        {
            if (Session["login"] == null)
            {
                return Redirect("~/");
            }
            if (id == null || id == "")
            {
                return Redirect("~/ex/barang/add");
            }

            db_adpEntities adp = new db_adpEntities();

            if (adp.barangs.Where(p => p.id_barang == id).ToList().Count < 1)
            {
                TempData.Add("message", "barang tidak tersedia !");
                TempData.Add("type", "warning");
                return Redirect("~/ex/barang/list");
            }

            else
            {
                barang b = adp.barangs.ToList().Find(p => p.id_barang == id);
                adp.barangs.Remove(b);
                adp.SaveChanges();
                TempData.Add("message", "barang dengan id "+id+" telah dihapus !");
                TempData.Add("type", "danger");
                return Redirect("~/ex/barang/list");
            }
        }


    }
}