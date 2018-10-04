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
    [RoutePrefix("api/in-mems")]
    public class InMemsController : ApiController
    {
        // POST api/values
        [HttpPost]
        [Route]
        public async Task<IHttpActionResult> Post()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = new MultipartMemoryStreamProvider();

            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);
                string result = string.Empty;

                // This illustrates how to get the file names.
                foreach (StreamContent item in provider.Contents)
                {
                    string name = item.Headers.ContentDisposition.Name;
                    name = name.Substring(1, name.Length - 2);
                    result += $"{name}: {await item.ReadAsStringAsync()}, ";
                }
                return Ok(result);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

    }
}
