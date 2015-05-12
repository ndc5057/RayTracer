using RayTracer.SceneObjects;

namespace RayTracer
{
    public class Intersection
    {
        public SceneObject Object { get; set; }
        public Ray DirectionRay { get; set; }
        public double Distance { get; set; }
    }
}
