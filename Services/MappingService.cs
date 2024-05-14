using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using localinezationBackend.Models;
using localinezationBackend.Models.DTO;


namespace Backend_localinezationBackend.Services
{
    public static class MappingService
    {
        public static TranslationRequestDTO MapToDTO(TranslationRequestModel model)
        {
            return new TranslationRequestDTO
            {
                Id = model.Id,
                RequestorUserId = model.RequestorUserId,
                RequestLanguage = model.RequestLanguage,
                RequestName = model.RequestName,
                RequestDialogue = model.RequestDialogue,
                Media = new MediaDTO
                {
                    Id = model.MediaId,
                    Title = model.Media.Title,
                },

                RequestReferences = model.RequestReferences?.Select(rr => new RequestReferenceDTO
                {
                    Src = rr.Src,
                    IsVideo = rr.IsVideo,

                }).ToList() ?? new List<RequestReferenceDTO>() // Handle null case
            };
        }
    }
}
