using System;
using UnityEngine;

// Token: 0x0200083B RID: 2107
public class UISphereHotSpot : global::UIHotSpot
{
	// Token: 0x06004907 RID: 18695 RVA: 0x001167F8 File Offset: 0x001149F8
	public UISphereHotSpot() : base(global::UIHotSpot.Kind.Sphere, true)
	{
	}

	// Token: 0x17000DFC RID: 3580
	// (get) Token: 0x06004908 RID: 18696 RVA: 0x00116814 File Offset: 0x00114A14
	// (set) Token: 0x06004909 RID: 18697 RVA: 0x00116824 File Offset: 0x00114A24
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

	// Token: 0x0600490A RID: 18698 RVA: 0x00116834 File Offset: 0x00114A34
	internal Bounds? Internal_CalculateBounds(bool moved)
	{
		float num = this.radius * 2f;
		return new Bounds?(new Bounds(this.center, new Vector3(num, num, num)));
	}

	// Token: 0x0600490B RID: 18699 RVA: 0x00116868 File Offset: 0x00114A68
	internal bool Internal_RaycastRef(Ray ray, ref global::UIHotSpot.Hit hit)
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

	// Token: 0x0600490C RID: 18700 RVA: 0x00116928 File Offset: 0x00114B28
	private void OnDrawGizmos()
	{
		Gizmos.matrix = base.gizmoMatrix;
		Gizmos.color = base.gizmoColor;
		Gizmos.DrawWireSphere(this.center, this.radius);
	}

	// Token: 0x0400271A RID: 10010
	private const global::UIHotSpot.Kind kKind = global::UIHotSpot.Kind.Sphere;

	// Token: 0x0400271B RID: 10011
	public new Vector3 center;

	// Token: 0x0400271C RID: 10012
	public float radius = 0.5f;
}
