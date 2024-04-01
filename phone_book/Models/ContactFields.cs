namespace phone_book.Models
{
    public class ContactFields
    {   
        public string username { get; set; }
        public string password { get; set; }   
        public Contact C { get; set; }
        public AdditionalFields? AF { get; set; }
    }
}
