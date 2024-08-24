using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MimeKit.Cryptography;
using SchoolProjectCleanArchiticture.Data.Entites;
using SchoolProjectCleanArchiticture.Infrastructure.Repos;
using SchoolProjectCleanArchiticture.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Services.Implementation
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepo _repo;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _contextAccessor;
        public TeacherService(ITeacherRepo repo,IFileService fileService,IHttpContextAccessor httpContextAccessor )
        {
            this._repo = repo;
            this._fileService = fileService;
            _contextAccessor = httpContextAccessor;
        }
        public async  Task<string>  AddTeacherAsync(Teacher teacher,IFormFile Image)
        {
            if (Image != null)
            {
                var resultofUploading = await _fileService.UploadImage("Teachers", Image);
                var request = _contextAccessor.HttpContext.Request;
                var baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";
                teacher.ImageUrl = $"{baseUrl}/{resultofUploading}";
                var result=await _repo.AddAsync(teacher);
                if (result != null)
                {
                    return "Success";
                }
                return "NotSuccess";

            }
            return "ImageNotFound";
           
      
           
            
               
        }

        
    }
}
