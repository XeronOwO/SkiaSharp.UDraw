using System;
using System.Collections.Generic;
using System.Text;

namespace SkiaSharp.UDraw
{
	/// <summary>
	/// Sprite
	/// </summary>
	public class Sprite : Object
	{
		#region Constructor

		private Sprite(Texture2D texture, Rect rect, Vector2 pivot)
		{
			this.texture = texture;
			this.rect = rect;
			this.pivot = pivot;
		}

		private static readonly Sprite _blankSprite;

		static Sprite()
		{
			_blankSprite = new(Texture2D.blankTexture, Rect.one, new(0.5f, 0.5f));
		}

		#endregion

		#region Operations

		/// <summary>
		/// Convert Texture2D to Sprite
		/// </summary>
		/// <param name="texture2D">Texture2D</param>
		public static implicit operator Sprite(Texture2D texture2D)
		{
			return Create(texture2D);
		}

		/// <summary>
		/// Convert SKImage to Sprite
		/// </summary>
		/// <param name="image">Image</param>
		public static implicit operator Sprite(SKImage image)
		{
			return Create(new(image));
		}

		#endregion

		#region Properties

		/// <summary>
		/// Texture2D
		/// </summary>
		public Texture2D texture { get; private set; }

		/// <summary>
		/// The rect of the texture to be used
		/// </summary>
		public Rect rect { get; init; }

		/// <summary>
		/// The pivot of the texture
		/// </summary>
		public Vector2 pivot { get; init; }

		internal static Sprite blankSprite => _blankSprite;

		#endregion

		#region Create

		/// <summary>
		/// Create a new sprite
		/// </summary>
		/// <param name="texture">Texture2D</param>
		/// <returns>Sprite</returns>
		public static Sprite Create(Texture2D texture)
		{
			return Create(texture, new(0, 0, texture.width, texture.height));
		}

		private static readonly Vector2 _defaultPivot = new(0.5f, 0.5f);

		/// <summary>
		/// Create a new sprite
		/// </summary>
		/// <param name="texture">Texture2D</param>
		/// <param name="rect">The rect of the texture to be used</param>
		/// <returns>Sprite</returns>
		public static Sprite Create(Texture2D texture, Rect rect)
		{
			return Create(texture, rect, _defaultPivot);
		}

		/// <summary>
		/// Create a new sprite
		/// </summary>
		/// <param name="texture">Texture2D</param>
		/// <param name="rect">The rect of the texture to be used</param>
		/// <param name="pivot">The pivot of the texture</param>
		/// <returns>Sprite</returns>
		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot)
		{
			if (texture == null)
			{
				return null;
			}

			return new(texture, rect, pivot);
		}

		#endregion

		#region Draw

		internal void DrawImage(SKCanvas canvas, SKRect dest, SKPaint paint)
		{
			texture.Draw(canvas, rect, dest, paint);
		}

		#endregion

		#region Destroy

		/// <inheritdoc/>
		public override void Destroy()
		{
			texture = null;

			base.Destroy();
		}

		#endregion
	}
}
