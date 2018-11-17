using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000371 RID: 881
public static class ODBCachedEnumerator<T> where T : Object
{
	// Token: 0x06002178 RID: 8568 RVA: 0x0008262C File Offset: 0x0008082C
	public static IEnumerator<T> Cache<TEnumerator>(ref TEnumerator enumerator) where TEnumerator : struct, ODBEnumerator<T>
	{
		return ODBCachedEnumerator<T, TEnumerator>.Cache(ref enumerator);
	}
}
