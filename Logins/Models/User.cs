using System.ComponentModel.DataAnnotations;

namespace Logins.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string password { get; set; }
    }

}