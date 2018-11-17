using System;
using UnityEngine;

// Token: 0x02000750 RID: 1872
public class UIBrushHotSpot : UIHotSpot
{
	// Token: 0x06004452 RID: 17490 RVA: 0x0010A94C File Offset: 0x00108B4C
	public UIBrushHotSpot() : base(UIHotSpot.Kind.Brush, true)
	{
	}

	// Token: 0x06004453 RID: 17491 RVA: 0x0010A95C File Offset: 0x00108B5C
	internal Bounds? Internal_CalculateBounds(bool moved)
	{
		throw new NotImplementedException();
	}

	// Token: 0x06004454 RID: 17492 RVA: 0x0010A964 File Offset: 0x00108B64
	internal bool Internal_RaycastRef(Ray ray, ref UIHotSpot.Hit hit)
	{
		throw new NotImplementedException();
	}

	// Token: 0x040024A5 RID: 9381
	private const UIHotSpot.Kind kKind = UIHotSpot.Kind.Brush;
}
