using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using localinezationBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace localinezationBackend.Services.Context
{
    public class DataContext : DbContext
    {
        public DbSet<UserModel> UserInfo { get;set; }
       

        public DbSet<MediaItemModel> MediaInfo { get;set; }
        public DbSet<TranslationRequestModel> TranslationRequests { get;set; }
        public DbSet<TranslationModel> Translations{ get;set; }
        

        public DataContext(DbContextOptions<DataContext> options): base(options){}
        

             //this function will build out our table in teh databasee
       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define relationships and keys
            modelBuilder.Entity<TranslationRequestModel>()
                .HasOne(tr => tr.Media)
                .WithMany(m => m.TranslationRequests)
                .HasForeignKey(tr => tr.MediaId);
                
            modelBuilder.Entity<TranslationModel>()
                .HasOne(t => t.TranslationRequest)
                .WithMany(tr => tr.Translations)
                .HasForeignKey(t => t.TranslationRequestId);
        }

    }
}