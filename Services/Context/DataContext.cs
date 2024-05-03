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
        public DbSet<BlogItemModel> BlogInfo { get;set; }

        public DbSet<Media> Medias { get;set; }
        public DbSet<TranslationRequest> TranslationRequests { get;set; }
        public DbSet<Translation> Translations{ get;set; }
        

        public DataContext(DbContextOptions options): base(options){}
        

             //this function will build out our table in teh databasee
       protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

    }
}