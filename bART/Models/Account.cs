using System.ComponentModel.DataAnnotations;

namespace bART.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }

        public Incident? Incident { get; set; }
        
        public IEnumerable<Contact> Contacts { get; set; }
    }
}
