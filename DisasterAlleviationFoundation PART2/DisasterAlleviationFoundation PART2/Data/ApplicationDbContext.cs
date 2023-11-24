using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DisasterAlleviationFoundation.Models;

namespace DisasterAlleviationFoundation_PART2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<DisasterAlleviationFoundation.Models.AdminUserModel> AdminUserModel { get; set; } = default!;
        public DbSet<DisasterAlleviationFoundation.Models.UserProfileModel> UserProfileModel { get; set; } = default!;
        public DbSet<DisasterAlleviationFoundation.Models.GoodsDonationModel> GoodsDonationModel { get; set; } = default!;
        public DbSet<DisasterAlleviationFoundation.Models.MonetaryDonationModel> MonetaryDonationModel { get; set; } = default!;
        public DbSet<DisasterAlleviationFoundation.Models.DisasterModel> DisasterModel { get; set; } = default!;
    }
}