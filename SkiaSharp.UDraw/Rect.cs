using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SkiaSharp.UDraw
{
	/// <summary>
	/// Rectangle
	/// </summary>
	public struct Rect
	{
		#region Constructor

		/// <summary>
		/// Create Rect
		/// </summary>
		public Rect()
		{
			x = 0;
			y = 0;
			width = 0;
			height = 0;
		}

		/// <summary>
		/// Create a copy of rect
		/// </summary>
		/// <param name="rect">The rect to be copied</param>
		public Rect(Rect rect)
		{
			x = rect.x;
			y = rect.y;
			width = rect.width;
			height = rect.height;
		}

		/// <summary>
		/// Create a rect by pos and size
		/// </summary>
		/// <param name="position">Position</param>
		/// <param name="size">Size</param>
		public Rect(Vector2 position, Vector2 size)
		{
			x = position.x;
			y = position.y;
			width = size.x;
			height = size.y;
		}

		/// <summary>
		/// Create a rect by x, y, width and height
		/// </summary>
		/// <param name="x">X</param>
		/// <param name="y">Y</param>
		/// <param name="width">Width</param>
		/// <param name="height">Height</param>
		public Rect(float x, float y, float width, float height)
		{
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;
		}

		#endregion

		#region Properties

		/// <summary>
		/// X
		/// </summary>
		public float x;

		/// <summary>
		/// Y
		/// </summary>
		public float y;

		/// <summary>
		/// Width
		/// </summary>
		public float width;

		/// <summary>
		/// Height
		/// </summary>
		public float height;

		/// <summary>
		/// Position
		/// </summary>
		public Vector2 position
		{
			get
			{
				return new(x, y);
			}
			set
			{
				x = value.x;
				y = value.y;
			}
		}

		/// <summary>
		/// Size
		/// </summary>
		public Vector2 size
		{
			get
			{
				return new(width, height);
			}
			set
			{
				width = value.x;
				height = value.y;
			}
		}

		/// <summary>
		/// xMin bound
		/// </summary>
		public float xMin { get => x; set { width -= value - x; x = value; } }

		/// <summary>
		/// xMax bound
		/// </summary>
		public float xMax { get => x + width; set => width = value - x; }

		/// <summary>
		/// yMin bound
		/// </summary>
		public float yMin { get => y; set { height -= value - y; y = value; } }

		/// <summary>
		/// yMax bound
		/// </summary>
		public float yMax { get => y + height; set => height = value - y; }

		/// <summary>
		/// Center
		/// </summary>
		public Vector2 center => new(x + width / 2f, y + height / 2f);

		private static Rect zeroRect = new();

		/// <summary>
		/// Get a rect of zero
		/// </summary>
		public static Rect zero => zeroRect;

		private static Rect oneRect = new(0, 0, 1, 1);

		/// <summary>
		/// Get a rect of (0, 0, 1, 1)
		/// </summary>
		public static Rect one => oneRect;

		/// <summary>
		/// Returns (rect.x * v, rect.y * v, rect.width * v, rect.height * v)
		/// </summary>
		/// <param name="rect">Rect</param>
		/// <param name="v">Multiplier</param>
		/// <returns>(rect.x * v, rect.y * v, rect.width * v, rect.height * v)</returns>
		public static Rect operator*(Rect rect, float v)
		{
			return new(rect.x * v, rect.y * v, rect.width * v, rect.height * v);
		}

		/// <summary>
		/// Returns (rect.x / v, rect.y / v, rect.width / v, rect.height / v)
		/// </summary>
		/// <param name="rect">Rect</param>
		/// <param name="v">Divisor</param>
		/// <returns>(rect.x / v, rect.y / v, rect.width / v, rect.height / v)</returns>
		public static Rect operator/(Rect rect, float v)
		{
			return new(rect.x / v, rect.y / v, rect.width / v, rect.height / v);
		}

		/// <summary>
		/// Returns (rect.x * vector.x, rect.y * vector.y, rect.width * vector.x, rect.height * vector.y)
		/// </summary>
		/// <param name="rect">Rect</param>
		/// <param name="vector">Multiplier</param>
		/// <returns>(rect.x * vector.x, rect.y * vector.y, rect.width * vector.x, rect.height * vector.y)</returns>
		public static Rect operator*(Rect rect, Vector2 vector)
		{
			return new(rect.x * vector.x, rect.y * vector.y, rect.width * vector.x, rect.height * vector.y);
		}

		/// <summary>
		/// Returns (rect.x / vector.x, rect.y / vector.y, rect.width / vector.x, rect.height / vector.y)
		/// </summary>
		/// <param name="rect">Rect</param>
		/// <param name="vector">Divisor</param>
		/// <returns>(rect.x / vector.x, rect.y / vector.y, rect.width / vector.x, rect.height / vector.y)</returns>
		public static Rect operator/(Rect rect, Vector2 vector)
		{
			return new(rect.x / vector.x, rect.y / vector.y, rect.width / vector.x, rect.height / vector.y);
		}

		/// <summary>
		/// Convert Rect to SKRectI by Math.Round
		/// </summary>
		/// <param name="rect">Rect</param>
		public static implicit operator SKRectI(Rect rect)
		{
			return new((int)Math.Round(rect.x), (int)Math.Round(rect.y), (int)Math.Round(rect.x + rect.width), (int)Math.Round(rect.y + rect.height));
		}

		/// <summary>
		/// Convert SKRectI to Rect
		/// </summary>
		/// <param name="rect">SKRectI</param>
		public static implicit operator Rect(SKRectI rect)
		{
			return new(rect.Location.X, rect.Location.Y, rect.Size.Width, rect.Size.Height);
		}

		/// <summary>
		/// Convert Rect to SKRect
		/// </summary>
		/// <param name="rect">Rect</param>
		public static implicit operator SKRect(Rect rect)
		{
			return new(rect.x, rect.y, rect.x + rect.width, rect.y + rect.height);
		}

		/// <summary>
		/// Convert SKRect to Rect
		/// </summary>
		/// <param name="rect">SKRect</param>
		public static implicit operator Rect(SKRect rect)
		{
			return new(rect.Location.X, rect.Location.Y, rect.Size.Width, rect.Size.Height);
		}

		#endregion

		#region Functions

		/// <summary>
		/// Whether two rects intersect
		/// </summary>
		/// <param name="rect1">Rect 1</param>
		/// <param name="rect2">Rect 2</param>
		/// <returns>Whether two rects intersect</returns>
		public static bool Intersect(Rect rect1, Rect rect2)
		{
			return (!((rect2.y > rect1.y + rect1.height) || (rect2.y + rect2.height < rect1.y))) &&
				(!((rect2.x > rect1.x + rect1.width) || (rect2.x + rect2.width < rect1.x)));
		}

		/// <summary>
		/// Returns text of the Rect in the format of (x:{x}, y:{y}, width:{width}, height:{height})
		/// </summary>
		/// <returns>Text of the Rect</returns>
		public override string ToString()
		{
			return ToString(null, null);
		}

		/// <summary>
		/// Returns text of the Rect in the format of (x:{x}, y:{y}, width:{width}, height:{height})
		/// </summary>
		/// <param name="format">Format</param>
		/// <returns>Text of the Rect</returns>
		public string ToString(string format)
		{
			return ToString(format, null);
		}

		/// <summary>
		/// Returns text of the Rect in the format of (x:{x}, y:{y}, width:{width}, height:{height})
		/// </summary>
		/// <param name="format">Format</param>
		/// <param name="formatProvider">Format provider</param>
		/// <returns>Text of the Rect</returns>
		public string ToString(string format, IFormatProvider formatProvider)
		{
			if (string.IsNullOrEmpty(format))
			{
				format = "F2";
			}
			formatProvider ??= CultureInfo.InvariantCulture.NumberFormat;

			return string.Format(
				"(x:{0}, y:{1}, width:{2}, height:{3})",
				x.ToString(format, formatProvider),
				y.ToString(format, formatProvider),
				width.ToString(format, formatProvider),
				height.ToString(format, formatProvider)
				);
		}

		#endregion
	}
}
