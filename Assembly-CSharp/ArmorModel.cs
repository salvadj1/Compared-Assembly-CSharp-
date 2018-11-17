using System;
using Facepunch.Actor;
using UnityEngine;

// Token: 0x020005BB RID: 1467
public abstract class ArmorModel : ScriptableObject
{
	// Token: 0x06002F05 RID: 12037 RVA: 0x000B5B18 File Offset: 0x000B3D18
	internal ArmorModel(global::ArmorModelSlot slot)
	{
		this.slot = slot;
	}

	// Token: 0x170009FE RID: 2558
	// (get) Token: 0x06002F06 RID: 12038 RVA: 0x000B5B28 File Offset: 0x000B3D28
	public global::ArmorModel censoredModel
	{
		get
		{
			return this._censored;
		}
	}

	// Token: 0x170009FF RID: 2559
	// (get) Token: 0x06002F07 RID: 12039
	protected abstract global::ArmorModel _censored { get; }

	// Token: 0x17000A00 RID: 2560
	// (get) Token: 0x06002F08 RID: 12040 RVA: 0x000B5B30 File Offset: 0x000B3D30
	public bool hasCensoredModel
	{
		get
		{
			return this._censored;
		}
	}

	// Token: 0x17000A01 RID: 2561
	// (get) Token: 0x06002F09 RID: 12041 RVA: 0x000B5B40 File Offset: 0x000B3D40
	public global::ArmorModelSlotMask slotMask
	{
		get
		{
			return this.slot.ToMask();
		}
	}

	// Token: 0x17000A02 RID: 2562
	// (get) Token: 0x06002F0A RID: 12042 RVA: 0x000B5B50 File Offset: 0x000B3D50
	public Mesh sharedMesh
	{
		get
		{
			return (!this._actorMeshInfo) ? null : this._actorMeshInfo.sharedMesh;
		}
	}

	// Token: 0x17000A03 RID: 2563
	// (get) Token: 0x06002F0B RID: 12043 RVA: 0x000B5B74 File Offset: 0x000B3D74
	public ActorMeshInfo actorMeshInfo
	{
		get
		{
			return this._actorMeshInfo;
		}
	}

	// Token: 0x17000A04 RID: 2564
	// (get) Token: 0x06002F0C RID: 12044 RVA: 0x000B5B7C File Offset: 0x000B3D7C
	public ActorRig actorRig
	{
		get
		{
			return (!this._actorMeshInfo) ? null : this._actorMeshInfo.actorRig;
		}
	}

	// Token: 0x17000A05 RID: 2565
	// (get) Token: 0x06002F0D RID: 12045 RVA: 0x000B5BA0 File Offset: 0x000B3DA0
	public Material[] sharedMaterials
	{
		get
		{
			return this._materials;
		}
	}

	// Token: 0x0400198F RID: 6543
	[NonSerialized]
	public readonly global::ArmorModelSlot slot;

	// Token: 0x04001990 RID: 6544
	[SerializeField]
	private ActorMeshInfo _actorMeshInfo;

	// Token: 0x04001991 RID: 6545
	[SerializeField]
	private Material[] _materials;
}
