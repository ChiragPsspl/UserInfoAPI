using System.ComponentModel.DataAnnotations;

namespace UserInformation.Model
{
    public class UserRegistration
    {
        [Key]
        public int UserRegistrationId { get; set; }
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set;}
        public string Address { get; set; }        
    }

    public class JWTTokenResponse
    {
        public string Token { get; set; }
    }
}
