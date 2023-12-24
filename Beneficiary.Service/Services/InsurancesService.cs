using AutoMapper;
using Beneficiary.Service.Data;
using Beneficiary.Service.Dto.Request;
using Beneficiary.Service.Entities;
using Beneficiary.Service.Services.IService;

namespace Beneficiary.Service.Services
{
    public class InsurancesService : IInsuranceService
    {
        private readonly IMapper _mapper;
        private readonly AppDBContext _context;

        public InsurancesService(
            IMapper mapper,
            AppDBContext context
        )
        {
            _mapper = mapper;
            _context = context;
        }

        public void Create(AddInsuranceDto addInsuranceDto)
        {
            Insurance insurance = _mapper.Map<Insurance>(addInsuranceDto);
            _context.Insurances.Add(insurance);
            _context.SaveChanges();
        }

        public void CreateMultiple(List<AddInsuranceDto> addInsuranceDtos)
        {
            List<Insurance> insurances = _mapper.Map<List<Insurance>>(addInsuranceDtos);
            _context.Insurances.AddRange(insurances);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var insuranceToDelete = _context.Insurances.FirstOrDefault(i => i.Id == id);

            if (insuranceToDelete != null)
            {
                // Eliminar todas las relaciones asociadas al seguro
                var insuredInsuranceToDelete = _context.InsuredInsurances.Where(ii => ii.InsuranceId == id);
                _context.InsuredInsurances.RemoveRange(insuredInsuranceToDelete);

                // Eliminar el seguro
                _context.Insurances.Remove(insuranceToDelete);

                // Guardar cambios
                _context.SaveChanges();
            }
        }
    }
}
