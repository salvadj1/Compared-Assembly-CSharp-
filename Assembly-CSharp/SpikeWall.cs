using System;
using System.Collections.Generic;

// Token: 0x0200066C RID: 1644
public class SpikeWall : IDLocal
{
	// Token: 0x04001D3D RID: 7485
	public float returnFraction = 0.2f;

	// Token: 0x04001D3E RID: 7486
	public float dmgPerTick = 20f;

	// Token: 0x04001D3F RID: 7487
	public float baseReturnDmg = 5f;

	// Token: 0x04001D40 RID: 7488
	public List<TakeDamage> _touching;

	// Token: 0x04001D41 RID: 7489
	private bool running;
}
