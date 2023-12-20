using System;
using System.Collections.Generic;
using System.Text;

namespace SkiaSharp.UDraw.UI
{
	/// <summary>
	/// Image component
	/// </summary>
	[DisallowMultipleComponent, RequireComponent(typeof(RectTransform))]
	public class Image : MonoBehavior, ICanvasDraw
	{
		#region Constructor

		private Color _color;

		private SKPaint _paint;

		/// <inheritdoc/>
		public Image()
		{
			_color = Color.white;
			_paint = new SKPaint()
			{
				IsAntialias = true,
				FilterQuality = SKFilterQuality.High,
			};
		}

		#endregion

		#region Properties

		/// <summary>
		/// Sprite
		/// </summary>
		public Sprite sprite { get; set; }

		/// <summary>
		/// Color
		/// </summary>
		public Color color { get => _color; set => SetColor(value); }

		/// <summary>
		/// Antialias<br/>Default: true
		/// </summary>
		public bool isAntialias { get => _paint.IsAntialias; set => _paint.IsAntialias = value; }

		/// <summary>
		/// Filter quality<br/>Default: <see cref="SKFilterQuality.High"/>
		/// </summary>
		public SKFilterQuality filterQuality { get => _paint.FilterQuality; set => _paint.FilterQuality = value; }

		/// <summary>
		/// Image filter
		/// </summary>
		public SKImageFilter imageFilter { get => _paint.ImageFilter; set => _paint.ImageFilter = value; }

		#endregion

		#region Draw

		private void SetColor(Color color)
		{
			_color = color;
			_paint.ColorFilter = SKColorFilter.CreateColorMatrix(new float[]
			{
				color.r, 0, 0, 0, 0,
				0, color.g, 0, 0, 0,
				0, 0, color.b, 0, 0,
				0, 0, 0, color.a, 0,
			});
		}

		/// <inheritdoc/>
		public void BeginCanvasDraw(SKRectI rect, SKCanvas canvas)
		{

		}

		/// <inheritdoc/>
		public void CanvasDraw(SKRectI rect, SKCanvas canvas)
		{
			if (!enable)
			{
				return;
			}
			var rectTransform = GetComponent<RectTransform>() ?? throw new NullReferenceException("GetComponent<RectTransform>() is null in Image");
			if (!Rect.Intersect(rect, rectTransform.worldRect))
			{
				return;
			}

			rectTransform.TransformCanvas(canvas);

			(sprite ?? Sprite.blankSprite).DrawImage(canvas, rectTransform.rect, _paint);

			rectTransform.RestoreCanvas(canvas);
		}

		/// <inheritdoc/>
		public void EndCanvasDraw(SKRectI rect, SKCanvas canvas)
		{

		}

		#endregion

		#region Destroy

		/// <inheritdoc/>
		public override void Destroy()
		{
			sprite = null;
			if (_paint != null)
			{
				_paint.Dispose();
				_paint = null;
			}

			base.Destroy();
		}

		#endregion
	}
}
