// Define the namespace for Data Transfer Objects (DTOs), which are used to simplify and tailor data structures
// for transferring data between processes or through network operations, particularly from the server to a client.
namespace localinezationBackend.Models.DTO
{
    // DTO class for carrying reference details used in translation requests.
    public class RequestReferenceDTO
    {
        public string Src { get; set; } // The source URL or path to the media or document that is used as a reference.
                                        // This could be a link to an image, video file, document, etc.

        public bool IsVideo { get; set; } // A boolean flag indicating whether the reference is a video.
                                         // This helps the front-end or other consuming services to handle it appropriately
                                         // based on the type of media.
    }
}
