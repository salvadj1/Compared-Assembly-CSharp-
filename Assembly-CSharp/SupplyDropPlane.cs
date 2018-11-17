using System;
using uLink;
using UnityEngine;

// Token: 0x02000093 RID: 147
public class SupplyDropPlane : IDMain
{
	// Token: 0x0600031F RID: 799 RVA: 0x0000FC68 File Offset: 0x0000DE68
	public SupplyDropPlane() : this(0)
	{
	}

	// Token: 0x06000320 RID: 800 RVA: 0x0000FC74 File Offset: 0x0000DE74
	protected SupplyDropPlane(IDFlags idFlags) : base(idFlags)
	{
	}

	// Token: 0x06000321 RID: 801 RVA: 0x0000FCA4 File Offset: 0x0000DEA4
	private void uLink_OnNetworkInstantiate(NetworkMessageInfo info)
	{
		this._interp = base.GetComponent<TransformInterpolator>();
		this._interp.running = true;
	}

	// Token: 0x06000322 RID: 802 RVA: 0x0000FCC0 File Offset: 0x0000DEC0
	public void Update()
	{
		foreach (GameObject gameObject in this.propellers)
		{
			gameObject.transform.RotateAroundLocal(Vector3.forward, 12f * Time.deltaTime);
		}
	}

	// Token: 0x06000323 RID: 803 RVA: 0x0000FD08 File Offset: 0x0000DF08
	[RPC]
	public void GetNetworkUpdate(Vector3 pos, Quaternion rot, NetworkMessageInfo info)
	{
		this._interp.SetGoals(pos, rot, info.timestamp);
	}

	// Token: 0x0400029E RID: 670
	public GameObject[] propellers;

	// Token: 0x0400029F RID: 671
	public Vector3 startPos;

	// Token: 0x040002A0 RID: 672
	public Vector3 dropTargetPos;

	// Token: 0x040002A1 RID: 673
	public Quaternion startAng;

	// Token: 0x040002A2 RID: 674
	public float maxSpeed = 250f;

	// Token: 0x040002A3 RID: 675
	private bool passedTarget;

	// Token: 0x040002A4 RID: 676
	protected Vector3 targetPos;

	// Token: 0x040002A5 RID: 677
	protected float lastDist = float.PositiveInfinity;

	// Token: 0x040002A6 RID: 678
	protected bool approachingTarget = true;

	// Token: 0x040002A7 RID: 679
	protected float targetReachedTime;

	// Token: 0x040002A8 RID: 680
	protected bool droppedPayload;

	// Token: 0x040002A9 RID: 681
	public int TEMP_numCratesToDrop = 3;

	// Token: 0x040002AA RID: 682
	protected TransformInterpolator _interp;

	// Token: 0x040002AB RID: 683
	protected double _lastMoveTime;
}
