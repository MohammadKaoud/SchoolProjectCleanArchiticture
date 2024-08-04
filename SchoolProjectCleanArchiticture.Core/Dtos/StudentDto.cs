using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Dtos
{
    public  class StudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string DepartmentName { get; set; }
        public StudentDto(int id,string name,string address,string departmentName)
        {
            Id= id;
            Name= name;
            Address= address;
            DepartmentName= departmentName;
        }
        public StudentDto()
        {
            
        }
    }
}
