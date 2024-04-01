using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace phone_book.Models
{
    public class ContactType
    {
        [Key]
        public int ContactTypeId { get; set; }

        [Required]
        public string TypeName { get; set; }
    }
}
