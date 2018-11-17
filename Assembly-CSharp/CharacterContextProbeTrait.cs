using System;
using UnityEngine;

// Token: 0x0200050E RID: 1294
public class CharacterContextProbeTrait : global::CharacterTrait
{
	// Token: 0x17000998 RID: 2456
	// (get) Token: 0x06002C28 RID: 11304 RVA: 0x000A59AC File Offset: 0x000A3BAC
	public float rayLength
	{
		get
		{
			return this._rayLength;
		}
	}

	// Token: 0x04001618 RID: 5656
	[SerializeField]
	private float _rayLength = 3f;
}
