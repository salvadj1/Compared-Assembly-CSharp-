using System;
using UnityEngine;

// Token: 0x0200074F RID: 1871
public class UIBoxHotSpot : UIHotSpot
{
	// Token: 0x0600444E RID: 17486 RVA: 0x0010A894 File Offset: 0x00108A94
	public UIBoxHotSpot() : base(UIHotSpot.Kind.Box, true)
	{
	}

	// Token: 0x0600444F RID: 17487 RVA: 0x0010A8A4 File Offset: 0x00108AA4
	internal Bounds? Internal_CalculateBounds(bool moved)
	{
		return new Bounds?(new Bounds(this.center, this.size));
	}

	// Token: 0x06004450 RID: 17488 RVA: 0x0010A8BC File Offset: 0x00108ABC
	internal bool Internal_RaycastRef(Ray ray, ref UIHotSpot.Hit hit)
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

	// Token: 0x06004451 RID: 17489 RVA: 0x0010A918 File Offset: 0x00108B18
	private void OnDrawGizmos()
	{
		Gizmos.matrix = base.gizmoMatrix;
		Gizmos.color = base.gizmoColor;
		Gizmos.DrawWireCube(this.center, this.size);
	}

	// Token: 0x040024A2 RID: 9378
	private const UIHotSpot.Kind kKind = UIHotSpot.Kind.Box;

	// Token: 0x040024A3 RID: 9379
	public new Vector3 center;

	// Token: 0x040024A4 RID: 9380
	public new Vector3 size;
}
