using System;
using System.Collections.Generic;
using uLink;
using UnityEngine;

// Token: 0x020004A3 RID: 1187
public class SignalGrenade : RigidObj
{
	// Token: 0x060029E0 RID: 10720 RVA: 0x000A3E6C File Offset: 0x000A206C
	public SignalGrenade() : base(RigidObj.FeatureFlags.StreamInitialVelocity | RigidObj.FeatureFlags.StreamOwnerViewID | RigidObj.FeatureFlags.ServerCollisions)
	{
	}

	// Token: 0x060029E1 RID: 10721 RVA: 0x000A3E84 File Offset: 0x000A2084
	private new void uLink_OnNetworkInstantiate(NetworkMessageInfo info)
	{
		base.uLink_OnNetworkInstantiate(info);
	}

	// Token: 0x060029E2 RID: 10722 RVA: 0x000A3E90 File Offset: 0x000A2090
	protected override void OnHide()
	{
		if (base.renderer)
		{
			base.renderer.enabled = false;
		}
	}

	// Token: 0x060029E3 RID: 10723 RVA: 0x000A3EB0 File Offset: 0x000A20B0
	protected override void OnShow()
	{
		if (base.renderer)
		{
			base.renderer.enabled = true;
		}
	}

	// Token: 0x060029E4 RID: 10724 RVA: 0x000A3ED0 File Offset: 0x000A20D0
	protected override void OnDone()
	{
		Object @object = Object.Instantiate(this.explosionEffect, base.transform.position, Quaternion.LookRotation(Vector3.up));
		Object.Destroy(@object, 60f);
	}

	// Token: 0x060029E5 RID: 10725 RVA: 0x000A3F0C File Offset: 0x000A210C
	[RPC]
	private void ClientBounce(NetworkMessageInfo info)
	{
		InterpTimedEvent.Queue(this, "bounce", ref info);
	}

	// Token: 0x060029E6 RID: 10726 RVA: 0x000A3F1C File Offset: 0x000A211C
	private void PlayClientBounce()
	{
		this.bounceSound.Play(this.rigidbody.position, 0.25f, Random.Range(0.85f, 1.15f), 1f, 18f);
	}

	// Token: 0x060029E7 RID: 10727 RVA: 0x000A3F60 File Offset: 0x000A2160
	protected override bool OnInterpTimedEvent()
	{
		string tag = InterpTimedEvent.Tag;
		if (tag != null)
		{
			if (SignalGrenade.<>f__switch$map9 == null)
			{
				SignalGrenade.<>f__switch$map9 = new Dictionary<string, int>(1)
				{
					{
						"bounce",
						0
					}
				};
			}
			int num;
			if (SignalGrenade.<>f__switch$map9.TryGetValue(tag, out num))
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

	// Token: 0x040015CC RID: 5580
	private float fuseLength = 3f;

	// Token: 0x040015CD RID: 5581
	public GameObject explosionEffect;

	// Token: 0x040015CE RID: 5582
	public AudioClip bounceSound;

	// Token: 0x040015CF RID: 5583
	private float lastBounceTime;
}
