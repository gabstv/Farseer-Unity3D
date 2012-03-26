#if(!XNA)

#region License

/*
MIT License
Copyright © 2006 The Mono.Xna Team

All rights reserved.

Authors
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
    public struct FVector2 : IEquatable<FVector2>
    {
        #region Private Fields

        private static FVector2 zeroVector = new FVector2(0f, 0f);
        private static FVector2 unitVector = new FVector2(1f, 1f);
        private static FVector2 unitXVector = new FVector2(1f, 0f);
        private static FVector2 unitYVector = new FVector2(0f, 1f);

        #endregion Private Fields

        #region Public Fields

        public float X;
        public float Y;

        #endregion Public Fields

        #region Properties

        public static FVector2 Zero
        {
            get { return zeroVector; }
        }

        public static FVector2 One
        {
            get { return unitVector; }
        }

        public static FVector2 UnitX
        {
            get { return unitXVector; }
        }

        public static FVector2 UnitY
        {
            get { return unitYVector; }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Constructor foe standard 2D vector.
        /// </summary>
        /// <param name="x">
        /// A <see cref="System.Single"/>
        /// </param>
        /// <param name="y">
        /// A <see cref="System.Single"/>
        /// </param>
        public FVector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Constructor for "square" vector.
        /// </summary>
        /// <param name="value">
        /// A <see cref="System.Single"/>
        /// </param>
        public FVector2(float value)
        {
            X = value;
            Y = value;
        }

        #endregion Constructors

        #region Public Methods

        public static void Reflect(ref FVector2 vector, ref FVector2 normal, out FVector2 result)
        {
            float dot = Dot(vector, normal);
            result.X = vector.X - ((2f*dot)*normal.X);
            result.Y = vector.Y - ((2f*dot)*normal.Y);
        }

        public static FVector2 Reflect(FVector2 vector, FVector2 normal)
        {
            FVector2 result;
            Reflect(ref vector, ref normal, out result);
            return result;
        }

        public static FVector2 Add(FVector2 value1, FVector2 value2)
        {
            value1.X += value2.X;
            value1.Y += value2.Y;
            return value1;
        }

        public static void Add(ref FVector2 value1, ref FVector2 value2, out FVector2 result)
        {
            result.X = value1.X + value2.X;
            result.Y = value1.Y + value2.Y;
        }

        public static FVector2 Barycentric(FVector2 value1, FVector2 value2, FVector2 value3, float amount1, float amount2)
        {
            return new FVector2(
                MathHelper.Barycentric(value1.X, value2.X, value3.X, amount1, amount2),
                MathHelper.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2));
        }

        public static void Barycentric(ref FVector2 value1, ref FVector2 value2, ref FVector2 value3, float amount1,
                                       float amount2, out FVector2 result)
        {
            result = new FVector2(
                MathHelper.Barycentric(value1.X, value2.X, value3.X, amount1, amount2),
                MathHelper.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2));
        }

        public static FVector2 CatmullRom(FVector2 value1, FVector2 value2, FVector2 value3, FVector2 value4, float amount)
        {
            return new FVector2(
                MathHelper.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount),
                MathHelper.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount));
        }

        public static void CatmullRom(ref FVector2 value1, ref FVector2 value2, ref FVector2 value3, ref FVector2 value4,
                                      float amount, out FVector2 result)
        {
            result = new FVector2(
                MathHelper.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount),
                MathHelper.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount));
        }

        public static FVector2 Clamp(FVector2 value1, FVector2 min, FVector2 max)
        {
            return new FVector2(
                MathHelper.Clamp(value1.X, min.X, max.X),
                MathHelper.Clamp(value1.Y, min.Y, max.Y));
        }

        public static void Clamp(ref FVector2 value1, ref FVector2 min, ref FVector2 max, out FVector2 result)
        {
            result = new FVector2(
                MathHelper.Clamp(value1.X, min.X, max.X),
                MathHelper.Clamp(value1.Y, min.Y, max.Y));
        }

        /// <summary>
        /// Returns float precison distanve between two vectors
        /// </summary>
        /// <param name="value1">
        /// A <see cref="Vector2"/>
        /// </param>
        /// <param name="value2">
        /// A <see cref="Vector2"/>
        /// </param>
        /// <returns>
        /// A <see cref="System.Single"/>
        /// </returns>
        public static float Distance(FVector2 value1, FVector2 value2)
        {
            float result;
            DistanceSquared(ref value1, ref value2, out result);
            return (float) Math.Sqrt(result);
        }


        public static void Distance(ref FVector2 value1, ref FVector2 value2, out float result)
        {
            DistanceSquared(ref value1, ref value2, out result);
            result = (float) Math.Sqrt(result);
        }

        public static float DistanceSquared(FVector2 value1, FVector2 value2)
        {
            float result;
            DistanceSquared(ref value1, ref value2, out result);
            return result;
        }

        public static void DistanceSquared(ref FVector2 value1, ref FVector2 value2, out float result)
        {
            result = (value1.X - value2.X)*(value1.X - value2.X) + (value1.Y - value2.Y)*(value1.Y - value2.Y);
        }

        /// <summary>
        /// Devide first vector with the secund vector
        /// </summary>
        /// <param name="value1">
        /// A <see cref="Vector2"/>
        /// </param>
        /// <param name="value2">
        /// A <see cref="Vector2"/>
        /// </param>
        /// <returns>
        /// A <see cref="Vector2"/>
        /// </returns>
        public static FVector2 Divide(FVector2 value1, FVector2 value2)
        {
            value1.X /= value2.X;
            value1.Y /= value2.Y;
            return value1;
        }

        public static void Divide(ref FVector2 value1, ref FVector2 value2, out FVector2 result)
        {
            result.X = value1.X/value2.X;
            result.Y = value1.Y/value2.Y;
        }

        public static FVector2 Divide(FVector2 value1, float divider)
        {
            float factor = 1/divider;
            value1.X *= factor;
            value1.Y *= factor;
            return value1;
        }

        public static void Divide(ref FVector2 value1, float divider, out FVector2 result)
        {
            float factor = 1/divider;
            result.X = value1.X*factor;
            result.Y = value1.Y*factor;
        }

        public static float Dot(FVector2 value1, FVector2 value2)
        {
            return value1.X*value2.X + value1.Y*value2.Y;
        }

        public static void Dot(ref FVector2 value1, ref FVector2 value2, out float result)
        {
            result = value1.X*value2.X + value1.Y*value2.Y;
        }

        public override bool Equals(object obj)
        {
            return (obj is FVector2) ? this == ((FVector2) obj) : false;
        }

        public bool Equals(FVector2 other)
        {
            return this == other;
        }

        public override int GetHashCode()
        {
            return (int) (X + Y);
        }

        public static FVector2 Hermite(FVector2 value1, FVector2 tangent1, FVector2 value2, FVector2 tangent2, float amount)
        {
            FVector2 result = new FVector2();
            Hermite(ref value1, ref tangent1, ref value2, ref tangent2, amount, out result);
            return result;
        }

        public static void Hermite(ref FVector2 value1, ref FVector2 tangent1, ref FVector2 value2, ref FVector2 tangent2,
                                   float amount, out FVector2 result)
        {
            result.X = MathHelper.Hermite(value1.X, tangent1.X, value2.X, tangent2.X, amount);
            result.Y = MathHelper.Hermite(value1.Y, tangent1.Y, value2.Y, tangent2.Y, amount);
        }

        public float Length()
        {
            float result;
            DistanceSquared(ref this, ref zeroVector, out result);
            return (float) Math.Sqrt(result);
        }

        public float LengthSquared()
        {
            float result;
            DistanceSquared(ref this, ref zeroVector, out result);
            return result;
        }

        public static FVector2 Lerp(FVector2 value1, FVector2 value2, float amount)
        {
            return new FVector2(
                MathHelper.Lerp(value1.X, value2.X, amount),
                MathHelper.Lerp(value1.Y, value2.Y, amount));
        }

        public static void Lerp(ref FVector2 value1, ref FVector2 value2, float amount, out FVector2 result)
        {
            result = new FVector2(
                MathHelper.Lerp(value1.X, value2.X, amount),
                MathHelper.Lerp(value1.Y, value2.Y, amount));
        }

        public static FVector2 Max(FVector2 value1, FVector2 value2)
        {
            return new FVector2(
                MathHelper.Max(value1.X, value2.X),
                MathHelper.Max(value1.Y, value2.Y));
        }

        public static void Max(ref FVector2 value1, ref FVector2 value2, out FVector2 result)
        {
            result = new FVector2(
                MathHelper.Max(value1.X, value2.X),
                MathHelper.Max(value1.Y, value2.Y));
        }

        public static FVector2 Min(FVector2 value1, FVector2 value2)
        {
            return new FVector2(
                MathHelper.Min(value1.X, value2.X),
                MathHelper.Min(value1.Y, value2.Y));
        }

        public static void Min(ref FVector2 value1, ref FVector2 value2, out FVector2 result)
        {
            result = new FVector2(
                MathHelper.Min(value1.X, value2.X),
                MathHelper.Min(value1.Y, value2.Y));
        }

        public static FVector2 Multiply(FVector2 value1, FVector2 value2)
        {
            value1.X *= value2.X;
            value1.Y *= value2.Y;
            return value1;
        }

        public static FVector2 Multiply(FVector2 value1, float scaleFactor)
        {
            value1.X *= scaleFactor;
            value1.Y *= scaleFactor;
            return value1;
        }

        public static void Multiply(ref FVector2 value1, float scaleFactor, out FVector2 result)
        {
            result.X = value1.X*scaleFactor;
            result.Y = value1.Y*scaleFactor;
        }

        public static void Multiply(ref FVector2 value1, ref FVector2 value2, out FVector2 result)
        {
            result.X = value1.X*value2.X;
            result.Y = value1.Y*value2.Y;
        }

        public static FVector2 Negate(FVector2 value)
        {
            value.X = -value.X;
            value.Y = -value.Y;
            return value;
        }

        public static void Negate(ref FVector2 value, out FVector2 result)
        {
            result.X = -value.X;
            result.Y = -value.Y;
        }

        public void Normalize()
        {
            Normalize(ref this, out this);
        }

        public static FVector2 Normalize(FVector2 value)
        {
            Normalize(ref value, out value);
            return value;
        }

        public static void Normalize(ref FVector2 value, out FVector2 result)
        {
            float factor;
            DistanceSquared(ref value, ref zeroVector, out factor);
            factor = 1f/(float) Math.Sqrt(factor);
            result.X = value.X*factor;
            result.Y = value.Y*factor;
        }

        public static FVector2 SmoothStep(FVector2 value1, FVector2 value2, float amount)
        {
            return new FVector2(
                MathHelper.SmoothStep(value1.X, value2.X, amount),
                MathHelper.SmoothStep(value1.Y, value2.Y, amount));
        }

        public static void SmoothStep(ref FVector2 value1, ref FVector2 value2, float amount, out FVector2 result)
        {
            result = new FVector2(
                MathHelper.SmoothStep(value1.X, value2.X, amount),
                MathHelper.SmoothStep(value1.Y, value2.Y, amount));
        }

        public static FVector2 Subtract(FVector2 value1, FVector2 value2)
        {
            value1.X -= value2.X;
            value1.Y -= value2.Y;
            return value1;
        }

        public static void Subtract(ref FVector2 value1, ref FVector2 value2, out FVector2 result)
        {
            result.X = value1.X - value2.X;
            result.Y = value1.Y - value2.Y;
        }

        public static FVector2 Transform(FVector2 position, FMatrix matrix)
        {
            Transform(ref position, ref matrix, out position);
            return position;
        }

        public static void Transform(ref FVector2 position, ref FMatrix matrix, out FVector2 result)
        {
            result = new FVector2((position.X*matrix.M11) + (position.Y*matrix.M21) + matrix.M41,
                                 (position.X*matrix.M12) + (position.Y*matrix.M22) + matrix.M42);
        }

        public static void Transform(FVector2[] sourceArray, ref FMatrix matrix, FVector2[] destinationArray)
        {
            throw new NotImplementedException();
        }

        public static void Transform(FVector2[] sourceArray, int sourceIndex, ref FMatrix matrix,
                                     FVector2[] destinationArray, int destinationIndex, int length)
        {
            throw new NotImplementedException();
        }

        public static FVector2 TransformNormal(FVector2 normal, FMatrix matrix)
        {
            TransformNormal(ref normal, ref matrix, out normal);
            return normal;
        }

        public static void TransformNormal(ref FVector2 normal, ref FMatrix matrix, out FVector2 result)
        {
            result = new FVector2((normal.X*matrix.M11) + (normal.Y*matrix.M21),
                                 (normal.X*matrix.M12) + (normal.Y*matrix.M22));
        }

        public static void TransformNormal(FVector2[] sourceArray, ref FMatrix matrix, FVector2[] destinationArray)
        {
            throw new NotImplementedException();
        }

        public static void TransformNormal(FVector2[] sourceArray, int sourceIndex, ref FMatrix matrix,
                                           FVector2[] destinationArray, int destinationIndex, int length)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(24);
            sb.Append("{X:");
            sb.Append(X);
            sb.Append(" Y:");
            sb.Append(Y);
            sb.Append("}");
            return sb.ToString();
        }

        #endregion Public Methods

        #region Operators

        public static FVector2 operator -(FVector2 value)
        {
            value.X = -value.X;
            value.Y = -value.Y;
            return value;
        }


        public static bool operator ==(FVector2 value1, FVector2 value2)
        {
            return value1.X == value2.X && value1.Y == value2.Y;
        }


        public static bool operator !=(FVector2 value1, FVector2 value2)
        {
            return value1.X != value2.X || value1.Y != value2.Y;
        }


        public static FVector2 operator +(FVector2 value1, FVector2 value2)
        {
            value1.X += value2.X;
            value1.Y += value2.Y;
            return value1;
        }


        public static FVector2 operator -(FVector2 value1, FVector2 value2)
        {
            value1.X -= value2.X;
            value1.Y -= value2.Y;
            return value1;
        }


        public static FVector2 operator *(FVector2 value1, FVector2 value2)
        {
            value1.X *= value2.X;
            value1.Y *= value2.Y;
            return value1;
        }


        public static FVector2 operator *(FVector2 value, float scaleFactor)
        {
            value.X *= scaleFactor;
            value.Y *= scaleFactor;
            return value;
        }


        public static FVector2 operator *(float scaleFactor, FVector2 value)
        {
            value.X *= scaleFactor;
            value.Y *= scaleFactor;
            return value;
        }


        public static FVector2 operator /(FVector2 value1, FVector2 value2)
        {
            value1.X /= value2.X;
            value1.Y /= value2.Y;
            return value1;
        }


        public static FVector2 operator /(FVector2 value1, float divider)
        {
            float factor = 1/divider;
            value1.X *= factor;
            value1.Y *= factor;
            return value1;
        }

        #endregion Operators
    }
}

#endif