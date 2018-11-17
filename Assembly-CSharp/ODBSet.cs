using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000376 RID: 886
public sealed class ODBSet<T> : ODBList<T> where T : Object
{
	// Token: 0x060021AE RID: 8622 RVA: 0x00083134 File Offset: 0x00081334
	public ODBSet()
	{
	}

	// Token: 0x060021AF RID: 8623 RVA: 0x0008313C File Offset: 0x0008133C
	public ODBSet(IEnumerable<T> collection) : base(collection)
	{
	}

	// Token: 0x060021B0 RID: 8624 RVA: 0x00083148 File Offset: 0x00081348
	public bool Add(T item)
	{
		return base.DoAdd(item);
	}

	// Token: 0x060021B1 RID: 8625 RVA: 0x00083154 File Offset: 0x00081354
	public bool Add(T item, out ODBNode<T> node)
	{
		return base.DoAdd(item, out node);
	}

	// Token: 0x060021B2 RID: 8626 RVA: 0x00083160 File Offset: 0x00081360
	public bool Remove(T item)
	{
		return base.DoRemove(item);
	}

	// Token: 0x060021B3 RID: 8627 RVA: 0x0008316C File Offset: 0x0008136C
	public bool Remove(ref ODBNode<T> node)
	{
		return base.DoRemove(ref node);
	}

	// Token: 0x060021B4 RID: 8628 RVA: 0x00083178 File Offset: 0x00081378
	public void Clear()
	{
		base.DoClear();
	}

	// Token: 0x060021B5 RID: 8629 RVA: 0x00083180 File Offset: 0x00081380
	public void UnionWith(ODBList<T> list)
	{
		base.DoUnionWith(list);
	}

	// Token: 0x060021B6 RID: 8630 RVA: 0x0008318C File Offset: 0x0008138C
	public void IntersectWith(ODBList<T> list)
	{
		base.DoIntersectWith(list);
	}

	// Token: 0x060021B7 RID: 8631 RVA: 0x00083198 File Offset: 0x00081398
	public void ExceptWith(ODBList<T> list)
	{
		base.DoExceptWith(list);
	}

	// Token: 0x060021B8 RID: 8632 RVA: 0x000831A4 File Offset: 0x000813A4
	public void SymmetricExceptWith(ODBList<T> list)
	{
		base.DoSymmetricExceptWith(list);
	}
}
