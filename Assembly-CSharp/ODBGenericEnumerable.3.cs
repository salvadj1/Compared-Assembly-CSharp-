using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200041C RID: 1052
public sealed class ODBGenericEnumerable<T, TEnumerator, TEnumerable> : IDisposable, IEnumerable, IEnumerable<T> where T : Object where TEnumerator : struct, global::ODBEnumerator<T> where TEnumerable : struct, global::ODBEnumerable<T, TEnumerator>
{
	// Token: 0x060024CA RID: 9418 RVA: 0x00087834 File Offset: 0x00085A34
	private ODBGenericEnumerable(ref TEnumerable enumerable)
	{
		this.enumerable = enumerable;
	}

	// Token: 0x060024CC RID: 9420 RVA: 0x0008784C File Offset: 0x00085A4C
	IEnumerator<T> IEnumerable<T>.GetEnumerator()
	{
		TEnumerator enumerator = this.GetEnumerator();
		return global::ODBCachedEnumerator<T, TEnumerator>.Cache(ref enumerator);
	}

	// Token: 0x060024CD RID: 9421 RVA: 0x00087868 File Offset: 0x00085A68
	IEnumerator IEnumerable.GetEnumerator()
	{
		throw new NotSupportedException("Cannot use non generic IEnumerable interface with given object");
	}

	// Token: 0x060024CE RID: 9422 RVA: 0x00087874 File Offset: 0x00085A74
	public TEnumerator GetEnumerator()
	{
		if (this.disposed)
		{
			throw new ObjectDisposedException("enumerable");
		}
		return this.enumerable.GetEnumerator();
	}

	// Token: 0x060024CF RID: 9423 RVA: 0x000878A0 File Offset: 0x00085AA0
	public void Dispose()
	{
		if (!this.disposed)
		{
			this.enumerable = default(TEnumerable);
			this.disposed = true;
			this.next = global::ODBGenericEnumerable<T, TEnumerator, TEnumerable>.recycle;
			global::ODBGenericEnumerable<T, TEnumerator, TEnumerable>.recycle = this;
		}
	}

	// Token: 0x060024D0 RID: 9424 RVA: 0x000878E0 File Offset: 0x00085AE0
	public static global::ODBGenericEnumerable<T, TEnumerator, TEnumerable> Open(ref TEnumerable enumerable)
	{
		if (global::ODBGenericEnumerable<T, TEnumerator, TEnumerable>.recycle == null)
		{
			return new global::ODBGenericEnumerable<T, TEnumerator, TEnumerable>(ref enumerable);
		}
		global::ODBGenericEnumerable<T, TEnumerator, TEnumerable> odbgenericEnumerable = global::ODBGenericEnumerable<T, TEnumerator, TEnumerable>.recycle;
		odbgenericEnumerable.disposed = false;
		global::ODBGenericEnumerable<T, TEnumerator, TEnumerable>.recycle = odbgenericEnumerable.next;
		odbgenericEnumerable.enumerable = enumerable;
		return odbgenericEnumerable;
	}

	// Token: 0x04001109 RID: 4361
	private TEnumerable enumerable;

	// Token: 0x0400110A RID: 4362
	private global::ODBGenericEnumerable<T, TEnumerator, TEnumerable> next;

	// Token: 0x0400110B RID: 4363
	private bool disposed;

	// Token: 0x0400110C RID: 4364
	private static global::ODBGenericEnumerable<T, TEnumerator, TEnumerable> recycle;
}
