using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolProjectCleanArchiticture.Data.Entites;
using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SchoolProjectCleanArchiticture.Data.Entites.Identity;
using Microsoft.EntityFrameworkCore.Internal;
namespace SchoolProjectCleanArchiticture.Data
{
    public class ApplicationDbContext : IdentityDbContext<SUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions) 
        {
            
        }
        public DbSet<Student> students { get; set; }
        public DbSet<Subject>  subjects { get; set; }

        public DbSet<Department> departments { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<SUser>Users {  get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            
            
         
            modelBuilder.Entity<Department>().HasData(
                new List<Department>()
                {
                    new Department()
                    {
                        DepartmentId = 1,
                        DepartmentName="InformaticEng"

                    },
                    new Department() {
                        DepartmentId = 2,
                        DepartmentName="Dentistry"
                    }
                }

                );
            modelBuilder.Entity<Student>().HasData(
                new List<Student>() {
                    new Student()
                    {
                        Id=1,
                        NameEn="Mohammad",
                        NameAr="محمد",
                        Address="Mazzeh",
                        Phone="21215454",
                        DepartmentId=2,
                        
                    },
                    new Student()
                    {
                        Id=2,
                       
                        NameEn="Omar",
                        NameAr="عمر",
                        Address="Mazzeh",
                        Phone="21211111",
                        DepartmentId=1,

                    }
                }
                );

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
