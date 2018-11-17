using System;
using UnityEngine;

// Token: 0x0200045C RID: 1116
public class CharacterLoadoutTrait : CharacterTrait
{
	// Token: 0x1700093B RID: 2363
	// (get) Token: 0x060028A7 RID: 10407 RVA: 0x0009FB1C File Offset: 0x0009DD1C
	public Loadout loadout
	{
		get
		{
			return this._loadout;
		}
	}

	// Token: 0x0400149D RID: 5277
	[SerializeField]
	private Loadout _loadout;
}
