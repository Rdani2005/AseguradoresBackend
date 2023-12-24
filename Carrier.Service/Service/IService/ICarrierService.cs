using Carrier.Service.Dto.Request;
using Carrier.Service.Dto.Response;
using Carrier.Service.Entities;

namespace Carrier.Service.Service.IService
{
    public interface ICarrierService
    {
        InsuranceTransactionResponseDto createInsurance(InsuranceCreateCommandDto insuranceCreateCommand);
        List<InsuranceCompany> createMultipleInsurancesByExcel(List<InsuranceCreateCommandDto> insurancesByExcel);
        InsuranceTransactionResponseDto updateInsurance(InsuranceUpdateCommandDto insuranceUpdateCommand);
        InsuranceDto findInsuranceById(Guid id);
        MultipleInsuranceResponseDto findMultipleInsurances();
        InsuranceTransactionResponseDto deleteInsurance(Guid id);
    }
}
