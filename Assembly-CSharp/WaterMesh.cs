using System;
using UnityEngine;

// Token: 0x02000760 RID: 1888
public class WaterMesh : MonoBehaviour
{
	// Token: 0x040020E2 RID: 8418
	public global::WaterMesher root;

	// Token: 0x040020E3 RID: 8419
	public float underFlow;

	// Token: 0x040020E4 RID: 8420
	public float minDistance = 2f;

	// Token: 0x040020E5 RID: 8421
	public int sensitivity = 256;

	// Token: 0x040020E6 RID: 8422
	public bool smooth;

	// Token: 0x040020E7 RID: 8423
	public bool reverseOrder;
}
