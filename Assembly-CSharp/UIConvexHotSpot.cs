using System;
using UnityEngine;

// Token: 0x02000834 RID: 2100
public class UIConvexHotSpot : global::UIHotSpot
{
	// Token: 0x060048BC RID: 18620 RVA: 0x001144C0 File Offset: 0x001126C0
	public UIConvexHotSpot() : base(global::UIHotSpot.Kind.Convex, true)
	{
	}

	// Token: 0x060048BD RID: 18621 RVA: 0x001144CC File Offset: 0x001126CC
	internal Bounds? Internal_CalculateBounds(bool moved)
	{
		throw new NotImplementedException();
	}

	// Token: 0x060048BE RID: 18622 RVA: 0x001144D4 File Offset: 0x001126D4
	internal bool Internal_RaycastRef(Ray ray, ref global::UIHotSpot.Hit hit)
	{
		throw new NotImplementedException();
	}

	// Token: 0x040026E0 RID: 9952
	private const global::UIHotSpot.Kind kKind = global::UIHotSpot.Kind.Convex;
}
