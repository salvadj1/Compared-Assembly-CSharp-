using System;
using UnityEngine;

// Token: 0x0200029E RID: 670
[AddComponentMenu("")]
[RequireComponent(typeof(CCDesc))]
public sealed class CCTotemicFigure : CCTotem<CCTotem.TotemicFigure, CCTotemicFigure>
{
	// Token: 0x060017F6 RID: 6134 RVA: 0x0005D8A0 File Offset: 0x0005BAA0
	private void OnDrawGizmos()
	{
		if (this.totemicObject != null)
		{
			float num = 3.14159274f * Time.time + 0.7853982f * (float)this.totemicObject.BottomUpIndex;
			Vector3 vector = Camera.current.cameraToWorldMatrix.MultiplyVector(new Vector3(Mathf.Sin(num) * 0.25f + 0.75f, 0f, 0f));
			Vector3 vector2 = -vector;
			Gizmos.color = Color.green;
			Gizmos.DrawLine(this.totemicObject.TopOrigin, this.totemicObject.TopOrigin + vector);
			Gizmos.DrawLine(this.totemicObject.SlideTopOrigin + vector, this.totemicObject.TopOrigin + vector);
			Gizmos.color = Color.blue;
			Gizmos.DrawLine(this.totemicObject.BottomOrigin, this.totemicObject.BottomOrigin + vector2);
			Gizmos.DrawLine(this.totemicObject.BottomOrigin + vector2, this.totemicObject.SlideBottomOrigin + vector2);
			Gizmos.color = (((this.totemicObject.BottomUpIndex & 1) != 1) ? Color.red : new Color(1f, 0.4f, 0.4f, 1f));
			Gizmos.DrawLine(this.totemicObject.SlideBottomOrigin + vector, this.totemicObject.SlideBottomOrigin + vector2);
			Gizmos.DrawLine(this.totemicObject.SlideTopOrigin + vector, this.totemicObject.SlideTopOrigin + vector2);
			Gizmos.DrawLine(this.totemicObject.SlideBottomOrigin + vector2, this.totemicObject.SlideTopOrigin + vector2);
			Gizmos.DrawLine(this.totemicObject.SlideBottomOrigin + vector, this.totemicObject.SlideTopOrigin + vector);
		}
	}
}
