using SkiaSharp.UDraw.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkiaSharp.UDraw
{
	/// <summary>
	/// Canvas
	/// </summary>
	[DisallowMultipleComponent, RequireComponent(typeof(CanvasRenderer))]
	public class Canvas : MonoBehavior
	{
		internal void Draw(SKRectI rect, SKCanvas canvas)
		{
			if (!enable)
			{
				return;
			}

			canvas.SaveLayer();

			Draw(rect, canvas, transform);

			canvas.Restore();
		}

		private void Draw(SKRectI rect, SKCanvas canvas, Transform tr)
		{
			var components = tr.GetComponents();

			foreach (var component in components)
			{
				if (component is ICanvasDraw draw)
				{
					draw.BeginCanvasDraw(rect, canvas);
				}
			}

			foreach (var component in components)
			{
				if (component is ICanvasDraw draw)
				{
					draw.CanvasDraw(rect, canvas);
				}
			}

			// capture children
			foreach (var child in tr.children)
			{
				Draw(rect, canvas, child);
			}

			foreach (var component in components)
			{
				if (component is ICanvasDraw draw)
				{
					draw.EndCanvasDraw(rect, canvas);
				}
			}
		}
	}
}
