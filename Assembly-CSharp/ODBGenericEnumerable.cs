using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200036D RID: 877
public static class ODBGenericEnumerable<T> where T : Object
{
	// Token: 0x06002166 RID: 8550 RVA: 0x00082428 File Offset: 0x00080628
	public static IEnumerable<T> Open<TEnumerator, TEnumerable>(ref TEnumerable enumerable) where TEnumerator : struct, ODBEnumerator<T> where TEnumerable : struct, ODBEnumerable<T, TEnumerator>
	{
		return ODBGenericEnumerable<T, TEnumerator, TEnumerable>.Open(ref enumerable);
	}
}
