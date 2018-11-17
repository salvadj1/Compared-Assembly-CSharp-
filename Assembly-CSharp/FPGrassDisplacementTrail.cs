using System;
using UnityEngine;

// Token: 0x02000037 RID: 55
public class FPGrassDisplacementTrail : FPGrassDisplacementObject
{
	// Token: 0x06000210 RID: 528 RVA: 0x0000BCDC File Offset: 0x00009EDC
	public override void Initialize()
	{
		this._trail = base.GetComponent<TrailRenderer>();
	}

	// Token: 0x06000211 RID: 529 RVA: 0x0000BCEC File Offset: 0x00009EEC
	public override void DetachAndDestroy()
	{
		base.transform.parent = null;
		Object.Destroy(base.gameObject, this._trail.time * 1.5f);
	}

	// Token: 0x04000141 RID: 321
	public TrailRenderer _trail;
}
