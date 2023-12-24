using Beneficiary.Service.Dto.Request;
using Beneficiary.Service.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Beneficiary.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsurancesController : ControllerBase
    {
        private readonly IInsuranceService _insuranceService;

        public InsurancesController(
            IInsuranceService insuranceService    
        )
        {
            _insuranceService = insuranceService;
        }


        [HttpPost]
        public ActionResult addInsurance(
            [FromBody] AddInsuranceDto addInsuranceDto    
        )
        {
            Console.WriteLine("--> Asked to add a new Insurance");
            _insuranceService.Create(addInsuranceDto);
            return Ok("Insurance Created Successfully");
        }

        [HttpPost("upload")]
        public ActionResult UploadMultiple(
            [FromBody] AddMultipleInsurancesDto addMultipleInsurancesDto
        )
        {
            _insuranceService.CreateMultiple(addMultipleInsurancesDto.Insurances);
            return Ok("Insurances added successfylly");
        }

        [HttpDelete("{Id}")]
        public ActionResult DeleteInsurance(Guid Id)
        {
            _insuranceService.Delete(Id);
            return Ok("Insurance deleted Successfully");
        }

    }
}
