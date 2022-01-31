using System.ComponentModel.DataAnnotations;

namespace bART.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(30)]
        public string FirstName { get; set; }

        [MaxLength(30)]
        public string LastName { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        public Account? Account { get; set; }
    }
}
