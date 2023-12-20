using System;
using System.Collections.Generic;
using System.Text;

namespace SkiaSharp.UDraw.Exceptions
{
	/// <summary>
	/// Disallow multiple component exception
	/// </summary>
	public class DisallowMultipleComponentException : Exception
	{
		/// <summary>
		/// Create DisallowMultipleComponentException
		/// </summary>
		/// <param name="name">The name of duplicated component</param>
		public DisallowMultipleComponentException(string name) : base($"Disallow multiple component: {name}")
		{

		}
	}
}
