using System.ComponentModel.DataAnnotations;

namespace Beneficiary.Service.Dto.Request
{
    public class UpdateInsuredDto
    {
        [MaxLength(20)]
        public string NationalId { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(15)]
        public string Phone { get; set; }

        public DateTime BirthDay { get; set; }
    }
}
