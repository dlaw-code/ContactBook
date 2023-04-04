using AnotherContactBook.Model;

namespace AnotherContactBook
{
    public class ContactResponse
    {
        public List<Contact> Contacts { get; set; } = new List<Contact>();

        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
