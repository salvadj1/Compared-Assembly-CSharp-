using System;
using UnityEngine;

// Token: 0x020001E3 RID: 483
public abstract class BaseTraitMap : ScriptableObject
{
	// Token: 0x06000D42 RID: 3394 RVA: 0x00033948 File Offset: 0x00031B48
	internal BaseTraitMap()
	{
	}

	// Token: 0x06000D43 RID: 3395
	internal abstract void BindToRegistry();

	// Token: 0x06000D44 RID: 3396 RVA: 0x00033950 File Offset: 0x00031B50
	internal void BIND_REGISTRATION()
	{
		if (!this.bound)
		{
			this.BindToRegistry();
			this.bound = true;
		}
	}

	// Token: 0x04000826 RID: 2086
	[NonSerialized]
	private bool bound;
}
