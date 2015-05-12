using System;

namespace RayTracer.SceneObjects
{
    class Triangle : SceneObject
    {
        Vector3 Point1 { get; set; }
        Vector3 Point2 { get; set; }
        Vector3 Point3 { get; set; }

        public override Intersection Intersect(Ray ray)
        {
            throw new NotImplementedException();
        }

        public override Vector3 GetNormal(Vector3 position)
        {
            throw new NotImplementedException();
        }
    }
}
