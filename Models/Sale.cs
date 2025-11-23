using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommandProjectUniversal.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Service")]
        public int ServiceId { get; set; }
        public Service Service { get; set; } = null!;

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        [Required]
        public DateTime SaleDate { get; set; }

        [Required]
        public decimal SalePrice { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
