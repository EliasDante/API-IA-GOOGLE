using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Newtonsoft.Json;
using System.Net;
using System.Data;

namespace API_IA_GOOGLE.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        // GET: Default/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Default/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Default/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Default/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Default/Edit/5
        [HttpPost]
        public ActionResult Receive()
        {
            Request.InputStream.Position = 0;
            Request.InputStream.Seek(0, SeekOrigin.Begin);
            using (var reader = new StreamReader(Request.InputStream))
            {
                var json = reader.ReadToEnd();

                var csql = new cSQL();
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                string sql = "";


                sql = "SP_Insert_DATA ";
                sql += "  @Json  = " + "'" + json + "'";
                ds = csql.EjecutarSQL_DS(sql);



                var body = JsonConvert.DeserializeObject<dynamic>(json);
                try
                {
                    switch ((string)body.eventName)
                    {
                        case "order.completed":
                            // This is an order:completed event
                            // do what needs to be done here.
                            break;
                    }
                    // Return a valid status code such as 200 OK.
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch
                {
                    // When something goes wrong, return an invalid status code
                    // such as 400 BadRequest.
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
        }

        // GET: Default/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Default/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
