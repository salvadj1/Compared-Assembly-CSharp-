using System;
using UnityEngine;

// Token: 0x02000442 RID: 1090
public class AdaptiveNearPlane : MonoBehaviour
{
	// Token: 0x040013E2 RID: 5090
	public float maxNear = 0.65f;

	// Token: 0x040013E3 RID: 5091
	public float minNear = 0.22f;

	// Token: 0x040013E4 RID: 5092
	public float threshold = 0.05f;

	// Token: 0x040013E5 RID: 5093
	public LayerMask ignoreLayers = 0;

	// Token: 0x040013E6 RID: 5094
	public LayerMask forceLayers = 0;
}
