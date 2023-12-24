using System.ComponentModel.DataAnnotations;

namespace Beneficiary.Service.Dto.Request
{
    public class AddInsuranceDto
    {

        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string CarrierCode { get; set; }
    }
}
