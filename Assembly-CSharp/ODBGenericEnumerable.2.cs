using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200036E RID: 878
public static class ODBGenericEnumerable<T, TEnumerator> where T : Object where TEnumerator : struct, ODBEnumerator<T>
{
	// Token: 0x06002167 RID: 8551 RVA: 0x00082430 File Offset: 0x00080630
	public static IEnumerable<T> Open<TEnumerable>(ref TEnumerable enumerable) where TEnumerable : struct, ODBEnumerable<T, TEnumerator>
	{
		return ODBGenericEnumerable<T, TEnumerator, TEnumerable>.Open(ref enumerable);
	}
}
