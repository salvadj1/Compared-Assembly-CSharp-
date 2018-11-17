using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000216 RID: 534
public abstract class TraitMap<Key, Implementation> : global::TraitMap<Key> where Key : global::TraitKey where Implementation : global::TraitMap<Key, Implementation>
{
	// Token: 0x1700038A RID: 906
	// (get) Token: 0x06000E93 RID: 3731 RVA: 0x00037A80 File Offset: 0x00035C80
	internal sealed override global::TraitMap<Key> __baseMap
	{
		get
		{
			return this.B;
		}
	}

	// Token: 0x1700038B RID: 907
	// (get) Token: 0x06000E94 RID: 3732 RVA: 0x00037A90 File Offset: 0x00035C90
	public static bool AnyRegistered
	{
		get
		{
			return global::TraitMap<Key, Implementation>.anyRegistry;
		}
	}

	// Token: 0x1700038C RID: 908
	// (get) Token: 0x06000E95 RID: 3733 RVA: 0x00037A98 File Offset: 0x00035C98
	public static ICollection<Implementation> AllRegistered
	{
		get
		{
			if (!global::TraitMap<Key, Implementation>.anyRegistry)
			{
				return new Implementation[0];
			}
			return global::TraitMap<Key, Implementation>.LookupRegister.dict.Values;
		}
	}

	// Token: 0x06000E96 RID: 3734 RVA: 0x00037AB8 File Offset: 0x00035CB8
	public static bool ByName(string name, out Implementation map)
	{
		if (!global::TraitMap<Key, Implementation>.anyRegistry)
		{
			map = (Implementation)((object)null);
			return false;
		}
		return global::TraitMap<Key, Implementation>.LookupRegister.dict.TryGetValue(name, out map);
	}

	// Token: 0x06000E97 RID: 3735 RVA: 0x00037AEC File Offset: 0x00035CEC
	public static Implementation ByName(string name)
	{
		Implementation implementation;
		return (!global::TraitMap<Key, Implementation>.anyRegistry || !global::TraitMap<Key, Implementation>.LookupRegister.dict.TryGetValue(name, out implementation)) ? ((Implementation)((object)null)) : implementation;
	}

	// Token: 0x06000E98 RID: 3736 RVA: 0x00037B24 File Offset: 0x00035D24
	internal sealed override void BindToRegistry()
	{
		global::TraitMap<Key, Implementation>.LookupRegister.Add((Implementation)((object)this));
	}

	// Token: 0x04000942 RID: 2370
	[SerializeField]
	[HideInInspector]
	private Implementation B;

	// Token: 0x04000943 RID: 2371
	private static bool anyRegistry;

	// Token: 0x02000217 RID: 535
	private static class LookupRegister
	{
		// Token: 0x06000E99 RID: 3737 RVA: 0x00037B34 File Offset: 0x00035D34
		static LookupRegister()
		{
			global::TraitMap<Key, Implementation>.anyRegistry = true;
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x00037B4C File Offset: 0x00035D4C
		public static void Add(Implementation implementation)
		{
			global::TraitMap<Key, Implementation>.LookupRegister.dict[implementation.name] = implementation;
		}

		// Token: 0x04000944 RID: 2372
		public static readonly Dictionary<string, Implementation> dict = new Dictionary<string, Implementation>(StringComparer.InvariantCultureIgnoreCase);
	}
}
