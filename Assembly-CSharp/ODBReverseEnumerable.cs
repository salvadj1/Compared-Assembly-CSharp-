using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200036B RID: 875
public struct ODBReverseEnumerable<T> : IEnumerable, ODBEnumerable<T, ODBReverseEnumerator<T>>, IEnumerable<T> where T : Object
{
	// Token: 0x06002158 RID: 8536 RVA: 0x00082308 File Offset: 0x00080508
	public ODBReverseEnumerable(ODBNode<T> node)
	{
		this.sibling.has = true;
		this.sibling.item = node;
	}

	// Token: 0x06002159 RID: 8537 RVA: 0x00082324 File Offset: 0x00080524
	public ODBReverseEnumerable(ODBList<T> list)
	{
		this = new ODBReverseEnumerable<T>(list.last);
	}

	// Token: 0x0600215A RID: 8538 RVA: 0x00082334 File Offset: 0x00080534
	public ODBReverseEnumerable(ODBSibling<T> sibling)
	{
		this.sibling = sibling;
	}

	// Token: 0x0600215B RID: 8539 RVA: 0x00082340 File Offset: 0x00080540
	IEnumerator<T> IEnumerable<T>.GetEnumerator()
	{
		ODBReverseEnumerator<T> enumerator = this.GetEnumerator();
		return ODBCachedEnumerator<T, ODBReverseEnumerator<T>>.Cache(ref enumerator);
	}

	// Token: 0x0600215C RID: 8540 RVA: 0x0008235C File Offset: 0x0008055C
	IEnumerator IEnumerable.GetEnumerator()
	{
		return this.GetEnumerator();
	}

	// Token: 0x0600215D RID: 8541 RVA: 0x0008236C File Offset: 0x0008056C
	public ODBReverseEnumerator<T> GetEnumerator()
	{
		return new ODBReverseEnumerator<T>(this.sibling);
	}

	// Token: 0x0600215E RID: 8542 RVA: 0x0008237C File Offset: 0x0008057C
	public IEnumerable<T> ToGeneric()
	{
		ODBReverseEnumerable<T> odbreverseEnumerable = this;
		return ODBGenericEnumerable<T, ODBReverseEnumerator<T>, ODBReverseEnumerable<T>>.Open(ref odbreverseEnumerable);
	}

	// Token: 0x04000FA1 RID: 4001
	private ODBSibling<T> sibling;
}
