using System;

namespace RayTracer.SceneObjects
{
    class Plane : SceneObject
    {
        private Vector3 _normal;

        public Vector3 Normal
        {
            get { return _normal; }
            set
            {
                _normal = Vector3.Normalize(value);
            }
        }

        public double Distance { get; set; }

        public override double Intersect(Ray ray)
        {
            var denominator = Vector3.DotProduct(_normal, ray.Direction);
            if (Math.Abs(denominator) < Double.TOLERANCE)
                return double.PositiveInfinity;

            var t = -(Vector3.DotProduct(_normal, ray.Origin) + Distance) / denominator;

            return t > 0 ? t : double.PositiveInfinity;
        }
    }
}
