
// Define the namespace for Data Transfer Objects (DTOs), which are used to simplify and tailor data structures
// for efficient data transfer between different layers and components of an application.
namespace localinezationBackend.Models.DTO
{
    // DTO class for carrying hashed password information. 
    // This is typically used to safely transmit and store password data within the system.
    public class PasswordDTO
    {
        public string Salt { get; set; } // The salt used in conjunction with the user's password to enhance security.
                                        // Salting the password helps protect against rainbow table attacks by ensuring
                                        // that even identical passwords result in different hashes.

        public string Hash { get; set; } // The resulting hash from combining the user's password and the salt.
                                         // This hash is what is stored in the database instead of the plain password
                                         // to protect user data from being exposed in case of a data breach.
    }
}
