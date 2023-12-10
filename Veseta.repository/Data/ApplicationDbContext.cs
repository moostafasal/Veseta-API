using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Veseta.Core.entites;

namespace Veseta.repository.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().HasData
                (
                    new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                    new IdentityRole { Id = "2", Name = "Doctor", NormalizedName = "DOCTOR" },
                    new IdentityRole { Id = "3", Name = "Patient", NormalizedName = "PATIENT" }
                );
            builder.Entity<Specialization>().HasData
                (
                    new Specialization { Id = 1, SpecializationName = "Pediatrics" },
                    new Specialization { Id = 2, SpecializationName = "Ophthalmology" },
                    new Specialization { Id = 3, SpecializationName = "Cardiology" },
                    new Specialization { Id = 4, SpecializationName = "Endocrinology" },
                    new Specialization { Id = 5, SpecializationName = "Nephrology" }
                );
            builder.Entity<Booking>()
         .HasOne(b => b.Patient)
         .WithMany(p => p.Requests)
         .HasForeignKey(b => b.PatientId)
         .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Booking>()
                .HasOne(b => b.Doctor)
                .WithMany()
                .HasForeignKey(b => b.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Booking>()
                .HasOne(b => b.TimeSlot)
                .WithMany()
                .HasForeignKey(b => b.TimeSlotId)
                .OnDelete(DeleteBehavior.NoAction);
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Booking> Bookings { get; set; }


        private static async Task SeedAdminUser(UserManager<ApplicationUser> userManager)
        {
            var adminUser = new ApplicationUser
            {
                UserName = "admin@example.com",
                Email = "admin@example.com",
            };

            var user = await userManager.FindByEmailAsync(adminUser.Email);

            if (user == null)
            {
                var createAdminUser = await userManager.CreateAsync(adminUser, "Admin@123");

                if (createAdminUser.Succeeded)
                {
                    // Assign the "Admin" role to the admin user
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }


    }


}
