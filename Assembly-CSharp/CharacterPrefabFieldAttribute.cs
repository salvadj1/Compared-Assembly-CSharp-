using System;
using Facepunch.Attributes;

// Token: 0x020003D3 RID: 979
public sealed class CharacterPrefabFieldAttribute : ObjectLookupFieldFixedTypeAttribute
{
	// Token: 0x06002498 RID: 9368 RVA: 0x0008BD80 File Offset: 0x00089F80
	public CharacterPrefabFieldAttribute() : base(PrefabLookupKinds.Character, typeof(CharacterPrefab), SearchMode.MainAsset, null)
	{
	}
}
