using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace SkiaSharp.UDraw
{
	/// <summary>
	/// Color in float
	/// </summary>
	public struct Color
	{
		#region Constructor

		/// <summary>
		/// Red
		/// </summary>
		public float r;

		/// <summary>
		/// Green
		/// </summary>
		public float g;

		/// <summary>
		/// Blue
		/// </summary>
		public float b;

		/// <summary>
		/// Alpha
		/// </summary>
		public float a;

		/// <summary>
		/// Create color (0f, 0f, 0f, 1f)
		/// </summary>
		public Color()
		{
			r = 0;
			g = 0;
			b = 0;
			a = 0;
		}

		/// <summary>
		/// Create color (r, g, b, 1f)
		/// </summary>
		/// <param name="r">Red</param>
		/// <param name="g">Green</param>
		/// <param name="b">Blue</param>
		public Color(float r, float g, float b)
		{
			this.r = r;
			this.g = g;
			this.b = b;
			a = 1f;
		}

		/// <summary>
		/// Create color (r, g, b, a)
		/// </summary>
		/// <param name="r">Red</param>
		/// <param name="g">Green</param>
		/// <param name="b">Blue</param>
		/// <param name="a">Alpha</param>
		public Color(float r, float g, float b, float a)
		{
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = a;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Aqua
		/// </summary>
		public static Color aqua
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => new(0f, 1f, 1f, 1f);
		}

		/// <summary>
		/// Black
		/// </summary>
		public static Color black
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => new(0f, 0f, 0f, 1f);
		}

		/// <summary>
		/// Blue
		/// </summary>
		public static Color blue
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => new(0f, 0f, 1f, 1f);
		}

		/// <summary>
		/// Clear
		/// </summary>
		public static Color clear
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => new(0f, 0f, 0f, 0f);
		}

		/// <summary>
		/// Cyan
		/// </summary>
		public static Color cyan
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => new(0f, 1f, 1f, 1f);
		}

		/// <summary>
		/// Gray
		/// </summary>
		public static Color gray
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => new(0.5f, 0.5f, 0.5f, 1f);
		}

		/// <summary>
		/// Green
		/// </summary>
		public static Color green
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => new(0f, 1f, 0f, 1f);
		}

		/// <summary>
		/// Grey
		/// </summary>
		public static Color grey
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => new(0.5f, 0.5f, 0.5f, 1f);
		}

		/// <summary>
		/// Purple
		/// </summary>
		public static Color purple
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => new(160f / 255f, 32f / 255f, 240f / 255f, 1f);
		}

		/// <summary>
		/// Red
		/// </summary>
		public static Color red
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => new(1f, 0f, 0f, 1f);
		}

		/// <summary>
		/// White
		/// </summary>
		public static Color white
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => new(1f, 1f, 1f, 1f);
		}

		/// <summary>
		/// Yellow
		/// </summary>
		public static Color yellow
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => new(1f, 1f, 0f, 1f);
		}

		#endregion

		#region Operators

		/// <summary>
		/// Add
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static Color operator +(Color a, Color b)
		{
			return new Color(a.r + b.r, a.g + b.g, a.b + b.b, a.a + b.a);
		}

		/// <summary>
		/// Div
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static Color operator /(Color a, float b)
		{
			return new Color(a.r / b, a.g / b, a.b / b, a.a / b);
		}

		/// <summary>
		/// Equal
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static bool operator ==(Color a, Color b)
		{
			return a.r == b.r && a.g == b.g && a.b == b.b && a.a == b.a;
		}

		/// <summary>
		/// Inequal
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static bool operator !=(Color a, Color b)
		{
			return !(a == b);
		}

		/// <summary>
		/// Mul
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static Color operator *(Color a, Color b)
		{
			return new Color(a.r * b.r, a.g * b.g, a.b * b.b, a.a * b.a);
		}

		/// <summary>
		/// Mul
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static Color operator *(Color a, float b)
		{
			return new Color(a.r * b, a.g * b, a.b * b, a.a * b);
		}

		/// <summary>
		/// Mul
		/// </summary>
		/// <param name="b"></param>
		/// <param name="a"></param>
		/// <returns></returns>
		public static Color operator *(float b, Color a)
		{
			return new Color(a.r * b, a.g * b, a.b * b, a.a * b);
		}

		/// <summary>
		/// Sub
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static Color operator -(Color a, Color b)
		{
			return new Color(a.r - b.r, a.g - b.g, a.b - b.b, a.a - b.a);
		}

		/// <summary>
		/// Convert Color to SKColor
		/// </summary>
		/// <param name="color">Color</param>
		public static implicit operator SKColor(Color color)
		{
			return new(
				(byte)(color.r * 255f),
				(byte)(color.g * 255f),
				(byte)(color.b * 255f),
				(byte)(color.a * 255f)
				);
		}

		/// <summary>
		/// Convert SKColor to Color
		/// </summary>
		/// <param name="color"></param>
		public static implicit operator Color(SKColor color)
		{
			return new(
				color.Red / 255f,
				color.Green / 255f,
				color.Blue / 255f,
				color.Alpha / 255f
				);
		}

		#endregion

		#region To String

		/// <inheritdoc/>
		public override string ToString()
		{
			return ToString(null, null);
		}

		/// <inheritdoc/>
		public string ToString(string format)
		{
			return ToString(format, null);
		}

		/// <inheritdoc/>
		public string ToString(string format, IFormatProvider formatProvider)
		{
			if (string.IsNullOrEmpty(format))
			{
				format = "F3";
			}
			formatProvider ??= CultureInfo.InvariantCulture.NumberFormat;

			return string.Format(
				"RGBA({0}, {1}, {2}, {3})",
				r.ToString(format, formatProvider),
				g.ToString(format, formatProvider),
				b.ToString(format, formatProvider),
				a.ToString(format, formatProvider)
				);
		}

		#endregion
	}
}
