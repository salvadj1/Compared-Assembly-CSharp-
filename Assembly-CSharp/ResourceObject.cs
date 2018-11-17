using System;
using uLink;
using UnityEngine;

// Token: 0x02000668 RID: 1640
public class ResourceObject : IDMain
{
	// Token: 0x060038FE RID: 14590 RVA: 0x000D1888 File Offset: 0x000CFA88
	public ResourceObject() : base(0)
	{
	}

	// Token: 0x060038FF RID: 14591 RVA: 0x000D18A0 File Offset: 0x000CFAA0
	private void NGC_OnInstantiate(NGCView view)
	{
		this.myID = NetEntityID.Get(this);
		this._resTarg = base.GetComponent<ResourceTarget>();
	}

	// Token: 0x06003900 RID: 14592 RVA: 0x000D18BC File Offset: 0x000CFABC
	public void SetSpawner(GameObject spawner)
	{
		this._mySpawner = spawner.GetComponent<GenericSpawner>();
	}

	// Token: 0x06003901 RID: 14593 RVA: 0x000D18CC File Offset: 0x000CFACC
	public void ChangeModelIndex(int index)
	{
		this._meshCollider.sharedMesh = this.collisionMeshes[index];
		this._meshFilter.sharedMesh = this.visualMeshes[index];
		this._lastModelIndex = index;
	}

	// Token: 0x06003902 RID: 14594 RVA: 0x000D18FC File Offset: 0x000CFAFC
	public void DelayedModelChangeIndex()
	{
		this.ChangeModelIndex(this._pendingMeshIndex);
	}

	// Token: 0x06003903 RID: 14595 RVA: 0x000D190C File Offset: 0x000CFB0C
	[RPC]
	public void modelindex(int index, NetworkMessageInfo info)
	{
		bool flag = false;
		if (EnvironmentControlCenter.Singleton && EnvironmentControlCenter.Singleton.IsNight() && PlayerClient.GetLocalPlayer().controllable && Vector3.Distance(PlayerClient.GetLocalPlayer().controllable.transform.position, base.transform.position) > 20f)
		{
			flag = true;
		}
		if (this.clientMeshChangeEffect && this._lastModelIndex != -1 && !flag)
		{
			GameObject gameObject = Object.Instantiate(this.clientMeshChangeEffect, base.transform.position, base.transform.rotation) as GameObject;
			Object.Destroy(gameObject, 5f);
		}
		this._pendingMeshIndex = index;
		base.Invoke("DelayedModelChangeIndex", 0.15f);
	}

	// Token: 0x04001D21 RID: 7457
	private GenericSpawner _mySpawner;

	// Token: 0x04001D22 RID: 7458
	public Mesh[] visualMeshes;

	// Token: 0x04001D23 RID: 7459
	public Mesh[] collisionMeshes;

	// Token: 0x04001D24 RID: 7460
	public GameObject clientMeshChangeEffect;

	// Token: 0x04001D25 RID: 7461
	public ResourceTarget _resTarg;

	// Token: 0x04001D26 RID: 7462
	public MeshFilter _meshFilter;

	// Token: 0x04001D27 RID: 7463
	public MeshCollider _meshCollider;

	// Token: 0x04001D28 RID: 7464
	private int _pendingMeshIndex = -1;

	// Token: 0x04001D29 RID: 7465
	private int _lastModelIndex = -1;

	// Token: 0x04001D2A RID: 7466
	private NetEntityID myID;
}
