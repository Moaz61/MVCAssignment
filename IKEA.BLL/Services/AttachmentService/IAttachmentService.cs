using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace IKEA.BLL.Services.AttachmentService
{
    public interface IAttachmentService
    {
        //Upload
        public string? Upload(IFormFile file , string FolderName);

        //Delete
        bool Delete(string filePath);
    }
}
