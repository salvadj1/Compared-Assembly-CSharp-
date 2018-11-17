using System;
using UnityEngine;

// Token: 0x02000121 RID: 289
public class CharacterDeathDropPrefabTrait : global::CharacterTrait
{
	// Token: 0x17000189 RID: 393
	// (get) Token: 0x06000772 RID: 1906 RVA: 0x00021360 File Offset: 0x0001F560
	public bool hasPrefab
	{
		get
		{
			return (!this._loaded) ? this.prefab : (!this._loadFail);
		}
	}

	// Token: 0x1700018A RID: 394
	// (get) Token: 0x06000773 RID: 1907 RVA: 0x00021394 File Offset: 0x0001F594
	public string instantiateString
	{
		get
		{
			if (this.prefab)
			{
				return this._prefabName;
			}
			return null;
		}
	}

	// Token: 0x1700018B RID: 395
	// (get) Token: 0x06000774 RID: 1908 RVA: 0x000213B0 File Offset: 0x0001F5B0
	public Transform prefabTransform
	{
		get
		{
			GameObject prefab = this.prefab;
			return (!prefab) ? null : prefab.transform;
		}
	}

	// Token: 0x1700018C RID: 396
	// (get) Token: 0x06000775 RID: 1909 RVA: 0x000213DC File Offset: 0x0001F5DC
	private GameObject prefab
	{
		get
		{
			if (!this._loaded)
			{
				this._loaded = true;
				this._loadFail = ((int)global::NetCull.LoadPrefab(this._prefabName, out this._loadedPrefab) == 0);
			}
			return this._loadedPrefab;
		}
	}

	// Token: 0x040005C3 RID: 1475
	[SerializeField]
	private string _prefabName;

	// Token: 0x040005C4 RID: 1476
	[NonSerialized]
	private GameObject _loadedPrefab;

	// Token: 0x040005C5 RID: 1477
	[NonSerialized]
	private bool _loaded;

	// Token: 0x040005C6 RID: 1478
	[NonSerialized]
	private bool _loadFail;
}
