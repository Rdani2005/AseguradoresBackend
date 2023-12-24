using Carrier.Service.Dto.Request;
using Carrier.Service.Dto.Response;
using Carrier.Service.Entities;
using Carrier.Service.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Carrier.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrierController : ControllerBase
    {
        private readonly ICarrierService _carrierService;
        private readonly IExcelService _excelService;

        public CarrierController(
            ICarrierService carrierService,
            IExcelService excelService
        )
        {
            _carrierService = carrierService;
            _excelService = excelService;
        }


        [HttpGet]
        public IActionResult GetAll() {
            return Ok(_carrierService.findMultipleInsurances());
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(Guid Id) {
            InsuranceDto insuranceDto = _carrierService.findInsuranceById(Id);
            if (insuranceDto == null)
            {
                return NotFound("Insurance was not found");
            }
            return Ok(insuranceDto);
        }

        [HttpPost]
        public ActionResult AddInsurance(
            [FromBody] InsuranceCreateCommandDto insuranceCreateCommand
        )
        {
            var response = _carrierService.createInsurance(insuranceCreateCommand);
            sendToBeneficiaries(response.Insurance);
            return Ok(response);
        }

        [HttpPut("{Id}")]
        public ActionResult UpdateInsurance(
            Guid Id,
            [FromBody] InsuranceUpdateCommandDto insuranceUpdateCommand
        )
        {
            InsuranceTransactionResponseDto insuranceTransactionResponseDto = _carrierService.updateInsurance(insuranceUpdateCommand);
            if (insuranceTransactionResponseDto == null )
            {
                return NotFound("Insurance was not found");
            }

            return Ok(insuranceTransactionResponseDto);

        }

        [HttpDelete("{Id}")]
        public ActionResult deleteInsurance(Guid Id)
        {
            InsuranceTransactionResponseDto insuranceTransactionResponseDto = _carrierService.deleteInsurance(Id);
            if ( insuranceTransactionResponseDto == null )
            {
                return NotFound("Insurance was not found");
            }
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.DeleteAsync($"https://localhost:6001/api/Insurances/{Id}").Result;
            }
            return Ok("Insurance deleted successfully");

        }

        [HttpPost("upload")]
        public IActionResult UploadInsuranceExcel([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Invalid file");
            }

            // Verifica si el archivo es un archivo Excel según la extensión, por ejemplo, .xlsx
            if (!file.FileName.EndsWith(".xlsx"))
            {
                return BadRequest("Invalid file format. Please upload a valid Excel file.");
            }

            try
            {
                // Convierte el archivo a un array de bytes y pasa los datos al servicio
                byte[] fileBytes;
                using (var ms = new System.IO.MemoryStream())
                {
                    file.CopyTo(ms);
                    fileBytes = ms.ToArray();
                }

                // Utiliza el servicio para leer los datos del archivo Excel y almacenarlos en la base de datos
                List<InsuranceCreateCommandDto> data = _excelService.ReadInsuranceDataFromExcel(fileBytes);
                List<InsuranceCompany>  insurances = _carrierService.createMultipleInsurancesByExcel(data);
                sendExcelToBeneficiaries(insurances);
                return Ok("File uploaded successfully");
            }
            catch (Exception ex)
            {
                // Manejo de errores, puedes personalizar esto según tus necesidades
                return StatusCode(500, $"--> Internal server error: {ex.Message}");
            }
        }

        private void sendExcelToBeneficiaries(List<InsuranceCompany> data)
        {
            var json = new
            {
                insurances = data.Select(insurance => new
                {
                    id = insurance.Id,
                    name = insurance.Name,
                    carrierCode = insurance.CarrierCode
                })
            };
            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json");
                var response = httpClient.PostAsync("https://localhost:6001/api/Insurances/upload", content).Result;

            }
        }

        private void sendToBeneficiaries(InsuranceDto data)
        {
            // var json = JsonConvert.SerializeObject(data);
            var json = new
            {
                id =  data.Id, 
               name = data.Name,
               carrierCode = data.CarrierCode
            };
            
            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json");
                var response = httpClient.PostAsync("https://localhost:6001/api/Insurances", content).Result;
            }
        }
    }


}
