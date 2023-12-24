using System.ComponentModel.DataAnnotations;

namespace Carrier.Service.Entities
{
    public class InsuranceCompany
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string CarrierCode { get; set; }

        public decimal Assured { get; set; }

        public decimal Bonus { get; set; }
    }
}
