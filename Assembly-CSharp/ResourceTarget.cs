using System;
using System.Collections.Generic;
using Facepunch;
using UnityEngine;

// Token: 0x020004B8 RID: 1208
public class ResourceTarget : MonoBehaviour
{
	// Token: 0x04001636 RID: 5686
	[SerializeField]
	public List<ResourceGivePair> resourcesAvailable;

	// Token: 0x04001637 RID: 5687
	public float gatherEfficiencyMultiplier = 1f;

	// Token: 0x04001638 RID: 5688
	private float gatherProgress;

	// Token: 0x04001639 RID: 5689
	public ResourceTarget.ResourceTargetType type;

	// Token: 0x0400163A RID: 5690
	private int startingTotal;

	// Token: 0x0400163B RID: 5691
	[NonSerialized]
	private bool _initialized;

	// Token: 0x020004B9 RID: 1209
	public enum ResourceTargetType
	{
		// Token: 0x0400163D RID: 5693
		Animal,
		// Token: 0x0400163E RID: 5694
		WoodPile,
		// Token: 0x0400163F RID: 5695
		StaticTree,
		// Token: 0x04001640 RID: 5696
		Rock1,
		// Token: 0x04001641 RID: 5697
		Rock2,
		// Token: 0x04001642 RID: 5698
		Rock3,
		// Token: 0x04001643 RID: 5699
		LAST = 5
	}
}
