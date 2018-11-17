using System;
using UnityEngine;

// Token: 0x020002F3 RID: 755
internal interface IIDLocalInterpolator
{
	// Token: 0x1700078D RID: 1933
	// (get) Token: 0x06001A71 RID: 6769
	IDMain idMain { get; }

	// Token: 0x1700078E RID: 1934
	// (get) Token: 0x06001A72 RID: 6770
	IDLocal self { get; }

	// Token: 0x06001A73 RID: 6771
	void SetGoals(Vector3 pos, Quaternion rot, double timestamp);
}
