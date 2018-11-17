using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200041A RID: 1050
public static class ODBGenericEnumerable<T> where T : Object
{
	// Token: 0x060024C8 RID: 9416 RVA: 0x00087824 File Offset: 0x00085A24
	public static IEnumerable<T> Open<TEnumerator, TEnumerable>(ref TEnumerable enumerable) where TEnumerator : struct, global::ODBEnumerator<T> where TEnumerable : struct, global::ODBEnumerable<T, TEnumerator>
	{
		return global::ODBGenericEnumerable<T, TEnumerator, TEnumerable>.Open(ref enumerable);
	}
}
