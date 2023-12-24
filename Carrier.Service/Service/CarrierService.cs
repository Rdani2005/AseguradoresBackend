using AutoMapper;
using Carrier.Service.Data;
using Carrier.Service.Dto.Request;
using Carrier.Service.Dto.Response;
using Carrier.Service.Entities;
using Carrier.Service.Service.IService;

namespace Carrier.Service.Service
{
    public class CarrierService : ICarrierService
    {
        private readonly IMapper _mapper;
        private readonly AppDBContext _context;
        public CarrierService(
            IMapper mapper,
            AppDBContext context
        )
        {
            _mapper = mapper;   
            _context = context;
        }

        public InsuranceTransactionResponseDto createInsurance(InsuranceCreateCommandDto insuranceCreateCommand)
        {
            InsuranceCompany insuranceCompany = _mapper.Map<InsuranceCompany>(insuranceCreateCommand);
            _context.InsuranceCompanies.Add(insuranceCompany);
            _context.SaveChanges();
            InsuranceDto dto = _mapper.Map<InsuranceDto>(insuranceCompany);
            return new InsuranceTransactionResponseDto
            {
                Insurance = dto,
                Message = "Insurance Created Successfully"
            };
        }

        public List<InsuranceCompany> createMultipleInsurancesByExcel(List<InsuranceCreateCommandDto> insurancesByExcel)
        {
            var insuranceEntities = _mapper.Map<List<InsuranceCompany>>(insurancesByExcel);
            _context.InsuranceCompanies.AddRange(insuranceEntities);
            _context.SaveChanges();
            return insuranceEntities;
        }

        public InsuranceTransactionResponseDto deleteInsurance(Guid id)
        {

            // Finds The Entity by Id and remove it from the context
            var insuranceEntity = _context.InsuranceCompanies.Find(id);
            if (insuranceEntity != null)
            {
                _context.InsuranceCompanies.Remove(insuranceEntity);
                _context.SaveChanges();
                return new InsuranceTransactionResponseDto
                {
                    Message = "Insurance Deleted Successfully"
                };
            }

            return null;
        }

        public InsuranceDto findInsuranceById(Guid id)
        {
            var insuranceEntity = _context.InsuranceCompanies.FirstOrDefault(c => c.Id == id);
            if (insuranceEntity == null)
            {
                return null;
            }
            return _mapper.Map<InsuranceDto>(insuranceEntity);
        }

        public MultipleInsuranceResponseDto findMultipleInsurances()
        {
            var insuranceEntities = _context.InsuranceCompanies.ToList();
            var insuranceDtos = _mapper.Map<List<InsuranceDto>>(insuranceEntities);

            return new MultipleInsuranceResponseDto
            {
                Insurances = insuranceDtos
            };
        }

        public InsuranceTransactionResponseDto updateInsurance(InsuranceUpdateCommandDto insuranceUpdateCommand)
        {
            var insuranceEntity = _context.InsuranceCompanies.Find(insuranceUpdateCommand.Id);
            if (insuranceEntity == null)
            {
                return null;
                
            }
            _mapper.Map(insuranceUpdateCommand, insuranceEntity);
            _context.SaveChanges();

            var updatedDto = _mapper.Map<InsuranceDto>(insuranceEntity);
            return new InsuranceTransactionResponseDto
            {
                Insurance = updatedDto,
                Message = "Insurance Updated Successfully"
            };


        }
    }
}
