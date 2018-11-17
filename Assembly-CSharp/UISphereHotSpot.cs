using System;
using UnityEngine;

// Token: 0x02000759 RID: 1881
public class UISphereHotSpot : UIHotSpot
{
	// Token: 0x060044A6 RID: 17574 RVA: 0x0010CE78 File Offset: 0x0010B078
	public UISphereHotSpot() : base(UIHotSpot.Kind.Sphere, true)
	{
	}

	// Token: 0x17000D6C RID: 3436
	// (get) Token: 0x060044A7 RID: 17575 RVA: 0x0010CE94 File Offset: 0x0010B094
	// (set) Token: 0x060044A8 RID: 17576 RVA: 0x0010CEA4 File Offset: 0x0010B0A4
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

	// Token: 0x060044A9 RID: 17577 RVA: 0x0010CEB4 File Offset: 0x0010B0B4
	internal Bounds? Internal_CalculateBounds(bool moved)
	{
		float num = this.radius * 2f;
		return new Bounds?(new Bounds(this.center, new Vector3(num, num, num)));
	}

	// Token: 0x060044AA RID: 17578 RVA: 0x0010CEE8 File Offset: 0x0010B0E8
	internal bool Internal_RaycastRef(Ray ray, ref UIHotSpot.Hit hit)
	{
		float num;
		float num2;
		IntersectHint intersectHint = Intersect3D.RayCircleInfiniteForward(ray, this.center, this.radius, ref num, ref num2);
		switch (intersectHint)
		{
		case 1:
		case 2:
		case 3:
			hit.distance = Mathf.Min(num, num2);
			if (hit.distance < 0f && (hit.distance = Mathf.Max(num, num2)) < 0f)
			{
				return false;
			}
			hit.point = ray.GetPoint(hit.distance);
			hit.normal = Vector3.Normalize(hit.point - this.center);
			return true;
		default:
			Debug.Log(intersectHint, this);
			return false;
		}
	}

	// Token: 0x060044AB RID: 17579 RVA: 0x0010CFA8 File Offset: 0x0010B1A8
	private void OnDrawGizmos()
	{
		Gizmos.matrix = base.gizmoMatrix;
		Gizmos.color = base.gizmoColor;
		Gizmos.DrawWireSphere(this.center, this.radius);
	}

	// Token: 0x040024E3 RID: 9443
	private const UIHotSpot.Kind kKind = UIHotSpot.Kind.Sphere;

	// Token: 0x040024E4 RID: 9444
	public new Vector3 center;

	// Token: 0x040024E5 RID: 9445
	public float radius = 0.5f;
}
