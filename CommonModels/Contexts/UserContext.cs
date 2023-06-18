using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonModelsLib.Contexts
{
    public class UserContext : DbContext
    {
        public DbSet<UserBase?> userBases { get; set; }
        public UserContext(DbContextOptions<UserContext> opts) : base(opts)
        {

        }

        public static DbContextOptions<UserContext> options { get; set; }
        public static UserContext Obj => new UserContext(options);

        public static async Task<UserBase?> GetUserBaseByUserName(string userName)
        {
            return await Obj.userBases.Where(a => a.Username == userName).FirstOrDefaultAsync();
        }
        public static async Task<UserBase?> GetUserBaseByEmail(string Email)
        {
            return await Obj.userBases.Where(a => a.Email == Email).FirstOrDefaultAsync();
        }
    }
}
