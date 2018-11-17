using System;
using UnityEngine;

// Token: 0x0200003B RID: 59
public sealed class FPGrassAssets : MonoBehaviour, IFPGrassAsset
{
	// Token: 0x06000233 RID: 563 RVA: 0x0000C724 File Offset: 0x0000A924
	public bool Contains(Object asset)
	{
		return Array.IndexOf<Object>(this.All, asset) != -1;
	}

	// Token: 0x04000166 RID: 358
	public Object[] All;
}
