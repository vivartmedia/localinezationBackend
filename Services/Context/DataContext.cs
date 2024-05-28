// Import required models and Entity Framework Core namespace for ORM capabilities
using localinezationBackend.Models;
using Microsoft.EntityFrameworkCore;

// Define the namespace that the DataContext resides within
namespace localinezationBackend.Services.Context
{
    // Inherits from DbContext, a base class provided by Entity Framework Core to work with database using models
    public class DataContext : DbContext
    {
        // Define DbSets to work with the database tables, each DbSet represents a table in the database
        public DbSet<UserModel> UserInfo { get; set; } // Table for User information
        public DbSet<MediaItemModel> MediaInfo { get; set; } // Table for Media Items
        public DbSet<TranslationRequestModel> TranslationRequests { get; set; } // Table for Translation Requests
        public DbSet<TranslationModel> Translations { get; set; } // Table for Translations

        // Constructor that takes DbContextOptions, used to configure the database (such as specifying the database provider, connection string, etc.)
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        // Override the OnModelCreating method to configure model properties and relationships
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Call the base method

            // Configure the relationship between TranslationRequestModel and MediaItemModel
            modelBuilder.Entity<TranslationRequestModel>()
                .HasOne(tr => tr.Media) // Each TranslationRequest has one Media
                .WithMany(m => m.TranslationRequests) // Each Media can have many TranslationRequests
                .HasForeignKey(tr => tr.MediaId); // ForeignKey in TranslationRequestModel pointing to MediaId in MediaItemModel

            // Configure the relationship between TranslationModel and TranslationRequestModel
            modelBuilder.Entity<TranslationModel>()
                .HasOne(t => t.TranslationRequest) // Each Translation has one TranslationRequest
                .WithMany(tr => tr.Translations) // Each TranslationRequest can have many Translations
                .HasForeignKey(t => t.TranslationRequestId); // ForeignKey in TranslationModel pointing to TranslationRequestId in TranslationRequestModel

            // Configure the relationship between RequestReference and TranslationRequestModel
            modelBuilder.Entity<RequestReference>()
                .HasOne(rr => rr.TranslationRequest) // Each RequestReference is associated with one TranslationRequest
                .WithMany(tr => tr.RequestReferences) // Each TranslationRequest can have many RequestReferences
                .HasForeignKey(rr => rr.TranslationRequestId); // ForeignKey in RequestReference pointing to TranslationRequestId in TranslationRequestModel
        }
    }
}
