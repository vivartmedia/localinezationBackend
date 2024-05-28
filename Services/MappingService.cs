// Importing necessary models for data representation
using localinezationBackend.Models;
using localinezationBackend.Models.DTO;

// Define the namespace that the service belongs to
namespace Backend_localinezationBackend.Services
{
    // Define the class as static, meaning it cannot be instantiated and should only contain static members
    public static class MappingService
    {
        // Public static method to convert a TranslationRequestModel to its corresponding Data Transfer Object (DTO)
        public static TranslationRequestDTO MapToDTO(TranslationRequestModel model)
        {
            // Returns a new instance of TranslationRequestDTO populated with data from the TranslationRequestModel
            return new TranslationRequestDTO
            {
                // Simple properties are directly assigned
                Id = model.Id,
                RequestorUserId = model.RequestorUserId,
                RequestLanguage = model.RequestLanguage,
                RequestName = model.RequestName,
                RequestDialogue = model.RequestDialogue,

                // Nested object Media is mapped from MediaModel to MediaDTO
                Media = new MediaDTO
                {
                    Id = model.MediaId, // ID is directly taken from the model
                    Title = model.Media.Title, // Title is accessed through the navigation property
                },

                // Collection property RequestReferences is projected into a new list of RequestReferenceDTO
                RequestReferences = model.RequestReferences?.Select(rr => new RequestReferenceDTO
                {
                    Src = rr.Src,  // Source URL of the reference
                    IsVideo = rr.IsVideo, // Indicates if the reference is a video
                }).ToList() ?? new List<RequestReferenceDTO>() // Using null-conditional operator to handle null cases gracefully
            };
        }
    }
}
