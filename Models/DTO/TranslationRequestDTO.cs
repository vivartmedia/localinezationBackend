using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace localinezationBackend.Models.DTO
{
    public class TranslationRequestDTO
    {
        public int Id { get; set; }
        public int RequestorUserId { get; set; }
        public string? RequestLanguage { get; set; }
        public string? RequestName { get; set; }
        public string? RequestDialogue { get; set; }
        public MediaDTO Media { get; set; }
        public List<RequestReferenceDTO> RequestReferences { get; set; }// List of request references
        
    }

    
}