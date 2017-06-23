using MyGuide.Models;
using System.Collections.Generic;

namespace MyGuide.Domain.Services
{
    public interface IRoutingService
    {
        Destination GetHomeCoordinates(long homeId);

        List<Route> GetRoutes(long userId);

        void SaveRoute(Route lastRoute);

        List<Destination> GetDestinationsByRouteId(long id);
    }
}
