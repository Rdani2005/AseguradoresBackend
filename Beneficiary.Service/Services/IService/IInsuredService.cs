using Beneficiary.Service.Dto.Request;
using Beneficiary.Service.Dto.Response;

namespace Beneficiary.Service.Services.IService
{
    public interface IInsuredService
    {
        IEnumerable<InsuredDto> GetAll();
        InsuredDto GetById(Guid id);
        InsuranceTransactionDto Create(AddInsuredDto addInsuredDto);
        void CreateMultiple(List<AddInsuredDto> insuredDtos);
        InsuranceTransactionDto Update(Guid id, UpdateInsuredDto updateInsuredDto);
        InsuranceTransactionDto Delete(Guid id);
        void AddInsurance(Guid insuredId, AddRelationDto addRelationDto);
    }
}
