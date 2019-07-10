using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _3.Models;

namespace _3.Controllers
{
    public class LatihanDuaController : Controller
    {
        [Route(@"latihandua/add")]
        public ActionResult AddProduct()
        {
            if(TempData.Peek("ListProduct") == null)
            {
                List<Product> ListProduct = new List<Product>();
                TempData.Add("ListProduct", ListProduct);
            }
            return View("Add");
        }

        [HttpPost]
        [Route(@"latihandua/add")]
        public ActionResult AddProduct(Product product)
        {
            if (TempData.Peek("ListProduct") == null)
            {
                return Redirect("~/latihandua/add");
            }
            List<Product> ListProduct = (List<Product>)TempData.Peek("ListProduct");
            ListProduct.Add(product);
            return Redirect("~/latihandua/view");
        }

        [Route(@"latihandua/view")]
        public ActionResult ViewProduct()
        {
            if (TempData.Peek("ListProduct") == null)
            {
                return Redirect("~/latihandua/add");
            }
            List<Product> ListProduct = (List<Product>)TempData.Peek("ListProduct");
            return View("View",ListProduct);
        }

        [Route(@"latihandua/delete/{id}")]
        public ActionResult DeleteProduct(string id = "")
        {
            if(id == "")
            {
                return Redirect("~/latihandua/view");
            }
            if (TempData.Peek("ListProduct") == null)
            {
                return Redirect("~/latihandua/view");
            }
            List<Product> ListProduct = (List<Product>)TempData.Peek("ListProduct");
            ListProduct.RemoveAll(p => p.IdProduct.Equals(id));
            return Redirect("~/latihandua/view");
        }

        [HttpGet]
        [Route(@"latihandua/update/{id}")]
        public ActionResult UpdateProduct(string id = "")
        {
            if (id == "")
            {
                return Redirect("~/latihandua/view");
            }
            if (TempData.Peek("ListProduct") == null)
            {
                return Redirect("~/latihandua/view");
            }
            List<Product> ListProduct = (List<Product>)TempData.Peek("ListProduct");
            if(ListProduct.FindAll(p => p.IdProduct.Equals(id)).Count < 1)
            {
                return Redirect("~/latihandua/view");
            }
            Product product = ListProduct.Find(p => p.IdProduct.Equals(id));
            return View("Update",product);
        }

        [HttpPost]
        [Route(@"latihandua/update")]
        public ActionResult UpdateProduct(Product product)
        {
            if (TempData.Peek("ListProduct") == null)
            {
                return Redirect("~/latihandua/view");
            }

            List<Product> ListProduct = (List<Product>)TempData.Peek("ListProduct");
            int i = ListProduct.FindIndex(p => p.IdProduct.Equals(product.IdProduct));

            if(i < 0)
            {
                return Redirect("~/latihandua/view");

            }
            ListProduct[i] = product;
            return Redirect("~/latihandua/view");
        }
    }
}