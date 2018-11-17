using System;
using UnityEngine;

// Token: 0x0200083A RID: 2106
public class UIRectHotSpot : global::UIHotSpot
{
	// Token: 0x06004903 RID: 18691 RVA: 0x001165EC File Offset: 0x001147EC
	public UIRectHotSpot() : base(global::UIHotSpot.Kind.Rect, true)
	{
	}

	// Token: 0x06004904 RID: 18692 RVA: 0x00116604 File Offset: 0x00114804
	internal Bounds? Internal_CalculateBounds(bool moved)
	{
		return new Bounds?(new Bounds(this.center, this.size));
	}

	// Token: 0x06004905 RID: 18693 RVA: 0x00116624 File Offset: 0x00114824
	internal bool Internal_RaycastRef(Ray ray, ref global::UIHotSpot.Hit hit)
	{
		if (this.size.x < 2.802597E-45f || this.size.y < 2.802597E-45f)
		{
			return false;
		}
		hit.normal = global::UIHotSpot.forward;
		Plane plane;
		plane..ctor(global::UIHotSpot.forward, this.center);
		float num;
		if (!plane.Raycast(ray, ref num))
		{
			hit = default(global::UIHotSpot.Hit);
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

	// Token: 0x06004906 RID: 18694 RVA: 0x001167BC File Offset: 0x001149BC
	private void OnDrawGizmos()
	{
		Gizmos.matrix = base.gizmoMatrix;
		Gizmos.color = base.gizmoColor;
		Gizmos.DrawWireCube(this.center, this.size);
	}

	// Token: 0x04002716 RID: 10006
	private const global::UIHotSpot.Kind kKind = global::UIHotSpot.Kind.Rect;

	// Token: 0x04002717 RID: 10007
	private const float kEpsilon = 2.802597E-45f;

	// Token: 0x04002718 RID: 10008
	public new Vector3 center;

	// Token: 0x04002719 RID: 10009
	public new Vector2 size = Vector2.one;
}
