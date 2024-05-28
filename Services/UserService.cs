// Import necessary libraries and models for handling security and database context
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using localinezationBackend.Models;
using localinezationBackend.Models.DTO;
using localinezationBackend.Services.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

// Define the namespace for the service class
namespace localinezationBackend.Services
{
    // UserService class inherits ControllerBase to utilize controller functionalities like returning action results
    public class UserService : ControllerBase
    {
        // Private field for accessing the database context
        private readonly DataContext _context;

        // Constructor for injecting the database context into the service
        public UserService(DataContext context)
        {
            _context = context;
        }

        // Checks if a user exists in the database by username
        public bool DoesUserExist(string Username)
        {
            // Query the database to see if any user matches the given username
            return _context.UserInfo.SingleOrDefault(user => user.Username == Username) != null;
        }

        // Adds a new user to the database
        public ActionResult AddUser(CreateAccountDTO UserToAdd)
        {
            // Check if the username already exists to avoid duplicates
            if (DoesUserExist(UserToAdd.Username))
            {
                return new JsonResult(new { message = "Username already exists. Please choose a different username." })
                { StatusCode = 400 }; // Return a BadRequest if the username exists
            }

            // Create a new user model and populate properties
            UserModel newUser = new UserModel
            {
                Username = UserToAdd.Username,
                // Password hashing is handled separately
            };

            // Hash the password using a helper method defined later in this class
            var hashPassword = HashPassword(UserToAdd.Password);
            newUser.Salt = hashPassword.Salt;
            newUser.Hash = hashPassword.Hash;

            // Add the new user to the database
            _context.UserInfo.Add(newUser);
            int saveResult = _context.SaveChanges();

            // Check if the user was successfully added
            if (saveResult > 0)
            {
                return new JsonResult(new { message = "User created successfully." })
                { StatusCode = 200 }; // Return OK if the user is created
            }
            else
            {
                return new JsonResult(new { message = "An error occurred while creating the user." })
                { StatusCode = 500 }; // Return an Internal Server Error if the save failed
            }
        }

        // Generates a hashed password using a salt
        public PasswordDTO HashPassword(string password)
        {
            PasswordDTO newHashPassword = new PasswordDTO();
            byte[] SaltByte = new byte[64]; // Create a byte array for salt
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider(); // Create a random number generator
            provider.GetNonZeroBytes(SaltByte); // Fill the byte array with non-zero bytes
            string salt = Convert.ToBase64String(SaltByte); // Convert salt bytes to a string

            // Create a hasher with the password, salt, and iteration count
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, SaltByte, 10000);
            string hash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)); // Generate a 256-byte hash

            newHashPassword.Salt = salt;
            newHashPassword.Hash = hash;

            return newHashPassword;
        }

        // Verifies a user's password against the stored hash
        public bool VerifyUsersPassword(string? password, string? storedHash, string? storedSalt)
        {
            byte[] SaltBytes = Convert.FromBase64String(storedSalt); // Convert the stored salt back to bytes
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, SaltBytes, 10000); // Recreate the hasher
            string newHash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)); // Compute the hash for the provided password

            return newHash == storedHash; // Compare the new hash to the stored hash
        }

        // Handles user login
        public IActionResult Login(LoginDTO User)
        {
            IActionResult Result = Unauthorized(); // Default to unauthorized

            // Check if the user exists
            if (DoesUserExist(User.Username))
            {
                UserModel founderUser = GetUserByUsername(User.Username); // Retrieve the user from the database

                // Verify the user's password
                if (VerifyUsersPassword(User.Password, founderUser.Hash, founderUser.Salt))
                {
                    // Create token security key using a secret key
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256); // Create signing credentials

                    // Set up token options including issuer, audience, claims, expiration, and signing credentials
                    var tokeOptions = new JwtSecurityToken(
                        issuer: "http://localhost:5000",
                        audience: "http://localhost:5000",
                        claims: new List<Claim>(), // Claims can be added here
                        expires: DateTime.Now.AddMinutes(30), // Token expires after 30 minutes
                        signingCredentials: signinCredentials
                    );

                    // Generate the token
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

                    // Return the token in a successful response
                    Result = Ok(new { Token = tokenString });
                }
            }

            return Result;
        }

        // Retrieves a user by their username
        public UserModel GetUserByUsername(string username)
        {
            return _context.UserInfo.SingleOrDefault(User => User.Username == username);
        }

        // Updates a user in the database
        public bool UpdateUser(UserModel userToUpdate)
        {
            _context.Update<UserModel>(userToUpdate);
            return _context.SaveChanges() != 0;
        }

        // Updates a user's username by their ID
        public bool UpdateUsername(int id, string username)
        {
            UserModel foundUser = GetUserById(id);

            bool result = false;
            if (foundUser != null)
            {
                foundUser.Username = username;
                _context.Update<UserModel>(foundUser);
                result = _context.SaveChanges() != 0;
            }

            return result;
        }

        // Updates a user's credentials
        public bool UpdateUserCredentials(int id, string username, string password)
        {
            UserModel foundUser = GetUserById(id);
            bool result = false;

            if (foundUser != null)
            {
                // Regenerate salt and hash for the new password
                RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
                byte[] saltBytes = new byte[64];
                provider.GetNonZeroBytes(saltBytes);
                string salt = Convert.ToBase64String(saltBytes);

                Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 10000);
                string hash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

                // Update user's username, salt, and hash
                foundUser.Username = username;
                foundUser.Salt = salt;
                foundUser.Hash = hash;

                _context.Update<UserModel>(foundUser);
                result = _context.SaveChanges() != 0;
            }

            return result;
        }

        // Retrieves a user by their ID
        public UserModel GetUserById(int id)
        {
            return _context.UserInfo.SingleOrDefault(user => user.ID == id);
        }

        // Deletes a user by username
        public bool DeleteUser(string userToDelete)
        {
            UserModel foundUser = GetUserByUsername(userToDelete);

            bool result = false;
            if (foundUser != null)
            {
                _context.Remove<UserModel>(foundUser);
                result = _context.SaveChanges() != 0;
            }

            return result;
        }

        // Retrieves basic user information by username
        public UserIdDTO GetUserIdDTOByUsername(string username)
        {
            UserModel foundUser = _context.UserInfo.SingleOrDefault(user => user.Username == username);

            if (foundUser == null)
            {
                throw new Exception("User not found");
            }

            return new UserIdDTO
            {
                UserId = foundUser.ID,
                PublisherName = foundUser.Username
            };
        }

        // Retrieves basic user information by user ID
        public UserIdDTO GetUserByUserId(int id)
        {
            var user = _context.UserInfo.Find(id);
            if (user != null)
            {
                return new UserIdDTO
                {
                    UserId = user.ID,
                    PublisherName = user.Username
                };
            }
            return null;  // Return null if user is not found
        }
    }
}
