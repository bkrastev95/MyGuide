using Google.OrTools.ConstraintSolver;
using MyGuide.Domain.Services;
using MyGuide.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyGuide.OrTools
{
    public class TspService : ITspService
    {
        public List<Destination> SolveTsp(List<Destination> destinations)
        {
            var tspSize = destinations.Count;
            // One route because there is one traveller:
            var numberOfRoutes = 1;
            // The homet town is inserted at position 0 in the list, so the "depot" index is 0:
            var depotIndex = 0;

            // Compose the model:
            var model = new RoutingModel(tspSize, numberOfRoutes, depotIndex);

            // Setup search params
            var searchParameters = RoutingModel.DefaultSearchParameters();
            // Find a first solution with the cheapest addition algorithm:
            searchParameters.FirstSolutionStrategy = FirstSolutionStrategy.Types.Value.PathCheapestArc;
            // Use GLS for local search metaheuristic:
            searchParameters.LocalSearchMetaheuristic = LocalSearchMetaheuristic.Types.Value.GuidedLocalSearch;
            // Lower valuest make the local search more fine-grained
            searchParameters.GuidedLocalSearchLambdaCoefficient = 0.1;
            // The calculations are given one second to perform:
            searchParameters.TimeLimitMs = 1000;

            //Setup arc costs evaluation:
            var arcCostEvaluator = new DistanceCallback(destinations.Select(d => new Point { X = d.X, Y = d.Y }).ToList());
            model.SetArcCostEvaluatorOfAllVehicles(arcCostEvaluator);

            // Solve the problem:
            var solution = model.SolveWithParameters(searchParameters);

            if (solution != null)
            {
                return ExtractWaybillsFromSolution(solution, model, destinations);
            }

            return new List<Destination>();
        }

        private List<Destination> ExtractWaybillsFromSolution(Assignment solution, RoutingModel model, List<Destination> destinations)
        {
            var result = new List<Destination>();
            var index = model.Start(0); 
            
            while (!model.IsEnd(index))
            {
                if (index < destinations.Count)
                {
                    var currentDestination = destinations[(int)index];
                    
                    result.Add(currentDestination);
                }
                
                index = solution.Value(model.NextVar(index));
            }
            
            return result;
        }
    }
}
