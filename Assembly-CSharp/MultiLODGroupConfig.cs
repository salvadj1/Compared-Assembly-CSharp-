using System;
using UnityEngine;

// Token: 0x0200059E RID: 1438
public class MultiLODGroupConfig : ScriptableObject
{
	// Token: 0x04001920 RID: 6432
	public const string LODGroupArray = "a";

	// Token: 0x04001921 RID: 6433
	public const string LODFractionArray = "l";

	// Token: 0x04001922 RID: 6434
	[SerializeField]
	private LODGroup[] a;

	// Token: 0x04001923 RID: 6435
	[SerializeField]
	public float[] l;
}
