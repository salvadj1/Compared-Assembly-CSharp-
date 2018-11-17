using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000414 RID: 1044
public interface ODBEnumerator<T> : IDisposable, IEnumerator, IEnumerator<T> where T : Object
{
	// Token: 0x060024A2 RID: 9378
	IEnumerator<T> ToGeneric();

	// Token: 0x17000885 RID: 2181
	// (get) Token: 0x060024A3 RID: 9379
	T ExplicitCurrent { get; }
}
