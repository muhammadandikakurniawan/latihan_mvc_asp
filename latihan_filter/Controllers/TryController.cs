﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using latihan_filter.Classes;
using System.Data;
using latihan_filter.Models;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.RegularExpressions;
using Vse.Web.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;

namespace latihan_filter.Controllers
{
    public class TryController : Controller
    {
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("apiclient/get")]
        public ActionResult Client()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:55814");

            var get = client.GetAsync("apiserver/barang");
            get.Wait();

            var result = get.Result;

            List<Barang> ListBarang = new List<Barang>();

            if (result.IsSuccessStatusCode)
            {
                var read = result.Content.ReadAsStringAsync();
                read.Wait();
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<Barang>));
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(read.Result));
                ListBarang = (List<Barang>)ser.ReadObject(ms);
                return View("GetData", ListBarang);
            }
            return View("GetData", ListBarang);
        }

        //[Route("apiclient/post")]
        //public ActionResult PostClient(Barang b)
        //{
        //    if(b != null)
        //    {
        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri("http://localhost:55814");

        //        var get = client.PutAsJsonAsync("apiserver/barang", b);
        //        get.Wait();

        //        var result = get.Result;

        //        if (result.IsSuccessStatusCode)
        //        {
        //            var read = result.Content.ReadAsStreamAsync();

        //        }
        //    }
        //    else
        //    {
        //        return View("Post");
        //    }
        //}
    }
}