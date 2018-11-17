using System;
using UnityEngine;

// Token: 0x0200045E RID: 1118
public class CharacterSleepingAvatarTrait : CharacterTrait
{
	// Token: 0x060028AD RID: 10413 RVA: 0x0009FB64 File Offset: 0x0009DD64
	private bool ValidatePrefab()
	{
		if (string.IsNullOrEmpty(this._sleepingAvatarPrefab))
		{
			return false;
		}
		GameObject gameObject;
		NetCull.PrefabSearch prefabSearch = NetCull.LoadPrefab(this._sleepingAvatarPrefab, out gameObject);
		if ((int)prefabSearch != 1)
		{
			Debug.LogError(string.Format("sleeping avatar prefab named \"{0}\" resulted in {1} which was not {2}(required)", this.prefab, prefabSearch, NetCull.PrefabSearch.NGC));
			return false;
		}
		IDMain component = gameObject.GetComponent<IDMain>();
		if (!(component is SleepingAvatar))
		{
			Debug.LogError(string.Format("Theres no Sleeping avatar on prefab \"{0}\"", this.prefab), gameObject);
			return false;
		}
		this._hasInventory = component.GetLocal<Inventory>();
		TakeDamage local = component.GetLocal<TakeDamage>();
		this._hasTakeDamage = local;
		this._takeDamageType = ((!this._hasTakeDamage) ? null : local.GetType());
		return true;
	}

	// Token: 0x1700093F RID: 2367
	// (get) Token: 0x060028AE RID: 10414 RVA: 0x0009FC28 File Offset: 0x0009DE28
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

	// Token: 0x17000940 RID: 2368
	// (get) Token: 0x060028AF RID: 10415 RVA: 0x0009FC70 File Offset: 0x0009DE70
	public bool hasTakeDamage
	{
		get
		{
			return this.valid && this._hasTakeDamage;
		}
	}

	// Token: 0x17000941 RID: 2369
	// (get) Token: 0x060028B0 RID: 10416 RVA: 0x0009FC88 File Offset: 0x0009DE88
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

	// Token: 0x17000942 RID: 2370
	// (get) Token: 0x060028B1 RID: 10417 RVA: 0x0009FCA8 File Offset: 0x0009DEA8
	public bool hasInventory
	{
		get
		{
			return this.valid && this._hasInventory;
		}
	}

	// Token: 0x17000943 RID: 2371
	// (get) Token: 0x060028B2 RID: 10418 RVA: 0x0009FCC0 File Offset: 0x0009DEC0
	public bool canDropInventories
	{
		get
		{
			return this._allowDroppingOfInventory && this.hasInventory;
		}
	}

	// Token: 0x17000944 RID: 2372
	// (get) Token: 0x060028B3 RID: 10419 RVA: 0x0009FCD8 File Offset: 0x0009DED8
	public string prefab
	{
		get
		{
			return this._sleepingAvatarPrefab ?? string.Empty;
		}
	}

	// Token: 0x17000945 RID: 2373
	// (get) Token: 0x060028B4 RID: 10420 RVA: 0x0009FCEC File Offset: 0x0009DEEC
	public bool grabsCarrierOnCreate
	{
		get
		{
			return this.valid && this._grabCarrierOnCreate;
		}
	}

	// Token: 0x060028B5 RID: 10421 RVA: 0x0009FD04 File Offset: 0x0009DF04
	public Vector3 SolvePlacement(Vector3 origin, Quaternion rot, int iter)
	{
		return TransformHelpers.TestBoxCorners(origin, rot, this.boxCenter, this.boxSize, 1024, iter);
	}

	// Token: 0x040014A1 RID: 5281
	[SerializeField]
	private string _sleepingAvatarPrefab;

	// Token: 0x040014A2 RID: 5282
	[SerializeField]
	private bool _allowDroppingOfInventory;

	// Token: 0x040014A3 RID: 5283
	[SerializeField]
	private bool _grabCarrierOnCreate;

	// Token: 0x040014A4 RID: 5284
	[SerializeField]
	private Vector3 boxCenter;

	// Token: 0x040014A5 RID: 5285
	[SerializeField]
	private Vector3 boxSize;

	// Token: 0x040014A6 RID: 5286
	[NonSerialized]
	private bool? _prefabValid;

	// Token: 0x040014A7 RID: 5287
	[NonSerialized]
	private bool _hasInventory;

	// Token: 0x040014A8 RID: 5288
	[NonSerialized]
	private bool _hasTakeDamage;

	// Token: 0x040014A9 RID: 5289
	[NonSerialized]
	private Type _takeDamageType;
}
