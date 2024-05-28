

// Define the namespace for Data Transfer Objects (DTOs), which are utilized to efficiently transfer data
// between the various layers of an application, particularly for user-facing operations such as authentication.
namespace localinezationBackend.Models.DTO
{
    // DTO class specifically designed for carrying user credentials during the login process.
    // This class encapsulates the necessary data for authenticating a user.
    public class LoginDTO
    {
        public string Username { get; set; } // Username of the user attempting to log in.
                                            // This property captures the identifier that the user provides
                                            // which can be their unique username or email, depending on the system design.

        public string Password { get; set; } // Password for the user attempting to log in.
                                             // This property holds the plaintext password as entered by the user,
                                             // which will be securely handled and verified against stored credentials
                                             // in the backend during the authentication process.
    }
}
