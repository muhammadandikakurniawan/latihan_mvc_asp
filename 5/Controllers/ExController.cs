using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _5.Models;

namespace _5.Controllers
{
    public class ExController : Controller
    {
       [Route("ex/product/add")]
       [HttpGet]
       public ActionResult Add()
        {
            if (Session["LisProduct"] == null)
            {
                    List<Product> ListProduct = new List<Product>();
                    Session.Add("LisProduct", ListProduct);
            }

            return View("Product/Add");
            
        }

        [Route("ex/product/add")]
        [HttpPost]
        public ActionResult Add(Product product)
        {

            if (ModelState.IsValid)
            {
                if (Session["LisProduct"] == null)
                {
                    return Redirect("~/ex/product/add");
                }
                List<Product> ListProduct = (List<Product>)Session["LisProduct"];
                if (ListProduct.Contains(ListProduct.Find(p => p.ProductId.ToUpper().Equals(product.ProductId.ToUpper()))))
                {
                    return Redirect("~/ex/product/list");
                }
                else
                {
                    List<Product> NListProduct = (List<Product>)Session["LisProduct"];
                    NListProduct.Add(product);

                    TempData.Add("message", "New product added Succesfully");
                    TempData.Add("type", "success");

                    return Redirect("~/ex/product/list");
                }

            }

            return View("Product/Add");

        }

        [HttpGet]
        [Route("ex/product/update/{id?}")]
        public ActionResult Update (string id = null)
        {
            if(id == null)
            {
                return Redirect("~/ex/product/list");
            }
            if (Session["LisProduct"] == null)
            {
                return Redirect("~/ex/product/list");
            }

            else
            {
                List<Product> ListProduct = (List<Product>)Session["LisProduct"];
                Product product = ListProduct.Find(p => p.ProductId.Equals(id));
                return View("Product/Update", product);
            }
        }

        [HttpPost]
        [Route("ex/product/update/{id?}")]
        public ActionResult Update(Product product)
        {
            if (Session["LisProduct"] == null)
            {
                return Redirect("~/ex/product/list");
            }
            List<Product> ListProduct = (List<Product>)Session["LisProduct"];
            if (ModelState.IsValid)
            {
                ListProduct[ListProduct.FindIndex(i => i.ProductId.Equals(product.ProductId))] = product;
                TempData.Add("message", "Product by id "+product.ProductId+" has been edited successfully");
                TempData.Add("type", "primary");
                return Redirect("~/ex/product/list");
            }
            
            Product p = ListProduct.Find(i => i.ProductId.Equals(product.ProductId));
            return View("Product/Update", p);

        }

        [Route("ex/product/delete/{id?}")]
        public ActionResult Delete(string id = null )
        {
            if(Session["LisProduct"] == null)
            {
                TempData.Add("message", "Product by id " + id + " failed to deleted");
                TempData.Add("type", "danger");
                return Redirect("~/ex/product/list");
            }
            List<Product> ListProduct = (List<Product>)Session["LisProduct"];
            if(id == null)
            {

                TempData.Add("message", "Product by id " + id + " failed to deleted id null");
                TempData.Add("type", "danger");
                return Redirect("~/ex/product/list");

            }
            if (ListProduct.Contains(ListProduct.Find(p => p.ProductId.Equals(id))) == false)
            {
                TempData.Add("message", "Product by id " + id + " failed to deleted contain");
                TempData.Add("type", "danger");
                return Redirect("~/ex/product/list");
            }
            ListProduct.RemoveAll(p => p.ProductId.Equals(id));
            TempData.Add("message", "Product by id " + id + " has been deleted successfully");
            TempData.Add("type", "danger");
            return Redirect("~/ex/product/list");
        }

        [Route("ex/product/list")]
        public ActionResult ProductList()
        {
            List<Product> ListProduct = (List<Product>)Session["LisProduct"];
            return View("Product/List",ListProduct);
        }
        
    }
}