using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SkiaSharp.UDraw
{
	/// <summary>
	/// Texture 2D
	/// </summary>
	public class Texture2D : Texture
	{
		#region Constructor & Fields

		private SKImage _image;

		/// <summary>
		/// Create a Texture2D
		/// </summary>
		/// <param name="image">Image</param>
		public Texture2D(SKImage image)
		{
			_image = image ?? throw new ArgumentNullException("image");
		}

		private static readonly Texture2D _blankTexture;

		static Texture2D()
		{
			using var blankBitmap = new SKBitmap(1, 1);
			blankBitmap.SetPixel(0, 0, new(255, 255, 255, 255));
			_blankTexture = new(SKImage.FromBitmap(blankBitmap));
		}

		#endregion

		#region Operations

		/// <summary>
		/// Convert SKImage to Texture2D
		/// </summary>
		/// <param name="image">Image</param>
		public static implicit operator Texture2D(SKImage image)
		{
			return new(image);
		}

		#endregion

		#region Properties

		/// <inheritdoc/>
		public override int width
		{
			get
			{
				return _image.Width;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <inheritdoc/>
		public override int height
		{
			get
			{
				return _image.Height;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>
		/// Blank texture
		/// </summary>
		public static Texture2D blankTexture => _blankTexture;

		#endregion

		#region Draw

		internal void Draw(SKCanvas canvas, SKRect source, SKRect dest, SKPaint paint)
		{
			canvas.DrawImage(_image, source, dest, paint);
		}

		#endregion

		#region Destroy

		/// <inheritdoc/>
		public override void Destroy()
		{
			_image = null;

			base.Destroy();
		}

		#endregion
	}
}
