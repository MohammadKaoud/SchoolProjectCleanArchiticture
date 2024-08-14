using Microsoft.EntityFrameworkCore;
using SchoolProjectCleanArchiticture.Data;
using SchoolProjectCleanArchiticture.Data.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Infrastructure.Repos
{
    public  class RefreshTokenRepo:GenericRepo<UserRefreshToken>, IRefreshTokenRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<UserRefreshToken> _users;
        public RefreshTokenRepo(ApplicationDbContext context):base(context) 
        {
            _context = context;

            _users=context.UsersRefreshTokens;
        }

    }
}
