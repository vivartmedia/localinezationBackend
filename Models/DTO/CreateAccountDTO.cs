// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;

namespace localinezationBackend.Models.DTO
{
    public class CreateAccountDTO
    {
        public int ID { get; set; }
        public string Username {get; set; }

        
        // public string Email { get; set; }//requsted by zach but again requested to cut it off
        public string Password {get; set; }

    }
}