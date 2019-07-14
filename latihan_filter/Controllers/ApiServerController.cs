using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using latihan_filter.Models;
using System.Data;

namespace latihan_filter.Controllers
{
    public class ApiServerController : ApiController
    {
        //[HttpGet]
        [Route("apiserver/barang")]
        public List<Barang> GetBarang(string id = null)
        {
            Barang barang = new Barang();
            List<Barang> data = barang.GetData();
            if (id != null)
            {
                return data.FindAll(v => v.id.ToString() == id);
            }
            return data;
        }

        //[HttpPost]
        [Route("apiserver/barang")]
        public Dictionary<string,string> PostBarang(Barang b)
        {
            Dictionary<string, string> response = new Dictionary<string, string>() { {"status","success"} };
            Barang barang = new Barang();
            try
            {
                response["status"] = b == null ? "Not Data Sended" : response["status"];

                barang.PostData(b);
            }
            catch (Exception)
            {
                response["status"] = "Internal Server Error";
            }

            return response;
        }


        //[HttpPut]
        [Route("apiserver/barang")]
        public Dictionary<string, string> PutBarang(Barang b)
        {
            Dictionary<string, string> response = new Dictionary<string, string>() { { "status", "success" } };
            Barang barang = new Barang();
            try
            {
                response["status"] = b == null ? "Not Data Sended" : response["status"];

                barang.PutData(b);
            }
            catch (Exception)
            {
                response["status"] = "Internal Server Error";
            }

            return response;
        }

        //[HttpDelete]
        [Route("apiserver/barang")]
        public Dictionary<string, string> DeleteBarang(Barang b)
        {
            Dictionary<string, string> response = new Dictionary<string, string>() { { "status", "success" } };
            Barang barang = new Barang();
            try
            {
                response["status"] = b == null ? "Not Data Sended" : response["status"];

                barang.DeleteData(b);
            }
            catch (Exception)
            {
                response["status"] = "Internal Server Error";
            }

            return response;
        }

    }
}
