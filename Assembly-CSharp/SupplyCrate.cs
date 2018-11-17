using System;
using uLink;
using UnityEngine;

// Token: 0x02000092 RID: 146
public class SupplyCrate : IDMain, IInterpTimedEventReceiver
{
	// Token: 0x06000318 RID: 792 RVA: 0x0000FB3C File Offset: 0x0000DD3C
	public SupplyCrate() : this(0)
	{
	}

	// Token: 0x06000319 RID: 793 RVA: 0x0000FB48 File Offset: 0x0000DD48
	protected SupplyCrate(IDFlags idFlags) : base(idFlags)
	{
	}

	// Token: 0x0600031A RID: 794 RVA: 0x0000FB58 File Offset: 0x0000DD58
	void IInterpTimedEventReceiver.OnInterpTimedEvent()
	{
		if (InterpTimedEvent.Tag == "LAND")
		{
			this.LandShared();
			GameObject gameObject = Object.Instantiate(this.landedEffect, base.transform.position, base.transform.rotation) as GameObject;
			Object.Destroy(gameObject, 2.5f);
			this._landed = true;
			this.chute.Landed();
		}
		else
		{
			InterpTimedEvent.MarkUnhandled();
		}
	}

	// Token: 0x0600031B RID: 795 RVA: 0x0000FBD0 File Offset: 0x0000DDD0
	private void uLink_OnNetworkInstantiate(NetworkMessageInfo info)
	{
		this.lootableObject.accessLocked = true;
		this._interp.running = true;
		base.rigidbody.isKinematic = true;
	}

	// Token: 0x0600031C RID: 796 RVA: 0x0000FC04 File Offset: 0x0000DE04
	private void LandShared()
	{
		this._landed = true;
		if (this.lootableObject)
		{
			this.lootableObject.accessLocked = false;
		}
		Object.Destroy(this.bubbleWrap);
	}

	// Token: 0x0600031D RID: 797 RVA: 0x0000FC40 File Offset: 0x0000DE40
	[RPC]
	protected void GetNetworkUpdate(Vector3 pos, Quaternion rot, NetworkMessageInfo info)
	{
		this._interp.SetGoals(pos, rot, info.timestamp);
	}

	// Token: 0x0600031E RID: 798 RVA: 0x0000FC58 File Offset: 0x0000DE58
	[RPC]
	public void Landed(NetworkMessageInfo info)
	{
		InterpTimedEvent.Queue(this, "LAND", ref info);
	}

	// Token: 0x04000296 RID: 662
	public RigidbodyInterpolator _interp;

	// Token: 0x04000297 RID: 663
	protected bool _landed;

	// Token: 0x04000298 RID: 664
	protected bool _landing;

	// Token: 0x04000299 RID: 665
	protected RPCMode updateRPCMode = 1;

	// Token: 0x0400029A RID: 666
	public SupplyParachute chute;

	// Token: 0x0400029B RID: 667
	public GameObject landedEffect;

	// Token: 0x0400029C RID: 668
	public LootableObject lootableObject;

	// Token: 0x0400029D RID: 669
	public GameObject bubbleWrap;
}
