using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace phone_book.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<string> ContactsList { get; set; } = [];

        public string UserName { get; set; }

        public string Password { get; set; }

        public long PhoneNumber { get; set; }
    }
}