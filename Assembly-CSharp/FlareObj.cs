using System;
using uLink;
using UnityEngine;

// Token: 0x020004A1 RID: 1185
public sealed class FlareObj : RigidObj
{
	// Token: 0x060029D8 RID: 10712 RVA: 0x000A3CF0 File Offset: 0x000A1EF0
	public FlareObj() : base(RigidObj.FeatureFlags.StreamInitialVelocity)
	{
	}

	// Token: 0x060029D9 RID: 10713 RVA: 0x000A3CFC File Offset: 0x000A1EFC
	private new void uLink_OnNetworkInstantiate(NetworkMessageInfo info)
	{
		this.lightInstance = (Object.Instantiate(this.lightPrefab, base.transform.position + new Vector3(0f, 0.1f, 0f), Quaternion.identity) as GameObject);
		this.lightInstance.transform.parent = base.transform;
		base.uLink_OnNetworkInstantiate(info);
	}

	// Token: 0x060029DA RID: 10714 RVA: 0x000A3D68 File Offset: 0x000A1F68
	protected override void OnDone()
	{
	}

	// Token: 0x060029DB RID: 10715 RVA: 0x000A3D6C File Offset: 0x000A1F6C
	protected override void OnHide()
	{
		if (this.lightInstance)
		{
			this.lightInstance.SetActive(false);
		}
		if (base.renderer)
		{
			base.renderer.enabled = false;
		}
	}

	// Token: 0x060029DC RID: 10716 RVA: 0x000A3DB4 File Offset: 0x000A1FB4
	protected override void OnShow()
	{
		if (this.lightInstance)
		{
			this.lightInstance.SetActive(true);
		}
		if (base.renderer)
		{
			base.renderer.enabled = true;
		}
	}

	// Token: 0x040015C6 RID: 5574
	private GameObject lightInstance;

	// Token: 0x040015C7 RID: 5575
	public AudioClip StrikeSound;

	// Token: 0x040015C8 RID: 5576
	public GameObject lightPrefab;
}
