// Define the namespace for the models, which typically contains data representations used throughout the application
namespace localinezationBackend.Models
{
    // UserModel class to represent a user in the system
    public class UserModel
    {
        // Unique identifier for the user, typically the primary key in a database
        public int ID { get; set; }

        // Username for the user; nullable in case not provided
        public string? Username { get; set; }
        
        // Salt used in hashing the user's password to enhance security
        public string? Salt { get; set; }

        // Hashed password resulting from combining the user's password with the salt
        public string? Hash { get; set; }
        
        // Default constructor for UserModel
        public UserModel()
        {
            
        }
    }

    // Data Transfer Object (DTO) class for user credentials
    public class UserCredentialsDto
    {
        // Unique identifier for the user; corresponds to UserModel.ID
        public int Id { get; set; }

        // Username of the user; to be filled in when credentials are passed
        public string Username { get; set; }

        // Plain text password of the user; used for login and then it should be hashed for storage
        public string Password { get; set; }
    }
}
