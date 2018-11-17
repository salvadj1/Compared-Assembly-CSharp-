using System;
using UnityEngine;

// Token: 0x02000049 RID: 73
public class FPGrassDisplacementTrail : global::FPGrassDisplacementObject
{
	// Token: 0x06000282 RID: 642 RVA: 0x0000D284 File Offset: 0x0000B484
	public override void Initialize()
	{
		this._trail = base.GetComponent<TrailRenderer>();
	}

	// Token: 0x06000283 RID: 643 RVA: 0x0000D294 File Offset: 0x0000B494
	public override void DetachAndDestroy()
	{
		base.transform.parent = null;
		Object.Destroy(base.gameObject, this._trail.time * 1.5f);
	}

	// Token: 0x040001A3 RID: 419
	public TrailRenderer _trail;
}
