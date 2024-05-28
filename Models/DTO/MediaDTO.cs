// Define the namespace for Data Transfer Objects (DTOs), which are utilized to transfer data efficiently 
// between processes or through network operations, especially within service architectures like APIs.
namespace localinezationBackend.Models.DTO
{
    // DTO class for carrying basic media information. This class is typically used to transmit media details
    // across different parts of the application or to external clients while minimizing overhead and securing sensitive details.
    public class MediaDTO
    {
        public int Id { get; set; } // Unique identifier for the media item, usually corresponding to the primary key in the database.
                                   // This is used to uniquely identify and access media items in various operations.

        public string Title { get; set; } // Title of the media item. This field is used to store and display the name
                                          // or title of the media, providing a human-readable identifier for the content.
    }
}
