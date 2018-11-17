using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000367 RID: 871
public interface ODBEnumerator<T> : IDisposable, IEnumerator, IEnumerator<T> where T : Object
{
	// Token: 0x06002140 RID: 8512
	IEnumerator<T> ToGeneric();

	// Token: 0x17000827 RID: 2087
	// (get) Token: 0x06002141 RID: 8513
	T ExplicitCurrent { get; }
}
