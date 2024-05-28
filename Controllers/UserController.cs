
// Import necessary namespaces and models
using localinezationBackend.Models;
using localinezationBackend.Models.DTO;
using localinezationBackend.Services;
using Microsoft.AspNetCore.Mvc;

// Define the namespace for the controller classes
namespace localinezationBackend.Controllers
{
    // Annotation to define this class as an API controller and sets the route template for the controller
    [ApiController]
    [Route("[controller]")]
        public class UserController : ControllerBase
    {
        // Private field to store injected user service instance
        private readonly UserService _data;

        // Constructor to inject user service dependency
        public UserController(UserService data)
        {
            _data = data;
        }

        // API endpoint for user login using POST request method
        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginDTO User)
        {
            // Calls the login method of the UserService using the provided user data
            return _data.Login(User);
        }

        // API endpoint for adding a new user using POST request method
        [HttpPost]
        [Route("AddUser")]
        public ActionResult AddUser(CreateAccountDTO UserToAdd)
        {
            // Calls the AddUser method of the UserService to add new user data
            return _data.AddUser(UserToAdd); // Use _data, not _userService
        }

        // API endpoint to update existing user using PUT request method
        [HttpPut]
        [Route("UpdateUser")]
        public bool UpdateUser(UserModel userToUpdate)
        {
            // Calls the UpdateUser method to update user data
            return _data.UpdateUser(userToUpdate);
        }

        // API endpoint to update user's username by id using PUT request method
        [HttpPut]
        [Route("UpdateUser/{id}/{username}")]
        public bool UpdateUser(int id, string username)
        {
            // Calls the UpdateUsername method to update the user's username based on id
            return _data.UpdateUsername(id, username);
        }

        // API endpoint to update user's credentials using PUT request method
        [HttpPut]
        [Route("UpdateCredentials")]
        public bool UpdateUserCredentials([FromBody] UserCredentialsDto credentials)
        {
            // Calls the UpdateUserCredentials method to update user's ID, username, and password
            return _data.UpdateUserCredentials(credentials.Id, credentials.Username, credentials.Password);
        }

        // API endpoint to delete a user using DELETE request method
        [HttpDelete]
        [Route("DeleteUser/{userToDelete}")]
        public bool DeleteUser(string userToDelete)
        {
            // Calls the DeleteUser method to remove the user based on the username
            return _data.DeleteUser(userToDelete);
        }

        // API endpoint to retrieve a user by username using GET request method
        [HttpGet]
        [Route("GetUserByUsername/{username}")]
        public ActionResult<UserIdDTO> GetUserByUsername(string username)
        {
            try
            {
                // Attempts to find the user by username and returns the user if found
                UserIdDTO user = _data.GetUserIdDTOByUsername(username);
                return Ok(user);
            }
            catch (Exception ex)
            {
                // Returns a 404 Not Found status with the error message if the user is not found
                return NotFound(ex.Message);
            }
        }

        // API endpoint to retrieve a user by user ID using GET request method
        [HttpGet]
        [Route("GetUserByUserId/{id}")]
        public ActionResult<UserModel> GetUserByUserId(int id)
        {
            // Tries to get user details by user ID
            var user = _data.GetUserByUserId(id);
            if (user != null)   
            {
                // If user is found, return the user details
                return Ok(user);
            }
            else
            {
                // If no user is found, return a 404 Not Found status with a message
                return NotFound($"User with ID {id} not found.");
            }
        }
    }
}
