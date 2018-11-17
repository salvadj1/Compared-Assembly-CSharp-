using System;
using Facepunch.Attributes;
using UnityEngine;

// Token: 0x02000482 RID: 1154
public sealed class NGCPrefabFieldAttribute : Facepunch.Attributes.ObjectLookupFieldFixedTypeAttribute
{
	// Token: 0x060027FC RID: 10236 RVA: 0x000911AC File Offset: 0x0008F3AC
	public NGCPrefabFieldAttribute() : base(Facepunch.Attributes.PrefabLookupKinds.NGC, typeof(GameObject), Facepunch.Attributes.SearchMode.MainAsset, null)
	{
	}
}
