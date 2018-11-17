using System;
using System.Collections.Generic;
using uLink;
using UnityEngine;

// Token: 0x020004A8 RID: 1192
public class TimedGrenade : RigidObj
{
	// Token: 0x060029F7 RID: 10743 RVA: 0x000A423C File Offset: 0x000A243C
	public TimedGrenade() : base(RigidObj.FeatureFlags.StreamInitialVelocity | RigidObj.FeatureFlags.StreamOwnerViewID | RigidObj.FeatureFlags.ServerCollisions)
	{
	}

	// Token: 0x060029F8 RID: 10744 RVA: 0x000A4278 File Offset: 0x000A2478
	private new void uLink_OnNetworkInstantiate(NetworkMessageInfo info)
	{
		base.uLink_OnNetworkInstantiate(info);
	}

	// Token: 0x060029F9 RID: 10745 RVA: 0x000A4284 File Offset: 0x000A2484
	protected override void OnHide()
	{
		if (base.renderer)
		{
			base.renderer.enabled = false;
		}
	}

	// Token: 0x060029FA RID: 10746 RVA: 0x000A42A4 File Offset: 0x000A24A4
	protected override void OnShow()
	{
		if (base.renderer)
		{
			base.renderer.enabled = true;
		}
	}

	// Token: 0x060029FB RID: 10747 RVA: 0x000A42C4 File Offset: 0x000A24C4
	protected override void OnDone()
	{
		base.collider.enabled = false;
		Vector3 position = this.rigidbody.position;
		if (this.explosionEffect)
		{
			Object.Instantiate(this.explosionEffect, position, Quaternion.identity);
		}
		Collider[] array = Physics.OverlapSphere(position, this.explosionRadius, 134217728);
		foreach (Collider collider in array)
		{
			Rigidbody attachedRigidbody = collider.attachedRigidbody;
			if (attachedRigidbody && !attachedRigidbody.isKinematic)
			{
				attachedRigidbody.AddExplosionForce(500f, position, this.explosionRadius, 2f);
			}
		}
	}

	// Token: 0x060029FC RID: 10748 RVA: 0x000A4374 File Offset: 0x000A2574
	[RPC]
	private void ClientBounce(NetworkMessageInfo info)
	{
		InterpTimedEvent.Queue(this, "bounce", ref info);
	}

	// Token: 0x060029FD RID: 10749 RVA: 0x000A4384 File Offset: 0x000A2584
	private void PlayClientBounce()
	{
		this.bounceSound.Play(this.rigidbody.position, 0.25f, Random.Range(0.85f, 1.15f), 1f, 18f);
	}

	// Token: 0x060029FE RID: 10750 RVA: 0x000A43C8 File Offset: 0x000A25C8
	protected override bool OnInterpTimedEvent()
	{
		string tag = InterpTimedEvent.Tag;
		if (tag != null)
		{
			if (TimedGrenade.<>f__switch$mapA == null)
			{
				TimedGrenade.<>f__switch$mapA = new Dictionary<string, int>(1)
				{
					{
						"bounce",
						0
					}
				};
			}
			int num;
			if (TimedGrenade.<>f__switch$mapA.TryGetValue(tag, out num))
			{
				if (num == 0)
				{
					this.PlayClientBounce();
					return true;
				}
			}
		}
		return base.OnInterpTimedEvent();
	}

	// Token: 0x040015DB RID: 5595
	private float fuseLength = 3f;

	// Token: 0x040015DC RID: 5596
	public GameObject explosionEffect;

	// Token: 0x040015DD RID: 5597
	public float explosionRadius = 30f;

	// Token: 0x040015DE RID: 5598
	public float damage = 200f;

	// Token: 0x040015DF RID: 5599
	public IDMain myOwner;

	// Token: 0x040015E0 RID: 5600
	public AudioClip bounceSound;

	// Token: 0x040015E1 RID: 5601
	private float lastBounceTime;
}
