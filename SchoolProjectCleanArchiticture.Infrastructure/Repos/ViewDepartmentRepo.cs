using Microsoft.EntityFrameworkCore;
using SchoolProjectCleanArchiticture.Data;
using SchoolProjectCleanArchiticture.Data.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Infrastructure.Repos
{
    public  class ViewDepartmentRepo:GenericRepo<DepartmentView>,IViewRepository<DepartmentView>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly DbSet<DepartmentView> departmentViewSet;
        public ViewDepartmentRepo(ApplicationDbContext applicationDbContext):base(applicationDbContext) 
        {
            _applicationDbContext = applicationDbContext;
            departmentViewSet = applicationDbContext.DepartmentView;
        }
    }
}
