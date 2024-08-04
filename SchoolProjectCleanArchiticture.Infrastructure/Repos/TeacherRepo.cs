using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolProjectCleanArchiticture.Data;
using SchoolProjectCleanArchiticture.Data.Entites;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Infrastructure.Repos
{
    public class TeacherRepo : GenericRepo<Teacher>, ITeacherRepo
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly DbSet<Teacher> _teachers;
        public TeacherRepo(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _teachers=applicationDbContext.Teachers;
        }

    }
}

