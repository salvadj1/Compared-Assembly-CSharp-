using System;
using UnityEngine;

// Token: 0x020002B6 RID: 694
internal interface IIDLocalInterpolator
{
	// Token: 0x17000739 RID: 1849
	// (get) Token: 0x060018E1 RID: 6369
	IDMain idMain { get; }

	// Token: 0x1700073A RID: 1850
	// (get) Token: 0x060018E2 RID: 6370
	IDLocal self { get; }

	// Token: 0x060018E3 RID: 6371
	void SetGoals(Vector3 pos, Quaternion rot, double timestamp);
}
