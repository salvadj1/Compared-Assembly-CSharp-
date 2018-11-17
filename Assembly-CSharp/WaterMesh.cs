using System;
using UnityEngine;

// Token: 0x0200069A RID: 1690
public class WaterMesh : MonoBehaviour
{
	// Token: 0x04001EE6 RID: 7910
	public WaterMesher root;

	// Token: 0x04001EE7 RID: 7911
	public float underFlow;

	// Token: 0x04001EE8 RID: 7912
	public float minDistance = 2f;

	// Token: 0x04001EE9 RID: 7913
	public int sensitivity = 256;

	// Token: 0x04001EEA RID: 7914
	public bool smooth;

	// Token: 0x04001EEB RID: 7915
	public bool reverseOrder;
}
