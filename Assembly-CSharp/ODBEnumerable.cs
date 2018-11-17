using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200036A RID: 874
public interface ODBEnumerable<T, TEnumerator> : IEnumerable, IEnumerable<T> where T : Object where TEnumerator : struct, ODBEnumerator<T>
{
	// Token: 0x06002156 RID: 8534
	TEnumerator GetEnumerator();

	// Token: 0x06002157 RID: 8535
	IEnumerable<T> ToGeneric();
}
