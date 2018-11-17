using System;
using UnityEngine;

// Token: 0x020004E3 RID: 1251
public class MultiLODGroupConfig : ScriptableObject
{
	// Token: 0x04001763 RID: 5987
	public const string LODGroupArray = "a";

	// Token: 0x04001764 RID: 5988
	public const string LODFractionArray = "l";

	// Token: 0x04001765 RID: 5989
	[SerializeField]
	private LODGroup[] a;

	// Token: 0x04001766 RID: 5990
	[SerializeField]
	public float[] l;
}
