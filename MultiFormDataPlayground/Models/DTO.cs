
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MultiFormDataPlayground.Models
{
    public class DTO
    {
        public string Name { get; set; }
        public HttpPostedFileBase Data { get; set; }
    }
}