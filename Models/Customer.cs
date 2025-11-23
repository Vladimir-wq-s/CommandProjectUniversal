using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CommandProjectUniversal.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Phone { get; set; } = string.Empty;

        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
}
