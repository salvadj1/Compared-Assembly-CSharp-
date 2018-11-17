using System;
using UnityEngine;

// Token: 0x02000128 RID: 296
public class CharacterItemDropPrefabTrait : global::CharacterTrait
{
	// Token: 0x170001A3 RID: 419
	// (get) Token: 0x060007AC RID: 1964 RVA: 0x00022000 File Offset: 0x00020200
	public GameObject prefab
	{
		get
		{
			return this._prefab;
		}
	}

	// Token: 0x040005E3 RID: 1507
	[SerializeField]
	private GameObject _prefab;
}
