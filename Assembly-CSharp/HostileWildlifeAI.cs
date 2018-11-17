using System;
using System.Collections.Generic;
using Facepunch.Procedural;
using uLink;
using UnityEngine;

// Token: 0x020004EA RID: 1258
public class HostileWildlifeAI : global::BasicWildLifeAI
{
	// Token: 0x06002B57 RID: 11095 RVA: 0x000A13A4 File Offset: 0x0009F5A4
	public void GoScentBlind(float dur)
	{
		this.nextScentListenTime = Time.time + dur;
	}

	// Token: 0x06002B58 RID: 11096 RVA: 0x000A13B4 File Offset: 0x0009F5B4
	public bool IsScentBlind()
	{
		return Time.time < this.nextScentListenTime;
	}

	// Token: 0x06002B59 RID: 11097 RVA: 0x000A13C4 File Offset: 0x0009F5C4
	[RPC]
	public void CL_Attack(uLink.NetworkMessageInfo info)
	{
		global::InterpTimedEvent.Queue(this, "ATK", ref info);
	}

	// Token: 0x06002B5A RID: 11098 RVA: 0x000A13D4 File Offset: 0x0009F5D4
	public virtual string GetAttackAnim()
	{
		return "bite";
	}

	// Token: 0x06002B5B RID: 11099 RVA: 0x000A13DC File Offset: 0x0009F5DC
	public void DoClientAttack()
	{
		base.animation.CrossFade(this.GetAttackAnim(), 0.1f, 0);
	}

	// Token: 0x06002B5C RID: 11100 RVA: 0x000A1400 File Offset: 0x0009F600
	protected override bool OnInterpTimedEvent()
	{
		string tag = global::InterpTimedEvent.Tag;
		if (tag != null)
		{
			if (global::HostileWildlifeAI.<>f__switch$map8 == null)
			{
				global::HostileWildlifeAI.<>f__switch$map8 = new Dictionary<string, int>(1)
				{
					{
						"ATK",
						0
					}
				};
			}
			int num;
			if (global::HostileWildlifeAI.<>f__switch$map8.TryGetValue(tag, out num))
			{
				if (num == 0)
				{
					this.DoClientAttack();
					return true;
				}
			}
		}
		return base.OnInterpTimedEvent();
	}

	// Token: 0x06002B5D RID: 11101 RVA: 0x000A1468 File Offset: 0x0009F668
	protected override bool PlaySnd(int type)
	{
		AudioClip audioClip = null;
		float volume = 1f;
		float minDistance = 5f;
		float maxDistance = 20f;
		bool flag = false;
		if (type == 5)
		{
			if (this.chaseSoundsFar != null)
			{
				audioClip = this.chaseSoundsFar[Random.Range(0, this.chaseSoundsFar.Length)];
			}
			volume = 1f;
			minDistance = 0.25f;
			maxDistance = 25f;
			flag = true;
		}
		else if (type == 6)
		{
			if (this.chaseSoundsClose != null)
			{
				audioClip = this.chaseSoundsClose[Random.Range(0, this.chaseSoundsClose.Length)];
			}
			volume = 1f;
			minDistance = 0f;
			maxDistance = 10f;
			flag = true;
		}
		else if (type == 2)
		{
			if (this.attackSounds != null)
			{
				audioClip = this.attackSounds[Random.Range(0, this.attackSounds.Length)];
			}
			volume = 1f;
			minDistance = 0f;
			maxDistance = 10f;
			flag = true;
		}
		if (audioClip && flag)
		{
			audioClip.PlayLocal(base.transform, Vector3.zero, volume, minDistance, maxDistance);
			return true;
		}
		return base.PlaySnd(type);
	}

	// Token: 0x0400152D RID: 5421
	protected global::TakeDamage _targetTD;

	// Token: 0x0400152E RID: 5422
	public float loseTargetRange = 100f;

	// Token: 0x0400152F RID: 5423
	public float attackRange = 1f;

	// Token: 0x04001530 RID: 5424
	public float attackRangeMax = 3f;

	// Token: 0x04001531 RID: 5425
	public float attackRate;

	// Token: 0x04001532 RID: 5426
	public float attackDamageMin;

	// Token: 0x04001533 RID: 5427
	public float attackDamageMax;

	// Token: 0x04001534 RID: 5428
	public string lastMoveAnim;

	// Token: 0x04001535 RID: 5429
	[SerializeField]
	protected global::AudioClipArray attackSounds;

	// Token: 0x04001536 RID: 5430
	[SerializeField]
	protected global::AudioClipArray chaseSoundsClose;

	// Token: 0x04001537 RID: 5431
	[SerializeField]
	protected global::AudioClipArray chaseSoundsFar;

	// Token: 0x04001538 RID: 5432
	protected Facepunch.Procedural.MillisClock nextTargetClock;

	// Token: 0x04001539 RID: 5433
	protected Facepunch.Procedural.MillisClock nextAttackClock;

	// Token: 0x0400153A RID: 5434
	protected Facepunch.Procedural.MillisClock attackStrikeClock;

	// Token: 0x0400153B RID: 5435
	protected Facepunch.Procedural.MillisClock chaseSoundClock;

	// Token: 0x0400153C RID: 5436
	protected Facepunch.Procedural.MillisClock targetReachClock;

	// Token: 0x0400153D RID: 5437
	protected Facepunch.Procedural.MillisClock stuckClock;

	// Token: 0x0400153E RID: 5438
	protected Facepunch.Procedural.MillisClock warnClock;

	// Token: 0x0400153F RID: 5439
	protected bool wasStuck;

	// Token: 0x04001540 RID: 5440
	public float nextScentListenTime;

	// Token: 0x04001541 RID: 5441
	public string dropOnDeathString;

	// Token: 0x04001542 RID: 5442
	public global::Character _myChar;
}
