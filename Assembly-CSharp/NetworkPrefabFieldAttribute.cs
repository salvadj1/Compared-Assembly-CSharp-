using System;
using Facepunch.Attributes;

// Token: 0x020003D1 RID: 977
public sealed class NetworkPrefabFieldAttribute : ObjectLookupFieldFixedTypeAttribute
{
	// Token: 0x06002496 RID: 9366 RVA: 0x0008BD58 File Offset: 0x00089F58
	public NetworkPrefabFieldAttribute() : base(PrefabLookupKinds.Net, null, SearchMode.MainAsset, null)
	{
	}
}
