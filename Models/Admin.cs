using System.ComponentModel.DataAnnotations;

namespace SttbApi.Models
{
    public class Admin
    {
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; } 

        public string Name { get; set; }
    }
}
