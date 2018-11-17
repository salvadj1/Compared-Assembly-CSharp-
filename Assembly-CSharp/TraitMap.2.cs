using System;
using Facepunch.Abstract;
using UnityEngine;

// Token: 0x020001E4 RID: 484
public abstract class TraitMap<Key> : BaseTraitMap where Key : TraitKey
{
	// Token: 0x17000342 RID: 834
	// (get) Token: 0x06000D46 RID: 3398
	internal abstract TraitMap<Key> __baseMap { get; }

	// Token: 0x17000343 RID: 835
	// (get) Token: 0x06000D47 RID: 3399 RVA: 0x00033974 File Offset: 0x00031B74
	private KeyTypeInfo<Key>.TraitDictionary map
	{
		get
		{
			if (!this.createdDict)
			{
				this.dict = new KeyTypeInfo<Key>.TraitDictionary(this.K);
				TraitMap<Key> _baseMap = this.__baseMap;
				if (_baseMap)
				{
					_baseMap.map.MergeUpon(this.dict);
				}
				this.createdDict = true;
			}
			return this.dict;
		}
	}

	// Token: 0x06000D48 RID: 3400 RVA: 0x000339D0 File Offset: 0x00031BD0
	public Key GetTrait(Type traitType)
	{
		return this.map.TryGet(traitType);
	}

	// Token: 0x06000D49 RID: 3401 RVA: 0x000339E0 File Offset: 0x00031BE0
	public T GetTrait<T>() where T : Key
	{
		return this.map.TryGetSoftCast<T>();
	}

	// Token: 0x04000827 RID: 2087
	[HideInInspector]
	[SerializeField]
	private Key[] K;

	// Token: 0x04000828 RID: 2088
	[NonSerialized]
	private KeyTypeInfo<Key>.TraitDictionary dict;

	// Token: 0x04000829 RID: 2089
	[NonSerialized]
	private bool createdDict;
}
