using System;
using Facepunch.Attributes;

// Token: 0x02000481 RID: 1153
public sealed class ControllablePrefabFieldAttribute : Facepunch.Attributes.ObjectLookupFieldFixedTypeAttribute
{
	// Token: 0x060027FB RID: 10235 RVA: 0x00091194 File Offset: 0x0008F394
	public ControllablePrefabFieldAttribute() : base(Facepunch.Attributes.PrefabLookupKinds.Controllable, typeof(global::ControllablePrefab), Facepunch.Attributes.SearchMode.MainAsset, null)
	{
	}
}
