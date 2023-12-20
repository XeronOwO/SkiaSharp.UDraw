using System;
using System.Collections.Generic;
using System.Text;

namespace SkiaSharp.UDraw
{
	/// <summary>
	/// When drawing on the canvas, the component derived from <see cref="ICanvasDraw"/> will be called
	/// </summary>
	public interface ICanvasDraw
	{
		/// <summary>
		/// Begin canvas drawing
		/// </summary>
		/// <param name="rect">The rect of drawing</param>
		/// <param name="canvas">Canvas</param>
		void BeginCanvasDraw(SKRectI rect, SKCanvas canvas);

		/// <summary>
		/// Canvas drawing
		/// </summary>
		/// <param name="rect">The rect of drawing</param>
		/// <param name="canvas">Canvas</param>
		void CanvasDraw(SKRectI rect, SKCanvas canvas);

		/// <summary>
		/// End canvas drawing
		/// </summary>
		/// <param name="rect">The rect of drawing</param>
		/// <param name="canvas">Canvas</param>
		void EndCanvasDraw(SKRectI rect, SKCanvas canvas);
	}
}
