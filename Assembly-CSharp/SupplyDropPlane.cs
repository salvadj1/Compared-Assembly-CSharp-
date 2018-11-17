using System;
using uLink;
using UnityEngine;

// Token: 0x020000A6 RID: 166
public class SupplyDropPlane : IDMain
{
	// Token: 0x06000397 RID: 919 RVA: 0x00011458 File Offset: 0x0000F658
	public SupplyDropPlane() : this(0)
	{
	}

	// Token: 0x06000398 RID: 920 RVA: 0x00011464 File Offset: 0x0000F664
	protected SupplyDropPlane(IDFlags idFlags) : base(idFlags)
	{
	}

	// Token: 0x06000399 RID: 921 RVA: 0x00011494 File Offset: 0x0000F694
	private void uLink_OnNetworkInstantiate(uLink.NetworkMessageInfo info)
	{
		this._interp = base.GetComponent<global::TransformInterpolator>();
		this._interp.running = true;
	}

	// Token: 0x0600039A RID: 922 RVA: 0x000114B0 File Offset: 0x0000F6B0
	public void Update()
	{
		foreach (GameObject gameObject in this.propellers)
		{
			gameObject.transform.RotateAroundLocal(Vector3.forward, 12f * Time.deltaTime);
		}
	}

	// Token: 0x0600039B RID: 923 RVA: 0x000114F8 File Offset: 0x0000F6F8
	[RPC]
	public void GetNetworkUpdate(Vector3 pos, Quaternion rot, uLink.NetworkMessageInfo info)
	{
		this._interp.SetGoals(pos, rot, info.timestamp);
	}

	// Token: 0x04000309 RID: 777
	public GameObject[] propellers;

	// Token: 0x0400030A RID: 778
	public Vector3 startPos;

	// Token: 0x0400030B RID: 779
	public Vector3 dropTargetPos;

	// Token: 0x0400030C RID: 780
	public Quaternion startAng;

	// Token: 0x0400030D RID: 781
	public float maxSpeed = 250f;

	// Token: 0x0400030E RID: 782
	private bool passedTarget;

	// Token: 0x0400030F RID: 783
	protected Vector3 targetPos;

	// Token: 0x04000310 RID: 784
	protected float lastDist = float.PositiveInfinity;

	// Token: 0x04000311 RID: 785
	protected bool approachingTarget = true;

	// Token: 0x04000312 RID: 786
	protected float targetReachedTime;

	// Token: 0x04000313 RID: 787
	protected bool droppedPayload;

	// Token: 0x04000314 RID: 788
	public int TEMP_numCratesToDrop = 3;

	// Token: 0x04000315 RID: 789
	protected global::TransformInterpolator _interp;

	// Token: 0x04000316 RID: 790
	protected double _lastMoveTime;
}
