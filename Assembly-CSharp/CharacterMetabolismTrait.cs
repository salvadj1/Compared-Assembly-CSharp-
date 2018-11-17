using System;
using UnityEngine;

// Token: 0x0200045D RID: 1117
public class CharacterMetabolismTrait : CharacterTrait
{
	// Token: 0x1700093C RID: 2364
	// (get) Token: 0x060028A9 RID: 10409 RVA: 0x0009FB44 File Offset: 0x0009DD44
	public float tickRate
	{
		get
		{
			return this._tickRate;
		}
	}

	// Token: 0x1700093D RID: 2365
	// (get) Token: 0x060028AA RID: 10410 RVA: 0x0009FB4C File Offset: 0x0009DD4C
	public bool selfTick
	{
		get
		{
			return this._selfTick;
		}
	}

	// Token: 0x1700093E RID: 2366
	// (get) Token: 0x060028AB RID: 10411 RVA: 0x0009FB54 File Offset: 0x0009DD54
	public float hungerDamagePerMin
	{
		get
		{
			return this._hungerDamagePerMin;
		}
	}

	// Token: 0x0400149E RID: 5278
	[SerializeField]
	private float _tickRate = 3f;

	// Token: 0x0400149F RID: 5279
	[SerializeField]
	private bool _selfTick;

	// Token: 0x040014A0 RID: 5280
	[SerializeField]
	private float _hungerDamagePerMin = 5f;
}
