using System;
using UnityEngine;

// Token: 0x020001E6 RID: 486
public static class FindChildHelper
{
	// Token: 0x06000D9C RID: 3484 RVA: 0x00035324 File Offset: 0x00033524
	private static Transform _GetFound()
	{
		Transform result = global::FindChildHelper.found;
		global::FindChildHelper.found = null;
		return result;
	}

	// Token: 0x06000D9D RID: 3485 RVA: 0x00035340 File Offset: 0x00033540
	private static bool __FindChildByNameRecurse(string name, Transform parent)
	{
		if (parent.childCount == 0)
		{
			return false;
		}
		global::FindChildHelper.found = parent.Find(name);
		if (global::FindChildHelper.found)
		{
			return true;
		}
		int childCount = parent.childCount;
		for (int i = 0; i < childCount; i++)
		{
			Transform child = parent.GetChild(i);
			if (child.childCount > 0 && global::FindChildHelper.__FindChildByNameRecurse(name, child))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000D9E RID: 3486 RVA: 0x000353B4 File Offset: 0x000335B4
	private static bool _FindChildByNameRecurse(string name, Transform parent)
	{
		return global::FindChildHelper.__FindChildByNameRecurse(name, parent);
	}

	// Token: 0x06000D9F RID: 3487 RVA: 0x000353CC File Offset: 0x000335CC
	private static Transform NoChildNamed(string name, Object parent)
	{
		return null;
	}

	// Token: 0x06000DA0 RID: 3488 RVA: 0x000353D0 File Offset: 0x000335D0
	[Obsolete("If this is being called in Start, Awake, or OnEnabled consider using the @PrefetchChildComponent on the variable.", false)]
	public static Transform FindChildByName(string name, Transform parent)
	{
		if (parent.name == name)
		{
			return parent;
		}
		if (global::FindChildHelper._FindChildByNameRecurse(name, parent))
		{
			return global::FindChildHelper._GetFound();
		}
		return global::FindChildHelper.NoChildNamed(name, parent);
	}

	// Token: 0x06000DA1 RID: 3489 RVA: 0x0003540C File Offset: 0x0003360C
	[Obsolete("If this is being called in Start, Awake, or OnEnabled consider using the @PrefetchChildComponent on the variable.", false)]
	public static Transform FindChildByName(string name, GameObject parent)
	{
		if (parent.name == name)
		{
			return parent.transform;
		}
		if (global::FindChildHelper._FindChildByNameRecurse(name, parent.transform))
		{
			return global::FindChildHelper._GetFound();
		}
		return global::FindChildHelper.NoChildNamed(name, parent);
	}

	// Token: 0x06000DA2 RID: 3490 RVA: 0x00035450 File Offset: 0x00033650
	[Obsolete("If this is being called in Start, Awake, or OnEnabled consider using the @PrefetchChildComponent on the variable.", false)]
	public static Transform FindChildByName(string name, Component parent)
	{
		if (parent.name == name)
		{
			return parent.transform;
		}
		if (global::FindChildHelper._FindChildByNameRecurse(name, parent.transform))
		{
			return global::FindChildHelper._GetFound();
		}
		return global::FindChildHelper.NoChildNamed(name, parent);
	}

	// Token: 0x06000DA3 RID: 3491 RVA: 0x00035494 File Offset: 0x00033694
	public static Transform GetChildAtIndex(Transform transform, int i)
	{
		if (0 > i)
		{
			return null;
		}
		if (transform.childCount > i)
		{
			return transform.GetChild(i);
		}
		return null;
	}

	// Token: 0x06000DA4 RID: 3492 RVA: 0x000354B4 File Offset: 0x000336B4
	public static Transform RandomChild(Transform transform)
	{
		int childCount = transform.childCount;
		int num = childCount;
		if (num == 0)
		{
			return null;
		}
		if (num != 1)
		{
			return global::FindChildHelper.GetChildAtIndex(transform, Random.Range(0, childCount));
		}
		return transform.GetChild(0);
	}

	// Token: 0x0400089A RID: 2202
	private static Transform found;
}
