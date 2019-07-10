using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3.Models
{
    public class Product
    {
        public string IdProduct     { get; set; }
        public string NameProduct   { get; set; }
        public double PriceProduct  { get; set; }
        public int    QtyProduct    { get; set; }
    }
}