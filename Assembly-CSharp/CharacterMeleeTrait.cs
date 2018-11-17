using System;
using UnityEngine;

// Token: 0x02000129 RID: 297
public class CharacterMeleeTrait : global::CharacterTrait
{
	// Token: 0x170001A4 RID: 420
	// (get) Token: 0x060007AE RID: 1966 RVA: 0x00022028 File Offset: 0x00020228
	public float minDamage
	{
		get
		{
			return this._minDamage;
		}
	}

	// Token: 0x170001A5 RID: 421
	// (get) Token: 0x060007AF RID: 1967 RVA: 0x00022030 File Offset: 0x00020230
	public float maxDamage
	{
		get
		{
			return this._maxDamage;
		}
	}

	// Token: 0x170001A6 RID: 422
	// (get) Token: 0x060007B0 RID: 1968 RVA: 0x00022038 File Offset: 0x00020238
	public float randomDamage
	{
		get
		{
			return this._minDamage + (this._maxDamage - this._minDamage) * Random.value;
		}
	}

	// Token: 0x040005E4 RID: 1508
	[SerializeField]
	private float _minDamage = 15f;

	// Token: 0x040005E5 RID: 1509
	[SerializeField]
	private float _maxDamage = 25f;
}
