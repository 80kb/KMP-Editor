using System.ComponentModel;

namespace System
{
    [TypeConverter(typeof(Vector3fConverter))]
    public struct Vector3f : ICloneable, IComparable, IComparable<Vector3f>
    {
        public const int SizeInBytes = 12;

        public static readonly Vector3f Max = new Vector3f(float.MaxValue, float.MaxValue, float.MaxValue);

        public static readonly Vector3f Min = new Vector3f(float.MinValue, float.MinValue, float.MinValue);

        public static readonly Vector3f Identity = new Vector3f(1f, 1f, 1f);

        public static readonly Vector3f Zero = new Vector3f(0f, 0f, 0f);

        public static readonly Vector3f XAxis = new Vector3f(1f, 0f, 0f);

        public static readonly Vector3f YAxis = new Vector3f(0f, 1f, 0f);

        public static readonly Vector3f ZAxis = new Vector3f(0f, 0f, 1f);

        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        public float this[int Index]
        {
            get
            {
                return Index switch
                {
                    0 => X,
                    1 => Y,
                    2 => Z,
                    _ => throw new IndexOutOfRangeException(),
                };
            }
            set
            {
                switch (Index)
                {
                    case 0:
                        X = value;
                        break;
                    case 1:
                        Y = value;
                        break;
                    case 2:
                        Z = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }

        public Vector3f(float Value)
        {
            X = Value;
            Y = Value;
            Z = Value;
        }

        public Vector3f(float[] XYZ)
        {
            if (XYZ.Length != 3)
            {
                throw new ArgumentException("Length of array must be 3!");
            }

            X = XYZ[0];
            Y = XYZ[1];
            Z = XYZ[2];
        }

        public Vector3f(float X, float Y, float Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }

        public float Length()
        {
            return (float)Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        public Vector3f Normalize()
        {
            float num = Length();
            X /= num;
            Y /= num;
            Z /= num;
            return this;
        }

        public float Dot(Vector3f Right)
        {
            return X * Right.X + Y * Right.Y + Z * Right.Z;
        }

        public Vector3f Cross(Vector3f Right)
        {
            return new Vector3f(Y * Right.Z - Right.Y * Z, Z * Right.X - Right.Z * X, X * Right.Y - Right.X * Y);
        }

        public static implicit operator float[](Vector3f Left)
        {
            return new float[3] { Left.X, Left.Y, Left.Z };
        }

        public static implicit operator Vector3f(float[] Left)
        {
            if (Left.Length != 3)
            {
                throw new ArgumentException("Length of array must be 3!");
            }

            return new Vector3f(Left);
        }

        public static explicit operator Vector2f(Vector3f Left)
        {
            return new Vector2f(Left.X, Left.Y);
        }

        public static Vector3f operator +(Vector3f Left, Vector3f Right)
        {
            return new Vector3f(Left.X + Right.X, Left.Y + Right.Y, Left.Z + Right.Z);
        }

        public static Vector3f operator +(Vector3f Left, Vector2f Right)
        {
            return new Vector3f(Left.X + Right.X, Left.Y + Right.Y, Left.Z);
        }

        public static Vector3f operator +(Vector3f Left, float Right)
        {
            return new Vector3f(Left.X + Right, Left.Y + Right, Left.Z + Right);
        }

        public static Vector3f operator -(Vector3f Left, Vector3f Right)
        {
            return new Vector3f(Left.X - Right.X, Left.Y - Right.Y, Left.Z - Right.Z);
        }

        public static Vector3f operator -(Vector3f Left, Vector2f Right)
        {
            return new Vector3f(Left.X - Right.X, Left.Y - Right.Y, Left.Z);
        }

        public static Vector3f operator -(Vector3f Left, float Right)
        {
            return new Vector3f(Left.X - Right, Left.Y - Right, Left.Z - Right);
        }

        public static Vector3f operator -(Vector3f Left)
        {
            return new Vector3f(0f - Left.X, 0f - Left.Y, 0f - Left.Z);
        }

        public static Vector3f operator *(Vector3f Left, Vector3f Right)
        {
            return new Vector3f(Left.X * Right.X, Left.Y * Right.Y, Left.Z * Right.Z);
        }

        public static Vector3f operator *(Vector3f Left, Vector2f Right)
        {
            return new Vector3f(Left.X * Right.X, Left.Y * Right.Y, Left.Z);
        }

        public static Vector3f operator *(Vector3f Left, float Right)
        {
            return new Vector3f(Left.X * Right, Left.Y * Right, Left.Z * Right);
        }

        public static Vector3f operator /(Vector3f Left, Vector3f Right)
        {
            return new Vector3f(Left.X / Right.X, Left.Y / Right.Y, Left.Z / Right.Z);
        }

        public static Vector3f operator /(Vector3f Left, Vector2f Right)
        {
            return new Vector3f(Left.X / Right.X, Left.Y / Right.Y, Left.Z);
        }

        public static Vector3f operator /(Vector3f Left, float Right)
        {
            return new Vector3f(Left.X / Right, Left.Y / Right, Left.Z / Right);
        }

        public static Vector3f operator /(float Left, Vector3f Right)
        {
            return Right / Left;
        }

        public static Vector3f operator ++(Vector3f Left)
        {
            return new Vector3f(Left.X + 1f, Left.Y + 1f, Left.Z + 1f);
        }

        public static Vector3f operator --(Vector3f Left)
        {
            return new Vector3f(Left.X - 1f, Left.Y - 1f, Left.Z - 1f);
        }

        public static bool operator ==(Vector3f Left, Vector3f Right)
        {
            if (Left.X == Right.X && Left.Y == Right.Y)
            {
                return Left.Z == Right.Z;
            }

            return false;
        }

        public static bool operator !=(Vector3f Left, Vector3f Right)
        {
            if (Left.X == Right.X && Left.Y == Right.Y)
            {
                return Left.Z != Right.Z;
            }

            return true;
        }

        public static bool operator <(Vector3f Left, Vector3f Right)
        {
            return Left.Length() < Right.Length();
        }

        public static bool operator <=(Vector3f Left, Vector3f Right)
        {
            return Left.Length() <= Right.Length();
        }

        public static bool operator >(Vector3f Left, Vector3f Right)
        {
            return Left.Length() > Right.Length();
        }

        public static bool operator >=(Vector3f Left, Vector3f Right)
        {
            return Left.Length() >= Right.Length();
        }

        public static int GetSize()
        {
            return 3;
        }

        public Vector3f ToXZY()
        {
            return new Vector3f(X, Z, 0f - Y);
        }

        public Vector3f ToXYZ()
        {
            return new Vector3f(X, 0f - Z, Y);
        }

        public object Clone()
        {
            return new Vector3f(X, Y, Z);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Vector3f))
            {
                return false;
            }

            Vector3f vector3f = (Vector3f)obj;
            if (vector3f.X == X && vector3f.Y == Y)
            {
                return vector3f.Z == Z;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return 17 * (23 + X.GetHashCode()) * (23 + Y.GetHashCode()) * (23 + Z.GetHashCode());
        }

        public override string ToString()
        {
            return $"<{X}, {Y}, {Z}>";
        }

        public int CompareTo(object Obj)
        {
            Vector3f vector3f = (Vector3f)Obj;
            return Length().CompareTo(vector3f.Length());
        }

        public int CompareTo(Vector3f Other)
        {
            return Length().CompareTo(Other.Length());
        }
    }
}