using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommandProjectUniversal.Models
{
    public class ServicePlan
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Provider")]
        public int ProviderId { get; set; }
        public Provider Provider { get; set; } = null!;

        [ForeignKey("Country")]
        public int CountryId { get; set; }
        public Country Country { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int SpeedMbps { get; set; }

        [Required]
        public decimal PricePerMonth { get; set; }

        [Required]
        public int DataLimitGB { get; set; }

        [Required]
        public int LaunchYear { get; set; }

        public ICollection<Service> Services { get; set; } = new List<Service>();
    }
}
