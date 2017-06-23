using MyGuide.Models;
using System.Collections.Generic;

namespace MyGuide.Domain.Services
{
    public interface INomenclatureService
    {
        List<Nomenclature> GetSettlements();

        List<Nomenclature> GetCategories();
    }
}
