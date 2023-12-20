using System;
using System.Collections.Generic;
using System.Text;

namespace SkiaSharp.UDraw.UI
{
	/// <summary>
	/// Rectangular box. Can be used as a debugger for RectTransform
	/// </summary>
	[DisallowMultipleComponent, RequireComponent(typeof(RectTransform))]
	public class Rectangle : MonoBehavior, ICanvasDraw
	{
		#region Constructor

		private SKPaint _paint;

		/// <inheritdoc/>
		public Rectangle()
		{
			_paint = new()
			{
				IsAntialias = true,
				FilterQuality = SKFilterQuality.High,
			};
			color = Color.white;
			strokeWidth = 1;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Color
		/// </summary>
		public Color color { get => _paint.Color; set => _paint.Color = value; }

		/// <summary>
		/// Stroke width
		/// </summary>
		public float strokeWidth { get => _paint.StrokeWidth; set => _paint.StrokeWidth = value; }

		/// <summary>
		/// Style
		/// </summary>
		public SKPaintStyle style { get => _paint.Style; set => _paint.Style = value; }

		#endregion

		#region Draw

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

			canvas.DrawRect(rectTransform.rect, _paint);

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

			base.Destroy();
		}

		#endregion
	}
}
