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

        public override Intersection Intersect(Ray ray)
        {
            var denominator = Vector3.DotProduct(_normal, ray.Direction);
            if (Math.Abs(denominator) < Double.TOLERANCE)
                return new Intersection
                {
                    Object = this,
                    Distance = double.PositiveInfinity,
                    DirectionRay = ray
                };

            var t = -(Vector3.DotProduct(_normal, ray.Origin) + Distance) / denominator;

            var dist =  t > 0 ? t : double.PositiveInfinity;
            return new Intersection
            {
                Object = this,
                Distance = dist,
                DirectionRay = ray
            };
        }

        public override Vector3 GetNormal(Vector3 position)
        {
            return Normal;
        }
    }
}
