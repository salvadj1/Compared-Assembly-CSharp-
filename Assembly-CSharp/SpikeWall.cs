using System;
using System.Collections.Generic;

// Token: 0x02000730 RID: 1840
public class SpikeWall : IDLocal
{
	// Token: 0x04001F35 RID: 7989
	public float returnFraction = 0.2f;

	// Token: 0x04001F36 RID: 7990
	public float dmgPerTick = 20f;

	// Token: 0x04001F37 RID: 7991
	public float baseReturnDmg = 5f;

	// Token: 0x04001F38 RID: 7992
	public List<global::TakeDamage> _touching;

	// Token: 0x04001F39 RID: 7993
	private bool running;
}
