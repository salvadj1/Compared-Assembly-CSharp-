using System;
using UnityEngine;

// Token: 0x02000831 RID: 2097
public class UIBoxHotSpot : global::UIHotSpot
{
	// Token: 0x060048AF RID: 18607 RVA: 0x00114214 File Offset: 0x00112414
	public UIBoxHotSpot() : base(global::UIHotSpot.Kind.Box, true)
	{
	}

	// Token: 0x060048B0 RID: 18608 RVA: 0x00114224 File Offset: 0x00112424
	internal Bounds? Internal_CalculateBounds(bool moved)
	{
		return new Bounds?(new Bounds(this.center, this.size));
	}

	// Token: 0x060048B1 RID: 18609 RVA: 0x0011423C File Offset: 0x0011243C
	internal bool Internal_RaycastRef(Ray ray, ref global::UIHotSpot.Hit hit)
	{
		Bounds bounds;
		bounds..ctor(this.center, this.size);
		if (bounds.IntersectRay(ray, ref hit.distance))
		{
			hit.point = ray.GetPoint(hit.distance);
			hit.normal = -ray.direction;
			return true;
		}
		return false;
	}

	// Token: 0x060048B2 RID: 18610 RVA: 0x00114298 File Offset: 0x00112498
	private void OnDrawGizmos()
	{
		Gizmos.matrix = base.gizmoMatrix;
		Gizmos.color = base.gizmoColor;
		Gizmos.DrawWireCube(this.center, this.size);
	}

	// Token: 0x040026D9 RID: 9945
	private const global::UIHotSpot.Kind kKind = global::UIHotSpot.Kind.Box;

	// Token: 0x040026DA RID: 9946
	public new Vector3 center;

	// Token: 0x040026DB RID: 9947
	public new Vector3 size;
}
