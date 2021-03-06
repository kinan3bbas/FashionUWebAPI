﻿using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using NewAPIProject.Extras;
using System.ComponentModel.DataAnnotations;
using System;
using System.Data.Entity;

namespace NewAPIProject.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Creation Date")]
        public DateTime CreationDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Last Modification Date")]
        public DateTime LastModificationDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Sex")]
        public string Sex { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Language")]
        public string Language { get; set; }


        [Display(Name = "Currency")]
        public string Currency { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Company> Companys { get; set; }

        public DbSet<ShippingRequest> ShippingRequests { get; set; }
        public DbSet<UsersDeviceTokens> UsersDeviceTokens { get; set; }
    }
}