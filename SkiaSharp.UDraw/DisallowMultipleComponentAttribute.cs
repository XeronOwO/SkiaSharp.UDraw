using System;
using System.Collections.Generic;
using System.Text;

namespace SkiaSharp.UDraw
{
	/// <summary>
	/// Disallow multiple component in one game object
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public class DisallowMultipleComponentAttribute : Attribute
	{
		/// <summary>
		/// Create DisallowMultipleComponentAttribute
		/// </summary>
		public DisallowMultipleComponentAttribute()
		{

		}
	}
}
