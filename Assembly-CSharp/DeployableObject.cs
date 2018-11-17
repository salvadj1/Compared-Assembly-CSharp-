using System;
using Facepunch.MeshBatch;
using RustProto;
using RustProto.Helpers;
using UnityEngine;

// Token: 0x0200070F RID: 1807
[global::NGCAutoAddScript]
public class DeployableObject : IDMain, global::IDeployedObjectMain, global::IServerSaveable, global::IServerSaveNotify, global::ICarriableTrans
{
	// Token: 0x06003C0C RID: 15372 RVA: 0x000D6A28 File Offset: 0x000D4C28
	public DeployableObject() : this(0)
	{
	}

	// Token: 0x06003C0D RID: 15373 RVA: 0x000D6A34 File Offset: 0x000D4C34
	protected DeployableObject(IDFlags flags) : base(flags)
	{
	}

	// Token: 0x06003C0E RID: 15374 RVA: 0x000D6A6C File Offset: 0x000D4C6C
	void global::IServerSaveNotify.PostLoad()
	{
	}

	// Token: 0x17000B8B RID: 2955
	// (get) Token: 0x06003C0F RID: 15375 RVA: 0x000D6A70 File Offset: 0x000D4C70
	global::DeployedObjectInfo global::IDeployedObjectMain.DeployedObjectInfo
	{
		get
		{
			global::DeployedObjectInfo result;
			result.userID = this.ownerID;
			result.valid = (this.ownerID != 0UL);
			return result;
		}
	}

	// Token: 0x06003C10 RID: 15376 RVA: 0x000D6AA0 File Offset: 0x000D4CA0
	public void Awake()
	{
	}

	// Token: 0x06003C11 RID: 15377 RVA: 0x000D6AA4 File Offset: 0x000D4CA4
	protected void OnPoolAlive()
	{
		this.ownerID = 0UL;
		this.ownerName = string.Empty;
		this.creatorID = 0UL;
	}

	// Token: 0x06003C12 RID: 15378 RVA: 0x000D6AC4 File Offset: 0x000D4CC4
	protected void OnPoolRetire()
	{
		this.healthDimmer.Reset();
	}

	// Token: 0x06003C13 RID: 15379 RVA: 0x000D6AD4 File Offset: 0x000D4CD4
	public void OnDestroy()
	{
		if (this._carrier)
		{
			this._carrier.RemoveObject(this);
			this._carrier = null;
		}
		base.OnDestroy();
	}

	// Token: 0x06003C14 RID: 15380 RVA: 0x000D6B00 File Offset: 0x000D4D00
	public void OnAddedToCarrier(global::TransCarrier carrier)
	{
		this._carrier = carrier;
	}

	// Token: 0x06003C15 RID: 15381 RVA: 0x000D6B0C File Offset: 0x000D4D0C
	public void OnDroppedFromCarrier(global::TransCarrier carrier)
	{
		this._carrier = null;
	}

	// Token: 0x06003C16 RID: 15382 RVA: 0x000D6B18 File Offset: 0x000D4D18
	public void Touched()
	{
		global::TransCarrier carrier = this.GetCarrier();
		if (!carrier)
		{
			return;
		}
		IDMain idMain = carrier.idMain;
		if (!idMain)
		{
			return;
		}
		if (idMain is global::StructureComponent)
		{
			((global::StructureComponent)idMain).Touched();
		}
	}

	// Token: 0x06003C17 RID: 15383 RVA: 0x000D6B64 File Offset: 0x000D4D64
	public global::TransCarrier GetCarrier()
	{
		return this._carrier;
	}

	// Token: 0x06003C18 RID: 15384 RVA: 0x000D6B6C File Offset: 0x000D4D6C
	public void WriteObjectSave(ref RustProto.SavedObject.Builder saveobj)
	{
		using (RustProto.Helpers.Recycler<RustProto.objectDeployable, RustProto.objectDeployable.Builder> recycler = RustProto.objectDeployable.Recycler())
		{
			RustProto.objectDeployable.Builder builder = recycler.OpenBuilder();
			builder.SetCreatorID(this.creatorID);
			builder.SetOwnerID(this.ownerID);
			saveobj.SetDeployable(builder);
		}
		using (RustProto.Helpers.Recycler<RustProto.objectICarriableTrans, RustProto.objectICarriableTrans.Builder> recycler2 = RustProto.objectICarriableTrans.Recycler())
		{
			RustProto.objectICarriableTrans.Builder builder2 = recycler2.OpenBuilder();
			global::NetEntityID netEntityID;
			if (this._carrier && (int)global::NetEntityID.Of(this._carrier, out netEntityID) != 0)
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

	// Token: 0x06003C19 RID: 15385 RVA: 0x000D6C54 File Offset: 0x000D4E54
	public void ReadObjectSave(ref RustProto.SavedObject saveobj)
	{
		if (saveobj.HasDeployable)
		{
			this.creatorID = saveobj.Deployable.CreatorID;
			this.ownerID = saveobj.Deployable.OwnerID;
		}
	}

	// Token: 0x06003C1A RID: 15386 RVA: 0x000D6C94 File Offset: 0x000D4E94
	public void GrabCarrier()
	{
		Ray ray;
		ray..ctor(base.transform.position + Vector3.up * 0.01f, Vector3.down);
		RaycastHit raycastHit;
		bool flag;
		MeshBatchInstance meshBatchInstance;
		if (Facepunch.MeshBatch.MeshBatchPhysics.Raycast(ray, ref raycastHit, 5f, ref flag, ref meshBatchInstance))
		{
			IDMain idmain = (!flag) ? IDBase.GetMain(raycastHit.collider) : meshBatchInstance.idMain;
			if (idmain)
			{
				global::TransCarrier local = idmain.GetLocal<global::TransCarrier>();
				if (local)
				{
					local.AddObject(this);
				}
			}
		}
	}

	// Token: 0x06003C1B RID: 15387 RVA: 0x000D6D2C File Offset: 0x000D4F2C
	public void CacheCreator()
	{
	}

	// Token: 0x06003C1C RID: 15388 RVA: 0x000D6D30 File Offset: 0x000D4F30
	[RPC]
	public void GetOwnerInfo(ulong creator, ulong owner)
	{
		this.creatorID = creator;
		this.ownerID = owner;
	}

	// Token: 0x06003C1D RID: 15389 RVA: 0x000D6D40 File Offset: 0x000D4F40
	[RPC]
	public void Client_OnKilled()
	{
		if (this.clientDeathEffect)
		{
			GameObject gameObject = Object.Instantiate(this.clientDeathEffect, base.transform.position, base.transform.rotation) as GameObject;
			Object.Destroy(gameObject, 5f);
		}
	}

	// Token: 0x06003C1E RID: 15390 RVA: 0x000D6D90 File Offset: 0x000D4F90
	[RPC]
	public void ClientHealthUpdate(float newHealth)
	{
		this.healthDimmer.UpdateHealthAmount(this, newHealth, false);
	}

	// Token: 0x06003C1F RID: 15391 RVA: 0x000D6DA0 File Offset: 0x000D4FA0
	public bool BelongsTo(global::Controllable controllable)
	{
		if (!controllable)
		{
			return false;
		}
		global::PlayerClient playerClient = controllable.playerClient;
		return playerClient && playerClient.userID == this.ownerID;
	}

	// Token: 0x06003C20 RID: 15392 RVA: 0x000D6DDC File Offset: 0x000D4FDC
	public static bool IsValidLocation(Vector3 location, Vector3 surfaceNormal, UnityEngine.Quaternion rotation, global::DeployableObject prefab)
	{
		if (prefab.doEdgeCheck)
		{
			return false;
		}
		float num = Vector3.Angle(surfaceNormal, Vector3.up);
		return num <= prefab.maxSlope;
	}

	// Token: 0x04001E32 RID: 7730
	public bool decayProtector;

	// Token: 0x04001E33 RID: 7731
	public bool cantPlaceOn;

	// Token: 0x04001E34 RID: 7732
	public bool doEdgeCheck;

	// Token: 0x04001E35 RID: 7733
	public float maxEdgeDifferential = 1f;

	// Token: 0x04001E36 RID: 7734
	public float maxSlope = 30f;

	// Token: 0x04001E37 RID: 7735
	public ulong creatorID;

	// Token: 0x04001E38 RID: 7736
	public ulong ownerID;

	// Token: 0x04001E39 RID: 7737
	public string ownerName = string.Empty;

	// Token: 0x04001E3A RID: 7738
	public bool handleDeathHere;

	// Token: 0x04001E3B RID: 7739
	public GameObject corpseObject;

	// Token: 0x04001E3C RID: 7740
	public global::TransCarrier _carrier;

	// Token: 0x04001E3D RID: 7741
	public GameObject clientDeathEffect;

	// Token: 0x04001E3E RID: 7742
	private global::EnvDecay _EnvDecay;

	// Token: 0x04001E3F RID: 7743
	[NonSerialized]
	private global::HealthDimmer healthDimmer;
}
