using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Api.Models;

namespace Api.Controllers
{
    public class TryController : ApiController
    {
        //[HttpGet]
        //[Route("try/data")]
        //public List<Dictionary<string, object>> Data()
        //{
        //    List<Dictionary<string,object>> data = new List<Dictionary<string, object>>
        //    {
        //        {
        //            new Dictionary<string, object>()
        //            {
        //                {"name","dika"},{"vocation","tkr"},{"id",1}
        //            }
        //        },
        //        {
        //            new Dictionary<string, object>()
        //            {
        //                {"name","dimas"},{"vocation","tkj"},{"id",2}
        //            }
        //        },
        //        {
        //            new Dictionary<string, object>()
        //            {
        //                {"name","anggi"},{"vocation","tkbb"},{"id",3}
        //            }
        //        }
        //    };
        //    return data;
        //}

        //[HttpGet]
        //[Route("try/data")]
        //public List<Dictionary<string, object>> Data(int id)
        //{
        //    List<Dictionary<string, object>> data = new List<Dictionary<string, object>>
        //    {
        //        {
        //            new Dictionary<string, object>()
        //            {
        //                {"name","dika"},{"vocation","tkr"},{"id",1}
        //            }
        //        },
        //        {
        //            new Dictionary<string, object>()
        //            {
        //                {"name","dimas"},{"vocation","tkj"},{"id",2}
        //            }
        //        },
        //        {
        //            new Dictionary<string, object>()
        //            {
        //                {"name","anggi"},{"vocation","tkbb"},{"id",3}
        //            }
        //        }
        //    };
        //    return data.FindAll(d => (int)d["id"] == id);
        //}

        [HttpGet]
        [Route("try/barang")]
        public List<barang> Barang()
        {
            ADP_Entities adp = new ADP_Entities();
            List<barang> ListBarang = adp.barangs.ToList();
            return ListBarang;
        }

        [HttpGet]
        [Route("try/employee")]
        public List<COPY_EMP> Employee(string id = null)
        {
            HR_Entities hr = new HR_Entities();
            if(id == null || id == "")
            {
                return hr.COPY_EMP.ToList();
            }
            else
            {
                return hr.COPY_EMP.Where(e => e.EMPLOYEE_ID.ToString() == id).ToList();
            }
        }

        //[HttpGet]
        //[Route("try/employee")]
        //public List<COPY_EMP> Employee(string id)
        //{
        //    HR_Entities hr = new HR_Entities();
        //    List<COPY_EMP> ListEmp;
        //    ListEmp = hr.COPY_EMP.ToList().FindAll(e => e.EMPLOYEE_ID.ToString() == id);
        //    return ListEmp;
        //} 

        //[HttpPost]
        //[Route("try/employee")]
        //public List<Dictionary<string,object>> AddEmployee([FromBody]COPY_EMP emp)
        //{
        //    try
        //    {
        //        HR_Entities hr = new HR_Entities();
        //        hr.COPY_EMP.Add(emp);
        //        hr.COPY_EMP.Add(emp);
        //        hr.SaveChanges();
        //        return new List<Dictionary<string, object>>()
        //        {
        //            new Dictionary<string,object>()
        //            {
        //                {"response","success"}
        //            }
        //        };
        //    }
        //    catch(Exception e)
        //    {
        //        return new List<Dictionary<string, object>>()
        //        {
        //            new Dictionary<string,object>()
        //            {
        //                {"response","fail"}
        //            }
        //        };
        //    }
        //}

        [HttpPost]
        [Route("try/employee")]
        public IHttpActionResult AddEmployee([FromBody]COPY_EMP emp)
        {
            try
            {
                HR_Entities hr = new HR_Entities();
                hr.COPY_EMP.Add(emp);
                hr.SaveChanges();
                if (emp == null) return BadRequest(); 
            }
            catch (Exception e)
            {
                return InternalServerError();
            }
            return Ok();
        }

        [HttpPut]
        [Route("try/employee")]
        public IHttpActionResult EditEmployee(COPY_EMP emp)
        {
            try
            {
                if(emp == null)
                {
                    return BadRequest();
                }
                HR_Entities hr = new HR_Entities();
                COPY_EMP employee = hr.COPY_EMP.ToList().Find(e => e.EMPLOYEE_ID == emp.EMPLOYEE_ID);
                employee.FIRST_NAME = emp.FIRST_NAME;
                employee.LAST_NAME = emp.LAST_NAME;
                employee.EMAIL = emp.EMAIL;
                hr.SaveChanges();
            }
            catch(Exception )
            {
                return InternalServerError();
            }
            return Ok();
        }

        [HttpDelete]
        [Route("try/employee")]
        public IHttpActionResult DeleteEmployee(COPY_EMP emp)
        {
            try
            {
                if (emp == null)
                {
                    return BadRequest();
                }
                HR_Entities hr = new HR_Entities();
                COPY_EMP employee = hr.COPY_EMP.ToList().Find(e => e.EMPLOYEE_ID == emp.EMPLOYEE_ID);
                hr.COPY_EMP.Remove(employee);
                hr.SaveChanges();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
            return Ok();
        }



















    }
}
