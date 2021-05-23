using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Api.DataIdentity
{
    public class DataContextIdentity : IdentityDbContext
    {
        public DataContextIdentity(DbContextOptions<DataContextIdentity> options):base(options)
        {
        }
    }
}
