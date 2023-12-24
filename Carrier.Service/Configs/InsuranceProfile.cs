using AutoMapper;
using Carrier.Service.Dto.Request;
using Carrier.Service.Dto.Response;
using Carrier.Service.Entities;

namespace Carrier.Service.Configs
{
    public class InsuranceProfile : Profile
    {
        public InsuranceProfile()
        {
            // Source -> Target
            CreateMap<InsuranceCreateCommandDto, InsuranceCompany>();
            CreateMap<InsuranceUpdateCommandDto, InsuranceCompany>();
            CreateMap<InsuranceCompany, InsuranceDto>();
        }
    }
}
