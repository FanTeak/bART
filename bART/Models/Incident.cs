using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bART.Models
{
    public class Incident
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
        
        public IEnumerable<Account> Accounts { get; set; }
    }
}