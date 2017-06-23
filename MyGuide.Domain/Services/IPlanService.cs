using MyGuide.Models;
using System.Collections.Generic;

namespace MyGuide.Domain.Services
{
    public interface IPlanService
    {
        List<Destination> SearchDestinations(long? cityId, long? categoryId, string keywords);
    }
}
