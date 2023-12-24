using AutoMapper;
using Beneficiary.Service.Data;
using Beneficiary.Service.Dto.Request;
using Beneficiary.Service.Dto.Response;
using Beneficiary.Service.Entities;
using Beneficiary.Service.Services.IService;

namespace Beneficiary.Service.Services
{
    public class InsuredService : IInsuredService
    {
        private readonly AppDBContext _context;
        private readonly IMapper _mapper;

        public InsuredService(AppDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public void AddInsurance(Guid insuredId, AddRelationDto addRelationDto)
        {
            var existingInsured = _context.Insuranceds.FirstOrDefault(i => i.Id == insuredId);
            if (existingInsured != null)
            {
                var insurance = _mapper.Map<Insurance>(addRelationDto);
                _context.InsuredInsurances.Add(new InsuranceInsured { Insured = existingInsured, Insurance = insurance });
                _context.SaveChanges();
            }
        }

        public InsuranceTransactionDto Create(AddInsuredDto addInsuredDto)
        {
            var insured = _mapper.Map<Insured>(addInsuredDto);
            _context.Insuranceds.Add(insured);
            _context.SaveChanges();
            var response = _mapper.Map<InsuredDto>(insured);
            return new InsuranceTransactionDto { 
                Message = "Insured added successfully",
                Insured = response
            };
        }

        public void CreateMultiple(List<AddInsuredDto> insuredDtos)
        {
            var insureds = _mapper.Map<List<Insured>>(insuredDtos);
            _context.Insuranceds.AddRange(insureds);
            throw new NotImplementedException();
        }

        public InsuranceTransactionDto Delete(Guid id)
        {
            var existingInsured = _context.Insuranceds.FirstOrDefault(i => i.Id == id);
            if (existingInsured != null)
            {
                _context.Insuranceds.Remove(existingInsured);
                _context.SaveChanges();
                return new InsuranceTransactionDto
                {
                    Message = "Insured deleted successfully"
                };
            }

            return null;
        }

        public IEnumerable<InsuredDto> GetAll()
        {
            var insureds = _context.Insuranceds.ToList();
            return _mapper.Map<IEnumerable<InsuredDto>>(insureds);
        }

        public InsuredDto GetById(Guid id)
        {
            var insured = _context.Insuranceds.FirstOrDefault(i => i.Id == id);
            return _mapper.Map<InsuredDto>(insured);
        }

        public InsuranceTransactionDto Update(Guid id, UpdateInsuredDto updateInsuredDto)
        {
            var existingInsured = _context.Insuranceds.FirstOrDefault(i => i.Id == id);
            if (existingInsured != null)
            {
                _mapper.Map(updateInsuredDto, existingInsured);
                _context.SaveChanges();
                InsuredDto insu = _mapper.Map<InsuredDto>(updateInsuredDto);
                return new InsuranceTransactionDto
                {
                    Insured = insu,
                    Message = "Insured updated successfully"
                };
            }
            return null;
        }
    }
}
