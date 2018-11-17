using System;
using Facepunch.Attributes;

// Token: 0x020003D2 RID: 978
public sealed class NetMainPrefabFieldAttribute : ObjectLookupFieldFixedTypeAttribute
{
	// Token: 0x06002497 RID: 9367 RVA: 0x0008BD68 File Offset: 0x00089F68
	public NetMainPrefabFieldAttribute() : base(PrefabLookupKinds.NetMain, typeof(NetMainPrefab), SearchMode.MainAsset, null)
	{
	}
}
