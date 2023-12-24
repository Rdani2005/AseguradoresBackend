using Beneficiary.Service.Dto.Request;
using Beneficiary.Service.Services.IService;
using OfficeOpenXml;

namespace Beneficiary.Service.Services
{
    public class ExcelService : IExcelService
    {
        public List<AddInsuredDto> ReadInsuredDataFromExcel(byte[] excelFile)
        {
            var insuranceList = new List<AddInsuredDto>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new OfficeOpenXml.ExcelPackage(new System.IO.MemoryStream(excelFile)))
            {
                var worksheet = package.Workbook.Worksheets[0]; // Suponiendo que los datos están en la primera hoja

                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    var insuranceDto = new AddInsuredDto
                    {
                        NationalId = worksheet.Cells[row, 1].GetValue<string>(),    // Columna 1: Id
                        Name = worksheet.Cells[row, 2].GetValue<string>(),        // Columna 1: Name
                        Phone = worksheet.Cells[row, 3].GetValue<string>(),  // Columna 2: Phone
                        BirthDay = worksheet.Cells[row, 4].GetValue<DateTime>(),      // Columna 3: BirthDay
                    };

                    insuranceList.Add(insuranceDto);
                }
            }

            return insuranceList;
        }
    }
}

