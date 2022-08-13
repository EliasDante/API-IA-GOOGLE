using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API_IA_GOOGLE.Data;
using System.Data;

namespace API_IA_GOOGLE.Controllers
{
    [Authorize]
    public class DTO_Response
    {
        public int Status { get; set; }
        public JObject Data { get; set; }
        public string Message { get; set; }
        public string IdTransaccion { get; set; }

    }

    public class Fcpayload
    {
        public Cfacebook facebook { get; set; }
    }

    public class Cfacebook
    {
        public string text { get; set; }
    }
    public class IAGOOGLEAPIController : ApiController
    {

        [HttpPost]
        public HttpResponseMessage Post([FromBody] JObject json)
        {
            try
            {
                var log = json.ToString();

                var csql = new cSQL();
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                string sql = "";


                sql = "SP_Insert_DATA ";
                sql += "  @Json  = " + "'" + log + "'";
                ds = csql.EjecutarSQL_DS(sql);

                var resp = new Fcpayload
                {
                    //Status = 1,
                    //Data = new { nuevoPlan.Id }, 
                    facebook = new Cfacebook { text = ds.Tables[0].Rows[0][0].ToString() }
            };

                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message.ToString() + " - " + ex.StackTrace.ToString());
                throw;
            }


        }

    }
}

