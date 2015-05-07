using System;

namespace RayTracer.SceneObjects
{
    public class Sphere : SceneObject
    {
        public Vector3 CenterPos { get; set; }
        public double Radius { get; set; }

        public override double Intersect(Ray ray)
        {
            //Calculate using Quadratic formula
            //  http://www.csee.umbc.edu/~olano/435f02/ray-sphere.html
            var intersectDistance = double.PositiveInfinity;
            var centerDistance = (ray.Origin - CenterPos);

            double b = Vector3.DotProduct(ray.Direction,centerDistance);
            double c = Vector3.DotProduct(centerDistance, centerDistance) - Radius*Radius;

            var discriminant = b*b - c;
            if (discriminant < 0)
                return intersectDistance;

            var sqrtDiscriminant = Math.Sqrt(discriminant);
            double t0 = -b - sqrtDiscriminant;
            double t1 = -b + sqrtDiscriminant;

            if (t0 > 0.01 && t0 < t1)
            {
                intersectDistance = t0;
            }
            if (t1 > 0.01 && t1 < t0)
            {
                intersectDistance = t1;
            }

            return intersectDistance;
        }
    }
}
