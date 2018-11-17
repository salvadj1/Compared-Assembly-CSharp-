using System;
using UnityEngine;

// Token: 0x02000102 RID: 258
public class CharacterDeathDropPrefabTrait : CharacterTrait
{
	// Token: 0x1700015B RID: 347
	// (get) Token: 0x060006A0 RID: 1696 RVA: 0x0001E78C File Offset: 0x0001C98C
	public bool hasPrefab
	{
		get
		{
			return (!this._loaded) ? this.prefab : (!this._loadFail);
		}
	}

	// Token: 0x1700015C RID: 348
	// (get) Token: 0x060006A1 RID: 1697 RVA: 0x0001E7C0 File Offset: 0x0001C9C0
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

	// Token: 0x1700015D RID: 349
	// (get) Token: 0x060006A2 RID: 1698 RVA: 0x0001E7DC File Offset: 0x0001C9DC
	public Transform prefabTransform
	{
		get
		{
			GameObject prefab = this.prefab;
			return (!prefab) ? null : prefab.transform;
		}
	}

	// Token: 0x1700015E RID: 350
	// (get) Token: 0x060006A3 RID: 1699 RVA: 0x0001E808 File Offset: 0x0001CA08
	private GameObject prefab
	{
		get
		{
			if (!this._loaded)
			{
				this._loaded = true;
				this._loadFail = ((int)NetCull.LoadPrefab(this._prefabName, out this._loadedPrefab) == 0);
			}
			return this._loadedPrefab;
		}
	}

	// Token: 0x040004F8 RID: 1272
	[SerializeField]
	private string _prefabName;

	// Token: 0x040004F9 RID: 1273
	[NonSerialized]
	private GameObject _loadedPrefab;

	// Token: 0x040004FA RID: 1274
	[NonSerialized]
	private bool _loaded;

	// Token: 0x040004FB RID: 1275
	[NonSerialized]
	private bool _loadFail;
}
