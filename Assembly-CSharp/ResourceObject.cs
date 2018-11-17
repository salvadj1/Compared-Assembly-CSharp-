using System;
using uLink;
using UnityEngine;

// Token: 0x0200072C RID: 1836
public class ResourceObject : IDMain
{
	// Token: 0x06003CF2 RID: 15602 RVA: 0x000DA268 File Offset: 0x000D8468
	public ResourceObject() : base(0)
	{
	}

	// Token: 0x06003CF3 RID: 15603 RVA: 0x000DA280 File Offset: 0x000D8480
	private void NGC_OnInstantiate(global::NGCView view)
	{
		this.myID = global::NetEntityID.Get(this);
		this._resTarg = base.GetComponent<global::ResourceTarget>();
	}

	// Token: 0x06003CF4 RID: 15604 RVA: 0x000DA29C File Offset: 0x000D849C
	public void SetSpawner(GameObject spawner)
	{
		this._mySpawner = spawner.GetComponent<global::GenericSpawner>();
	}

	// Token: 0x06003CF5 RID: 15605 RVA: 0x000DA2AC File Offset: 0x000D84AC
	public void ChangeModelIndex(int index)
	{
		this._meshCollider.sharedMesh = this.collisionMeshes[index];
		this._meshFilter.sharedMesh = this.visualMeshes[index];
		this._lastModelIndex = index;
	}

	// Token: 0x06003CF6 RID: 15606 RVA: 0x000DA2DC File Offset: 0x000D84DC
	public void DelayedModelChangeIndex()
	{
		this.ChangeModelIndex(this._pendingMeshIndex);
	}

	// Token: 0x06003CF7 RID: 15607 RVA: 0x000DA2EC File Offset: 0x000D84EC
	[RPC]
	public void modelindex(int index, uLink.NetworkMessageInfo info)
	{
		bool flag = false;
		if (global::EnvironmentControlCenter.Singleton && global::EnvironmentControlCenter.Singleton.IsNight() && global::PlayerClient.GetLocalPlayer().controllable && Vector3.Distance(global::PlayerClient.GetLocalPlayer().controllable.transform.position, base.transform.position) > 20f)
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

	// Token: 0x04001F19 RID: 7961
	private global::GenericSpawner _mySpawner;

	// Token: 0x04001F1A RID: 7962
	public Mesh[] visualMeshes;

	// Token: 0x04001F1B RID: 7963
	public Mesh[] collisionMeshes;

	// Token: 0x04001F1C RID: 7964
	public GameObject clientMeshChangeEffect;

	// Token: 0x04001F1D RID: 7965
	public global::ResourceTarget _resTarg;

	// Token: 0x04001F1E RID: 7966
	public MeshFilter _meshFilter;

	// Token: 0x04001F1F RID: 7967
	public MeshCollider _meshCollider;

	// Token: 0x04001F20 RID: 7968
	private int _pendingMeshIndex = -1;

	// Token: 0x04001F21 RID: 7969
	private int _lastModelIndex = -1;

	// Token: 0x04001F22 RID: 7970
	private global::NetEntityID myID;
}
