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
        public DbSet<UserBase> userBases { get; set; }
        public UserContext(DbContextOptions<UserContext> opts) : base(opts)
        {

        }

    }
}
