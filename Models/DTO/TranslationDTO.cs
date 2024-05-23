// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;

namespace localinezationBackend.Models.DTO
{
    public class TranslationDTO
    {
        public int Id { get; set; }
        public int TranslationRequestId { get; set; }
        public int TranslatorUserId { get; set; }
        public string? TranslatedText { get; set; }
        public bool IsApproved { get; set; }
        public string? Language { get; set; }
        public bool IsGuest { get; set; }
       
    }
}