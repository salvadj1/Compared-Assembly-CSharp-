using System;
using UnityEngine;

// Token: 0x02000832 RID: 2098
public class UIBrushHotSpot : global::UIHotSpot
{
	// Token: 0x060048B3 RID: 18611 RVA: 0x001142CC File Offset: 0x001124CC
	public UIBrushHotSpot() : base(global::UIHotSpot.Kind.Brush, true)
	{
	}

	// Token: 0x060048B4 RID: 18612 RVA: 0x001142DC File Offset: 0x001124DC
	internal Bounds? Internal_CalculateBounds(bool moved)
	{
		throw new NotImplementedException();
	}

	// Token: 0x060048B5 RID: 18613 RVA: 0x001142E4 File Offset: 0x001124E4
	internal bool Internal_RaycastRef(Ray ray, ref global::UIHotSpot.Hit hit)
	{
		throw new NotImplementedException();
	}

	// Token: 0x040026DC RID: 9948
	private const global::UIHotSpot.Kind kKind = global::UIHotSpot.Kind.Brush;
}
