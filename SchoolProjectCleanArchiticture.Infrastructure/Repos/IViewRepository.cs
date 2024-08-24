using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Infrastructure.Repos
{
    public  interface IViewRepository<T>:IGernericRepos<T> where T : class
    {
        
    }
}
