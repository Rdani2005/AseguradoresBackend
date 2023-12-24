using System.ComponentModel.DataAnnotations;

namespace Beneficiary.Service.Entities
{
    public class Insurance
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string CarrierCode { get; set; }

        public ICollection<InsuranceInsured> InsuredInsurances { get; set; }
    }
}
