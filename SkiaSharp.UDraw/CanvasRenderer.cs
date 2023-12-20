using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp.UDraw.UI;

namespace SkiaSharp.UDraw
{
    /// <summary>
    /// Canvas renderer component
    /// </summary>
    [DisallowMultipleComponent, RequireComponent(typeof(RectTransform))]
    public class CanvasRenderer : MonoBehavior
    {
        /// <inheritdoc/>
        public override void Capture(SKRectI rect, SKCanvas canvas)
        {
            if (!enable)
            {
                return;
            }
            var canvasComponent = GetComponent<Canvas>();
            if (canvasComponent == null)
            {
                return;
            }

            canvasComponent.Draw(rect, canvas);

            base.Capture(rect, canvas);
        }
    }
}
