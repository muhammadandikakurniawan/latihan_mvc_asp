using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Api.Models;

namespace Api.DTO
{
    public class EmployeeDTO
    {
        [Required]
        public decimal id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }

        //public string PHONE_NUMBER { get; set; }
        //public Nullable<System.DateTime> HIRE_DATE { get; set; }
        //public string JOB_ID { get; set; }
        //public Nullable<decimal> SALARY { get; set; }
        //public Nullable<decimal> COMMISSION_PCT { get; set; }
        //public Nullable<decimal> MANAGER_ID { get; set; }
        //public Nullable<decimal> DEPARTMENT_ID { get; set; }
    }
}