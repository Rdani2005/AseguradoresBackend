using Carrier.Service.Dto.Request;

namespace Carrier.Service.Service.IService
{
    public interface IExcelService
    {
        List<InsuranceCreateCommandDto> ReadInsuranceDataFromExcel(byte[] excelFile);
    }
}
