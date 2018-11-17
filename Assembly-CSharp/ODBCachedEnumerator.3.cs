using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200041F RID: 1055
public static class ODBCachedEnumerator
{
	// Token: 0x060024DB RID: 9435 RVA: 0x00087A30 File Offset: 0x00085C30
	public static IEnumerator<T> Cache<TEnumerator, T>(ref TEnumerator enumerator) where TEnumerator : struct, global::ODBEnumerator<T> where T : Object
	{
		return global::ODBCachedEnumerator<T>.Cache<TEnumerator>(ref enumerator);
	}
}
