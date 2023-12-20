using System;
using System.Collections.Generic;
using System.Text;

namespace SkiaSharp.UDraw
{
	/// <summary>
	/// Basic object
	/// </summary>
	public class Object
	{
		#region Constructor

		/// <summary>
		/// Create an object
		/// </summary>
		public Object() : this("Object")
		{

		}

		/// <summary>
		/// Create an object with name
		/// </summary>
		/// <param name="name">The name of the object</param>
		public Object(string name)
		{
			this.name = name;
		}

		#endregion

		#region Properties

		/// <summary>
		/// The name of the object
		/// </summary>
		public virtual string name { get; set; }

		#endregion

		#region Functions

		/// <summary>
		/// Destroy current object
		/// </summary>
		public virtual void Destroy()
		{

		}

		/// <summary>
		/// Destroy an object
		/// </summary>
		/// <param name="obj">The object to destroy</param>
		public static void Destroy(Object obj)
		{
			obj.Destroy();
		}

		#endregion
	}
}
