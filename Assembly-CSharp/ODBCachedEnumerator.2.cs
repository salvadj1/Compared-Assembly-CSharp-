using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200041E RID: 1054
public static class ODBCachedEnumerator<T> where T : Object
{
	// Token: 0x060024DA RID: 9434 RVA: 0x00087A28 File Offset: 0x00085C28
	public static IEnumerator<T> Cache<TEnumerator>(ref TEnumerator enumerator) where TEnumerator : struct, global::ODBEnumerator<T>
	{
		return global::ODBCachedEnumerator<T, TEnumerator>.Cache(ref enumerator);
	}
}
