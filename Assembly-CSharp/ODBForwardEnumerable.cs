using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000419 RID: 1049
public struct ODBForwardEnumerable<T> : IEnumerable, global::ODBEnumerable<T, global::ODBForwardEnumerator<T>>, IEnumerable<T> where T : Object
{
	// Token: 0x060024C1 RID: 9409 RVA: 0x00087794 File Offset: 0x00085994
	public ODBForwardEnumerable(global::ODBNode<T> node)
	{
		this.sibling.has = true;
		this.sibling.item = node;
	}

	// Token: 0x060024C2 RID: 9410 RVA: 0x000877B0 File Offset: 0x000859B0
	public ODBForwardEnumerable(global::ODBList<T> list)
	{
		this = new global::ODBForwardEnumerable<T>(list.last);
	}

	// Token: 0x060024C3 RID: 9411 RVA: 0x000877C0 File Offset: 0x000859C0
	public ODBForwardEnumerable(global::ODBSibling<T> sibling)
	{
		this.sibling = sibling;
	}

	// Token: 0x060024C4 RID: 9412 RVA: 0x000877CC File Offset: 0x000859CC
	IEnumerator<T> IEnumerable<T>.GetEnumerator()
	{
		global::ODBForwardEnumerator<T> enumerator = this.GetEnumerator();
		return global::ODBCachedEnumerator<T, global::ODBForwardEnumerator<T>>.Cache(ref enumerator);
	}

	// Token: 0x060024C5 RID: 9413 RVA: 0x000877E8 File Offset: 0x000859E8
	IEnumerator IEnumerable.GetEnumerator()
	{
		return this.GetEnumerator();
	}

	// Token: 0x060024C6 RID: 9414 RVA: 0x000877F8 File Offset: 0x000859F8
	public global::ODBForwardEnumerator<T> GetEnumerator()
	{
		return new global::ODBForwardEnumerator<T>(this.sibling);
	}

	// Token: 0x060024C7 RID: 9415 RVA: 0x00087808 File Offset: 0x00085A08
	public IEnumerable<T> ToGeneric()
	{
		global::ODBForwardEnumerable<T> odbforwardEnumerable = this;
		return global::ODBGenericEnumerable<T, global::ODBForwardEnumerator<T>, global::ODBForwardEnumerable<T>>.Open(ref odbforwardEnumerable);
	}

	// Token: 0x04001108 RID: 4360
	private global::ODBSibling<T> sibling;
}
