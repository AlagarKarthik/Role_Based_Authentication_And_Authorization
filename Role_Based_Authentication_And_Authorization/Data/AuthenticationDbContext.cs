
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Role_Based_Authentication_And_Authorization.Data
{
    public class AuthenticationDbContext : IdentityDbContext
    {

        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<User>().ToTable("users");

            base.OnModelCreating(builder);
            var adminRoleId = "eabc23a4-d6b8-44e1-9271-a3380e41b9f8";    // admin
            var managerRoleId = "d69c708b-f99e-4814-8545-9bd5b4f90efe";  // manager
            var reviewerRoleId = "5a6af809-7af5-410b-9236-3700399220bc"; // reviewer

            //Create Admin, Manager, Reviewer Roles

            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper(),
                    ConcurrencyStamp = adminRoleId
                },
                    new IdentityRole()
                {
                    Id = managerRoleId,
                    Name = "Manager",
                    NormalizedName = "Manager".ToUpper(),
                    ConcurrencyStamp = managerRoleId
                },
                    new IdentityRole()
                    {
                     Id = reviewerRoleId,
                     Name = "Reviewer",
                     NormalizedName = "Reviewer".ToUpper(),
                     ConcurrencyStamp = reviewerRoleId
                    }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            //Create Admin User
            var adminUserId = "0e669302-302f-4682-8008-b963f61e2aed";
            var managerUserId = "d403d43c-7bcb-4b36-92a5-ae9d66958673";
            var reviewerUserId = "62f880ed-ee89-4575-adbe-b8fe6425f793";
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "Admin",
            };

            var manager = new IdentityUser()
            {
                Id = managerUserId,
                UserName = "Manager",
            };

            var reviewer = new IdentityUser()
            {
                Id = reviewerUserId,
                UserName = "Reviewer",
            };


            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Unlock@2313");
            builder.Entity<IdentityUser>().HasData(admin);

            //Role for Admin, Manager, Reviewer


            var adminRoles = new List<IdentityUserRole<string>>()
           {
                new ()
                {
                    UserId = adminUserId,
                    RoleId = adminRoleId,
                },
                 new ()
                {
                    UserId = adminUserId,
                    RoleId = managerRoleId,
                },
                 new ()
                 {
                     UserId = adminUserId,
                     RoleId = reviewerRoleId,
                 }

           };
            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }
    }
}
