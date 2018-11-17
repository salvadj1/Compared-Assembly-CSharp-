using System;
using UnityEngine;

// Token: 0x0200010A RID: 266
public class CharacterMeleeTrait : CharacterTrait
{
	// Token: 0x17000176 RID: 374
	// (get) Token: 0x060006DC RID: 1756 RVA: 0x0001F454 File Offset: 0x0001D654
	public float minDamage
	{
		get
		{
			return this._minDamage;
		}
	}

	// Token: 0x17000177 RID: 375
	// (get) Token: 0x060006DD RID: 1757 RVA: 0x0001F45C File Offset: 0x0001D65C
	public float maxDamage
	{
		get
		{
			return this._maxDamage;
		}
	}

	// Token: 0x17000178 RID: 376
	// (get) Token: 0x060006DE RID: 1758 RVA: 0x0001F464 File Offset: 0x0001D664
	public float randomDamage
	{
		get
		{
			return this._minDamage + (this._maxDamage - this._minDamage) * Random.value;
		}
	}

	// Token: 0x04000519 RID: 1305
	[SerializeField]
	private float _minDamage = 15f;

	// Token: 0x0400051A RID: 1306
	[SerializeField]
	private float _maxDamage = 25f;
}
