using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200036F RID: 879
public sealed class ODBGenericEnumerable<T, TEnumerator, TEnumerable> : IDisposable, IEnumerable, IEnumerable<T> where T : Object where TEnumerator : struct, ODBEnumerator<T> where TEnumerable : struct, ODBEnumerable<T, TEnumerator>
{
	// Token: 0x06002168 RID: 8552 RVA: 0x00082438 File Offset: 0x00080638
	private ODBGenericEnumerable(ref TEnumerable enumerable)
	{
		this.enumerable = enumerable;
	}

	// Token: 0x0600216A RID: 8554 RVA: 0x00082450 File Offset: 0x00080650
	IEnumerator<T> IEnumerable<T>.GetEnumerator()
	{
		TEnumerator enumerator = this.GetEnumerator();
		return ODBCachedEnumerator<T, TEnumerator>.Cache(ref enumerator);
	}

	// Token: 0x0600216B RID: 8555 RVA: 0x0008246C File Offset: 0x0008066C
	IEnumerator IEnumerable.GetEnumerator()
	{
		throw new NotSupportedException("Cannot use non generic IEnumerable interface with given object");
	}

	// Token: 0x0600216C RID: 8556 RVA: 0x00082478 File Offset: 0x00080678
	public TEnumerator GetEnumerator()
	{
		if (this.disposed)
		{
			throw new ObjectDisposedException("enumerable");
		}
		return this.enumerable.GetEnumerator();
	}

	// Token: 0x0600216D RID: 8557 RVA: 0x000824A4 File Offset: 0x000806A4
	public void Dispose()
	{
		if (!this.disposed)
		{
			this.enumerable = default(TEnumerable);
			this.disposed = true;
			this.next = ODBGenericEnumerable<T, TEnumerator, TEnumerable>.recycle;
			ODBGenericEnumerable<T, TEnumerator, TEnumerable>.recycle = this;
		}
	}

	// Token: 0x0600216E RID: 8558 RVA: 0x000824E4 File Offset: 0x000806E4
	public static ODBGenericEnumerable<T, TEnumerator, TEnumerable> Open(ref TEnumerable enumerable)
	{
		if (ODBGenericEnumerable<T, TEnumerator, TEnumerable>.recycle == null)
		{
			return new ODBGenericEnumerable<T, TEnumerator, TEnumerable>(ref enumerable);
		}
		ODBGenericEnumerable<T, TEnumerator, TEnumerable> odbgenericEnumerable = ODBGenericEnumerable<T, TEnumerator, TEnumerable>.recycle;
		odbgenericEnumerable.disposed = false;
		ODBGenericEnumerable<T, TEnumerator, TEnumerable>.recycle = odbgenericEnumerable.next;
		odbgenericEnumerable.enumerable = enumerable;
		return odbgenericEnumerable;
	}

	// Token: 0x04000FA3 RID: 4003
	private TEnumerable enumerable;

	// Token: 0x04000FA4 RID: 4004
	private ODBGenericEnumerable<T, TEnumerator, TEnumerable> next;

	// Token: 0x04000FA5 RID: 4005
	private bool disposed;

	// Token: 0x04000FA6 RID: 4006
	private static ODBGenericEnumerable<T, TEnumerator, TEnumerable> recycle;
}
