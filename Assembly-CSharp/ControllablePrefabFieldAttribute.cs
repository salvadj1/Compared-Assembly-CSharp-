using System;
using Facepunch.Attributes;

// Token: 0x020003D4 RID: 980
public sealed class ControllablePrefabFieldAttribute : ObjectLookupFieldFixedTypeAttribute
{
	// Token: 0x06002499 RID: 9369 RVA: 0x0008BD98 File Offset: 0x00089F98
	public ControllablePrefabFieldAttribute() : base(PrefabLookupKinds.Controllable, typeof(ControllablePrefab), SearchMode.MainAsset, null)
	{
	}
}
