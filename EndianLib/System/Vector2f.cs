using System.ComponentModel;

namespace System
{
    [TypeConverter(typeof(Vector2fConverter))]
    public struct Vector2f
    {
        public const int SizeInBytes = 8;

        public static readonly Vector2f Identity = new Vector2f(1f, 1f);

        public static readonly Vector2f Max = new Vector2f(float.MaxValue, float.MaxValue);

        public static readonly Vector2f Min = new Vector2f(float.MinValue, float.MinValue);

        public float X { get; set; }

        public float Y { get; set; }

        public float this[int Index]
        {
            get
            {
                return Index switch
                {
                    0 => X,
                    1 => Y,
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
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }

        public Vector2f(float[] XY)
        {
            if (XY.Length != 2)
            {
                throw new ArgumentException("Length of array must be 2!");
            }

            X = XY[0];
            Y = XY[1];
        }

        public Vector2f(float X, float Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public float Length()
        {
            return (float)Math.Sqrt(X * X + Y * Y);
        }

        public Vector2f Normalize()
        {
            float num = Length();
            X /= num;
            Y /= num;
            return this;
        }

        public float Dot(Vector2f Right)
        {
            return X * Right.X + Y * Right.Y;
        }

        public static explicit operator double(Vector2f Left)
        {
            return Math.Sqrt(Left.X * Left.X + Left.Y * Left.Y);
        }

        public static explicit operator float(Vector2f Left)
        {
            return Left.Length();
        }

        public static explicit operator int(Vector2f Left)
        {
            return (int)Left.Length();
        }

        public static implicit operator float[](Vector2f Left)
        {
            return new float[2] { Left.X, Left.Y };
        }

        public static implicit operator Vector2f(float[] Left)
        {
            if (Left.Length != 2)
            {
                throw new ArgumentException("Length of array must be 2!");
            }

            return new Vector2f(Left);
        }

        public static explicit operator Vector3f(Vector2f Left)
        {
            return new Vector3f(Left.X, Left.Y, 0f);
        }

        public static Vector2f operator +(Vector2f Left, Vector2f Right)
        {
            return new Vector2f(Left.X + Right.X, Left.Y + Right.Y);
        }

        public static Vector2f operator +(Vector2f Left, Vector3f Right)
        {
            return new Vector2f(Left.X + Right.X, Left.Y + Right.Y);
        }

        public static Vector2f operator +(Vector2f Left, float Right)
        {
            return new Vector2f(Left.X + Right, Left.Y + Right);
        }

        public static Vector2f operator -(Vector2f Left, Vector2f Right)
        {
            return new Vector2f(Left.X - Right.X, Left.Y - Right.Y);
        }

        public static Vector2f operator -(Vector2f Left, Vector3f Right)
        {
            return new Vector2f(Left.X - Right.X, Left.Y - Right.Y);
        }

        public static Vector2f operator -(Vector2f Left, float Right)
        {
            return new Vector2f(Left.X - Right, Left.Y - Right);
        }

        public static Vector2f operator *(Vector2f Left, Vector2f Right)
        {
            return new Vector2f(Left.X * Right.X, Left.Y * Right.Y);
        }

        public static Vector2f operator *(Vector2f Left, Vector3f Right)
        {
            return new Vector2f(Left.X * Right.X, Left.Y * Right.Y);
        }

        public static Vector2f operator *(Vector2f Left, float Right)
        {
            return new Vector2f(Left.X * Right, Left.Y * Right);
        }

        public static Vector2f operator /(Vector2f Left, Vector2f Right)
        {
            return new Vector2f(Left.X / Right.X, Left.Y / Right.Y);
        }

        public static Vector2f operator /(Vector2f Left, Vector3f Right)
        {
            return new Vector2f(Left.X / Right.X, Left.Y / Right.Y);
        }

        public static Vector2f operator /(Vector2f Left, float Right)
        {
            return new Vector2f(Left.X / Right, Left.Y / Right);
        }

        public static Vector2f operator ++(Vector2f Left)
        {
            return new Vector2f(Left.X + 1f, Left.Y + 1f);
        }

        public static Vector2f operator --(Vector2f Left)
        {
            return new Vector2f(Left.X - 1f, Left.Y - 1f);
        }

        public static bool operator ==(Vector2f Left, Vector2f Right)
        {
            return Left.Equals(Right);
        }

        public static bool operator !=(Vector2f Left, Vector2f Right)
        {
            return !Left.Equals(Right);
        }

        public static bool operator <(Vector2f Left, Vector2f Right)
        {
            return (float)Left < (float)Right;
        }

        public static bool operator <=(Vector2f Left, Vector2f Right)
        {
            return (float)Left <= (float)Right;
        }

        public static bool operator >(Vector2f Left, Vector2f Right)
        {
            return (float)Left > (float)Right;
        }

        public static bool operator >=(Vector2f Left, Vector2f Right)
        {
            return (float)Left >= (float)Right;
        }

        public static int GetSize()
        {
            return 2;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Vector2f) || obj == null)
            {
                return false;
            }

            Vector2f vector2f = (Vector2f)obj;
            if (vector2f.X == X)
            {
                return vector2f.Y == Y;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return 17 * (23 + X.GetHashCode()) * (23 + Y.GetHashCode());
        }

        public override string ToString()
        {
            return "<" + X + ", " + Y + ">";
        }
    }
}