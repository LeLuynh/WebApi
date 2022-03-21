using System.ComponentModel.DataAnnotations;

namespace WebAPI.Dtos
{
    public class RegisterDto
    {
        public string UserName { get; set; }

        [Required] 
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Address { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage ="Password and confirmation password did not match")]
        public string ConfirmPassword { get; set; }
    }
}
