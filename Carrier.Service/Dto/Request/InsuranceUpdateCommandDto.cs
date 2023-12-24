namespace Carrier.Service.Dto.Request
{
    public class InsuranceUpdateCommandDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string CarrierCode { get; set; }

        public decimal Assured { get; set; }

        public decimal Bonus { get; set; }
    }
}
