using System;
using System.Collections.Generic;
using System.Text;

namespace SkiaSharp.UDraw
{
	/// <summary>
	/// Texture
	/// </summary>
	public abstract class Texture : Object
	{
		/// <summary>
		/// Width
		/// </summary>
		public abstract int width { get; set; }

		/// <summary>
		/// Height
		/// </summary>
		public abstract int height { get; set; }
	}
}
