using MyGuide.Domain.Services;
using MyGuide.Models;
using MyGuide.Repository;
using System.Collections.Generic;

namespace MyGuide.Services
{
    public class NomenclatureService : INomenclatureService
    {
        private NomenclatureRepository nomenclatureRepository;

        public NomenclatureService()
        {
            nomenclatureRepository = new NomenclatureRepository();
        }

        public List<Nomenclature> GetSettlements()
        {
            return nomenclatureRepository.GetSettlements();
        }

        public List<Nomenclature> GetCategories()
        {
            return nomenclatureRepository.GetCategories();
        }
    }
}
