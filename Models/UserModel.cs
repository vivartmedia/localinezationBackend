using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace localinezationBackend.Models
{
    public class UserModel
    {
        public int ID { get; set; }
        public string? Username { get; set; }

        
        public string? Email { get; set; }//requestedd by zach

        public string? Salt { get; set; }
        public string? Hash { get; set; }
        
        public UserModel()
        {
            
        }
    }

    public class UserCredentialsDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}