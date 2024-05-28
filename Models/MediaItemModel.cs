namespace localinezationBackend.Models
{
    // Represents a media item within the system
    public class MediaItemModel
    {
        public int ID { get; set; } // Unique identifier for the media, automatically generated as the Primary Key
        public int UserID { get; set; } // Foreign key that represents the user associated with the media, provided by the frontend
        public string? Title { get; set; } // Title of the media
        public string? CoverArt { get; set; } // URL or path to an image that serves as cover art for the media
        public string? OriginalLanguage { get; set; } // Original language of the media
        public string? Type { get; set; } // Type of media, e.g., video, book
        public string? Platform { get; set; } // Platform where the media is published, e.g., YouTube, Instagram
        public bool? IsPublished { get; set; } // Status to check if the media is published
        public bool? IsDeleted { get; set; } // Status to check if the media is marked as deleted

        // Collection of translation requests associated with the media
        public virtual ICollection<TranslationRequestModel>? TranslationRequests { get; set; }
    }

    // Model for storing references to media, used in translation requests
    public class RequestReference
    {
        public int Id { get; set; } // Primary key for RequestReference
        public string? Src { get; set; } // Source URL or path of the media reference
        public bool IsVideo { get; set; } // Flag to indicate if the reference is a video
        public int TranslationRequestId { get; set; } // Foreign key to associate with a translation request
        public TranslationRequestModel? TranslationRequest { get; set; } // Navigation property back to the related TranslationRequest
    }

    // Represents a request for translating a specific part of a media item
    public class TranslationRequestModel
    {
        public int Id { get; set; } // Automatically generated primary key for the translation request
        public int RequestorUserId { get; set; } // User ID of the requester
        public int MediaId { get; set; } // Foreign key to associate with a MediaItemModel
        public MediaItemModel? Media { get; set; } // Navigation property back to the related MediaItemModel
        public string? RequestLanguage { get; set; } // Language in which the translation is requested
        public virtual ICollection<TranslationModel>? Translations { get; set; } // Collection of completed translations for this request
        public string? RequestName { get; set; } // Name of the specific part of media to be translated
        public string? RequestDialogue { get; set; } // Text or dialogue within the media to be translated
        public virtual ICollection<RequestReference>? RequestReferences { get; set; } // References used to show the piece to be translated
    }

    // Model for storing details of a completed translation
    public class TranslationModel
    {
        public int Id { get; set; } // Primary key for the translation
        public int TranslationRequestId { get; set; } // Foreign key linking back to the corresponding TranslationRequest
        public TranslationRequestModel? TranslationRequest { get; set; } // Navigation property to the associated TranslationRequest
        public int TranslatorUserId { get; set; } // User ID of the translator
        public string? TranslatedText { get; set; } // The translated text
        public bool IsApproved { get; set; } // Status to indicate whether the translation has been approved
        public string? Language { get; set; } // Language of the translation
        public bool IsGuest { get; set; } // Flag to indicate if the translator was a guest (non-registered user)
    }
}
