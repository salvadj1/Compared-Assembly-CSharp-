using System;
using System.Collections.Generic;
using Facepunch.Procedural;
using uLink;
using UnityEngine;

// Token: 0x02000434 RID: 1076
public class HostileWildlifeAI : BasicWildLifeAI
{
	// Token: 0x060027C7 RID: 10183 RVA: 0x0009B424 File Offset: 0x00099624
	public void GoScentBlind(float dur)
	{
		this.nextScentListenTime = Time.time + dur;
	}

	// Token: 0x060027C8 RID: 10184 RVA: 0x0009B434 File Offset: 0x00099634
	public bool IsScentBlind()
	{
		return Time.time < this.nextScentListenTime;
	}

	// Token: 0x060027C9 RID: 10185 RVA: 0x0009B444 File Offset: 0x00099644
	[RPC]
	public void CL_Attack(NetworkMessageInfo info)
	{
		InterpTimedEvent.Queue(this, "ATK", ref info);
	}

	// Token: 0x060027CA RID: 10186 RVA: 0x0009B454 File Offset: 0x00099654
	public virtual string GetAttackAnim()
	{
		return "bite";
	}

	// Token: 0x060027CB RID: 10187 RVA: 0x0009B45C File Offset: 0x0009965C
	public void DoClientAttack()
	{
		base.animation.CrossFade(this.GetAttackAnim(), 0.1f, 0);
	}

	// Token: 0x060027CC RID: 10188 RVA: 0x0009B480 File Offset: 0x00099680
	protected override bool OnInterpTimedEvent()
	{
		string tag = InterpTimedEvent.Tag;
		if (tag != null)
		{
			if (HostileWildlifeAI.<>f__switch$map8 == null)
			{
				HostileWildlifeAI.<>f__switch$map8 = new Dictionary<string, int>(1)
				{
					{
						"ATK",
						0
					}
				};
			}
			int num;
			if (HostileWildlifeAI.<>f__switch$map8.TryGetValue(tag, out num))
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

	// Token: 0x060027CD RID: 10189 RVA: 0x0009B4E8 File Offset: 0x000996E8
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

	// Token: 0x040013AA RID: 5034
	protected TakeDamage _targetTD;

	// Token: 0x040013AB RID: 5035
	public float loseTargetRange = 100f;

	// Token: 0x040013AC RID: 5036
	public float attackRange = 1f;

	// Token: 0x040013AD RID: 5037
	public float attackRangeMax = 3f;

	// Token: 0x040013AE RID: 5038
	public float attackRate;

	// Token: 0x040013AF RID: 5039
	public float attackDamageMin;

	// Token: 0x040013B0 RID: 5040
	public float attackDamageMax;

	// Token: 0x040013B1 RID: 5041
	public string lastMoveAnim;

	// Token: 0x040013B2 RID: 5042
	[SerializeField]
	protected AudioClipArray attackSounds;

	// Token: 0x040013B3 RID: 5043
	[SerializeField]
	protected AudioClipArray chaseSoundsClose;

	// Token: 0x040013B4 RID: 5044
	[SerializeField]
	protected AudioClipArray chaseSoundsFar;

	// Token: 0x040013B5 RID: 5045
	protected MillisClock nextTargetClock;

	// Token: 0x040013B6 RID: 5046
	protected MillisClock nextAttackClock;

	// Token: 0x040013B7 RID: 5047
	protected MillisClock attackStrikeClock;

	// Token: 0x040013B8 RID: 5048
	protected MillisClock chaseSoundClock;

	// Token: 0x040013B9 RID: 5049
	protected MillisClock targetReachClock;

	// Token: 0x040013BA RID: 5050
	protected MillisClock stuckClock;

	// Token: 0x040013BB RID: 5051
	protected MillisClock warnClock;

	// Token: 0x040013BC RID: 5052
	protected bool wasStuck;

	// Token: 0x040013BD RID: 5053
	public float nextScentListenTime;

	// Token: 0x040013BE RID: 5054
	public string dropOnDeathString;

	// Token: 0x040013BF RID: 5055
	public Character _myChar;
}
