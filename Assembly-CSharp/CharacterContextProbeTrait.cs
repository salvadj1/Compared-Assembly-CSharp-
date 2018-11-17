using System;
using UnityEngine;

// Token: 0x02000458 RID: 1112
public class CharacterContextProbeTrait : CharacterTrait
{
	// Token: 0x17000930 RID: 2352
	// (get) Token: 0x06002898 RID: 10392 RVA: 0x0009FA2C File Offset: 0x0009DC2C
	public float rayLength
	{
		get
		{
			return this._rayLength;
		}
	}

	// Token: 0x04001495 RID: 5269
	[SerializeField]
	private float _rayLength = 3f;
}
