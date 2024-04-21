using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebCar.Models;

namespace WebCar.DbContext
{
    public class myDbContext : IdentityDbContext<ApplicationUser>
    {
        public myDbContext(DbContextOptions<myDbContext> options) : base(options)
        {
        }
    }
}

