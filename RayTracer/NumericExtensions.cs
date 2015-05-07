using System;

namespace RayTracer
{
    public static class NumericExtensions
    {
        public static double ToRadians(this double val)
        {
            return (Math.PI / 180) * val;
        }
    }

    public static class Double
    {
        public const double TOLERANCE = 0.001;
    }
}