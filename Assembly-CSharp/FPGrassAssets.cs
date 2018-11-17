using System;
using UnityEngine;

// Token: 0x0200004D RID: 77
public sealed class FPGrassAssets : MonoBehaviour, global::IFPGrassAsset
{
	// Token: 0x060002A5 RID: 677 RVA: 0x0000DCCC File Offset: 0x0000BECC
	public bool Contains(Object asset)
	{
		return Array.IndexOf<Object>(this.All, asset) != -1;
	}

	// Token: 0x040001C8 RID: 456
	public Object[] All;
}
