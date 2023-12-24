namespace Carrier.Service.Dto.Request
{
    public class InsuranceCreateCommandDto
    {
        public string Name { get; set; }

        public string CarrierCode { get; set; }

        public decimal Assured { get; set; }

        public decimal Bonus { get; set; }
    }
}
