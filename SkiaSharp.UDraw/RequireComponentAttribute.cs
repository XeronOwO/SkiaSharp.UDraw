using System;
using System.Collections.Generic;
using System.Text;

namespace SkiaSharp.UDraw
{
	/// <summary>
	/// Require component
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public class RequireComponentAttribute : Attribute
	{
		internal Type[] requiredTypes;

		/// <summary>
		/// Require component
		/// </summary>
		/// <param name="component">Type</param>
		public RequireComponentAttribute(Type component)
		{
			requiredTypes = new[]
			{
				component,
			};
		}

		/// <summary>
		/// Require component
		/// </summary>
		/// <param name="component0">Type</param>
		/// <param name="component1">Type</param>
		public RequireComponentAttribute(Type component0, Type component1)
		{
			requiredTypes = new[]
			{
				component0,
				component1,
			};
		}

		/// <summary>
		/// Require component
		/// </summary>
		/// <param name="component0">Type</param>
		/// <param name="component1">Type</param>
		/// <param name="component2">Type</param>
		public RequireComponentAttribute(Type component0, Type component1, Type component2)
		{
			requiredTypes = new[]
			{
				component0,
				component1,
				component2,
			};
		}
	}
}
