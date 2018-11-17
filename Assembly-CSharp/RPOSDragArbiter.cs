using System;
using UnityEngine;

// Token: 0x02000416 RID: 1046
public abstract class RPOSDragArbiter
{
	// Token: 0x170008ED RID: 2285
	// (get) Token: 0x060026A7 RID: 9895
	public abstract RPOSInventoryCell Instigator { get; }

	// Token: 0x170008EE RID: 2286
	// (get) Token: 0x060026A8 RID: 9896
	public abstract RPOSInventoryCell Under { get; }

	// Token: 0x060026A9 RID: 9897
	public abstract void HoverEnter(GameObject landing);

	// Token: 0x060026AA RID: 9898
	public abstract void HoverExit(GameObject landing);

	// Token: 0x060026AB RID: 9899
	public abstract void Land(GameObject landing);
}
