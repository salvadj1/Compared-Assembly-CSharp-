using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000418 RID: 1048
public struct ODBReverseEnumerable<T> : IEnumerable, global::ODBEnumerable<T, global::ODBReverseEnumerator<T>>, IEnumerable<T> where T : Object
{
	// Token: 0x060024BA RID: 9402 RVA: 0x00087704 File Offset: 0x00085904
	public ODBReverseEnumerable(global::ODBNode<T> node)
	{
		this.sibling.has = true;
		this.sibling.item = node;
	}

	// Token: 0x060024BB RID: 9403 RVA: 0x00087720 File Offset: 0x00085920
	public ODBReverseEnumerable(global::ODBList<T> list)
	{
		this = new global::ODBReverseEnumerable<T>(list.last);
	}

	// Token: 0x060024BC RID: 9404 RVA: 0x00087730 File Offset: 0x00085930
	public ODBReverseEnumerable(global::ODBSibling<T> sibling)
	{
		this.sibling = sibling;
	}

	// Token: 0x060024BD RID: 9405 RVA: 0x0008773C File Offset: 0x0008593C
	IEnumerator<T> IEnumerable<T>.GetEnumerator()
	{
		global::ODBReverseEnumerator<T> enumerator = this.GetEnumerator();
		return global::ODBCachedEnumerator<T, global::ODBReverseEnumerator<T>>.Cache(ref enumerator);
	}

	// Token: 0x060024BE RID: 9406 RVA: 0x00087758 File Offset: 0x00085958
	IEnumerator IEnumerable.GetEnumerator()
	{
		return this.GetEnumerator();
	}

	// Token: 0x060024BF RID: 9407 RVA: 0x00087768 File Offset: 0x00085968
	public global::ODBReverseEnumerator<T> GetEnumerator()
	{
		return new global::ODBReverseEnumerator<T>(this.sibling);
	}

	// Token: 0x060024C0 RID: 9408 RVA: 0x00087778 File Offset: 0x00085978
	public IEnumerable<T> ToGeneric()
	{
		global::ODBReverseEnumerable<T> odbreverseEnumerable = this;
		return global::ODBGenericEnumerable<T, global::ODBReverseEnumerator<T>, global::ODBReverseEnumerable<T>>.Open(ref odbreverseEnumerable);
	}

	// Token: 0x04001107 RID: 4359
	private global::ODBSibling<T> sibling;
}
