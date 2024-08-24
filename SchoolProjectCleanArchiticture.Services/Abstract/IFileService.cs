using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Services.Abstract
{
    public interface IFileService
    {
        public Task<string>UploadImage(string Location,IFormFile File);
    }
}
