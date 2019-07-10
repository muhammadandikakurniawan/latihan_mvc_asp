using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _2.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public static List<Person> ListPerson = new List<Person>();
    }
}