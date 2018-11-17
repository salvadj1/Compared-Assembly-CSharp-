using System;
using uLink;
using UnityEngine;

// Token: 0x020000A5 RID: 165
public class SupplyCrate : IDMain, global::IInterpTimedEventReceiver
{
	// Token: 0x06000390 RID: 912 RVA: 0x0001132C File Offset: 0x0000F52C
	public SupplyCrate() : this(0)
	{
	}

	// Token: 0x06000391 RID: 913 RVA: 0x00011338 File Offset: 0x0000F538
	protected SupplyCrate(IDFlags idFlags) : base(idFlags)
	{
	}

	// Token: 0x06000392 RID: 914 RVA: 0x00011348 File Offset: 0x0000F548
	void global::IInterpTimedEventReceiver.OnInterpTimedEvent()
	{
		if (global::InterpTimedEvent.Tag == "LAND")
		{
			this.LandShared();
			GameObject gameObject = Object.Instantiate(this.landedEffect, base.transform.position, base.transform.rotation) as GameObject;
			Object.Destroy(gameObject, 2.5f);
			this._landed = true;
			this.chute.Landed();
		}
		else
		{
			global::InterpTimedEvent.MarkUnhandled();
		}
	}

	// Token: 0x06000393 RID: 915 RVA: 0x000113C0 File Offset: 0x0000F5C0
	private void uLink_OnNetworkInstantiate(uLink.NetworkMessageInfo info)
	{
		this.lootableObject.accessLocked = true;
		this._interp.running = true;
		base.rigidbody.isKinematic = true;
	}

	// Token: 0x06000394 RID: 916 RVA: 0x000113F4 File Offset: 0x0000F5F4
	private void LandShared()
	{
		this._landed = true;
		if (this.lootableObject)
		{
			this.lootableObject.accessLocked = false;
		}
		Object.Destroy(this.bubbleWrap);
	}

	// Token: 0x06000395 RID: 917 RVA: 0x00011430 File Offset: 0x0000F630
	[RPC]
	protected void GetNetworkUpdate(Vector3 pos, Quaternion rot, uLink.NetworkMessageInfo info)
	{
		this._interp.SetGoals(pos, rot, info.timestamp);
	}

	// Token: 0x06000396 RID: 918 RVA: 0x00011448 File Offset: 0x0000F648
	[RPC]
	public void Landed(uLink.NetworkMessageInfo info)
	{
		global::InterpTimedEvent.Queue(this, "LAND", ref info);
	}

	// Token: 0x04000301 RID: 769
	public global::RigidbodyInterpolator _interp;

	// Token: 0x04000302 RID: 770
	protected bool _landed;

	// Token: 0x04000303 RID: 771
	protected bool _landing;

	// Token: 0x04000304 RID: 772
	protected uLink.RPCMode updateRPCMode = 1;

	// Token: 0x04000305 RID: 773
	public global::SupplyParachute chute;

	// Token: 0x04000306 RID: 774
	public GameObject landedEffect;

	// Token: 0x04000307 RID: 775
	public global::LootableObject lootableObject;

	// Token: 0x04000308 RID: 776
	public GameObject bubbleWrap;
}
