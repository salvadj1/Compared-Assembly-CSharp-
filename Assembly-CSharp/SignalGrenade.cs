using System;
using System.Collections.Generic;
using uLink;
using UnityEngine;

// Token: 0x0200055E RID: 1374
public class SignalGrenade : global::RigidObj
{
	// Token: 0x06002D92 RID: 11666 RVA: 0x000ABC04 File Offset: 0x000A9E04
	public SignalGrenade() : base(global::RigidObj.FeatureFlags.StreamInitialVelocity | global::RigidObj.FeatureFlags.StreamOwnerViewID | global::RigidObj.FeatureFlags.ServerCollisions)
	{
	}

	// Token: 0x06002D93 RID: 11667 RVA: 0x000ABC1C File Offset: 0x000A9E1C
	private new void uLink_OnNetworkInstantiate(uLink.NetworkMessageInfo info)
	{
		base.uLink_OnNetworkInstantiate(info);
	}

	// Token: 0x06002D94 RID: 11668 RVA: 0x000ABC28 File Offset: 0x000A9E28
	protected override void OnHide()
	{
		if (base.renderer)
		{
			base.renderer.enabled = false;
		}
	}

	// Token: 0x06002D95 RID: 11669 RVA: 0x000ABC48 File Offset: 0x000A9E48
	protected override void OnShow()
	{
		if (base.renderer)
		{
			base.renderer.enabled = true;
		}
	}

	// Token: 0x06002D96 RID: 11670 RVA: 0x000ABC68 File Offset: 0x000A9E68
	protected override void OnDone()
	{
		Object @object = Object.Instantiate(this.explosionEffect, base.transform.position, Quaternion.LookRotation(Vector3.up));
		Object.Destroy(@object, 60f);
	}

	// Token: 0x06002D97 RID: 11671 RVA: 0x000ABCA4 File Offset: 0x000A9EA4
	[RPC]
	private void ClientBounce(uLink.NetworkMessageInfo info)
	{
		global::InterpTimedEvent.Queue(this, "bounce", ref info);
	}

	// Token: 0x06002D98 RID: 11672 RVA: 0x000ABCB4 File Offset: 0x000A9EB4
	private void PlayClientBounce()
	{
		this.bounceSound.Play(this.rigidbody.position, 0.25f, Random.Range(0.85f, 1.15f), 1f, 18f);
	}

	// Token: 0x06002D99 RID: 11673 RVA: 0x000ABCF8 File Offset: 0x000A9EF8
	protected override bool OnInterpTimedEvent()
	{
		string tag = global::InterpTimedEvent.Tag;
		if (tag != null)
		{
			if (global::SignalGrenade.<>f__switch$map9 == null)
			{
				global::SignalGrenade.<>f__switch$map9 = new Dictionary<string, int>(1)
				{
					{
						"bounce",
						0
					}
				};
			}
			int num;
			if (global::SignalGrenade.<>f__switch$map9.TryGetValue(tag, out num))
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

	// Token: 0x04001789 RID: 6025
	private float fuseLength = 3f;

	// Token: 0x0400178A RID: 6026
	public GameObject explosionEffect;

	// Token: 0x0400178B RID: 6027
	public AudioClip bounceSound;

	// Token: 0x0400178C RID: 6028
	private float lastBounceTime;
}
