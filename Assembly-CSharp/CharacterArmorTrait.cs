using System;
using UnityEngine;

// Token: 0x0200050D RID: 1293
public class CharacterArmorTrait : global::CharacterTrait
{
	// Token: 0x17000997 RID: 2455
	// (get) Token: 0x06002C26 RID: 11302 RVA: 0x000A5990 File Offset: 0x000A3B90
	public global::ArmorModelGroup defaultGroup
	{
		get
		{
			return this._defaultGroup;
		}
	}

	// Token: 0x04001617 RID: 5655
	[SerializeField]
	private global::ArmorModelGroup _defaultGroup;
}
