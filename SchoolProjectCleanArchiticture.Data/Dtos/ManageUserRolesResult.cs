using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Data.Dtos
{
    public  class ManageUserRolesResult
    {
        public string Id { get; set; }
        public List<UserRole> UserRoles { get; set; }
        
    }
    public class UserRole
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool hasRole { get; set; }
    }
}
