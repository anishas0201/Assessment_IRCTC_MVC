using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment_IRCTC_Revervation.Models
{
    public class RegisterUser
    {
        public int registrationId { get; set; }
        public string fullName { get; set; }
        public string emailId { get; set; }
        public string encryptedPassword { get; set; }
        public string decryptedPassword { get; set; }
        public string userType { get; set; }
    }
    public class MailList
    {
        public int registrationId { get; set; }
        public string emailId { get; set; }
    }
}
