using System.Collections.Generic;

namespace localinezationBackend.Models
{
    public class Media
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public string Title { get; set; }   
        public string CoverArt { get; set; }
        public string OriginalLanguage { get; set; }
        public string Type  { get; set; }
        public string Platform { get; set; }
        public List<TranslationRequest> TranslationRequests { get; set;}  
    }

    public class TranslationRequest
    {
        public int Id { get; set; }
        public int MediaId { get; set; }    
        public Media Media { get; set; }
        public string RequestLanguage { get; set; }
        public List<Translation> Translations { get; set; }
    }

    public class Translation
    {
        public int Id { get; set; }
        public int TranslationRequestId { get; set; }
        public TranslationRequest TranslationRequest { get; set; }
        public string TranslatorUserName { get; set; }
        public bool IsGuest { get; set; }
        public string TranslatedText { get; set; }
    }
}
