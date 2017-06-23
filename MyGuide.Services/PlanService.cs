using MyGuide.Domain.Services;
using MyGuide.Models;
using MyGuide.Repository;
using System.Collections.Generic;

namespace MyGuide.Services
{
    public class PlanService : IPlanService
    {
        private PlanRepository planRepository;

        public PlanService()
        {
            planRepository = new PlanRepository();
        }

        public List<Destination> SearchDestinations(long? cityId, long? categoryId, string keywords)
        {
            string keywordsToQuery = KeywordsToTsQuery(keywords);

            return planRepository.SearchDestinations(cityId, categoryId, keywordsToQuery);
        }

        private string KeywordsToTsQuery(string keywords)
        {
            if (string.IsNullOrEmpty(keywords))
            {
                return null;
            }

            string keywordsToQuery = string.Empty;
            var keys = keywords.Split(' ', ',');

            foreach (var key in keys)
            {
                keywordsToQuery += $"{key} |";
            }

            return keywordsToQuery.Substring(0, keywordsToQuery.LastIndexOf('|'));
        }
    }
}
