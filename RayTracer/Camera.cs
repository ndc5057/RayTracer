namespace RayTracer
{
    public class Camera
    {
        public Vector3 Pos { get; set; }
        public Vector3 Forward { get; set; }
        public Vector3 Up { get; set; }
        public Vector3 Right { get; set; }
        public double FieldOfViewY { get; set; }

        public static Camera Create(Vector3 pos, Vector3 lookAt, Vector3 up, double fov)
        {
            var forward = Vector3.Normalize(lookAt - pos);
            var right = Vector3.Normalize(forward*Vector3.Normalize(up));

            return new Camera
            {
                Pos = pos,
                Forward = forward,
                Up = up,
                Right = right,
                FieldOfViewY = fov
            };
        }
    }
}