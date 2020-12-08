using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recursiva.Fan.WebApi.Models
{
    public class FileUpload
    {
        public IFormFile File { get; set; }
    }
}
