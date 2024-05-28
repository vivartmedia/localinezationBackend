// Define the namespace for Data Transfer Objects (DTOs) which are used to transfer data between processes
namespace localinezationBackend.Models.DTO
{
    // A simple DTO class that is used to transport user identity and publisher information across the system
    public class UserIdDTO
    {
        public int UserId { get; set; } // Represents the unique identifier of a user

        public string PublisherName { get; set; } // Represents the name of the user in the context of being a publisher or content creator
    }
}
