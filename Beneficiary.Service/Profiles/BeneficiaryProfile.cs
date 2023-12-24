using AutoMapper;
using Beneficiary.Service.Dto.Request;
using Beneficiary.Service.Dto.Response;
using Beneficiary.Service.Entities;

namespace Beneficiary.Service.Profiles
{
    public class BeneficiaryProfile : Profile
    {
        public BeneficiaryProfile()
        {
            CreateMap<Insured, InsuredDto>();
            CreateMap<Insurance, InsuranceDto>();

            CreateMap<AddInsuredDto, Insured>();
            CreateMap<AddInsuranceDto, Insurance>();

            CreateMap<AddRelationDto, InsuranceInsured>()
                .ForMember(dest => dest.InsuredId, opt => opt.MapFrom(src => src.InsuredId))
                .ForMember(dest => dest.InsuranceId, opt => opt.MapFrom(src => src.InsuranceId));

            CreateMap<UpdateInsuredDto, Insured>();

        }
    }
}
