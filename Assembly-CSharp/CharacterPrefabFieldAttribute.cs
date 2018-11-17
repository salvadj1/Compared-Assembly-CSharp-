using System;
using Facepunch.Attributes;

// Token: 0x02000480 RID: 1152
public sealed class CharacterPrefabFieldAttribute : Facepunch.Attributes.ObjectLookupFieldFixedTypeAttribute
{
	// Token: 0x060027FA RID: 10234 RVA: 0x0009117C File Offset: 0x0008F37C
	public CharacterPrefabFieldAttribute() : base(Facepunch.Attributes.PrefabLookupKinds.Character, typeof(global::CharacterPrefab), Facepunch.Attributes.SearchMode.MainAsset, null)
	{
	}
}
