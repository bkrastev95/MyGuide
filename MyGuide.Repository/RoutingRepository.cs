using System;
using MyGuide.Models;
using System.Linq;
using Npgsql;
using System.Data;
using System.Collections.Generic;

namespace MyGuide.Repository
{
    public class RoutingRepository : BaseRepository
    {
        public Destination GetHomeCoordinates(long homeId)
        {
            return Query<Destination>("public.gethomecoords", new { homeid = homeId });
        }

        public List<Route> GetRoutes(long userId)
        {
            return QueryMultiple<Route>("public.getuserroutes", new { puserid = 1 });
        }

        public void SaveRoute(Route lastRoute)
        {

            var command = new NpgsqlCommand("public.saveroute");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new NpgsqlParameter
            {
                ParameterName = "userid",
                NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Bigint,
                NpgsqlValue = lastRoute.UserId
            });

            command.Parameters.Add(new NpgsqlParameter
            {
                ParameterName = "homeid",
                NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Bigint,
                NpgsqlValue = lastRoute.HomeId
            });

            command.Parameters.Add(new NpgsqlParameter
            {
                ParameterName = "date",
                NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Timestamp,
                NpgsqlValue = lastRoute.Date
            });

            command.Parameters.Add(new NpgsqlParameter
            {
                ParameterName = "destinations",
                NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array | NpgsqlTypes.NpgsqlDbType.Bigint,
                NpgsqlValue = lastRoute.Destinations.Select(d => d.Id).ToArray()
            });

            using (var conn = Connection)
            {
                conn.Open();
                command.Connection = conn as NpgsqlConnection;

                command.ExecuteNonQuery();
            }

            //Execute(
            //    "public.saveroute",
            //    new
            //    {
            //        userid = lastRoute.UserId,
            //        date = lastRoute.Date,
            //        destinations = lastRoute.Destinations.Select(d => d.Id).ToArray()
            //    });
        }

        public List<Destination> GetDestinationsByRouteId(long id)
        {
            return QueryMultiple<Destination>("public.getdestbyrouteid", new { routeid = id });
        }
    }
}
