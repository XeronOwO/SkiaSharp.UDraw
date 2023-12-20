using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SkiaSharp.UDraw
{
	/// <summary>
	/// Vector 2
	/// </summary>
	public struct Vector2
	{
		#region Construtor

		/// <summary>
		/// Create a Vector2 initialized with zero
		/// </summary>
		public Vector2()
		{
			x = 0;
			y = 0;
		}

		/// <summary>
		/// Create a Vector2 initialized with vector
		/// </summary>
		/// <param name="vector">Vector</param>
		public Vector2(Vector2 vector)
		{
			x = vector.x;
			y = vector.y;
		}

		/// <summary>
		/// Create a Vector2 initialized with x and y
		/// </summary>
		/// <param name="x">X coordinate</param>
		/// <param name="y">Y coordinate</param>
		public Vector2(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		#endregion

		#region Properties

		/// <summary>
		/// X coordinate
		/// </summary>
		public float x;

		/// <summary>
		/// Y coordinate
		/// </summary>
		public float y;

		/// <summary>
		/// The magnitude(length) of the vector
		/// </summary>
		public float magnitude
		{
			get
			{
				return (float)Math.Sqrt(x * x + y * y);
			}
		}

		/// <summary>
		/// Get normalized vector
		/// </summary>
		public Vector2 normalized
		{
			get
			{
				var vector = this;
				vector.Normalize();
				return vector;
			}
		}

		private static readonly Vector2 zeroVector = new();

		/// <summary>
		/// Get Vector2 initialized with zero
		/// </summary>
		public static Vector2 zero => zeroVector;

		private static readonly Vector2 oneVector = new(1, 1);

		/// <summary>
		/// Get Vector2 initialized with one
		/// </summary>
		public static Vector2 one => oneVector;

		#endregion

		#region Functions

		#region Methods

		/// <summary>
		/// Returns v1.x * v2.x + v1.y * v2.y
		/// </summary>
		/// <param name="v1">Vector 1</param>
		/// <param name="v2">Vector 2</param>
		/// <returns>v1.x * v2.x + v1.y * v2.y</returns>
		public static float Dot(Vector2 v1, Vector2 v2)
		{
			return v1.x * v2.x + v1.y * v2.y;
		}

		/// <summary>
		/// Returns x * vector.x + y * vector.y
		/// </summary>
		/// <param name="vector">Vector</param>
		/// <returns>x * vector.x + y * vector.y</returns>
		public float Dot(Vector2 vector)
		{
			return x * vector.x + y * vector.y;
		}

		/// <summary>
		/// Lerp
		/// </summary>
		/// <param name="from">Vector 1</param>
		/// <param name="to">Vector 2</param>
		/// <param name="percent">Percent. Automatically limited to [0f, 1f]</param>
		/// <returns>Lerp value</returns>
		public static Vector2 Lerp(Vector2 from, Vector2 to, float percent)
		{
			if (percent < 0)
			{
				percent = 0;
			}
			else if (percent > 1)
			{
				percent = 1;
			}
			return new(from.x + (to.x - from.x) * percent, from.y + (to.y - from.y) * percent);
		}

		/// <summary>
		/// Lerp unclamped
		/// </summary>
		/// <param name="from">Vector 1</param>
		/// <param name="to">Vector 2</param>
		/// <param name="percent">Percent</param>
		/// <returns>Lerp unclamped value</returns>
		public static Vector2 LerpUnclamped(Vector2 from, Vector2 to, float percent)
		{
			return new(from.x + (to.x - from.x) * percent, from.y + (to.y - from.y) * percent);
		}

		/// <summary>
		/// Normalize current vector
		/// </summary>
		public void Normalize()
		{
			var magnitude = this.magnitude;
			var canDiv = magnitude > 1e-5f;
			if (canDiv)
			{
				this /= magnitude;
			}
			else
			{
				this = zero;
			}
		}

		/// <summary>
		/// Equal operation
		/// </summary>
		/// <param name="obj">Object</param>
		/// <returns>Weather this and obj is the same</returns>
		public override bool Equals(object obj)
		{
			return obj is Vector2 vector && Equals(vector);
		}

		/// <summary>
		/// Equal operation
		/// </summary>
		/// <param name="vector">Vector</param>
		/// <returns>Weather this and vector is the same</returns>
		public bool Equals(Vector2 vector)
		{
			return x == vector.x && y == vector.y;
		}

		/// <summary>
		/// Get hash code
		/// </summary>
		/// <returns>Hash code</returns>
		public override int GetHashCode()
		{
			return x.GetHashCode() ^ y.GetHashCode() << 2;
		}

		#endregion

		#region Operator

		/// <summary>
		/// Returns v1 + v2
		/// </summary>
		/// <param name="v1">Vector 1</param>
		/// <param name="v2">Vector 2</param>
		/// <returns>v1 + v2</returns>
		public static Vector2 operator+(Vector2 v1, Vector2 v2)
		{
			return new(v1.x + v2.x, v1.y + v2.y);
		}

		/// <summary>
		/// Returns v1 - v2
		/// </summary>
		/// <param name="v1">Vector 1</param>
		/// <param name="v2">Vector 2</param>
		/// <returns>v1 - v2</returns>
		public static Vector2 operator-(Vector2 v1, Vector2 v2)
		{
			return new(v1.x - v2.x, v1.y - v2.y);
		}

		/// <summary>
		/// Returns -v
		/// </summary>
		/// <param name="v">Vector</param>
		/// <returns>-v</returns>
		public static Vector2 operator-(Vector2 v)
		{
			return new(-v.x, -v.y);
		}

		/// <summary>
		/// Returns (x * v, y * v)
		/// </summary>
		/// <param name="vector">The vector</param>
		/// <param name="v">Multiplier</param>
		/// <returns>(x * v, y * v)</returns>
		public static Vector2 operator*(Vector2 vector, float v)
		{
			return new(vector.x * v, vector.y * v);
		}

		/// <summary>
		/// Returns (x * v, y * v)
		/// </summary>
		/// <param name="vector">The vector</param>
		/// <param name="v">Multiplier</param>
		/// <returns>(x * v, y * v)</returns>
		public static Vector2 operator*(float v, Vector2 vector)
		{
			return new(vector.x * v, vector.y * v);
		}

		/// <summary>
		/// Returns (v1.x * v2.x, v1.y * v2.y)
		/// </summary>
		/// <param name="v1">Vector 1</param>
		/// <param name="v2">Vector 2</param>
		/// <returns>(v1.x * v2.x, v1.y * v2.y)</returns>
		public static Vector2 operator*(Vector2 v1, Vector2 v2)
		{
			return new(v1.x * v2.x, v1.y * v2.y);
		}

		/// <summary>
		/// Returns (x / v, y / v)
		/// </summary>
		/// <param name="vector">The vector</param>
		/// <param name="v">Divisor</param>
		/// <returns>(x / v, y / v)</returns>
		public static Vector2 operator/(Vector2 vector, float v)
		{
			return new(vector.x / v, vector.y / v);
		}

		/// <summary>
		/// Returns (v1.x / v2.x, v1.y / v2.y)
		/// </summary>
		/// <param name="v1">Vector 1</param>
		/// <param name="v2">Vector 2</param>
		/// <returns>(v1.x / v2.x, v1.y / v2.y)</returns>
		public static Vector2 operator/(Vector2 v1, Vector2 v2)
		{
			return new(v1.x / v2.x, v1.y / v2.y);
		}

		/// <summary>
		/// Equal operation
		/// </summary>
		/// <param name="v1">Vector 1</param>
		/// <param name="v2">Vector 2</param>
		/// <returns>Weather two vectors is the same</returns>
		public static bool operator==(Vector2 v1, Vector2 v2)
		{
			return v1.x == v2.x && v1.y == v2.y;
		}

		/// <summary>
		/// Not equal operation
		/// </summary>
		/// <param name="v1">Vector 1</param>
		/// <param name="v2">Vector 2</param>
		/// <returns>Weather two vectors is not the same</returns>
		public static bool operator!=(Vector2 v1, Vector2 v2)
		{
			return v1.x != v2.x || v1.y != v2.y;
		}

		/// <summary>
		/// Convert SKPoint to Vector2
		/// </summary>
		/// <param name="point">SKPoint</param>
		public static implicit operator Vector2(SKPoint point)
		{
			return new(point.X, point.Y);
		}

		/// <summary>
		/// Convert SKSize to Vector2
		/// </summary>
		/// <param name="size">SKSize</param>
		public static implicit operator Vector2(SKSize size)
		{
			return new(size.Width, size.Height);
		}

		/// <summary>
		/// Convert Vector2 to SKPoint
		/// </summary>
		/// <param name="vector">Vector2</param>
		public static implicit operator SKPoint(Vector2 vector)
		{
			return new(vector.x, vector.y);
		}

		/// <summary>
		/// Convert Vector2 to SKSize
		/// </summary>
		/// <param name="vector">Vector2</param>
		public static implicit operator SKSize(Vector2 vector)
		{
			return new(vector.x, vector.y);
		}

		#endregion

		#endregion

		#region To String

		/// <summary>
		/// Returns text of the Vector2 in the format of ({x}, {y})
		/// </summary>
		/// <returns>Text of the Vector2</returns>
		public override string ToString()
		{
			return ToString(null, null);
		}

		/// <summary>
		/// Returns text of the Vector2 in the format of ({x}, {y})
		/// </summary>
		/// <param name="format">Format</param>
		/// <returns>Text of the Vector2</returns>
		public string ToString(string format)
		{
			return ToString(format, null);
		}

		/// <summary>
		/// Returns text of the Vector2 in the format of ({x}, {y})
		/// </summary>
		/// <param name="format">Format</param>
		/// <param name="formatProvider">Format provider</param>
		/// <returns>Text of the Vector2</returns>
		public string ToString(string format, IFormatProvider formatProvider)
		{
			if (string.IsNullOrEmpty(format))
			{
				format = "F2";
			}
			formatProvider ??= CultureInfo.InvariantCulture.NumberFormat;

			return string.Format(
				"({0}, {1})",
				x.ToString(format, formatProvider),
				y.ToString(format, formatProvider)
				);
		}

		#endregion
	}
}
