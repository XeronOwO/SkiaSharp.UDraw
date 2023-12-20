using SkiaSharp.UDraw.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkiaSharp.UDraw
{
	/// <summary>
	/// A scene that contains game objects
	/// </summary>
	public class Scene : Object, IDisposable
	{
		#region Constructor

		/// <summary>
		/// The root game object
		/// </summary>
		public Transform root { get; private set; }

		private DateTime _time;

		/// <summary>
		/// Create a scene
		/// </summary>
		public Scene() : this("Scene")
		{

		}

		/// <summary>
		/// Create a scene with name
		/// </summary>
		/// <param name="name">Name of the scene</param>
		public Scene(string name) : base(name)
		{
			root = new GameObject("Root").transform;

			_time = DateTime.Now;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Background color
		/// </summary>
		public SKColor backgroundColor { get; set; }

		#endregion

		#region Functions

		/// <summary>
		/// Add a game object to the root
		/// </summary>
		/// <param name="gameObject">The game object to be added to the root</param>
		public void AddGameObject(GameObject gameObject)
		{
			gameObject.transform.parent = root;
		}

		#region Others

		/// <inheritdoc/>
		public override void Destroy()
		{
			if (root != null)
			{
				root.Destroy();
				root = null;
			}

			base.Destroy();
		}

		/// <inheritdoc/>
		public override string ToString()
		{
			return name;
		}

		/// <summary>
		/// <inheritdoc/><br/>Also calls <see cref="Destroy"/>
		/// </summary>
		public void Dispose()
		{
			Destroy();
		}

		#endregion

		#region Message / Event

		private void SendPreUpdate()
		{
			root.SendPreUpdate();
		}

		private void SendUpdate()
		{
			var time = DateTime.Now;
			root.SendUpdate((time - _time).TotalSeconds);
			_time = time;
		}

		private void SendCapture(SKRectI rect, SKCanvas canvas)
		{
			root.SendCapture(rect, canvas);
		}

		#endregion

		#region Draw

		/// <summary>
		/// Capture the whole scene as an image<br/>Image rect will be calculated automatically
		/// </summary>
		/// <returns>Image</returns>
		public SKImage Capture()
		{
			SendPreUpdate();
			SendUpdate();

			SKRectI rect = root.transform.CalcRect() ?? new Rect(Vector2.zero, new(32, 32));

			var imageInfo = new SKImageInfo(rect.Width, rect.Height, SKColorType.Rgba8888, SKAlphaType.Premul);
			using var surface = SKSurface.Create(imageInfo);
			using var canvas = surface.Canvas;
			canvas.Translate(rect.Location);
			canvas.Clear(backgroundColor);
			SendCapture(rect, canvas);

			return surface.Snapshot();
		}

		/// <summary>
		/// Capture the whole scene as an image
		/// </summary>
		/// <param name="rect">Image rect</param>
		/// <returns>Image</returns>
		public SKImage Capture(SKRectI rect)
		{
			SendPreUpdate();
			SendUpdate();

			var imageInfo = new SKImageInfo(rect.Width, rect.Height, SKColorType.Rgba8888, SKAlphaType.Premul);
			using var surface = SKSurface.Create(imageInfo);
			using var canvas = surface.Canvas;
			var pos = rect.Location;
			canvas.Translate(-pos.X, -pos.Y);
			canvas.Clear(backgroundColor);
			SendCapture(rect, canvas);

			return surface.Snapshot();
		}

		#endregion

		#endregion
	}
}
