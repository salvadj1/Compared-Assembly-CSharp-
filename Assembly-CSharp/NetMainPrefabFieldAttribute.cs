using System;
using Facepunch.Attributes;

// Token: 0x0200047F RID: 1151
public sealed class NetMainPrefabFieldAttribute : Facepunch.Attributes.ObjectLookupFieldFixedTypeAttribute
{
	// Token: 0x060027F9 RID: 10233 RVA: 0x00091164 File Offset: 0x0008F364
	public NetMainPrefabFieldAttribute() : base(Facepunch.Attributes.PrefabLookupKinds.NetMain, typeof(global::NetMainPrefab), Facepunch.Attributes.SearchMode.MainAsset, null)
	{
	}
}
