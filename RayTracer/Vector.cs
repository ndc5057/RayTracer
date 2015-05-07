using System;

namespace RayTracer
{
    public class Vector3
    {
        private double _x;
        private double _y;
        private double _z;

        public double X
        {
            get { return _x; }
            set { _x = value; }
        }

        public double Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public double Z
        {
            get { return _z; }
            set { _z = value; }
        }

        public double Length 
        {
            get { return Math.Sqrt(_x * _x + _y * _y + _z * _z); }
        }

        public Vector3(double x, double y, double z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        public static bool operator ==(Vector3 vector1, Vector3 vector2)
        {
            if (vector1 == null && vector2 == null)
                return true;

            if (vector1 == null || vector2 == null)
                return false;

            return (Math.Abs(vector1.X - vector2.X) < Double.TOLERANCE
                    && Math.Abs(vector1.Y - vector2.Y) < Double.TOLERANCE
                    && Math.Abs(vector1.Z - vector2.Z) < Double.TOLERANCE);
        }

        public static bool operator !=(Vector3 vector1, Vector3 vector2)
        {
            return !(vector1 == vector2);
        }

        public static Vector3 operator +(Vector3 vector1, Vector3 vector2)
        {
            return new Vector3(vector1.X + vector2.X, 
                              vector1.Y + vector2.Y, 
                              vector1.Z + vector2.Z);
        }

        public static Vector3 operator -(Vector3 vector1, Vector3 vector2)
        {
            return new Vector3(vector1.X - vector2.X,
                              vector1.Y - vector2.Y,
                              vector1.Z - vector2.Z);
        }
        
        public static Vector3 operator *(Vector3 vector3, double scalar)
        {
            return new Vector3(vector3.X * scalar,
                              vector3.Y * scalar,
                              vector3.Z * scalar);
        }

        public static Vector3 operator *(double scalar, Vector3 vector3)
        {
            return vector3*scalar;
        }

        public static Vector3 operator *(Vector3 vector1, Vector3 vector2)
        {
            return new Vector3(vector1.Y*vector2.Z - vector1.Z*vector2.Y,
                               vector1.Z*vector2.X - vector1.X*vector2.Z,
                               vector1.X*vector2.Y - vector1.Y*vector2.X);
        }

        public static Vector3 operator /(Vector3 vector3, double scalar)
        {
            return new Vector3(vector3.X * (1.0/scalar),
                              vector3.Y * (1.0/scalar),
                              vector3.Z * (1.0/scalar));
        }

        public static double DotProduct(Vector3 vector1, Vector3 vector2)
        {
            return vector1.X*vector2.X + vector1.Y*vector2.Y + vector1.Z*vector2.Z;
        }

        public static Vector3 Normalize(Vector3 vector)
        {
            var length = vector.Length;

            if (Math.Abs(length) < Double.TOLERANCE)
                return vector;

            return new Vector3(vector.X /= length,
                               vector.Y /= length,
                               vector.Z /= length);
        }


        protected bool Equals(Vector3 vector2)
        {
            return X.Equals(vector2.X) && Y.Equals(vector2.Y) && Z.Equals(vector2.Z);
        }


        public void Normalize()
        {
            var length = Length;

            if (Math.Abs(length) < Double.TOLERANCE)
                return;

            _x /= length;
            _y /= length;
            _z /= length;
        }


        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Vector3)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Z.GetHashCode();
                return hashCode;
            }
        }
    }
}
