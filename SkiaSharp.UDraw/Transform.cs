using SkiaSharp.UDraw.Exceptions;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace SkiaSharp.UDraw
{
	/// <summary>
	/// Transform component
	/// </summary>
	[DisallowMultipleComponent]
	public class Transform : MonoBehavior
	{
		#region Properties

		private Transform _parent;

		/// <summary>
		/// Parent transform
		/// </summary>
		public Transform parent { get => _parent; set => SetParent(value); }

		/// <summary>
		/// Get the root transform
		/// </summary>
		public Transform root => _parent == null ? this : _parent.root;

		internal List<Transform> children { get; set; } = new();

		/// <summary>
		/// Child count
		/// </summary>
		public new int childCount => children.Count;

		/// <summary>
		/// World position
		/// </summary>
		public Vector2 position
		{
			get
			{
				if (parent == null)
				{
					return localPosition;
				}
				return parent.position + localPosition;
			}
			set
			{
				if (parent == null)
				{
					localPosition = value;
					return;
				}
				localPosition = value - parent.position;
			}
		}

		/// <summary>
		/// Local position relative to the parent transform
		/// </summary>
		public Vector2 localPosition { get; set; }

		/// <summary>
		/// World scale
		/// </summary>
		public Vector2 scale
		{
			get
			{
				if (parent == null)
				{
					return localScale;
				}
				return new(parent.scale.x * localScale.x, parent.scale.y * localScale.y);
			}
			set
			{
				if (parent == null)
				{
					localScale = value;
					return;
				}
				localScale = new(value.x / parent.scale.x, value.y / parent.scale.y);
			}
		}

		/// <summary>
		/// Local scale relative to the parent transform
		/// </summary>
		public Vector2 localScale { get; set; } = Vector2.one;

		/// <summary>
		/// Local rotation ralative to the parent transform in radians
		/// </summary>
		public float localRotation { get; set; }

		#endregion

		#region Functions

		#region Common

		/// <summary>
		/// Set parent transform
		/// </summary>
		/// <param name="newParent">New parent transform</param>
		public void SetParent(Transform newParent)
		{
			if (newParent == this)
			{
				throw new ArgumentException("Cannot set Transform.newParent to itself");
			}

			var oldParent = _parent;
			if (oldParent != null)
			{
				oldParent.children.Remove(this);
			}
			if (newParent != null)
			{
				if (!newParent.children.Contains(this))
				{
					newParent.children.Add(this);
				}
			}
			_parent = newParent;
		}

		/// <summary>
		/// Get the child transform by index. Index should be [0, childCount)
		/// </summary>
		/// <param name="index"></param>
		/// <returns>Child</returns>
		public new Transform GetChild(int index)
		{
			return children[index];
		}

		internal void ReplaceOldTransform(Transform oldTransform)
		{
			parent = oldTransform.parent;
			children.Clear();
			children.AddRange(oldTransform.children);
			localPosition = oldTransform.localPosition;
			localScale = oldTransform.localScale;
			localRotation = oldTransform.localRotation;

			foreach (var child in oldTransform.children.ToArray())
			{
				child.parent = this;
			}
		}

		#endregion

		#region Draw

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static Rect? MergeRect(Rect? rect1, Rect? rect2)
		{
			if (rect1 == null && rect2 == null)
			{
				return null;
			}
			if (rect1 != null && rect2 == null)
			{
				return rect1;
			}
			if (rect1 == null && rect2 != null)
			{
				return rect2;
			}
			var _rect1 = rect1.Value;
			var _rect2 = rect2.Value;

			var xMin = Mathf.Min(_rect1.x, _rect2.x);
			var yMin = Mathf.Min(_rect1.y, _rect2.y);
			var xMax = Mathf.Max(_rect1.x + _rect1.width, _rect2.x + _rect2.width);
			var yMax = Mathf.Max(_rect1.y + _rect1.height, _rect2.y + _rect2.height);
			return new Rect(xMin, yMin, xMax - xMin, yMax - yMin);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static Vector2 MinVector(Vector2 v1, Vector2 v2)
		{
			return new(Mathf.Min(v1.x, v2.x), Mathf.Min(v1.y, v2.y));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static Vector2 MaxVector(Vector2 v1, Vector2 v2)
		{
			return new(Mathf.Max(v1.x, v2.x), Mathf.Max(v1.y, v2.y));
		}

		internal virtual Rect? CalcRect()
		{
			Rect? result = null;

			foreach (var child in children)
			{
				result = MergeRect(child.CalcRect(), result);
			}

			return result;
		}

		internal void TransformCanvas(SKCanvas canvas)
		{
			canvas.Save();

			canvas.Translate(position);
			canvas.RotateRadians(localRotation);
			canvas.Scale(scale);
		}

		internal void RestoreCanvas(SKCanvas canvas)
		{
			canvas.Restore();
		}

		#endregion

		#region Message

		internal override void SendPreUpdate()
		{
			foreach (var component in gameObject.components)
			{
				if (component != this)
				{
					component.SendPreUpdate();
				}
				else
				{
					base.SendPreUpdate();
				}
			}

			foreach (var child in children)
			{
				child.SendPreUpdate();
			}
		}

		internal override void SendUpdate(double delta)
		{
			foreach (var component in gameObject.components)
			{
				if (component != this)
				{
					component.SendUpdate(delta);
				}
				else
				{
					base.SendUpdate(delta);
				}
			}

			foreach (var child in children)
			{
				child.SendUpdate(delta);
			}
		}

		internal override void SendCapture(SKRectI rect, SKCanvas canvas)
		{
			foreach (var component in gameObject.components)
			{
				if (component != this)
				{
					component.SendCapture(rect, canvas);
				}
				else
				{
					base.SendCapture(rect, canvas);
				}
			}

			foreach (var child in children)
			{
				child.SendCapture(rect, canvas);
			}
		}

		#endregion

		#region Destroy

		/// <inheritdoc/>
		public override void Destroy()
		{
			if (children != null)
			{
				if (children.Count > 0)
				{
					foreach (var child in children.ToArray())
					{
						Destroy(child);
					}
				}
				children = null;
			}
			if (parent != null)
			{
				parent = null;
			}
			if (gameObject != null)
			{
				gameObject.transform = null;
			}

			base.Destroy();
		}

		#endregion

		#endregion
	}
}
