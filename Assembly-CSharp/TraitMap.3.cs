using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001E5 RID: 485
public abstract class TraitMap<Key, Implementation> : TraitMap<Key> where Key : TraitKey where Implementation : TraitMap<Key, Implementation>
{
	// Token: 0x17000344 RID: 836
	// (get) Token: 0x06000D4B RID: 3403 RVA: 0x000339F8 File Offset: 0x00031BF8
	internal sealed override TraitMap<Key> __baseMap
	{
		get
		{
			return this.B;
		}
	}

	// Token: 0x17000345 RID: 837
	// (get) Token: 0x06000D4C RID: 3404 RVA: 0x00033A08 File Offset: 0x00031C08
	public static bool AnyRegistered
	{
		get
		{
			return TraitMap<Key, Implementation>.anyRegistry;
		}
	}

	// Token: 0x17000346 RID: 838
	// (get) Token: 0x06000D4D RID: 3405 RVA: 0x00033A10 File Offset: 0x00031C10
	public static ICollection<Implementation> AllRegistered
	{
		get
		{
			if (!TraitMap<Key, Implementation>.anyRegistry)
			{
				return new Implementation[0];
			}
			return TraitMap<Key, Implementation>.LookupRegister.dict.Values;
		}
	}

	// Token: 0x06000D4E RID: 3406 RVA: 0x00033A30 File Offset: 0x00031C30
	public static bool ByName(string name, out Implementation map)
	{
		if (!TraitMap<Key, Implementation>.anyRegistry)
		{
			map = (Implementation)((object)null);
			return false;
		}
		return TraitMap<Key, Implementation>.LookupRegister.dict.TryGetValue(name, out map);
	}

	// Token: 0x06000D4F RID: 3407 RVA: 0x00033A64 File Offset: 0x00031C64
	public static Implementation ByName(string name)
	{
		Implementation implementation;
		return (!TraitMap<Key, Implementation>.anyRegistry || !TraitMap<Key, Implementation>.LookupRegister.dict.TryGetValue(name, out implementation)) ? ((Implementation)((object)null)) : implementation;
	}

	// Token: 0x06000D50 RID: 3408 RVA: 0x00033A9C File Offset: 0x00031C9C
	internal sealed override void BindToRegistry()
	{
		TraitMap<Key, Implementation>.LookupRegister.Add((Implementation)((object)this));
	}

	// Token: 0x0400082A RID: 2090
	[HideInInspector]
	[SerializeField]
	private Implementation B;

	// Token: 0x0400082B RID: 2091
	private static bool anyRegistry;

	// Token: 0x020001E6 RID: 486
	private static class LookupRegister
	{
		// Token: 0x06000D51 RID: 3409 RVA: 0x00033AAC File Offset: 0x00031CAC
		static LookupRegister()
		{
			TraitMap<Key, Implementation>.anyRegistry = true;
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x00033AC4 File Offset: 0x00031CC4
		public static void Add(Implementation implementation)
		{
			TraitMap<Key, Implementation>.LookupRegister.dict[implementation.name] = implementation;
		}

		// Token: 0x0400082C RID: 2092
		public static readonly Dictionary<string, Implementation> dict = new Dictionary<string, Implementation>(StringComparer.InvariantCultureIgnoreCase);
	}
}
