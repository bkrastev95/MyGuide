using Google.OrTools.ConstraintSolver;
using System;
using System.Collections.Generic;

namespace MyGuide.OrTools
{
    public class DistanceCallback : NodeEvaluator2
    {
        private readonly List<Point> points;

        public DistanceCallback(List<Point> points)
        {
            this.points = points;
        }

        public override long Run(int firstIndex, int secondIndex)
        {
            return ManhattanDistance(firstIndex, secondIndex);
        }

        private long ManhattanDistance(int firstIndex, int secondIndex)
        {
            var xDistance = Math.Abs(points[firstIndex].X - points[secondIndex].X) * 100000;
            var yDistance = Math.Abs(points[firstIndex].Y - points[secondIndex].Y) * 100000;

            return Convert.ToInt64(xDistance + yDistance);
        }
    }
}
