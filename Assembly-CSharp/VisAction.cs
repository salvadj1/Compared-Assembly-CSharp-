using System;
using UnityEngine;

// Token: 0x0200043F RID: 1087
public abstract class VisAction : ScriptableObject
{
	// Token: 0x06002623 RID: 9763
	public abstract void Accomplish(IDMain self, IDMain instigator);

	// Token: 0x06002624 RID: 9764
	public abstract void UnAcomplish(IDMain self, IDMain instigator);
}
