#if(!XNA)

#region License

/*
MIT License
Copyright © 2006 The Mono.Xna Team

All rights reserved.

Authors:
 * Alan McGovern

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

#endregion License

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.Xna.Framework
{
    [StructLayout(LayoutKind.Sequential)]
    public struct FVector3 : IEquatable<FVector3>
    {
        #region Private Fields

        private static FVector3 zero = new FVector3(0f, 0f, 0f);
        private static FVector3 one = new FVector3(1f, 1f, 1f);
        private static FVector3 unitX = new FVector3(1f, 0f, 0f);
        private static FVector3 unitY = new FVector3(0f, 1f, 0f);
        private static FVector3 unitZ = new FVector3(0f, 0f, 1f);
        private static FVector3 up = new FVector3(0f, 1f, 0f);
        private static FVector3 down = new FVector3(0f, -1f, 0f);
        private static FVector3 right = new FVector3(1f, 0f, 0f);
        private static FVector3 left = new FVector3(-1f, 0f, 0f);
        private static FVector3 forward = new FVector3(0f, 0f, -1f);
        private static FVector3 backward = new FVector3(0f, 0f, 1f);

        #endregion Private Fields

        #region Public Fields

        public float X;
        public float Y;
        public float Z;

        #endregion Public Fields

        #region Properties

        public static FVector3 Zero
        {
            get { return zero; }
        }

        public static FVector3 One
        {
            get { return one; }
        }

        public static FVector3 UnitX
        {
            get { return unitX; }
        }

        public static FVector3 UnitY
        {
            get { return unitY; }
        }

        public static FVector3 UnitZ
        {
            get { return unitZ; }
        }

        public static FVector3 Up
        {
            get { return up; }
        }

        public static FVector3 Down
        {
            get { return down; }
        }

        public static FVector3 Right
        {
            get { return right; }
        }

        public static FVector3 Left
        {
            get { return left; }
        }

        public static FVector3 Forward
        {
            get { return forward; }
        }

        public static FVector3 Backward
        {
            get { return backward; }
        }

        #endregion Properties

        #region Constructors

        public FVector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }


        public FVector3(float value)
        {
            X = value;
            Y = value;
            Z = value;
        }


        public FVector3(FVector2 value, float z)
        {
            X = value.X;
            Y = value.Y;
            Z = z;
        }

        #endregion Constructors

        #region Public Methods

        public static FVector3 Add(FVector3 value1, FVector3 value2)
        {
            value1.X += value2.X;
            value1.Y += value2.Y;
            value1.Z += value2.Z;
            return value1;
        }

        public static void Add(ref FVector3 value1, ref FVector3 value2, out FVector3 result)
        {
            result.X = value1.X + value2.X;
            result.Y = value1.Y + value2.Y;
            result.Z = value1.Z + value2.Z;
        }

        public static FVector3 Barycentric(FVector3 value1, FVector3 value2, FVector3 value3, float amount1, float amount2)
        {
            return new FVector3(
                MathHelper.Barycentric(value1.X, value2.X, value3.X, amount1, amount2),
                MathHelper.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2),
                MathHelper.Barycentric(value1.Z, value2.Z, value3.Z, amount1, amount2));
        }

        public static void Barycentric(ref FVector3 value1, ref FVector3 value2, ref FVector3 value3, float amount1,
                                       float amount2, out FVector3 result)
        {
            result = new FVector3(
                MathHelper.Barycentric(value1.X, value2.X, value3.X, amount1, amount2),
                MathHelper.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2),
                MathHelper.Barycentric(value1.Z, value2.Z, value3.Z, amount1, amount2));
        }

        public static FVector3 CatmullRom(FVector3 value1, FVector3 value2, FVector3 value3, FVector3 value4, float amount)
        {
            return new FVector3(
                MathHelper.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount),
                MathHelper.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount),
                MathHelper.CatmullRom(value1.Z, value2.Z, value3.Z, value4.Z, amount));
        }

        public static void CatmullRom(ref FVector3 value1, ref FVector3 value2, ref FVector3 value3, ref FVector3 value4,
                                      float amount, out FVector3 result)
        {
            result = new FVector3(
                MathHelper.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount),
                MathHelper.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount),
                MathHelper.CatmullRom(value1.Z, value2.Z, value3.Z, value4.Z, amount));
        }

        public static FVector3 Clamp(FVector3 value1, FVector3 min, FVector3 max)
        {
            return new FVector3(
                MathHelper.Clamp(value1.X, min.X, max.X),
                MathHelper.Clamp(value1.Y, min.Y, max.Y),
                MathHelper.Clamp(value1.Z, min.Z, max.Z));
        }

        public static void Clamp(ref FVector3 value1, ref FVector3 min, ref FVector3 max, out FVector3 result)
        {
            result = new FVector3(
                MathHelper.Clamp(value1.X, min.X, max.X),
                MathHelper.Clamp(value1.Y, min.Y, max.Y),
                MathHelper.Clamp(value1.Z, min.Z, max.Z));
        }

        public static FVector3 Cross(FVector3 vector1, FVector3 vector2)
        {
            Cross(ref vector1, ref vector2, out vector1);
            return vector1;
        }

        public static void Cross(ref FVector3 vector1, ref FVector3 vector2, out FVector3 result)
        {
            result = new FVector3(vector1.Y*vector2.Z - vector2.Y*vector1.Z,
                                 -(vector1.X*vector2.Z - vector2.X*vector1.Z),
                                 vector1.X*vector2.Y - vector2.X*vector1.Y);
        }

        public static float Distance(FVector3 vector1, FVector3 vector2)
        {
            float result;
            DistanceSquared(ref vector1, ref vector2, out result);
            return (float) Math.Sqrt(result);
        }

        public static void Distance(ref FVector3 value1, ref FVector3 value2, out float result)
        {
            DistanceSquared(ref value1, ref value2, out result);
            result = (float) Math.Sqrt(result);
        }

        public static float DistanceSquared(FVector3 value1, FVector3 value2)
        {
            float result;
            DistanceSquared(ref value1, ref value2, out result);
            return result;
        }

        public static void DistanceSquared(ref FVector3 value1, ref FVector3 value2, out float result)
        {
            result = (value1.X - value2.X)*(value1.X - value2.X) +
                     (value1.Y - value2.Y)*(value1.Y - value2.Y) +
                     (value1.Z - value2.Z)*(value1.Z - value2.Z);
        }

        public static FVector3 Divide(FVector3 value1, FVector3 value2)
        {
            value1.X /= value2.X;
            value1.Y /= value2.Y;
            value1.Z /= value2.Z;
            return value1;
        }

        public static FVector3 Divide(FVector3 value1, float value2)
        {
            float factor = 1/value2;
            value1.X *= factor;
            value1.Y *= factor;
            value1.Z *= factor;
            return value1;
        }

        public static void Divide(ref FVector3 value1, float divisor, out FVector3 result)
        {
            float factor = 1/divisor;
            result.X = value1.X*factor;
            result.Y = value1.Y*factor;
            result.Z = value1.Z*factor;
        }

        public static void Divide(ref FVector3 value1, ref FVector3 value2, out FVector3 result)
        {
            result.X = value1.X/value2.X;
            result.Y = value1.Y/value2.Y;
            result.Z = value1.Z/value2.Z;
        }

        public static float Dot(FVector3 vector1, FVector3 vector2)
        {
            return vector1.X*vector2.X + vector1.Y*vector2.Y + vector1.Z*vector2.Z;
        }

        public static void Dot(ref FVector3 vector1, ref FVector3 vector2, out float result)
        {
            result = vector1.X*vector2.X + vector1.Y*vector2.Y + vector1.Z*vector2.Z;
        }

        public override bool Equals(object obj)
        {
            return (obj is FVector3) ? this == (FVector3) obj : false;
        }

        public bool Equals(FVector3 other)
        {
            return this == other;
        }

        public override int GetHashCode()
        {
            return (int) (X + Y + Z);
        }

        public static FVector3 Hermite(FVector3 value1, FVector3 tangent1, FVector3 value2, FVector3 tangent2, float amount)
        {
            FVector3 result = new FVector3();
            Hermite(ref value1, ref tangent1, ref value2, ref tangent2, amount, out result);
            return result;
        }

        public static void Hermite(ref FVector3 value1, ref FVector3 tangent1, ref FVector3 value2, ref FVector3 tangent2,
                                   float amount, out FVector3 result)
        {
            result.X = MathHelper.Hermite(value1.X, tangent1.X, value2.X, tangent2.X, amount);
            result.Y = MathHelper.Hermite(value1.Y, tangent1.Y, value2.Y, tangent2.Y, amount);
            result.Z = MathHelper.Hermite(value1.Z, tangent1.Z, value2.Z, tangent2.Z, amount);
        }

        public float Length()
        {
            float result;
            DistanceSquared(ref this, ref zero, out result);
            return (float) Math.Sqrt(result);
        }

        public float LengthSquared()
        {
            float result;
            DistanceSquared(ref this, ref zero, out result);
            return result;
        }

        public static FVector3 Lerp(FVector3 value1, FVector3 value2, float amount)
        {
            return new FVector3(
                MathHelper.Lerp(value1.X, value2.X, amount),
                MathHelper.Lerp(value1.Y, value2.Y, amount),
                MathHelper.Lerp(value1.Z, value2.Z, amount));
        }

        public static void Lerp(ref FVector3 value1, ref FVector3 value2, float amount, out FVector3 result)
        {
            result = new FVector3(
                MathHelper.Lerp(value1.X, value2.X, amount),
                MathHelper.Lerp(value1.Y, value2.Y, amount),
                MathHelper.Lerp(value1.Z, value2.Z, amount));
        }

        public static FVector3 Max(FVector3 value1, FVector3 value2)
        {
            return new FVector3(
                MathHelper.Max(value1.X, value2.X),
                MathHelper.Max(value1.Y, value2.Y),
                MathHelper.Max(value1.Z, value2.Z));
        }

        public static void Max(ref FVector3 value1, ref FVector3 value2, out FVector3 result)
        {
            result = new FVector3(
                MathHelper.Max(value1.X, value2.X),
                MathHelper.Max(value1.Y, value2.Y),
                MathHelper.Max(value1.Z, value2.Z));
        }

        public static FVector3 Min(FVector3 value1, FVector3 value2)
        {
            return new FVector3(
                MathHelper.Min(value1.X, value2.X),
                MathHelper.Min(value1.Y, value2.Y),
                MathHelper.Min(value1.Z, value2.Z));
        }

        public static void Min(ref FVector3 value1, ref FVector3 value2, out FVector3 result)
        {
            result = new FVector3(
                MathHelper.Min(value1.X, value2.X),
                MathHelper.Min(value1.Y, value2.Y),
                MathHelper.Min(value1.Z, value2.Z));
        }

        public static FVector3 Multiply(FVector3 value1, FVector3 value2)
        {
            value1.X *= value2.X;
            value1.Y *= value2.Y;
            value1.Z *= value2.Z;
            return value1;
        }

        public static FVector3 Multiply(FVector3 value1, float scaleFactor)
        {
            value1.X *= scaleFactor;
            value1.Y *= scaleFactor;
            value1.Z *= scaleFactor;
            return value1;
        }

        public static void Multiply(ref FVector3 value1, float scaleFactor, out FVector3 result)
        {
            result.X = value1.X*scaleFactor;
            result.Y = value1.Y*scaleFactor;
            result.Z = value1.Z*scaleFactor;
        }

        public static void Multiply(ref FVector3 value1, ref FVector3 value2, out FVector3 result)
        {
            result.X = value1.X*value2.X;
            result.Y = value1.Y*value2.Y;
            result.Z = value1.Z*value2.Z;
        }

        public static FVector3 Negate(FVector3 value)
        {
            value = new FVector3(-value.X, -value.Y, -value.Z);
            return value;
        }

        public static void Negate(ref FVector3 value, out FVector3 result)
        {
            result = new FVector3(-value.X, -value.Y, -value.Z);
        }

        public void Normalize()
        {
            Normalize(ref this, out this);
        }

        public static FVector3 Normalize(FVector3 vector)
        {
            Normalize(ref vector, out vector);
            return vector;
        }

        public static void Normalize(ref FVector3 value, out FVector3 result)
        {
            float factor;
            Distance(ref value, ref zero, out factor);
            factor = 1f/factor;
            result.X = value.X*factor;
            result.Y = value.Y*factor;
            result.Z = value.Z*factor;
        }

        public static FVector3 Reflect(FVector3 vector, FVector3 normal)
        {
            FVector3 result;
            Reflect(ref vector, ref normal, out result);
            return result;
        }

        public static void Reflect(ref FVector3 vector, ref FVector3 normal, out FVector3 result)
        {
            float dot = Dot(vector, normal);
            result.X = vector.X - ((2f*dot)*normal.X);
            result.Y = vector.Y - ((2f*dot)*normal.Y);
            result.Z = vector.Z - ((2f*dot)*normal.Z);
        }

        public static FVector3 SmoothStep(FVector3 value1, FVector3 value2, float amount)
        {
            return new FVector3(
                MathHelper.SmoothStep(value1.X, value2.X, amount),
                MathHelper.SmoothStep(value1.Y, value2.Y, amount),
                MathHelper.SmoothStep(value1.Z, value2.Z, amount));
        }

        public static void SmoothStep(ref FVector3 value1, ref FVector3 value2, float amount, out FVector3 result)
        {
            result = new FVector3(
                MathHelper.SmoothStep(value1.X, value2.X, amount),
                MathHelper.SmoothStep(value1.Y, value2.Y, amount),
                MathHelper.SmoothStep(value1.Z, value2.Z, amount));
        }

        public static FVector3 Subtract(FVector3 value1, FVector3 value2)
        {
            value1.X -= value2.X;
            value1.Y -= value2.Y;
            value1.Z -= value2.Z;
            return value1;
        }

        public static void Subtract(ref FVector3 value1, ref FVector3 value2, out FVector3 result)
        {
            result.X = value1.X - value2.X;
            result.Y = value1.Y - value2.Y;
            result.Z = value1.Z - value2.Z;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(32);
            sb.Append("{X:");
            sb.Append(X);
            sb.Append(" Y:");
            sb.Append(Y);
            sb.Append(" Z:");
            sb.Append(Z);
            sb.Append("}");
            return sb.ToString();
        }

        public static FVector3 Transform(FVector3 position, FMatrix matrix)
        {
            Transform(ref position, ref matrix, out position);
            return position;
        }

        public static void Transform(ref FVector3 position, ref FMatrix matrix, out FVector3 result)
        {
            result =
                new FVector3((position.X*matrix.M11) + (position.Y*matrix.M21) + (position.Z*matrix.M31) + matrix.M41,
                            (position.X*matrix.M12) + (position.Y*matrix.M22) + (position.Z*matrix.M32) + matrix.M42,
                            (position.X*matrix.M13) + (position.Y*matrix.M23) + (position.Z*matrix.M33) + matrix.M43);
        }

        public static void Transform(FVector3[] sourceArray, ref FMatrix matrix, FVector3[] destinationArray)
        {
            throw new NotImplementedException();
        }

        public static void Transform(FVector3[] sourceArray, int sourceIndex, ref FMatrix matrix,
                                     FVector3[] destinationArray, int destinationIndex, int length)
        {
            throw new NotImplementedException();
        }

        public static void TransformNormal(FVector3[] sourceArray, ref FMatrix matrix, FVector3[] destinationArray)
        {
            throw new NotImplementedException();
        }

        public static void TransformNormal(FVector3[] sourceArray, int sourceIndex, ref FMatrix matrix,
                                           FVector3[] destinationArray, int destinationIndex, int length)
        {
            throw new NotImplementedException();
        }

        public static FVector3 TransformNormal(FVector3 normal, FMatrix matrix)
        {
            TransformNormal(ref normal, ref matrix, out normal);
            return normal;
        }

        public static void TransformNormal(ref FVector3 normal, ref FMatrix matrix, out FVector3 result)
        {
            result = new FVector3((normal.X*matrix.M11) + (normal.Y*matrix.M21) + (normal.Z*matrix.M31),
                                 (normal.X*matrix.M12) + (normal.Y*matrix.M22) + (normal.Z*matrix.M32),
                                 (normal.X*matrix.M13) + (normal.Y*matrix.M23) + (normal.Z*matrix.M33));
        }

        #endregion Public methods

        #region Operators

        public static bool operator ==(FVector3 value1, FVector3 value2)
        {
            return value1.X == value2.X
                   && value1.Y == value2.Y
                   && value1.Z == value2.Z;
        }

        public static bool operator !=(FVector3 value1, FVector3 value2)
        {
            return !(value1 == value2);
        }

        public static FVector3 operator +(FVector3 value1, FVector3 value2)
        {
            value1.X += value2.X;
            value1.Y += value2.Y;
            value1.Z += value2.Z;
            return value1;
        }

        public static FVector3 operator -(FVector3 value)
        {
            value = new FVector3(-value.X, -value.Y, -value.Z);
            return value;
        }

        public static FVector3 operator -(FVector3 value1, FVector3 value2)
        {
            value1.X -= value2.X;
            value1.Y -= value2.Y;
            value1.Z -= value2.Z;
            return value1;
        }

        public static FVector3 operator *(FVector3 value1, FVector3 value2)
        {
            value1.X *= value2.X;
            value1.Y *= value2.Y;
            value1.Z *= value2.Z;
            return value1;
        }

        public static FVector3 operator *(FVector3 value, float scaleFactor)
        {
            value.X *= scaleFactor;
            value.Y *= scaleFactor;
            value.Z *= scaleFactor;
            return value;
        }

        public static FVector3 operator *(float scaleFactor, FVector3 value)
        {
            value.X *= scaleFactor;
            value.Y *= scaleFactor;
            value.Z *= scaleFactor;
            return value;
        }

        public static FVector3 operator /(FVector3 value1, FVector3 value2)
        {
            value1.X /= value2.X;
            value1.Y /= value2.Y;
            value1.Z /= value2.Z;
            return value1;
        }

        public static FVector3 operator /(FVector3 value, float divider)
        {
            float factor = 1/divider;
            value.X *= factor;
            value.Y *= factor;
            value.Z *= factor;
            return value;
        }

        #endregion
    }
}

#endif