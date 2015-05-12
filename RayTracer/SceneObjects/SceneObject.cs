namespace RayTracer.SceneObjects
{
    public abstract class SceneObject
    {
        public Surface Surface { get; set; } = new Surface();

        public abstract Intersection Intersect(Ray ray);
        public abstract Vector3 GetNormal(Vector3 position);
    }
}