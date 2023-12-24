using AutoMapper;
using Beneficiary.Service.Data;
using Beneficiary.Service.Dto.Request;
using Beneficiary.Service.Dto.Response;
using Beneficiary.Service.Entities;
using Beneficiary.Service.Services.IService;
using System.Data.Entity;

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
            var existingInsured = _context.Insuranced.FirstOrDefault(i => i.Id == insuredId);
            if (existingInsured != null)
            {
                var insurance = _context.Insurances.FirstOrDefault(i => i.Id == addRelationDto.InsuranceId);
                _context.InsuredInsurances.Add(new InsuranceInsured { Insured = existingInsured, Insurance = insurance });
                _context.SaveChanges();
            }
        }

        public InsuranceTransactionDto Create(AddInsuredDto addInsuredDto)
        {
            var insured = _mapper.Map<Insured>(addInsuredDto);
            _context.Insuranced.Add(insured);
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
            _context.Insuranced.AddRange(insureds);
            _context.SaveChanges();
        }

        public InsuranceTransactionDto Delete(Guid id)
        {
            var existingInsured = _context.Insuranced.FirstOrDefault(i => i.Id == id);
            if (existingInsured != null)
            {
                _context.Insuranced.Remove(existingInsured);
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
            var insureds = _context.Insuranced.ToList();
            List<InsuredDto> response = new List<InsuredDto>();
            foreach (var insured in insureds)
            {
                List<InsuranceInsured> relations = _context.InsuredInsurances
                    .Include(r => r.Insurance)
                    .Where(r => r.InsuredId == insured.Id)
                    .ToList();
                List<Guid> ensuranceIds = relations.Select(r => r.InsuranceId).ToList();
                var insuredDto = _mapper.Map<InsuredDto>(insured);
                List<Insurance> ensurances = _context.Insurances
                    .Where(e => ensuranceIds.Contains(e.Id))
                    .ToList();


                insuredDto.Insurances = _mapper.Map<List<InsuranceDto>>(ensurances);
                response.Add(insuredDto);

            }
            return response;
        }

        public InsuredDto GetById(Guid id)
        {
            var insured = _context.Insuranced.FirstOrDefault(i => i.Id == id);

            List<InsuranceInsured> relations = _context.InsuredInsurances
                   .Include(r => r.Insurance)
                   .Where(r => r.InsuredId == insured.Id)
                   .ToList();
            List<Guid> ensuranceIds = relations.Select(r => r.InsuranceId).ToList();
            var insuredDto = _mapper.Map<InsuredDto>(insured);
            List<Insurance> ensurances = _context.Insurances
                .Where(e => ensuranceIds.Contains(e.Id))
                .ToList();


            insuredDto.Insurances = _mapper.Map<List<InsuranceDto>>(ensurances);



            return insuredDto;
        }

        public InsuranceTransactionDto Update(Guid id, UpdateInsuredDto updateInsuredDto)
        {
            var existingInsured = _context.Insuranced.FirstOrDefault(i => i.Id == id);
            if (existingInsured != null)
            {
                _mapper.Map(updateInsuredDto, existingInsured);
                _context.SaveChanges();
                InsuredDto insu = _mapper.Map<InsuredDto>(existingInsured);
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
