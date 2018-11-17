using System;
using System.Collections.Generic;
using uLink;
using UnityEngine;

// Token: 0x02000563 RID: 1379
public class TimedGrenade : global::RigidObj
{
	// Token: 0x06002DA9 RID: 11689 RVA: 0x000ABFD4 File Offset: 0x000AA1D4
	public TimedGrenade() : base(global::RigidObj.FeatureFlags.StreamInitialVelocity | global::RigidObj.FeatureFlags.StreamOwnerViewID | global::RigidObj.FeatureFlags.ServerCollisions)
	{
	}

	// Token: 0x06002DAA RID: 11690 RVA: 0x000AC010 File Offset: 0x000AA210
	private new void uLink_OnNetworkInstantiate(uLink.NetworkMessageInfo info)
	{
		base.uLink_OnNetworkInstantiate(info);
	}

	// Token: 0x06002DAB RID: 11691 RVA: 0x000AC01C File Offset: 0x000AA21C
	protected override void OnHide()
	{
		if (base.renderer)
		{
			base.renderer.enabled = false;
		}
	}

	// Token: 0x06002DAC RID: 11692 RVA: 0x000AC03C File Offset: 0x000AA23C
	protected override void OnShow()
	{
		if (base.renderer)
		{
			base.renderer.enabled = true;
		}
	}

	// Token: 0x06002DAD RID: 11693 RVA: 0x000AC05C File Offset: 0x000AA25C
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

	// Token: 0x06002DAE RID: 11694 RVA: 0x000AC10C File Offset: 0x000AA30C
	[RPC]
	private void ClientBounce(uLink.NetworkMessageInfo info)
	{
		global::InterpTimedEvent.Queue(this, "bounce", ref info);
	}

	// Token: 0x06002DAF RID: 11695 RVA: 0x000AC11C File Offset: 0x000AA31C
	private void PlayClientBounce()
	{
		this.bounceSound.Play(this.rigidbody.position, 0.25f, Random.Range(0.85f, 1.15f), 1f, 18f);
	}

	// Token: 0x06002DB0 RID: 11696 RVA: 0x000AC160 File Offset: 0x000AA360
	protected override bool OnInterpTimedEvent()
	{
		string tag = global::InterpTimedEvent.Tag;
		if (tag != null)
		{
			if (global::TimedGrenade.<>f__switch$mapA == null)
			{
				global::TimedGrenade.<>f__switch$mapA = new Dictionary<string, int>(1)
				{
					{
						"bounce",
						0
					}
				};
			}
			int num;
			if (global::TimedGrenade.<>f__switch$mapA.TryGetValue(tag, out num))
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

	// Token: 0x04001798 RID: 6040
	private float fuseLength = 3f;

	// Token: 0x04001799 RID: 6041
	public GameObject explosionEffect;

	// Token: 0x0400179A RID: 6042
	public float explosionRadius = 30f;

	// Token: 0x0400179B RID: 6043
	public float damage = 200f;

	// Token: 0x0400179C RID: 6044
	public IDMain myOwner;

	// Token: 0x0400179D RID: 6045
	public AudioClip bounceSound;

	// Token: 0x0400179E RID: 6046
	private float lastBounceTime;
}
