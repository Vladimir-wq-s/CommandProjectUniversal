using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommandProjectUniversal.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ServicePlan")]
        public int ServicePlanId { get; set; }
        public ServicePlan ServicePlan { get; set; } = null!;

        [MaxLength(100)]
        public string? Name { get; set; }

        [MaxLength(50)]
        public string? ContractNumber { get; set; }

        [Required]
        public DateTime InstallationDate { get; set; }

        [MaxLength(200)]
        public string? Address { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public Sale? Sale { get; set; }
    }
}
