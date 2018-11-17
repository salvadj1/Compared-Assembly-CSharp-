using System;
using UnityEngine;

// Token: 0x020001B6 RID: 438
public static class FindChildHelper
{
	// Token: 0x06000C5C RID: 3164 RVA: 0x00031438 File Offset: 0x0002F638
	private static Transform _GetFound()
	{
		Transform result = FindChildHelper.found;
		FindChildHelper.found = null;
		return result;
	}

	// Token: 0x06000C5D RID: 3165 RVA: 0x00031454 File Offset: 0x0002F654
	private static bool __FindChildByNameRecurse(string name, Transform parent)
	{
		if (parent.childCount == 0)
		{
			return false;
		}
		FindChildHelper.found = parent.Find(name);
		if (FindChildHelper.found)
		{
			return true;
		}
		int childCount = parent.childCount;
		for (int i = 0; i < childCount; i++)
		{
			Transform child = parent.GetChild(i);
			if (child.childCount > 0 && FindChildHelper.__FindChildByNameRecurse(name, child))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000C5E RID: 3166 RVA: 0x000314C8 File Offset: 0x0002F6C8
	private static bool _FindChildByNameRecurse(string name, Transform parent)
	{
		return FindChildHelper.__FindChildByNameRecurse(name, parent);
	}

	// Token: 0x06000C5F RID: 3167 RVA: 0x000314E0 File Offset: 0x0002F6E0
	private static Transform NoChildNamed(string name, Object parent)
	{
		return null;
	}

	// Token: 0x06000C60 RID: 3168 RVA: 0x000314E4 File Offset: 0x0002F6E4
	[Obsolete("If this is being called in Start, Awake, or OnEnabled consider using the @PrefetchChildComponent on the variable.", false)]
	public static Transform FindChildByName(string name, Transform parent)
	{
		if (parent.name == name)
		{
			return parent;
		}
		if (FindChildHelper._FindChildByNameRecurse(name, parent))
		{
			return FindChildHelper._GetFound();
		}
		return FindChildHelper.NoChildNamed(name, parent);
	}

	// Token: 0x06000C61 RID: 3169 RVA: 0x00031520 File Offset: 0x0002F720
	[Obsolete("If this is being called in Start, Awake, or OnEnabled consider using the @PrefetchChildComponent on the variable.", false)]
	public static Transform FindChildByName(string name, GameObject parent)
	{
		if (parent.name == name)
		{
			return parent.transform;
		}
		if (FindChildHelper._FindChildByNameRecurse(name, parent.transform))
		{
			return FindChildHelper._GetFound();
		}
		return FindChildHelper.NoChildNamed(name, parent);
	}

	// Token: 0x06000C62 RID: 3170 RVA: 0x00031564 File Offset: 0x0002F764
	[Obsolete("If this is being called in Start, Awake, or OnEnabled consider using the @PrefetchChildComponent on the variable.", false)]
	public static Transform FindChildByName(string name, Component parent)
	{
		if (parent.name == name)
		{
			return parent.transform;
		}
		if (FindChildHelper._FindChildByNameRecurse(name, parent.transform))
		{
			return FindChildHelper._GetFound();
		}
		return FindChildHelper.NoChildNamed(name, parent);
	}

	// Token: 0x06000C63 RID: 3171 RVA: 0x000315A8 File Offset: 0x0002F7A8
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

	// Token: 0x06000C64 RID: 3172 RVA: 0x000315C8 File Offset: 0x0002F7C8
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
			return FindChildHelper.GetChildAtIndex(transform, Random.Range(0, childCount));
		}
		return transform.GetChild(0);
	}

	// Token: 0x04000786 RID: 1926
	private static Transform found;
}
