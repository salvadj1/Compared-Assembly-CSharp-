using System;
using uLink;
using UnityEngine;

// Token: 0x02000546 RID: 1350
public class FallDamage : global::IDLocalCharacter
{
	// Token: 0x06002D10 RID: 11536 RVA: 0x000A88D0 File Offset: 0x000A6AD0
	public float GetLegInjury()
	{
		return this.injuryLevel;
	}

	// Token: 0x06002D11 RID: 11537 RVA: 0x000A88D8 File Offset: 0x000A6AD8
	public void AddLegInjury(float inj)
	{
		this.SetLegInjury(this.GetLegInjury() + inj);
	}

	// Token: 0x06002D12 RID: 11538 RVA: 0x000A88E8 File Offset: 0x000A6AE8
	public void SetLegInjury(float injAmount)
	{
		this.injuryLevel = injAmount;
		if (base.character.localPlayerControlled)
		{
			global::RPOS.InjuryUpdate();
		}
	}

	// Token: 0x06002D13 RID: 11539 RVA: 0x000A8908 File Offset: 0x000A6B08
	[RPC]
	protected void fIo(float injAmount)
	{
		this.SetLegInjury(injAmount);
	}

	// Token: 0x06002D14 RID: 11540 RVA: 0x000A8914 File Offset: 0x000A6B14
	public void ResetInjuryTime()
	{
		base.CancelInvoke("ClearInjury");
		float num = falldamage.injury_length * Random.Range(0.9f, 1.1f);
		base.Invoke("ClearInjury", num);
	}

	// Token: 0x06002D15 RID: 11541 RVA: 0x000A8950 File Offset: 0x000A6B50
	public void ClearInjury()
	{
		this.SetLegInjury(0f);
	}

	// Token: 0x06002D16 RID: 11542 RVA: 0x000A8960 File Offset: 0x000A6B60
	public void FallImpact(float fallspeed)
	{
		this.legBreakSound.Play(base.transform.position, 1f, 3f, 10f);
		if (base.localControlled)
		{
			global::HeadBob component = global::CameraMount.current.GetComponent<global::HeadBob>();
			component.AddEffect(this.fallbob);
		}
	}

	// Token: 0x06002D17 RID: 11543 RVA: 0x000A89B8 File Offset: 0x000A6BB8
	public void SendFallImpact(Vector3 velocity)
	{
		if (global::LocalDamageDisplay.fallDamage)
		{
			global::FallDamage.fallSpeed = -18f;
		}
		if (!global::LocalDamageDisplay.fallDamage)
		{
			global::FallDamage.fallSpeed = -20000f;
		}
		if (velocity.y > global::FallDamage.fallSpeed)
		{
			return;
		}
		base.networkView.RPC<Vector3>("fIm", 0, velocity);
	}

	// Token: 0x06002D18 RID: 11544 RVA: 0x000A8A0C File Offset: 0x000A6C0C
	[RPC]
	protected void fIc(float fallspeed)
	{
		this.FallImpact(fallspeed);
	}

	// Token: 0x06002D19 RID: 11545 RVA: 0x000A8A18 File Offset: 0x000A6C18
	[RPC]
	protected void fIm(Vector3 velocity, uLink.NetworkMessageInfo info)
	{
	}

	// Token: 0x06002D1A RID: 11546 RVA: 0x000A8A1C File Offset: 0x000A6C1C
	[RPC]
	protected void ReadFallImpact(Vector3 velocity, uLink.NetworkMessageInfo info)
	{
	}

	// Token: 0x040016DB RID: 5851
	private const string kRPCName_InjuryInfo = "fIo";

	// Token: 0x040016DC RID: 5852
	private const string kRPCName_ReadFallImpactClient = "fIc";

	// Token: 0x040016DD RID: 5853
	private const string kRPCName_ReadFallImpactServer = "fIm";

	// Token: 0x040016DE RID: 5854
	public AudioClip legBreakSound;

	// Token: 0x040016DF RID: 5855
	public global::BobEffect fallbob;

	// Token: 0x040016E0 RID: 5856
	private float injuryLevel;

	// Token: 0x040016E1 RID: 5857
	private float injuredTime;

	// Token: 0x040016E2 RID: 5858
	public static float fallSpeed;
}
