using System.Collections.Generic;

namespace localinezationBackend.Models
{
    public class MediaItemModel
    {
        public int ID { get; set; }// Unique identifier for the media (Primary Key)=GENERATED AUTOMATICALLY
        public int UserID { get; set; } // foreign key = Frontend gives this number to be added to backend
        public string? Title { get; set; }
        public string? CoverArt { get; set; }//string or url or path the an image
        public string? OriginalLanguage { get; set; }
        public string? Tags { get; set; }
        public string? Categories { get; set; }
        public string? Type { get; set; }//e.g., video, book ...
        public string? Platform { get; set; }// e.g. youtube instagram.... 
        public bool? IsPublished { get; set; }
        public bool? IsDeleted { get; set; }

        //collection of translation requests related to the media
        public virtual ICollection<TranslationRequestModel>? TranslationRequests { get; set; }


    }

    public class TranslationRequestModel
    {
        public int Id { get; set; }// (Primary key) generated automagically to the translation request
        public int RequestorUserId { get; set; }
        public int MediaId { get; set; }    // (Foreign key to 'MediaItemMOdel) has to be set up in the front end to link the media with this id
        public MediaItemModel? Media { get; set; }// navigation property back to related MediaItemModel
        public string? RequestLanguage { get; set; }//UserId of the user who requested the translation
        public virtual ICollection<TranslationModel>? Translations { get; set; }// colllection of translations fulfilled under this request

    }

    public class TranslationModel
{
    public int Id { get; set; } // Primary key for the translation
    public int TranslationRequestId { get; set; } // Foreign key to TranslationRequest
    public TranslationRequestModel? TranslationRequest { get; set; } // Navigation property to TranslationRequest
    public string? TranslatorUserId { get; set; } // UserId of the translator, set it up in frontend
    public string? TranslatedText { get; set; }
    public string? IsApproved { get; set; } // For approval if needed/optional
}
}
