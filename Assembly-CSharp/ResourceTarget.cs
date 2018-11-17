using System;
using System.Collections.Generic;
using Facepunch;
using UnityEngine;

// Token: 0x02000573 RID: 1395
public class ResourceTarget : MonoBehaviour
{
	// Token: 0x040017F3 RID: 6131
	[SerializeField]
	public List<global::ResourceGivePair> resourcesAvailable;

	// Token: 0x040017F4 RID: 6132
	public float gatherEfficiencyMultiplier = 1f;

	// Token: 0x040017F5 RID: 6133
	private float gatherProgress;

	// Token: 0x040017F6 RID: 6134
	public global::ResourceTarget.ResourceTargetType type;

	// Token: 0x040017F7 RID: 6135
	private int startingTotal;

	// Token: 0x040017F8 RID: 6136
	[NonSerialized]
	private bool _initialized;

	// Token: 0x02000574 RID: 1396
	public enum ResourceTargetType
	{
		// Token: 0x040017FA RID: 6138
		Animal,
		// Token: 0x040017FB RID: 6139
		WoodPile,
		// Token: 0x040017FC RID: 6140
		StaticTree,
		// Token: 0x040017FD RID: 6141
		Rock1,
		// Token: 0x040017FE RID: 6142
		Rock2,
		// Token: 0x040017FF RID: 6143
		Rock3,
		// Token: 0x04001800 RID: 6144
		LAST = 5
	}
}
