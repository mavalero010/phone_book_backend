using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace phone_book.Models
{
    public class AdditionalFields
    {
        [Key]
        public int FieldId { get; set; }


        public string? Nit { get; set; }

        public string? State { get; set; }

        public DateTime? Birthday { get; set; }

        public DateTime? Foundation { get; set; }



    }
}
