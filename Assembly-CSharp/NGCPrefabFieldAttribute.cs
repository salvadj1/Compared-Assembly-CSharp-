using System;
using Facepunch.Attributes;
using UnityEngine;

// Token: 0x020003D5 RID: 981
public sealed class NGCPrefabFieldAttribute : ObjectLookupFieldFixedTypeAttribute
{
	// Token: 0x0600249A RID: 9370 RVA: 0x0008BDB0 File Offset: 0x00089FB0
	public NGCPrefabFieldAttribute() : base(PrefabLookupKinds.NGC, typeof(GameObject), SearchMode.MainAsset, null)
	{
	}
}
