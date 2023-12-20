using System;
using System.Collections.Generic;
using System.Text;

namespace SkiaSharp.UDraw
{
	/// <summary>
	/// Behavior that controlls game object
	/// </summary>
	public class MonoBehavior : Object
	{
		#region Properties

		/// <summary>
		/// Is the current behavior enabled
		/// </summary>
		public bool enable { get; set; } = true;

		/// <summary>
		/// The game object attached to
		/// </summary>
		public GameObject gameObject { get; internal set; }

		/// <summary>
		/// The transform of the current game object
		/// </summary>
		public Transform transform
		{
			get
			{
				if (gameObject == null)
				{
					return null;
				}
				return gameObject.transform;
			}
		}

		/// <summary>
		/// <inheritdoc/><br/><see cref="MonoBehavior"/> shares the same name with <see cref="gameObject"/>
		/// </summary>
		public override string name
		{
			get
			{
				if (gameObject != null)
				{
					return gameObject.name;
				}
				return null;
			}
			set
			{
				if (gameObject != null)
				{
					gameObject.name = value;
				}
			}
		}

		/// <inheritdoc cref="Transform.childCount"/>
		public int childCount => transform.childCount;

		#endregion

		#region Functions

		#region Transform

		/// <inheritdoc cref="Transform.GetChild(int)"/>
		public Transform GetChild(int index) => transform.GetChild(index);

		#endregion

		#region Game Object

		/// <inheritdoc cref="GameObject.AddComponent{T}()"/>
		public T AddComponent<T>() where T : MonoBehavior, new() => gameObject.AddComponent<T>();

		/// <inheritdoc cref="GameObject.AddComponent(Type)"/>
		public MonoBehavior AddComponent(Type type) => gameObject.AddComponent(type);

		/// <inheritdoc cref="GameObject.GetComponent{T}"/>
		public T GetComponent<T>() where T : MonoBehavior, new() => gameObject.GetComponent<T>();

		/// <inheritdoc cref="GameObject.GetComponent(Type)"/>
		public MonoBehavior GetComponent(Type type) => gameObject.GetComponent(type);

		/// <inheritdoc cref="GameObject.GetComponents()"/>
		public MonoBehavior[] GetComponents() => gameObject.GetComponents();

		/// <inheritdoc cref="GameObject.GetComponents{T}"/>
		public T[] GetComponents<T>() where T : MonoBehavior, new() => gameObject.GetComponents<T>();

		/// <inheritdoc cref="GameObject.GetComponents(Type)"/>
		public MonoBehavior[] GetComponents(Type type) => gameObject.GetComponents(type);

		/// <inheritdoc cref="GameObject.GetComponentInChildren{T}()"/>
		public T GetComponentInChildren<T>() where T : MonoBehavior, new() => gameObject.GetComponentInChildren<T>();

		/// <inheritdoc cref="GameObject.GetComponentInChildren(Type)"/>
		public MonoBehavior GetComponentInChildren(Type type) => gameObject.GetComponentInChildren(type);

		/// <inheritdoc cref="GameObject.GetComponentsInChildren{T}"/>
		public T[] GetComponentsInChildren<T>() where T : MonoBehavior, new() => gameObject.GetComponentsInChildren<T>();

		/// <inheritdoc cref="GameObject.GetComponentsInChildren(Type)"/>
		public MonoBehavior[] GetComponentsInChildren(Type type) => gameObject.GetComponentsInChildren(type);

		#endregion

		#region Message / Event

		/// <summary>
		/// Called after creating the current component
		/// </summary>
		public virtual void Awake()
		{

		}

		/// <summary>
		/// Called before the first frame update to this component. Only once<br/>Note: Consider the call to <see cref="Scene.Capture()"/> as one frame
		/// </summary>
		public virtual void Start()
		{

		}

		/// <summary>
		/// Called on update, usually before Capture
		/// </summary>
		/// <param name="delta"></param>
		public virtual void Update(double delta)
		{

		}

		/// <summary>
		/// When a capture is requested
		/// </summary>
		/// <param name="rect">Surface rect</param>
		/// <param name="canvas">Canvas</param>
		public virtual void Capture(SKRectI rect, SKCanvas canvas)
		{

		}

		private bool _isStart = true;

		internal virtual void SendPreUpdate()
		{
			if (!enable)
			{
				return;
			}
			if (_isStart)
			{
				_isStart = false;
				Start();
			}
		}

		internal virtual void SendUpdate(double delta)
		{
			if (!enable)
			{
				return;
			}
			Update(delta);
		}

		internal virtual void SendCapture(SKRectI rect, SKCanvas canvas)
		{
			if (!enable)
			{
				return;
			}
			Capture(rect, canvas);
		}

		#endregion

		#region Destroy

		/// <inheritdoc/>
		public override void Destroy()
		{
			if (gameObject != null)
			{
				gameObject.components.Remove(this);
				gameObject = null;
			}

			base.Destroy();
		}

		#endregion

		#endregion
	}
}
