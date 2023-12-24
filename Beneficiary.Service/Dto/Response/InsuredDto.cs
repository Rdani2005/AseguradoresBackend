namespace Beneficiary.Service.Dto.Response
{
    public class InsuredDto
    {
        public Guid Id { get; set; }
        public string NationalId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDay { get; set; }
        public ICollection<InsuranceDto> Insurances { get; set; }
    }
}
