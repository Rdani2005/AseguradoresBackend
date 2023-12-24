namespace Beneficiary.Service.Entities
{
    public class InsuranceInsured
    {
        public Guid InsuredId { get; set; }
        public Insured Insured { get; set; }

        public Guid InsuranceId { get; set; }
        public Insurance Insurance { get; set; }
    }
}
