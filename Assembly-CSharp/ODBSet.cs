using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000423 RID: 1059
public sealed class ODBSet<T> : global::ODBList<T> where T : Object
{
	// Token: 0x06002510 RID: 9488 RVA: 0x00088530 File Offset: 0x00086730
	public ODBSet()
	{
	}

	// Token: 0x06002511 RID: 9489 RVA: 0x00088538 File Offset: 0x00086738
	public ODBSet(IEnumerable<T> collection) : base(collection)
	{
	}

	// Token: 0x06002512 RID: 9490 RVA: 0x00088544 File Offset: 0x00086744
	public bool Add(T item)
	{
		return base.DoAdd(item);
	}

	// Token: 0x06002513 RID: 9491 RVA: 0x00088550 File Offset: 0x00086750
	public bool Add(T item, out global::ODBNode<T> node)
	{
		return base.DoAdd(item, out node);
	}

	// Token: 0x06002514 RID: 9492 RVA: 0x0008855C File Offset: 0x0008675C
	public bool Remove(T item)
	{
		return base.DoRemove(item);
	}

	// Token: 0x06002515 RID: 9493 RVA: 0x00088568 File Offset: 0x00086768
	public bool Remove(ref global::ODBNode<T> node)
	{
		return base.DoRemove(ref node);
	}

	// Token: 0x06002516 RID: 9494 RVA: 0x00088574 File Offset: 0x00086774
	public void Clear()
	{
		base.DoClear();
	}

	// Token: 0x06002517 RID: 9495 RVA: 0x0008857C File Offset: 0x0008677C
	public void UnionWith(global::ODBList<T> list)
	{
		base.DoUnionWith(list);
	}

	// Token: 0x06002518 RID: 9496 RVA: 0x00088588 File Offset: 0x00086788
	public void IntersectWith(global::ODBList<T> list)
	{
		base.DoIntersectWith(list);
	}

	// Token: 0x06002519 RID: 9497 RVA: 0x00088594 File Offset: 0x00086794
	public void ExceptWith(global::ODBList<T> list)
	{
		base.DoExceptWith(list);
	}

	// Token: 0x0600251A RID: 9498 RVA: 0x000885A0 File Offset: 0x000867A0
	public void SymmetricExceptWith(global::ODBList<T> list)
	{
		base.DoSymmetricExceptWith(list);
	}
}
