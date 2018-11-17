using System;
using Facepunch.MeshBatch;
using RustProto;
using RustProto.Helpers;
using UnityEngine;

// Token: 0x0200064C RID: 1612
[NGCAutoAddScript]
public class DeployableObject : IDMain, IDeployedObjectMain, IServerSaveable, IServerSaveNotify, ICarriableTrans
{
	// Token: 0x06003820 RID: 14368 RVA: 0x000CE178 File Offset: 0x000CC378
	public DeployableObject() : this(0)
	{
	}

	// Token: 0x06003821 RID: 14369 RVA: 0x000CE184 File Offset: 0x000CC384
	protected DeployableObject(IDFlags flags) : base(flags)
	{
	}

	// Token: 0x06003822 RID: 14370 RVA: 0x000CE1BC File Offset: 0x000CC3BC
	void IServerSaveNotify.PostLoad()
	{
	}

	// Token: 0x17000B0B RID: 2827
	// (get) Token: 0x06003823 RID: 14371 RVA: 0x000CE1C0 File Offset: 0x000CC3C0
	DeployedObjectInfo IDeployedObjectMain.DeployedObjectInfo
	{
		get
		{
			DeployedObjectInfo result;
			result.userID = this.ownerID;
			result.valid = (this.ownerID != 0UL);
			return result;
		}
	}

	// Token: 0x06003824 RID: 14372 RVA: 0x000CE1F0 File Offset: 0x000CC3F0
	public void Awake()
	{
	}

	// Token: 0x06003825 RID: 14373 RVA: 0x000CE1F4 File Offset: 0x000CC3F4
	protected void OnPoolAlive()
	{
		this.ownerID = 0UL;
		this.ownerName = string.Empty;
		this.creatorID = 0UL;
	}

	// Token: 0x06003826 RID: 14374 RVA: 0x000CE214 File Offset: 0x000CC414
	protected void OnPoolRetire()
	{
		this.healthDimmer.Reset();
	}

	// Token: 0x06003827 RID: 14375 RVA: 0x000CE224 File Offset: 0x000CC424
	public void OnDestroy()
	{
		if (this._carrier)
		{
			this._carrier.RemoveObject(this);
			this._carrier = null;
		}
		base.OnDestroy();
	}

	// Token: 0x06003828 RID: 14376 RVA: 0x000CE250 File Offset: 0x000CC450
	public void OnAddedToCarrier(TransCarrier carrier)
	{
		this._carrier = carrier;
	}

	// Token: 0x06003829 RID: 14377 RVA: 0x000CE25C File Offset: 0x000CC45C
	public void OnDroppedFromCarrier(TransCarrier carrier)
	{
		this._carrier = null;
	}

	// Token: 0x0600382A RID: 14378 RVA: 0x000CE268 File Offset: 0x000CC468
	public void Touched()
	{
		TransCarrier carrier = this.GetCarrier();
		if (!carrier)
		{
			return;
		}
		IDMain idMain = carrier.idMain;
		if (!idMain)
		{
			return;
		}
		if (idMain is StructureComponent)
		{
			((StructureComponent)idMain).Touched();
		}
	}

	// Token: 0x0600382B RID: 14379 RVA: 0x000CE2B4 File Offset: 0x000CC4B4
	public TransCarrier GetCarrier()
	{
		return this._carrier;
	}

	// Token: 0x0600382C RID: 14380 RVA: 0x000CE2BC File Offset: 0x000CC4BC
	public void WriteObjectSave(ref SavedObject.Builder saveobj)
	{
		using (Recycler<objectDeployable, objectDeployable.Builder> recycler = objectDeployable.Recycler())
		{
			objectDeployable.Builder builder = recycler.OpenBuilder();
			builder.SetCreatorID(this.creatorID);
			builder.SetOwnerID(this.ownerID);
			saveobj.SetDeployable(builder);
		}
		using (Recycler<objectICarriableTrans, objectICarriableTrans.Builder> recycler2 = objectICarriableTrans.Recycler())
		{
			objectICarriableTrans.Builder builder2 = recycler2.OpenBuilder();
			NetEntityID netEntityID;
			if (this._carrier && (int)NetEntityID.Of(this._carrier, out netEntityID) != 0)
			{
				builder2.SetTransCarrierID(netEntityID.id);
			}
			else
			{
				builder2.ClearTransCarrierID();
			}
			saveobj.SetCarriableTrans(builder2);
		}
	}

	// Token: 0x0600382D RID: 14381 RVA: 0x000CE3A4 File Offset: 0x000CC5A4
	public void ReadObjectSave(ref SavedObject saveobj)
	{
		if (saveobj.HasDeployable)
		{
			this.creatorID = saveobj.Deployable.CreatorID;
			this.ownerID = saveobj.Deployable.OwnerID;
		}
	}

	// Token: 0x0600382E RID: 14382 RVA: 0x000CE3E4 File Offset: 0x000CC5E4
	public void GrabCarrier()
	{
		Ray ray;
		ray..ctor(base.transform.position + Vector3.up * 0.01f, Vector3.down);
		RaycastHit raycastHit;
		bool flag;
		MeshBatchInstance meshBatchInstance;
		if (MeshBatchPhysics.Raycast(ray, ref raycastHit, 5f, ref flag, ref meshBatchInstance))
		{
			IDMain idmain = (!flag) ? IDBase.GetMain(raycastHit.collider) : meshBatchInstance.idMain;
			if (idmain)
			{
				TransCarrier local = idmain.GetLocal<TransCarrier>();
				if (local)
				{
					local.AddObject(this);
				}
			}
		}
	}

	// Token: 0x0600382F RID: 14383 RVA: 0x000CE47C File Offset: 0x000CC67C
	public void CacheCreator()
	{
	}

	// Token: 0x06003830 RID: 14384 RVA: 0x000CE480 File Offset: 0x000CC680
	[RPC]
	public void GetOwnerInfo(ulong creator, ulong owner)
	{
		this.creatorID = creator;
		this.ownerID = owner;
	}

	// Token: 0x06003831 RID: 14385 RVA: 0x000CE490 File Offset: 0x000CC690
	[RPC]
	public void Client_OnKilled()
	{
		if (this.clientDeathEffect)
		{
			GameObject gameObject = Object.Instantiate(this.clientDeathEffect, base.transform.position, base.transform.rotation) as GameObject;
			Object.Destroy(gameObject, 5f);
		}
	}

	// Token: 0x06003832 RID: 14386 RVA: 0x000CE4E0 File Offset: 0x000CC6E0
	[RPC]
	public void ClientHealthUpdate(float newHealth)
	{
		this.healthDimmer.UpdateHealthAmount(this, newHealth, false);
	}

	// Token: 0x06003833 RID: 14387 RVA: 0x000CE4F0 File Offset: 0x000CC6F0
	public bool BelongsTo(Controllable controllable)
	{
		if (!controllable)
		{
			return false;
		}
		PlayerClient playerClient = controllable.playerClient;
		return playerClient && playerClient.userID == this.ownerID;
	}

	// Token: 0x06003834 RID: 14388 RVA: 0x000CE52C File Offset: 0x000CC72C
	public static bool IsValidLocation(Vector3 location, Vector3 surfaceNormal, Quaternion rotation, DeployableObject prefab)
	{
		if (prefab.doEdgeCheck)
		{
			return false;
		}
		float num = Vector3.Angle(surfaceNormal, Vector3.up);
		return num <= prefab.maxSlope;
	}

	// Token: 0x04001C3D RID: 7229
	public bool decayProtector;

	// Token: 0x04001C3E RID: 7230
	public bool cantPlaceOn;

	// Token: 0x04001C3F RID: 7231
	public bool doEdgeCheck;

	// Token: 0x04001C40 RID: 7232
	public float maxEdgeDifferential = 1f;

	// Token: 0x04001C41 RID: 7233
	public float maxSlope = 30f;

	// Token: 0x04001C42 RID: 7234
	public ulong creatorID;

	// Token: 0x04001C43 RID: 7235
	public ulong ownerID;

	// Token: 0x04001C44 RID: 7236
	public string ownerName = string.Empty;

	// Token: 0x04001C45 RID: 7237
	public bool handleDeathHere;

	// Token: 0x04001C46 RID: 7238
	public GameObject corpseObject;

	// Token: 0x04001C47 RID: 7239
	public TransCarrier _carrier;

	// Token: 0x04001C48 RID: 7240
	public GameObject clientDeathEffect;

	// Token: 0x04001C49 RID: 7241
	private EnvDecay _EnvDecay;

	// Token: 0x04001C4A RID: 7242
	[NonSerialized]
	private HealthDimmer healthDimmer;
}
