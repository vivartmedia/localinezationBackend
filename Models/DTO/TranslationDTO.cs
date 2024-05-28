// Define the namespace for Data Transfer Objects (DTOs), which are structures used to transfer data across different layers of an application,
// especially from the API to the consumer.
namespace localinezationBackend.Models.DTO
{
    // DTO class for carrying details of translations.
    public class TranslationDTO
    {
        public int Id { get; set; } // Unique identifier for the translation entry, typically a primary key in the database.

        public int TranslationRequestId { get; set; } // Foreign key linking this translation to its corresponding translation request.
        
        public int TranslatorUserId { get; set; } // User ID of the translator who performed the translation. This helps in tracking who made specific translations.

        public string? TranslatedText { get; set; } // The actual text that has been translated. This is the core content of this DTO.

        public bool IsApproved { get; set; } // Boolean flag indicating whether the translation has been approved. Useful for quality control processes.

        public string? Language { get; set; } // The language into which the text was translated. This specifies the target language of the translation.

        public bool IsGuest { get; set; } // Boolean flag indicating whether the translation was made by a guest (non-registered) user. This can impact permissions and visibility.
    }
}
