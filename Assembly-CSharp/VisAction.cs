using System;
using UnityEngine;

// Token: 0x02000392 RID: 914
public abstract class VisAction : ScriptableObject
{
	// Token: 0x060022C1 RID: 8897
	public abstract void Accomplish(IDMain self, IDMain instigator);

	// Token: 0x060022C2 RID: 8898
	public abstract void UnAcomplish(IDMain self, IDMain instigator);
}
