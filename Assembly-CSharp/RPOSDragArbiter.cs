using System;
using UnityEngine;

// Token: 0x020004CB RID: 1227
public abstract class RPOSDragArbiter
{
	// Token: 0x17000953 RID: 2387
	// (get) Token: 0x06002A31 RID: 10801
	public abstract global::RPOSInventoryCell Instigator { get; }

	// Token: 0x17000954 RID: 2388
	// (get) Token: 0x06002A32 RID: 10802
	public abstract global::RPOSInventoryCell Under { get; }

	// Token: 0x06002A33 RID: 10803
	public abstract void HoverEnter(GameObject landing);

	// Token: 0x06002A34 RID: 10804
	public abstract void HoverExit(GameObject landing);

	// Token: 0x06002A35 RID: 10805
	public abstract void Land(GameObject landing);
}
