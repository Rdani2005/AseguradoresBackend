using System.ComponentModel.DataAnnotations;

namespace Beneficiary.Service.Dto.Request
{
    public class AddRelationDto
    {
        [Required]
        public Guid InsuredId { get; set; }

        [Required]
        public Guid InsuranceId { get; set; }
    }
}
