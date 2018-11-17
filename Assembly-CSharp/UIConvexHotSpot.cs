using System;
using UnityEngine;

// Token: 0x02000752 RID: 1874
public class UIConvexHotSpot : UIHotSpot
{
	// Token: 0x0600445B RID: 17499 RVA: 0x0010AB40 File Offset: 0x00108D40
	public UIConvexHotSpot() : base(UIHotSpot.Kind.Convex, true)
	{
	}

	// Token: 0x0600445C RID: 17500 RVA: 0x0010AB4C File Offset: 0x00108D4C
	internal Bounds? Internal_CalculateBounds(bool moved)
	{
		throw new NotImplementedException();
	}

	// Token: 0x0600445D RID: 17501 RVA: 0x0010AB54 File Offset: 0x00108D54
	internal bool Internal_RaycastRef(Ray ray, ref UIHotSpot.Hit hit)
	{
		throw new NotImplementedException();
	}

	// Token: 0x040024A9 RID: 9385
	private const UIHotSpot.Kind kKind = UIHotSpot.Kind.Convex;
}
