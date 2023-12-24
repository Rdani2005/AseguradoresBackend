using Beneficiary.Service.Dto.Request;

namespace Beneficiary.Service.Services.IService
{
    public interface IExcelService
    {
        List<AddInsuredDto> ReadInsuranceDataFromExcel(byte[] excelFile);
    }
}
