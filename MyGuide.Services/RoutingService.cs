using System.Collections.Generic;
using MyGuide.Domain.Services;
using MyGuide.Repository;
using MyGuide.Models;
using System;

namespace MyGuide.Services
{
    public class RoutingService : IRoutingService
    {
        private RoutingRepository routingRepository;

        public RoutingService()
        {
            routingRepository = new RoutingRepository();
        }

        public Destination GetHomeCoordinates(long homeId)
        {
            return routingRepository.GetHomeCoordinates(homeId);
        }

        public List<Route> GetRoutes(long userId)
        {
            return routingRepository.GetRoutes(userId);
        }

        public void SaveRoute(Route lastRoute)
        {
            routingRepository.SaveRoute(lastRoute);
        }

        public List<Destination> GetDestinationsByRouteId(long id)
        {
            return routingRepository.GetDestinationsByRouteId(id);
        }
    }
}
