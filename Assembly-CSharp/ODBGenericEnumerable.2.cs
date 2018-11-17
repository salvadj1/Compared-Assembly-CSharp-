using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200041B RID: 1051
public static class ODBGenericEnumerable<T, TEnumerator> where T : Object where TEnumerator : struct, global::ODBEnumerator<T>
{
	// Token: 0x060024C9 RID: 9417 RVA: 0x0008782C File Offset: 0x00085A2C
	public static IEnumerable<T> Open<TEnumerable>(ref TEnumerable enumerable) where TEnumerable : struct, global::ODBEnumerable<T, TEnumerator>
	{
		return global::ODBGenericEnumerable<T, TEnumerator, TEnumerable>.Open(ref enumerable);
	}
}
