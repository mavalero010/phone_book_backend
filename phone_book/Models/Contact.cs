using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace phone_book.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(AdditionalFields))]
        public int? AdditionalFields { get; set; }


        [Required]
        public string Name { get; set; }

        [ForeignKey(nameof(ContactType))]
        public int ContactTypeId { get; set; }


        public string UserName { get; set; }

        public string Comments { get; set; }    

        public long PhoneNumber { get; set; }
    }
}
