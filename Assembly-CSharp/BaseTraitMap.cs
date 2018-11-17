using System;
using UnityEngine;

// Token: 0x02000214 RID: 532
public abstract class BaseTraitMap : ScriptableObject
{
	// Token: 0x06000E8A RID: 3722 RVA: 0x000379D0 File Offset: 0x00035BD0
	internal BaseTraitMap()
	{
	}

	// Token: 0x06000E8B RID: 3723
	internal abstract void BindToRegistry();

	// Token: 0x06000E8C RID: 3724 RVA: 0x000379D8 File Offset: 0x00035BD8
	internal void BIND_REGISTRATION()
	{
		if (!this.bound)
		{
			this.BindToRegistry();
			this.bound = true;
		}
	}

	// Token: 0x0400093E RID: 2366
	[NonSerialized]
	private bool bound;
}
