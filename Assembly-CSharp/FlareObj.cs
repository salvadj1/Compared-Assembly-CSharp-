using System;
using uLink;
using UnityEngine;

// Token: 0x0200055C RID: 1372
public sealed class FlareObj : global::RigidObj
{
	// Token: 0x06002D8A RID: 11658 RVA: 0x000ABA88 File Offset: 0x000A9C88
	public FlareObj() : base(global::RigidObj.FeatureFlags.StreamInitialVelocity)
	{
	}

	// Token: 0x06002D8B RID: 11659 RVA: 0x000ABA94 File Offset: 0x000A9C94
	private new void uLink_OnNetworkInstantiate(uLink.NetworkMessageInfo info)
	{
		this.lightInstance = (Object.Instantiate(this.lightPrefab, base.transform.position + new Vector3(0f, 0.1f, 0f), Quaternion.identity) as GameObject);
		this.lightInstance.transform.parent = base.transform;
		base.uLink_OnNetworkInstantiate(info);
	}

	// Token: 0x06002D8C RID: 11660 RVA: 0x000ABB00 File Offset: 0x000A9D00
	protected override void OnDone()
	{
	}

	// Token: 0x06002D8D RID: 11661 RVA: 0x000ABB04 File Offset: 0x000A9D04
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

	// Token: 0x06002D8E RID: 11662 RVA: 0x000ABB4C File Offset: 0x000A9D4C
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

	// Token: 0x04001783 RID: 6019
	private GameObject lightInstance;

	// Token: 0x04001784 RID: 6020
	public AudioClip StrikeSound;

	// Token: 0x04001785 RID: 6021
	public GameObject lightPrefab;
}
