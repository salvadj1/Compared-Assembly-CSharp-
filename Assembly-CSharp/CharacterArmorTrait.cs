using System;
using UnityEngine;

// Token: 0x02000457 RID: 1111
public class CharacterArmorTrait : CharacterTrait
{
	// Token: 0x1700092F RID: 2351
	// (get) Token: 0x06002896 RID: 10390 RVA: 0x0009FA10 File Offset: 0x0009DC10
	public ArmorModelGroup defaultGroup
	{
		get
		{
			return this._defaultGroup;
		}
	}

	// Token: 0x04001494 RID: 5268
	[SerializeField]
	private ArmorModelGroup _defaultGroup;
}
