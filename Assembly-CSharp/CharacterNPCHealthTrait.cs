using System;
using UnityEngine;

// Token: 0x0200012A RID: 298
public class CharacterNPCHealthTrait : global::CharacterTrait
{
	// Token: 0x170001A7 RID: 423
	// (get) Token: 0x060007B2 RID: 1970 RVA: 0x0002205C File Offset: 0x0002025C
	public float initialHealth
	{
		get
		{
			return this._initialHealth;
		}
	}

	// Token: 0x040005E6 RID: 1510
	[SerializeField]
	private float _initialHealth;
}
