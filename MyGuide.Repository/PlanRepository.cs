using MyGuide.Models;
using System.Collections.Generic;

namespace MyGuide.Repository
{
    public class PlanRepository : BaseRepository
    {
        public List<Destination> SearchDestinations(long? cityId, long? categoryId, string keywords)
        {
            return QueryMultiple<Destination>("public.searchdest", new {
                cityid = cityId,
                categoryid = categoryId,
                keywords = string.IsNullOrEmpty(keywords) ? null : keywords
            });
        }
    }
}
