
// Define the namespace for Data Transfer Objects (DTOs), which are designed to encapsulate and transport data 
// efficiently between the server and clients or between different layers of an application.
namespace localinezationBackend.Models.DTO
{
    // DTO class designed for encapsulating data necessary to create a new user account.
    // This class is used during the user registration process to handle input data securely and efficiently.
    public class CreateAccountDTO
    {
        public int ID { get; set; } // Unique identifier for the user. Often used for referencing in the database.
                                    // This ID is typically assigned by the database upon creation of the user record.

        public string Username {get; set; } // Username chosen by the user. This is intended to be a unique identifier
                                            // that the user will use to log in to the system.

        // public string Email { get; set; } // Initially requested by Zach to be included for registration,
                                           // but was later removed from the requirements.

        public string Password {get; set; } // The user's chosen password. This will be hashed before being stored
                                            // in the database to ensure security. The plaintext password is used only
                                            // during the initial account creation process for authentication purposes.
    }
}
