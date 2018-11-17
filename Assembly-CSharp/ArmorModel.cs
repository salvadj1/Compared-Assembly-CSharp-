using System;
using Facepunch.Actor;
using UnityEngine;

// Token: 0x020004FE RID: 1278
public abstract class ArmorModel : ScriptableObject
{
	// Token: 0x06002B45 RID: 11077 RVA: 0x000ADA7C File Offset: 0x000ABC7C
	internal ArmorModel(ArmorModelSlot slot)
	{
		this.slot = slot;
	}

	// Token: 0x1700098A RID: 2442
	// (get) Token: 0x06002B46 RID: 11078 RVA: 0x000ADA8C File Offset: 0x000ABC8C
	public ArmorModel censoredModel
	{
		get
		{
			return this._censored;
		}
	}

	// Token: 0x1700098B RID: 2443
	// (get) Token: 0x06002B47 RID: 11079
	protected abstract ArmorModel _censored { get; }

	// Token: 0x1700098C RID: 2444
	// (get) Token: 0x06002B48 RID: 11080 RVA: 0x000ADA94 File Offset: 0x000ABC94
	public bool hasCensoredModel
	{
		get
		{
			return this._censored;
		}
	}

	// Token: 0x1700098D RID: 2445
	// (get) Token: 0x06002B49 RID: 11081 RVA: 0x000ADAA4 File Offset: 0x000ABCA4
	public ArmorModelSlotMask slotMask
	{
		get
		{
			return this.slot.ToMask();
		}
	}

	// Token: 0x1700098E RID: 2446
	// (get) Token: 0x06002B4A RID: 11082 RVA: 0x000ADAB4 File Offset: 0x000ABCB4
	public Mesh sharedMesh
	{
		get
		{
			return (!this._actorMeshInfo) ? null : this._actorMeshInfo.sharedMesh;
		}
	}

	// Token: 0x1700098F RID: 2447
	// (get) Token: 0x06002B4B RID: 11083 RVA: 0x000ADAD8 File Offset: 0x000ABCD8
	public ActorMeshInfo actorMeshInfo
	{
		get
		{
			return this._actorMeshInfo;
		}
	}

	// Token: 0x17000990 RID: 2448
	// (get) Token: 0x06002B4C RID: 11084 RVA: 0x000ADAE0 File Offset: 0x000ABCE0
	public ActorRig actorRig
	{
		get
		{
			return (!this._actorMeshInfo) ? null : this._actorMeshInfo.actorRig;
		}
	}

	// Token: 0x17000991 RID: 2449
	// (get) Token: 0x06002B4D RID: 11085 RVA: 0x000ADB04 File Offset: 0x000ABD04
	public Material[] sharedMaterials
	{
		get
		{
			return this._materials;
		}
	}

	// Token: 0x040017C3 RID: 6083
	[NonSerialized]
	public readonly ArmorModelSlot slot;

	// Token: 0x040017C4 RID: 6084
	[SerializeField]
	private ActorMeshInfo _actorMeshInfo;

	// Token: 0x040017C5 RID: 6085
	[SerializeField]
	private Material[] _materials;
}
