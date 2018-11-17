using System;
using UnityEngine;

// Token: 0x02000758 RID: 1880
public class UIRectHotSpot : UIHotSpot
{
	// Token: 0x060044A2 RID: 17570 RVA: 0x0010CC6C File Offset: 0x0010AE6C
	public UIRectHotSpot() : base(UIHotSpot.Kind.Rect, true)
	{
	}

	// Token: 0x060044A3 RID: 17571 RVA: 0x0010CC84 File Offset: 0x0010AE84
	internal Bounds? Internal_CalculateBounds(bool moved)
	{
		return new Bounds?(new Bounds(this.center, this.size));
	}

	// Token: 0x060044A4 RID: 17572 RVA: 0x0010CCA4 File Offset: 0x0010AEA4
	internal bool Internal_RaycastRef(Ray ray, ref UIHotSpot.Hit hit)
	{
		if (this.size.x < 2.802597E-45f || this.size.y < 2.802597E-45f)
		{
			return false;
		}
		hit.normal = UIHotSpot.forward;
		Plane plane;
		plane..ctor(UIHotSpot.forward, this.center);
		float num;
		if (!plane.Raycast(ray, ref num))
		{
			hit = default(UIHotSpot.Hit);
			return false;
		}
		hit.point = ray.GetPoint(num);
		Vector2 vector;
		vector.x = ((hit.point.x >= this.center.x) ? (hit.point.x - this.center.x) : (this.center.x - hit.point.x));
		vector.y = ((hit.point.y >= this.center.y) ? (hit.point.y - this.center.y) : (this.center.y - hit.point.y));
		if (vector.x * 2f <= this.size.x && vector.y * 2f <= this.size.y)
		{
			hit.distance = Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y);
			return true;
		}
		return false;
	}

	// Token: 0x060044A5 RID: 17573 RVA: 0x0010CE3C File Offset: 0x0010B03C
	private void OnDrawGizmos()
	{
		Gizmos.matrix = base.gizmoMatrix;
		Gizmos.color = base.gizmoColor;
		Gizmos.DrawWireCube(this.center, this.size);
	}

	// Token: 0x040024DF RID: 9439
	private const UIHotSpot.Kind kKind = UIHotSpot.Kind.Rect;

	// Token: 0x040024E0 RID: 9440
	private const float kEpsilon = 2.802597E-45f;

	// Token: 0x040024E1 RID: 9441
	public new Vector3 center;

	// Token: 0x040024E2 RID: 9442
	public new Vector2 size = Vector2.one;
}
