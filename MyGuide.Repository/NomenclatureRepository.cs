using MyGuide.Models;
using System.Collections.Generic;

namespace MyGuide.Repository
{
    public class NomenclatureRepository : BaseRepository
    {
        public List<Nomenclature> GetSettlements()
        {
            return QueryMultiple<Nomenclature>("public.getsettlement");
        }

        public List<Nomenclature> GetCategories()
        {
            return QueryMultiple<Nomenclature>("public.getcategory");
        }
    }
}
