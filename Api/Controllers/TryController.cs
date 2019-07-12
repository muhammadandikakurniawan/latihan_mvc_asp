using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Api.Models;
using Api.DTO;
using Api.Filters;
using System.Threading;

namespace Api.Controllers
{[AuthenticationFilter]
    public class TryController : ApiController
    {
        [HttpGet]
        [Route("getprincipal")]
        public string Principal()
        {
            return Thread.CurrentPrincipal.Identity.Name;
        }
        
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
        public List<EmployeeDTO> Employee(string id = null)
        {
            
            HR_Entities hr = new HR_Entities();

            //bentuk 1
            List<EmployeeDTO> ListEmployeeDTO = hr.COPY_EMP.Select(e =>
                   new EmployeeDTO()
                   {
                       id = e.EMPLOYEE_ID,
                       firstname = e.FIRST_NAME,
                       lastname = e.LAST_NAME,
                       email = e.EMAIL
                   }
                ).ToList();

            //bentuk 3
            //List <COPY_EMP> ListCopyEmployee = hr.COPY_EMP.ToList();
            //List<EmployeeDTO> ListEmployee = new List<EmployeeDTO>();
            //foreach (COPY_EMP e in ListCopyEmployee)
            //{
            //    EmployeeDTO employee = new EmployeeDTO();
            //    employee.EMPLOYEE_ID = e.EMPLOYEE_ID;
            //    employee.FIRST_NAME = e.FIRST_NAME;
            //    employee.LAST_NAME = e.LAST_NAME;
            //    ListEmployee.Add(employee);
            //}
            if (id == null || id == "")
            {
                return ListEmployeeDTO;
            }
            else
            {
                return ListEmployeeDTO.FindAll(e => e.id.ToString() == id);
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
        public IHttpActionResult AddEmployee([FromBody]EmployeeDTO emp)
        {
            try
            {
                if (!ActionContext.ModelState.IsValid)
                {
                    return BadRequest("Request is ugly");
                }
                HR_Entities hr = new HR_Entities();
                COPY_EMP copy_emp = new COPY_EMP()
                {
                    EMPLOYEE_ID = emp.id,
                    FIRST_NAME = emp.firstname,
                    LAST_NAME = emp.lastname,
                    EMAIL = emp.email
                };
                hr.COPY_EMP.Add(copy_emp);
                hr.SaveChanges();
                if (emp == null) return BadRequest(); 
            }
            catch (Exception e)
            {
                return InternalServerError();
            }
            return Ok();
        }

        //public IHttpActionResult AddEmployee([FromBody]COPY_EMP emp)
        //{
        //    try
        //    {
        //        HR_Entities hr = new HR_Entities();
        //        hr.COPY_EMP.Add(emp);
        //        hr.SaveChanges();
        //        if (emp == null) return BadRequest();
        //    }
        //    catch (Exception e)
        //    {
        //        return InternalServerError();
        //    }
        //    return Ok();
        //}

        [HttpPut]
        [Route("try/employee")]
        public IHttpActionResult EditEmployee(EmployeeDTO emp)
        {
            try
            {
                if(emp == null)
                {
                    return BadRequest();
                }
                HR_Entities hr = new HR_Entities();
               COPY_EMP employee = hr.COPY_EMP.ToList().Find(e => e.EMPLOYEE_ID == emp.id);
                employee.EMPLOYEE_ID = emp.id;
                employee.FIRST_NAME = emp.firstname;
                employee.LAST_NAME = emp.lastname;
                employee.EMAIL = emp.email;
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
        public IHttpActionResult DeleteEmployee(EmployeeDTO emp)
        {
            try
            {
                if (emp == null)
                {
                    return BadRequest();
                }
                HR_Entities hr = new HR_Entities();
                COPY_EMP employee = hr.COPY_EMP.ToList().Find(e => e.EMPLOYEE_ID == emp.id);
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
