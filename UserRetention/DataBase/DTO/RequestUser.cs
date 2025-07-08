using System.ComponentModel.DataAnnotations;

namespace UserRetention.DataBase.DTO
{
    public class RequestUser
    {
        [Required(ErrorMessage = "Name is required."), StringLength(50)]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; }
    }
}
