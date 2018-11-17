using System;
using UnityEngine;

// Token: 0x0200012E RID: 302
public class CharacterRagdollTrait : global::CharacterTrait
{
	// Token: 0x170001BA RID: 442
	// (get) Token: 0x060007CE RID: 1998 RVA: 0x0002234C File Offset: 0x0002054C
	public GameObject ragdollPrefab
	{
		get
		{
			return this._ragdollPrefab;
		}
	}

	// Token: 0x040005F9 RID: 1529
	[SerializeField]
	private GameObject _ragdollPrefab;
}
