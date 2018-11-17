using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200036C RID: 876
public struct ODBForwardEnumerable<T> : IEnumerable, ODBEnumerable<T, ODBForwardEnumerator<T>>, IEnumerable<T> where T : Object
{
	// Token: 0x0600215F RID: 8543 RVA: 0x00082398 File Offset: 0x00080598
	public ODBForwardEnumerable(ODBNode<T> node)
	{
		this.sibling.has = true;
		this.sibling.item = node;
	}

	// Token: 0x06002160 RID: 8544 RVA: 0x000823B4 File Offset: 0x000805B4
	public ODBForwardEnumerable(ODBList<T> list)
	{
		this = new ODBForwardEnumerable<T>(list.last);
	}

	// Token: 0x06002161 RID: 8545 RVA: 0x000823C4 File Offset: 0x000805C4
	public ODBForwardEnumerable(ODBSibling<T> sibling)
	{
		this.sibling = sibling;
	}

	// Token: 0x06002162 RID: 8546 RVA: 0x000823D0 File Offset: 0x000805D0
	IEnumerator<T> IEnumerable<T>.GetEnumerator()
	{
		ODBForwardEnumerator<T> enumerator = this.GetEnumerator();
		return ODBCachedEnumerator<T, ODBForwardEnumerator<T>>.Cache(ref enumerator);
	}

	// Token: 0x06002163 RID: 8547 RVA: 0x000823EC File Offset: 0x000805EC
	IEnumerator IEnumerable.GetEnumerator()
	{
		return this.GetEnumerator();
	}

	// Token: 0x06002164 RID: 8548 RVA: 0x000823FC File Offset: 0x000805FC
	public ODBForwardEnumerator<T> GetEnumerator()
	{
		return new ODBForwardEnumerator<T>(this.sibling);
	}

	// Token: 0x06002165 RID: 8549 RVA: 0x0008240C File Offset: 0x0008060C
	public IEnumerable<T> ToGeneric()
	{
		ODBForwardEnumerable<T> odbforwardEnumerable = this;
		return ODBGenericEnumerable<T, ODBForwardEnumerator<T>, ODBForwardEnumerable<T>>.Open(ref odbforwardEnumerable);
	}

	// Token: 0x04000FA2 RID: 4002
	private ODBSibling<T> sibling;
}
