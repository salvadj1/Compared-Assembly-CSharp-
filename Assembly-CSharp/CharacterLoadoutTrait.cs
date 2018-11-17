using System;
using UnityEngine;

// Token: 0x02000512 RID: 1298
public class CharacterLoadoutTrait : global::CharacterTrait
{
	// Token: 0x170009A3 RID: 2467
	// (get) Token: 0x06002C37 RID: 11319 RVA: 0x000A5A9C File Offset: 0x000A3C9C
	public global::Loadout loadout
	{
		get
		{
			return this._loadout;
		}
	}

	// Token: 0x04001620 RID: 5664
	[SerializeField]
	private global::Loadout _loadout;
}
