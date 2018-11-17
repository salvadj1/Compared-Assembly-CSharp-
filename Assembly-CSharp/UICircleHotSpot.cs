using System;
using UnityEngine;

// Token: 0x02000833 RID: 2099
public class UICircleHotSpot : global::UIHotSpot
{
	// Token: 0x060048B6 RID: 18614 RVA: 0x001142EC File Offset: 0x001124EC
	public UICircleHotSpot() : base(global::UIHotSpot.Kind.Circle, true)
	{
	}

	// Token: 0x17000DDE RID: 3550
	// (get) Token: 0x060048B7 RID: 18615 RVA: 0x00114304 File Offset: 0x00112504
	// (set) Token: 0x060048B8 RID: 18616 RVA: 0x00114314 File Offset: 0x00112514
	public new float size
	{
		get
		{
			return this.radius * 2f;
		}
		set
		{
			this.radius = value / 2f;
		}
	}

	// Token: 0x060048B9 RID: 18617 RVA: 0x00114324 File Offset: 0x00112524
	internal Bounds? Internal_CalculateBounds(bool moved)
	{
		float num = this.radius * 2f;
		return new Bounds?(new Bounds(this.center, new Vector3(num, num, 0f)));
	}

	// Token: 0x060048BA RID: 18618 RVA: 0x0011435C File Offset: 0x0011255C
	internal bool Internal_RaycastRef(Ray ray, ref global::UIHotSpot.Hit hit)
	{
		if (this.radius == 0f)
		{
			return false;
		}
		Plane plane;
		plane..ctor(global::UIHotSpot.forward, this.center);
		float num;
		if (!plane.Raycast(ray, ref num))
		{
			hit = default(global::UIHotSpot.Hit);
			return false;
		}
		hit.point = ray.GetPoint(num);
		hit.normal = ((!plane.GetSide(ray.origin)) ? global::UIHotSpot.backward : global::UIHotSpot.forward);
		Vector2 vector;
		vector.x = hit.point.x - this.center.x;
		vector.y = hit.point.y - this.center.y;
		float num2 = vector.x * vector.x + vector.y * vector.y;
		if (num2 < this.radius * this.radius)
		{
			hit.distance = Mathf.Sqrt(num2);
			return true;
		}
		return false;
	}

	// Token: 0x060048BB RID: 18619 RVA: 0x00114464 File Offset: 0x00112664
	private void OnDrawGizmos()
	{
		Gizmos.matrix = base.gizmoMatrix * Matrix4x4.TRS(this.center, Quaternion.identity, new Vector3(1f, 1f, 0.0001f));
		Gizmos.color = base.gizmoColor;
		Gizmos.DrawWireSphere(Vector3.zero, this.radius);
	}

	// Token: 0x040026DD RID: 9949
	private const global::UIHotSpot.Kind kKind = global::UIHotSpot.Kind.Circle;

	// Token: 0x040026DE RID: 9950
	public new Vector3 center;

	// Token: 0x040026DF RID: 9951
	public float radius = 0.5f;
}
