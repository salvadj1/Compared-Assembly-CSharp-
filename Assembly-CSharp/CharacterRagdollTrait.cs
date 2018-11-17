using System;
using UnityEngine;

// Token: 0x0200010F RID: 271
public class CharacterRagdollTrait : CharacterTrait
{
	// Token: 0x1700018C RID: 396
	// (get) Token: 0x060006FC RID: 1788 RVA: 0x0001F778 File Offset: 0x0001D978
	public GameObject ragdollPrefab
	{
		get
		{
			return this._ragdollPrefab;
		}
	}

	// Token: 0x0400052E RID: 1326
	[SerializeField]
	private GameObject _ragdollPrefab;
}
