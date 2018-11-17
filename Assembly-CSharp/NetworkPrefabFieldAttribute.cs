using System;
using Facepunch.Attributes;

// Token: 0x0200047E RID: 1150
public sealed class NetworkPrefabFieldAttribute : Facepunch.Attributes.ObjectLookupFieldFixedTypeAttribute
{
	// Token: 0x060027F8 RID: 10232 RVA: 0x00091154 File Offset: 0x0008F354
	public NetworkPrefabFieldAttribute() : base(Facepunch.Attributes.PrefabLookupKinds.Net, null, Facepunch.Attributes.SearchMode.MainAsset, null)
	{
	}
}
