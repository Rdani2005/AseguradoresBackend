using Carrier.Service.Dto.Request;
using Carrier.Service.Service.IService;
using OfficeOpenXml;

namespace Carrier.Service.Service
{
    public class ExcelService : IExcelService
    {
        public List<InsuranceCreateCommandDto> ReadInsuranceDataFromExcel(byte[] excelFile)
        {
            var insuranceList = new List<InsuranceCreateCommandDto>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new OfficeOpenXml.ExcelPackage(new System.IO.MemoryStream(excelFile)))
            {
                var worksheet = package.Workbook.Worksheets[0]; // Suponiendo que los datos están en la primera hoja

                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    var insuranceDto = new InsuranceCreateCommandDto
                    {
                        Name = worksheet.Cells[row, 1].GetValue<string>(),        // Columna 1: Name
                        CarrierCode = worksheet.Cells[row, 2].GetValue<string>(),  // Columna 2: CarrierCode
                        Assured = worksheet.Cells[row, 3].GetValue<decimal>(),      // Columna 3: Assured
                        Bonus = worksheet.Cells[row, 4].GetValue<decimal>()            // Columna 4: Bonus
                    };

                    insuranceList.Add(insuranceDto);
                }
            }

            return insuranceList;
        }
    }
}
