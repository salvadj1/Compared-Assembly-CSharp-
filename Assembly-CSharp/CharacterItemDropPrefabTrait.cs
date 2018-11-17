using System;
using UnityEngine;

// Token: 0x02000109 RID: 265
public class CharacterItemDropPrefabTrait : CharacterTrait
{
	// Token: 0x17000175 RID: 373
	// (get) Token: 0x060006DA RID: 1754 RVA: 0x0001F42C File Offset: 0x0001D62C
	public GameObject prefab
	{
		get
		{
			return this._prefab;
		}
	}

	// Token: 0x04000518 RID: 1304
	[SerializeField]
	private GameObject _prefab;
}
