using System.ComponentModel.DataAnnotations;

namespace Beneficiary.Service.Dto.Request
{
    public class AddInsuredDto
    {
        [Required]
        [MaxLength(20)]
        public string NationalId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(15)]
        public string Phone { get; set; }

        [Required]
        public DateTime BirthDay { get; set; }
    }
}
