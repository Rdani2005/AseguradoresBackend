using Beneficiary.Service.Dto.Request;

namespace Beneficiary.Service.Services.IService
{
    public interface IInsuranceService
    {
        void Create(AddInsuranceDto addInsuranceDto);
        void CreateMultiple(List<AddInsuranceDto> addInsuranceDtos);
        void Delete(Guid id);
    }
}
