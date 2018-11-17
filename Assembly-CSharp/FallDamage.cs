using System;
using uLink;
using UnityEngine;

// Token: 0x0200048B RID: 1163
public class FallDamage : IDLocalCharacter
{
	// Token: 0x0600295E RID: 10590 RVA: 0x000A24D4 File Offset: 0x000A06D4
	public float GetLegInjury()
	{
		return this.injuryLevel;
	}

	// Token: 0x0600295F RID: 10591 RVA: 0x000A24DC File Offset: 0x000A06DC
	public void AddLegInjury(float inj)
	{
		this.SetLegInjury(this.GetLegInjury() + inj);
	}

	// Token: 0x06002960 RID: 10592 RVA: 0x000A24EC File Offset: 0x000A06EC
	public void SetLegInjury(float injAmount)
	{
		this.injuryLevel = injAmount;
		if (base.character.localPlayerControlled)
		{
			RPOS.InjuryUpdate();
		}
	}

	// Token: 0x06002961 RID: 10593 RVA: 0x000A250C File Offset: 0x000A070C
	[RPC]
	protected void fIo(float injAmount)
	{
		this.SetLegInjury(injAmount);
	}

	// Token: 0x06002962 RID: 10594 RVA: 0x000A2518 File Offset: 0x000A0718
	public void ResetInjuryTime()
	{
		base.CancelInvoke("ClearInjury");
		float num = falldamage.injury_length * Random.Range(0.9f, 1.1f);
		base.Invoke("ClearInjury", num);
	}

	// Token: 0x06002963 RID: 10595 RVA: 0x000A2554 File Offset: 0x000A0754
	public void ClearInjury()
	{
		this.SetLegInjury(0f);
	}

	// Token: 0x06002964 RID: 10596 RVA: 0x000A2564 File Offset: 0x000A0764
	public void FallImpact(float fallspeed)
	{
		this.legBreakSound.Play(base.transform.position, 1f, 3f, 10f);
		if (base.localControlled)
		{
			HeadBob component = CameraMount.current.GetComponent<HeadBob>();
			component.AddEffect(this.fallbob);
		}
	}

	// Token: 0x06002965 RID: 10597 RVA: 0x000A25BC File Offset: 0x000A07BC
	public void SendFallImpact(Vector3 velocity)
	{
		if (velocity.y > -18f)
		{
			return;
		}
		base.networkView.RPC<Vector3>("fIm", 0, velocity);
	}

	// Token: 0x06002966 RID: 10598 RVA: 0x000A25F0 File Offset: 0x000A07F0
	[RPC]
	protected void fIc(float fallspeed)
	{
		this.FallImpact(fallspeed);
	}

	// Token: 0x06002967 RID: 10599 RVA: 0x000A25FC File Offset: 0x000A07FC
	[RPC]
	protected void fIm(Vector3 velocity, NetworkMessageInfo info)
	{
	}

	// Token: 0x06002968 RID: 10600 RVA: 0x000A2600 File Offset: 0x000A0800
	[RPC]
	protected void ReadFallImpact(Vector3 velocity, NetworkMessageInfo info)
	{
	}

	// Token: 0x04001545 RID: 5445
	private const string kRPCName_InjuryInfo = "fIo";

	// Token: 0x04001546 RID: 5446
	private const string kRPCName_ReadFallImpactClient = "fIc";

	// Token: 0x04001547 RID: 5447
	private const string kRPCName_ReadFallImpactServer = "fIm";

	// Token: 0x04001548 RID: 5448
	public AudioClip legBreakSound;

	// Token: 0x04001549 RID: 5449
	public BobEffect fallbob;

	// Token: 0x0400154A RID: 5450
	private float injuryLevel;

	// Token: 0x0400154B RID: 5451
	private float injuredTime;
}
