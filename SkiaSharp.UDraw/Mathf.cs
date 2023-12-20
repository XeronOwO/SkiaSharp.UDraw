using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace SkiaSharp.UDraw
{
	/// <summary>
	/// Math for float
	/// </summary>
	public static class Mathf
	{
		/// <summary>
		/// Float epsilon
		/// </summary>
		public const float Epsilon = float.Epsilon == 0f ? 1.1754944E-38f : float.Epsilon;

		/// <summary>
		/// Positive inf
		/// </summary>
		public const float Infinity = float.PositiveInfinity;

		/// <summary>
		/// Negative inf
		/// </summary>
		public const float NegativeInfinity = float.NegativeInfinity;

		/// <summary>
		/// π
		/// </summary>
		public const float PI = 3.1415927f;

		/// <summary>
		/// Degrees to radians
		/// </summary>
		public const float Deg2Rad = 0.017453292f;

		/// <summary>
		/// Radians to degrees
		/// </summary>
		public const float Rad2Deg = 57.29578f;

		/// <inheritdoc cref="Math.Abs(float)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Abs(float value)
		{
			return Math.Abs(value);
		}

		/// <inheritdoc cref="Math.Abs(int)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Abs(int value)
		{
			return Math.Abs(value);
		}

		/// <inheritdoc cref="Math.Acos(double)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Acos(float d)
		{
			return (float)Math.Acos(d);
		}

		/// <summary>
		/// Approximately
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns>Approximately result</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Approximately(float a, float b)
		{
			return Abs(b - a) < Max(1E-06f * Max(Abs(a), Abs(b)), Epsilon * 8f);
		}

		/// <inheritdoc cref="Math.Asin(double)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Asin(float d)
		{
			return (float)Math.Asin(d);
		}

		/// <inheritdoc cref="Math.Atan(double)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Atan(float d)
		{
			return (float)Math.Atan(d);
		}

		/// <inheritdoc cref="Math.Atan2(double, double)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Atan2(float y, float x)
		{
			return (float)Math.Atan2(y, x);
		}

		/// <summary>
		/// Clamp
		/// </summary>
		/// <param name="value"></param>
		/// <param name="min"></param>
		/// <param name="max"></param>
		/// <returns>Clamp result</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Clamp(float value, float min, float max)
		{
			if (value < min)
			{
				return min;
			}
			else
			{
				if (value > max)
				{
					return max;
				}
				return value;
			}
		}

		/// <inheritdoc cref="Clamp(float, float, float)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Clamp(int value, int min, int max)
		{
			if (value < min)
			{
				return min;
			}
			else
			{
				if (value > max)
				{
					return max;
				}
				return value;
			}
		}

		/// <summary>
		/// Clamp 01
		/// </summary>
		/// <param name="value"></param>
		/// <returns>Clamp result</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Clamp01(float value)
		{
			if (value < 0)
			{
				return 0;
			}
			else
			{
				if (value > 1)
				{
					return 1;
				}
				return value;
			}
		}

		/// <inheritdoc cref="Math.Ceiling(double)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Ceil(float a)
		{
			return (float)Math.Ceiling(a);
		}

		/// <inheritdoc cref="Math.Ceiling(double)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CeilToInt(float a)
		{
			return (int)Math.Ceiling(a);
		}

		/// <inheritdoc cref="Math.Cos(double)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Cos(float d)
		{
			return (float)Math.Cos(d);
		}

		/// <inheritdoc cref="Math.Exp(double)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Exp(float d)
		{
			return (float)Math.Exp(d);
		}

		/// <inheritdoc cref="Math.Floor(double)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Floor(float d)
		{
			return (float)Math.Floor(d);
		}

		/// <inheritdoc cref="Math.Floor(double)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int FloorToInt(float d)
		{
			return (int)Math.Floor(d);
		}

		/// <summary>
		/// Lerp
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <param name="t"></param>
		/// <returns>Lerp result</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Lerp(float a, float b, float t)
		{
			return a + (b - a) * Clamp01(t);
		}

		/// <inheritdoc cref="Math.Log(double)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Log(float d)
		{
			return (float)Math.Log(d);
		}

		/// <inheritdoc cref="Math.Log(double, double)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Log(float a, float newBase)
		{
			return (float)Math.Log(a, newBase);
		}

		/// <inheritdoc cref="Math.Log10(double)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Log10(float d)
		{
			return (float)Math.Log10(d);
		}

		/// <summary>
		/// Get the max value of a and b
		/// </summary>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		/// <returns>The max value of a and b</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Max(float a, float b)
		{
			return a > b ? a : b;
		}

		/// <summary>
		/// Get the max value of a and b
		/// </summary>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		/// <returns>The max value of a and b</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Max(int a, int b)
		{
			return a > b ? a : b;
		}

		/// <summary>
		/// Get the min value of a and b
		/// </summary>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		/// <returns>The min value of a and b</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Min(float a, float b)
		{
			return a < b ? a : b;
		}

		/// <summary>
		/// Get the min value of a and b
		/// </summary>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		/// <returns>The min value of a and b</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Min(int a, int b)
		{
			return a < b ? a : b;
		}

		/// <summary>
		/// Repeat
		/// </summary>
		/// <param name="t"></param>
		/// <param name="length"></param>
		/// <returns>Repeat value</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Repeat(float t, float length)
		{
			return Clamp(t - Floor(t / length), 0, length);
		}

		/// <inheritdoc cref="Math.Round(double)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Round(float d)
		{
			return (float)Math.Round(d);
		}

		/// <summary>
		/// Get the sign of value
		/// </summary>
		/// <param name="f">Value</param>
		/// <returns>Sign</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Sign(float f)
		{
			return f < 0 ? -1 : 1;
		}

		/// <inheritdoc cref="Math.Sin(double)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Sin(float d)
		{
			return (float)Math.Sin(d);
		}

		/// <inheritdoc cref="Math.Sqrt(double)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Sqrt(float d)
		{
			return (float)Math.Sqrt(d);
		}

		/// <inheritdoc cref="Math.Tan(double)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Tan(float d)
		{
			return (float)Math.Tan(d);
		}
	}
}
