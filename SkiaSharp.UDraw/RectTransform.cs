using System;
using System.Collections.Generic;
using System.Text;

namespace SkiaSharp.UDraw
{
	/// <summary>
	/// Rect transform component
	/// </summary>
	[DisallowMultipleComponent]
	public class RectTransform : Transform
	{
		#region Constructor

		/// <inheritdoc/>
		public RectTransform()
		{

		}

		#endregion

		#region Properties

		/// <summary>
		/// Size delta
		/// </summary>
		public Vector2 sizeDelta { get; set; } = Vector2.zero;

		/// <summary>
		/// Anchor min point
		/// </summary>
		public Vector2 anchorMin { get; set; } = new Vector2(0.5f, 0.5f);

		/// <summary>
		/// Anchor max point
		/// </summary>
		public Vector2 anchorMax { get; set; } = new Vector2(0.5f, 0.5f);

		/// <summary>
		/// Pivot
		/// </summary>
		public Vector2 pivot { get; set; } = new Vector2(0.5f, 0.5f);

		/// <summary>
		/// The actual size of the current RectTransform
		/// </summary>
		public Vector2 size
		{
			get
			{
				if (parent == null)
				{
					return sizeDelta;
				}
				if (parent is not RectTransform parentRectTransform)
				{
					return sizeDelta;
				}
				var parentSize = parentRectTransform.size;
				var anchorDelta = anchorMax - anchorMin;
				var anchorSize = parentSize * anchorDelta;
				return anchorSize + sizeDelta;
			}
		}

		/// <summary>
		/// The local rect of the current RectTransform
		/// </summary>
		public Rect rect
		{
			get
			{
				var size = this.size;
				var topLeft = new Vector2(-pivot.x * size.x, -pivot.y * size.y);
				return new(topLeft, size);
			}
		}

		/// <summary>
		/// The min world rect that the current RectTransform takes up after transform
		/// </summary>
		internal Rect worldRect
		{
			get
			{
				var rect = this.rect;

				var minPoint = rect.position;
				var maxPoint = rect.position;
				var points = new Vector2[]
				{
					new(rect.x, rect.y),
					new(rect.x + rect.width, rect.y),
					new(rect.x, rect.y + rect.height),
					new(rect.x + rect.width, rect.y + rect.height),
				};
				var rotateMat = Matrix2x2.FromRotation(localRotation);
				foreach (var point in points)
				{
					minPoint = MinVector(minPoint, rotateMat * point);
					maxPoint = MaxVector(maxPoint, rotateMat * point);
				}

				var actualRect = new Rect(minPoint, maxPoint - minPoint);
				actualRect *= scale;
				actualRect.position += position;
				return actualRect;
			}
		}

		/// <summary>
		/// Anchor rect relative to parent
		/// </summary>
		internal Rect anchorRect
		{
			get
			{
				Rect parentRect;
				if (parent == null)
				{
					parentRect = new();
				}
				else if (parent is RectTransform parentRectTransform)
				{
					parentRect = parentRectTransform.rect;
				}
				else
				{
					parentRect = new(parent.transform.localPosition, Vector2.zero);
				}
				var anchorMin = this.anchorMin;
				var anchorMax = this.anchorMax;
				var position = parentRect.position + parentRect.size * anchorMin;
				var size = parentRect.size * (anchorMax - anchorMin);
				return new(position, size);
			}
		}

		/// <summary>
		/// Anchored position
		/// </summary>
		public Vector2 anchoredPosition
		{
			get
			{
				var anchorRect = this.anchorRect;
				var anchorPivotPosition = anchorRect.position + anchorRect.size * pivot;
				return localPosition - anchorPivotPosition;
			}
			set
			{
				var anchorRect = this.anchorRect;
				var anchorPivotPosition = anchorRect.position + anchorRect.size * pivot;
				localPosition = anchorPivotPosition + value;
			}
		}

		/// <summary>
		/// Offset min
		/// </summary>
		public Vector2 offsetMin
		{
			get
			{
				return localPosition + rect.position - anchorRect.position;
			}
			set
			{
				var anchorRect = this.anchorRect;
				var targetTopLeft = anchorRect.position + value;
				var targetTopRight = anchorRect.position + anchorRect.size + offsetMax;
				var targetRect = new Rect(targetTopLeft, targetTopRight - targetTopLeft);
				sizeDelta = targetRect.size - anchorRect.size;
				var rect = this.rect;
				var deltaPosition = rect.position + localPosition - anchorRect.position - value;
				localPosition -= deltaPosition;
			}
		}

		/// <summary>
		/// Offset max
		/// </summary>
		public Vector2 offsetMax
		{
			get
			{
				var rect = this.rect;
				var anchorRect = this.anchorRect;
				return localPosition + rect.position + rect.size - anchorRect.position - anchorRect.size;
			}
			set
			{
				var anchorRect = this.anchorRect;
				var targetTopLeft = anchorRect.position + offsetMin;
				var targetTopRight = anchorRect.position + anchorRect.size + value;
				var targetRect = new Rect(targetTopLeft, targetTopRight - targetTopLeft);
				sizeDelta = targetRect.size - anchorRect.size;
				var rect = this.rect;
				var deltaPosition = rect.position + rect.size + localPosition - anchorRect.position - anchorRect.size - value;
				localPosition -= deltaPosition;
			}
		}

		#endregion

		#region Functions

		/// <inheritdoc/>
		public override void Destroy()
		{
			// Special case: create Transform to replace RectTransform
			if (gameObject != null)
			{
				var newTransform = new Transform()
				{
					gameObject = gameObject,
				};
				newTransform.ReplaceOldTransform(this);
				gameObject.components.Add(newTransform);
				gameObject.transform = newTransform;
			}

			base.Destroy();
		}

		internal override Rect? CalcRect()
		{
			Rect? result = MergeRect(worldRect, null);
			result = MergeRect(base.CalcRect(), result);

			return result;
		}

		#endregion
	}
}
