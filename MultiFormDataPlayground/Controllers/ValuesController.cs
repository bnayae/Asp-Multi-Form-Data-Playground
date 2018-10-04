using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using MultiFormDataPlayground.Models;

namespace MultiFormDataPlayground.Controllers
{
    [RoutePrefix("api/values")]
    public class ValuesController : ApiController
    {
        // GET api/values
        [Route]
        public async Task<DTO[]> Get()
        {
            return new DTO[0];
        }

        // GET api/values/5
        [Route("{name}")]
        public Task<DTO> Get(string name)
        {
            throw new NotImplementedException();
        }
        [Route("b")]
        [HttpPost]
        public async Task<IHttpActionResult> PostX(
            //[FromBody]string name,
            [FromBody]params HttpPostedFileBase[] commands)
        {
            return Ok();
        }

        // POST api/values
        [HttpPost]
        [Route]
        public async Task<HttpResponseMessage> Post()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                // This illustrates how to get the file names.
                foreach (MultipartFileData file in provider.FileData)
                {
                    Trace.WriteLine(file.Headers.ContentDisposition.FileName);
                    Trace.WriteLine("Server file path: " + file.LocalFileName);
                }
                var name = provider.FormData["name"];
                var date = provider.FormData["date"];
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

    }
}
