using System;
using UnityEngine;

// Token: 0x02000513 RID: 1299
public class CharacterMetabolismTrait : global::CharacterTrait
{
	// Token: 0x170009A4 RID: 2468
	// (get) Token: 0x06002C39 RID: 11321 RVA: 0x000A5AC4 File Offset: 0x000A3CC4
	public float tickRate
	{
		get
		{
			return this._tickRate;
		}
	}

	// Token: 0x170009A5 RID: 2469
	// (get) Token: 0x06002C3A RID: 11322 RVA: 0x000A5ACC File Offset: 0x000A3CCC
	public bool selfTick
	{
		get
		{
			return this._selfTick;
		}
	}

	// Token: 0x170009A6 RID: 2470
	// (get) Token: 0x06002C3B RID: 11323 RVA: 0x000A5AD4 File Offset: 0x000A3CD4
	public float hungerDamagePerMin
	{
		get
		{
			return this._hungerDamagePerMin;
		}
	}

	// Token: 0x04001621 RID: 5665
	[SerializeField]
	private float _tickRate = 3f;

	// Token: 0x04001622 RID: 5666
	[SerializeField]
	private bool _selfTick;

	// Token: 0x04001623 RID: 5667
	[SerializeField]
	private float _hungerDamagePerMin = 5f;
}
