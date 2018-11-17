using System;
using UnityEngine;

// Token: 0x02000514 RID: 1300
public class CharacterSleepingAvatarTrait : global::CharacterTrait
{
	// Token: 0x06002C3D RID: 11325 RVA: 0x000A5AE4 File Offset: 0x000A3CE4
	private bool ValidatePrefab()
	{
		if (string.IsNullOrEmpty(this._sleepingAvatarPrefab))
		{
			return false;
		}
		GameObject gameObject;
		global::NetCull.PrefabSearch prefabSearch = global::NetCull.LoadPrefab(this._sleepingAvatarPrefab, out gameObject);
		if ((int)prefabSearch != 1)
		{
			Debug.LogError(string.Format("sleeping avatar prefab named \"{0}\" resulted in {1} which was not {2}(required)", this.prefab, prefabSearch, global::NetCull.PrefabSearch.NGC));
			return false;
		}
		IDMain component = gameObject.GetComponent<IDMain>();
		if (!(component is global::SleepingAvatar))
		{
			Debug.LogError(string.Format("Theres no Sleeping avatar on prefab \"{0}\"", this.prefab), gameObject);
			return false;
		}
		this._hasInventory = component.GetLocal<global::Inventory>();
		global::TakeDamage local = component.GetLocal<global::TakeDamage>();
		this._hasTakeDamage = local;
		this._takeDamageType = ((!this._hasTakeDamage) ? null : local.GetType());
		return true;
	}

	// Token: 0x170009A7 RID: 2471
	// (get) Token: 0x06002C3E RID: 11326 RVA: 0x000A5BA8 File Offset: 0x000A3DA8
	public bool valid
	{
		get
		{
			bool? prefabValid = this._prefabValid;
			bool value;
			if (prefabValid != null)
			{
				value = prefabValid.Value;
			}
			else
			{
				bool? flag = this._prefabValid = new bool?(this.ValidatePrefab());
				value = flag.Value;
			}
			return value;
		}
	}

	// Token: 0x170009A8 RID: 2472
	// (get) Token: 0x06002C3F RID: 11327 RVA: 0x000A5BF0 File Offset: 0x000A3DF0
	public bool hasTakeDamage
	{
		get
		{
			return this.valid && this._hasTakeDamage;
		}
	}

	// Token: 0x170009A9 RID: 2473
	// (get) Token: 0x06002C40 RID: 11328 RVA: 0x000A5C08 File Offset: 0x000A3E08
	public Type takeDamageType
	{
		get
		{
			if (!this.hasTakeDamage)
			{
				throw new InvalidOperationException("You need to check hasTakeDamage before requesting this. hasTakeDamage == False");
			}
			return this._takeDamageType;
		}
	}

	// Token: 0x170009AA RID: 2474
	// (get) Token: 0x06002C41 RID: 11329 RVA: 0x000A5C28 File Offset: 0x000A3E28
	public bool hasInventory
	{
		get
		{
			return this.valid && this._hasInventory;
		}
	}

	// Token: 0x170009AB RID: 2475
	// (get) Token: 0x06002C42 RID: 11330 RVA: 0x000A5C40 File Offset: 0x000A3E40
	public bool canDropInventories
	{
		get
		{
			return this._allowDroppingOfInventory && this.hasInventory;
		}
	}

	// Token: 0x170009AC RID: 2476
	// (get) Token: 0x06002C43 RID: 11331 RVA: 0x000A5C58 File Offset: 0x000A3E58
	public string prefab
	{
		get
		{
			return this._sleepingAvatarPrefab ?? string.Empty;
		}
	}

	// Token: 0x170009AD RID: 2477
	// (get) Token: 0x06002C44 RID: 11332 RVA: 0x000A5C6C File Offset: 0x000A3E6C
	public bool grabsCarrierOnCreate
	{
		get
		{
			return this.valid && this._grabCarrierOnCreate;
		}
	}

	// Token: 0x06002C45 RID: 11333 RVA: 0x000A5C84 File Offset: 0x000A3E84
	public Vector3 SolvePlacement(Vector3 origin, Quaternion rot, int iter)
	{
		return global::TransformHelpers.TestBoxCorners(origin, rot, this.boxCenter, this.boxSize, 1024, iter);
	}

	// Token: 0x04001624 RID: 5668
	[SerializeField]
	private string _sleepingAvatarPrefab;

	// Token: 0x04001625 RID: 5669
	[SerializeField]
	private bool _allowDroppingOfInventory;

	// Token: 0x04001626 RID: 5670
	[SerializeField]
	private bool _grabCarrierOnCreate;

	// Token: 0x04001627 RID: 5671
	[SerializeField]
	private Vector3 boxCenter;

	// Token: 0x04001628 RID: 5672
	[SerializeField]
	private Vector3 boxSize;

	// Token: 0x04001629 RID: 5673
	[NonSerialized]
	private bool? _prefabValid;

	// Token: 0x0400162A RID: 5674
	[NonSerialized]
	private bool _hasInventory;

	// Token: 0x0400162B RID: 5675
	[NonSerialized]
	private bool _hasTakeDamage;

	// Token: 0x0400162C RID: 5676
	[NonSerialized]
	private Type _takeDamageType;
}
