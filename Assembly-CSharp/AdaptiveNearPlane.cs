using System;
using UnityEngine;

// Token: 0x020004F8 RID: 1272
public class AdaptiveNearPlane : MonoBehaviour
{
	// Token: 0x04001565 RID: 5477
	public float maxNear = 0.65f;

	// Token: 0x04001566 RID: 5478
	public float minNear = 0.22f;

	// Token: 0x04001567 RID: 5479
	public float threshold = 0.05f;

	// Token: 0x04001568 RID: 5480
	public LayerMask ignoreLayers = 0;

	// Token: 0x04001569 RID: 5481
	public LayerMask forceLayers = 0;
}
