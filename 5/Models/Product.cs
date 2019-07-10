using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace _5.Models
{
    public class Product
    {
        [Required(ErrorMessage = "Product Id Must Be filled")]
        [StringLength(maximumLength:3,ErrorMessage = "Maximum 3 character")]
        public string ProductId { set; get; }

        [Required(ErrorMessage = "Product Name Must Be filled")]
        [StringLength(maximumLength: 10, MinimumLength = 1)]
        public string ProductName { set; get; }

        [Required(ErrorMessage = "ProductPrice Must Be filled")]
        [RegularExpression("[0-9]+",ErrorMessage = "only number")]
        public double ProductPrice { set; get; }

        [Required(ErrorMessage = "ProductPrice Must Be filled")]
        [RegularExpression("[1-9]+", ErrorMessage = "only number")]
        public double ProductStock { set; get; }
    }
}