using System;
using UnityEngine;

// Token: 0x0200010B RID: 267
public class CharacterNPCHealthTrait : CharacterTrait
{
	// Token: 0x17000179 RID: 377
	// (get) Token: 0x060006E0 RID: 1760 RVA: 0x0001F488 File Offset: 0x0001D688
	public float initialHealth
	{
		get
		{
			return this._initialHealth;
		}
	}

	// Token: 0x0400051B RID: 1307
	[SerializeField]
	private float _initialHealth;
}
