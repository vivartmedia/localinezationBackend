// Define the namespace for Data Transfer Objects (DTOs), which are used to encapsulate data and send it from one part of the application to another, typically from server to client
namespace localinezationBackend.Models.DTO
{
    // DTO class for carrying translation request data
    public class TranslationRequestDTO
    {
        public int Id { get; set; } // Unique identifier for the translation request, a primary key in the database
        
        public int RequestorUserId { get; set; } // User ID of the person who made the translation request
        
        public string? RequestLanguage { get; set; } // The language in which the translation is requested
        
        public string? RequestName { get; set; } // The name or title of the translation request, which can be used to identify it in a list
        
        public string? RequestDialogue { get; set; } // The specific text or dialogue from the media that needs translation
        
        public MediaDTO Media { get; set; } // MediaDTO object representing the media associated with this translation request
        
        public List<RequestReferenceDTO> RequestReferences { get; set; } // A list of references that provide context or additional information for the translation, such as images or documents
    }
}
