using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Beneficiary.Service.Entities
{
    public class Insured
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string NationalId { get; set; }

        [Required]
        [MaxLength(100)] 
        public string Name { get; set; }

        [MaxLength(15)] 
        public string Phone { get; set; }

        [Column(TypeName = "Date")]
        public DateTime BirthDay { get; set; }

        public ICollection<InsuranceInsured> InsuredInsurances { get; set; }
    }
}
