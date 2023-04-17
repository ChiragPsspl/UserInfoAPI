using System.ComponentModel.DataAnnotations;

namespace UserInformation.Model
{
    public class LogInfo
    {
        [Key]
        public int LogInfoId { get; set; }
        public virtual int? UserRegistrationId { get; set; }
        public bool IsLogin { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

}
