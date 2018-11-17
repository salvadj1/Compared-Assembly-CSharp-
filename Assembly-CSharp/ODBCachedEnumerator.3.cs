using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000372 RID: 882
public static class ODBCachedEnumerator
{
	// Token: 0x06002179 RID: 8569 RVA: 0x00082634 File Offset: 0x00080834
	public static IEnumerator<T> Cache<TEnumerator, T>(ref TEnumerator enumerator) where TEnumerator : struct, ODBEnumerator<T> where T : Object
	{
		return ODBCachedEnumerator<T>.Cache<TEnumerator>(ref enumerator);
	}
}
