using System;
using Facepunch.Abstract;
using UnityEngine;

// Token: 0x02000215 RID: 533
public abstract class TraitMap<Key> : global::BaseTraitMap where Key : global::TraitKey
{
	// Token: 0x17000388 RID: 904
	// (get) Token: 0x06000E8E RID: 3726
	internal abstract global::TraitMap<Key> __baseMap { get; }

	// Token: 0x17000389 RID: 905
	// (get) Token: 0x06000E8F RID: 3727 RVA: 0x000379FC File Offset: 0x00035BFC
	private KeyTypeInfo<Key>.TraitDictionary map
	{
		get
		{
			if (!this.createdDict)
			{
				this.dict = new KeyTypeInfo<Key>.TraitDictionary(this.K);
				global::TraitMap<Key> _baseMap = this.__baseMap;
				if (_baseMap)
				{
					_baseMap.map.MergeUpon(this.dict);
				}
				this.createdDict = true;
			}
			return this.dict;
		}
	}

	// Token: 0x06000E90 RID: 3728 RVA: 0x00037A58 File Offset: 0x00035C58
	public Key GetTrait(Type traitType)
	{
		return this.map.TryGet(traitType);
	}

	// Token: 0x06000E91 RID: 3729 RVA: 0x00037A68 File Offset: 0x00035C68
	public T GetTrait<T>() where T : Key
	{
		return this.map.TryGetSoftCast<T>();
	}

	// Token: 0x0400093F RID: 2367
	[HideInInspector]
	[SerializeField]
	private Key[] K;

	// Token: 0x04000940 RID: 2368
	[NonSerialized]
	private KeyTypeInfo<Key>.TraitDictionary dict;

	// Token: 0x04000941 RID: 2369
	[NonSerialized]
	private bool createdDict;
}
