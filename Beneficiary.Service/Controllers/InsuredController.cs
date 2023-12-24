using Beneficiary.Service.Dto.Request;
using Beneficiary.Service.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace Beneficiary.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsuredController : ControllerBase
    {
        private readonly IExcelService _excelService;
        private readonly IInsuredService _insuredService;

        public InsuredController(IExcelService excelService, IInsuredService insuredService)
        {
            _excelService = excelService;
            _insuredService = insuredService;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(
                _insuredService.GetAll()    
            );
        }

        [HttpGet("{Id}")]
        public ActionResult GetById(Guid Id)
        {
            var response = _insuredService.GetById(Id);
            if (response == null)
            {
                return NotFound("Insured could not be found");
            }
            return Ok(response);
        }

        [HttpPost]
        public ActionResult AddInsured([FromBody] AddInsuredDto addInsuredDto)
        {
            return Ok(
                _insuredService.Create(addInsuredDto)
            );
        }

        [HttpPost("upload")]
        public ActionResult AddMultipleInsured(
            [FromForm] IFormFile file
        )
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
                var data = _excelService.ReadInsuranceDataFromExcel(fileBytes);
                _insuredService.CreateMultiple(data);
                return Ok("File uploaded successfully");
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, $"--> Internal server error: {ex.Message}");
            }
        }

        [HttpPost("{insuredId}/AddInsurance")]
        public ActionResult AddInsurance(Guid insuredId, [FromBody] AddRelationDto addRelationDto)
        {
            try
            {
                _insuredService.AddInsurance(insuredId, addRelationDto);
                return Ok("Insurance added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{Id}")]
        public ActionResult UpdateInsured(Guid Id, [FromBody] UpdateInsuredDto updateInsuredDto)
        {
            var response = _insuredService.Update(Id, updateInsuredDto);
            if (response == null)
            {
                return NotFound("Insured was not found.");
            }

            return Ok(response);
        }


        [HttpDelete("{Id}")]
        public ActionResult DeleteById(Guid Id)
        {
            var response = _insuredService.Delete(Id);
            if (response == null)
            {
                return NotFound("Insured was not found.");
            }

            return Ok(response.Message);
        }


    }
}
