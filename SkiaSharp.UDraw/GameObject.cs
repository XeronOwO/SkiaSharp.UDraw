using SkiaSharp.UDraw.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SkiaSharp.UDraw
{
	/// <summary>
	/// Game object, can be added to a Scene
	/// </summary>
	public class GameObject : Object
	{
		#region Constructor

		/// <summary>
		/// Create a game object
		/// </summary>
		public GameObject() : this("GameObject")
		{

		}

		/// <summary>
		/// Create a game object with name
		/// </summary>
		/// <param name="name">The name of the game object</param>
		public GameObject(string name) : base(name)
		{
			_components = new();
			transform = AddComponent<Transform>();
		}

		#endregion

		#region Properties

		/// <summary>
		/// Game object transform
		/// </summary>
		public Transform transform { get; internal set; }

		#endregion

		#region Functions

		#region Components

		private readonly List<MonoBehavior> _components;

		internal List<MonoBehavior> components => _components;

		/// <summary>
		/// Add a component to game object
		/// </summary>
		/// <typeparam name="T">The type of component</typeparam>
		/// <returns>The instance of the component</returns>
		/// <exception cref="DisallowMultipleComponentException"></exception>
		public T AddComponent<T>() where T : MonoBehavior, new()
		{
			return (T)AddComponent(typeof(T));
		}

		private static readonly Type _monoBahavoirType = typeof(MonoBehavior);

		/// <summary>
		/// Add a component to game object
		/// </summary>
		/// <param name="type">The type of component<br/>Type must be derived from <see cref="MonoBehavior"/></param>
		/// <returns>The instance of the component</returns>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="DisallowMultipleComponentException"></exception>
		public MonoBehavior AddComponent(Type type)
		{
			// Check for type
			if (!_monoBahavoirType.IsAssignableFrom(type))
			{
				throw new ArgumentException($"{_monoBahavoirType} is not assignable from {type}");
			}

			// Check for duplication
			var disallowMultipleAttribute = type.GetCustomAttribute<DisallowMultipleComponentAttribute>();
			if (disallowMultipleAttribute != null)
			{
				var duplicated = false;
				foreach (var component in _components)
				{
					if (component.GetType() == type)
					{
						duplicated = true;
						break;
					}
				}

				if (duplicated)
				{
					throw new DisallowMultipleComponentException(type.FullName);
				}
			}

			// Create component
			var instance = (MonoBehavior)Activator.CreateInstance(type);
			instance.gameObject = this;
			_components.Add(instance);

			// Special case: create RectTransform to replace Transform
			if (instance is RectTransform rectTransform)
			{
				var oldTransform = transform;
				if (oldTransform != null)
				{
					rectTransform.ReplaceOldTransform(oldTransform);
					Destroy(oldTransform);
				}

				transform = rectTransform;
			}

			// Check for required components
			var requiredComponentAttribute = type.GetCustomAttribute<RequireComponentAttribute>();
			if (requiredComponentAttribute != null)
			{
				foreach (var requiredType in requiredComponentAttribute.requiredTypes)
				{
					var exist = false;
					foreach (var component in _components)
					{
						if (component.GetType() == requiredType)
						{
							exist = true;
							break;
						}
					}
					if (!exist)
					{
						AddComponent(requiredType);
					}
				}
			}

			// Call Awake()
			instance.Awake();

			return instance;
		}

		/// <summary>
		/// Get the first component matching the type T
		/// </summary>
		/// <typeparam name="T">Component type</typeparam>
		/// <returns>The first component matching type the T</returns>
		public T GetComponent<T>() where T : MonoBehavior, new()
		{
			return (T)GetComponent(typeof(T));
		}

		/// <summary>
		/// Get the first component matching the type
		/// </summary>
		/// <param name="type">Component type</param>
		/// <returns>The first component matching the type</returns>
		public MonoBehavior GetComponent(Type type)
		{
			foreach (var component in _components)
			{
				if (component.GetType() == type)
				{
					return component;
				}
			}

			return null;
		}

		/// <summary>
		/// Get all components
		/// </summary>
		/// <returns>An array of components</returns>
		public MonoBehavior[] GetComponents()
		{
			return _components.ToArray();
		}

		/// <summary>
		/// Get all components matching the type T
		/// </summary>
		/// <typeparam name="T">Component type</typeparam>
		/// <returns>An array of components matching the type T</returns>
		public T[] GetComponents<T>() where T : MonoBehavior, new()
		{
			return GetComponents(typeof(T)).Cast<T>().ToArray();
		}

		/// <summary>
		/// Get all components matching the type
		/// </summary>
		/// <param name="type">Component type</param>
		/// <returns>An array of components matching the type</returns>
		public MonoBehavior[] GetComponents(Type type)
		{
			var list = new List<MonoBehavior>();
			foreach (var component in _components)
			{
				if (component.GetType() == type)
				{
					list.Add(component);
				}
			}

			return list.ToArray();
		}

		/// <summary>
		/// Get the first component in the children matching the type T
		/// </summary>
		/// <typeparam name="T">Component type</typeparam>
		/// <returns>The first component in the children matching the type T</returns>
		public T GetComponentInChildren<T>() where T : MonoBehavior, new()
		{
			return (T)GetComponentInChildren(typeof(T));
		}

		/// <summary>
		/// Get the first component in the children matching the type
		/// </summary>
		/// <param name="type">Component type</param>
		/// <returns>The first component in the children matching the type</returns>
		public MonoBehavior GetComponentInChildren(Type type)
		{
			var result = GetComponent(type);
			if (result != null)
			{
				return result;
			}
			foreach (var child in transform.children)
			{
				result = child.gameObject.GetComponentInChildren(type);
				if (result != null)
				{
					return result;
				}
			}
			return result;
		}

		/// <summary>
		/// Get all components in the children matching the type T
		/// </summary>
		/// <typeparam name="T">Component type</typeparam>
		/// <returns>An array of components matching the type</returns>
		public T[] GetComponentsInChildren<T>() where T : MonoBehavior, new()
		{
			return GetComponentsInChildren(typeof(T)).Cast<T>().ToArray();
		}

		/// <summary>
		/// Get all components in the children matching the type
		/// </summary>
		/// <param name="type">Component type</param>
		/// <returns>An array of components matching the type</returns>
		public MonoBehavior[] GetComponentsInChildren(Type type)
		{
			var list = new List<MonoBehavior>();
			list.AddRange(GetComponents(type));
			foreach (var child in transform.children)
			{
				list.AddRange(child.gameObject.GetComponentsInChildren(type));
			}
			return list.ToArray();
		}

		#endregion

		/// <inheritdoc/>
		public override void Destroy()
		{
			foreach (var component in components.ToArray())
			{
				Destroy(component);
			}

			base.Destroy();
		}

		/// <inheritdoc/>
		public override string ToString()
		{
			return name;
		}

		#endregion
	}
}
