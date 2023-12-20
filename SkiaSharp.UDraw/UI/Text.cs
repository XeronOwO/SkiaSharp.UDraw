using System;
using System.Collections.Generic;
using System.Text;

namespace SkiaSharp.UDraw.UI
{
	/// <summary>
	/// Text component
	/// </summary>
	[DisallowMultipleComponent, RequireComponent(typeof(RectTransform))]
	public class Text : MonoBehavior, ICanvasDraw
	{
		#region Constructor

		private SKPaint _paint;

		/// <inheritdoc/>
		public Text()
		{
			_paint = new SKPaint()
			{
				IsAntialias = true,
				FilterQuality = SKFilterQuality.High,
			};
			color = Color.white;
			text = string.Empty;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Color
		/// </summary>
		public Color color { get => _paint.Color; set => _paint.Color = value; }

		/// <summary>
		/// Text
		/// </summary>
		public string text { get; set; }

		/// <summary>
		/// Font
		/// </summary>
		public SKTypeface font { get => _paint.Typeface; set => _paint.Typeface = value; }

		/// <summary>
		/// Font size
		/// </summary>
		public float fontSize { get => _paint.TextSize; set => _paint.TextSize = value; }

		/// <summary>
		/// Antialias<br/>Default: true
		/// </summary>
		public bool isAntialias { get => _paint.IsAntialias; set => _paint.IsAntialias = value; }

		/// <summary>
		/// Filter quality<br/>Default: <see cref="SKFilterQuality.High"/>
		/// </summary>
		public SKFilterQuality filterQuality { get => _paint.FilterQuality; set => _paint.FilterQuality = value; }

		#endregion

		#region Draw

		/// <summary>
		/// Measure the size of the text
		/// </summary>
		/// <returns>Size</returns>
		public Rect Measure()
		{
			var rect = new SKRect();
			_paint.MeasureText(text, ref rect);
			return rect;
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

			var position = rectTransform.rect.position;
			position.y += Measure().height;
			canvas.DrawText(text, position, _paint);

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
			if (_paint != null)
			{
				_paint.Dispose();
				_paint = null;
			}
			text = null;

			base.Destroy();
		}

		#endregion
	}
}
