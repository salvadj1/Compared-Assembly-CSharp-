using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000417 RID: 1047
public interface ODBEnumerable<T, TEnumerator> : IEnumerable, IEnumerable<T> where T : Object where TEnumerator : struct, global::ODBEnumerator<T>
{
	// Token: 0x060024B8 RID: 9400
	TEnumerator GetEnumerator();

	// Token: 0x060024B9 RID: 9401
	IEnumerable<T> ToGeneric();
}
