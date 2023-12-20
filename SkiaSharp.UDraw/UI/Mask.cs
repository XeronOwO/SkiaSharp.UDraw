using System;
using System.Collections.Generic;
using System.Text;

namespace SkiaSharp.UDraw.UI
{
	/// <summary>
	/// Mask component
	/// </summary>
	[DisallowMultipleComponent, RequireComponent(typeof(RectTransform))]
	public class Mask : MonoBehavior, ICanvasDraw
	{
		/// <summary>
		/// Region for drawing
		/// </summary>
		public SKRegion region { get; set; }

		/// <summary>
		/// Clip operation<br/>Default: <see cref="SKClipOperation.Intersect"/>
		/// </summary>
		public SKClipOperation clipOperation { get; set; } = SKClipOperation.Intersect;

		/// <inheritdoc/>
		public void BeginCanvasDraw(SKRectI rect, SKCanvas canvas)
		{
			if (!enable)
			{
				return;
			}
			if (region == null)
			{
				return;
			}

			canvas.Save();

			canvas.ClipRegion(region, clipOperation);
		}

		/// <inheritdoc/>
		public void CanvasDraw(SKRectI rect, SKCanvas canvas)
		{

		}

		/// <inheritdoc/>
		public void EndCanvasDraw(SKRectI rect, SKCanvas canvas)
		{
			if (!enable)
			{
				return;
			}
			if (region == null)
			{
				return;
			}

			canvas.Restore();
		}

		/// <inheritdoc/>
		public override void Destroy()
		{
			region = null;

			base.Destroy();
		}
	}
}
