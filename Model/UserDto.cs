using System.ComponentModel.DataAnnotations;

namespace ITIL.Model
{
    public class UserDto
    {        
        [Required]
        public string Email {get; set;}

        [Required]
        public string Password {get; set;}
    }
}