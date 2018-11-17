using System;
using UnityEngine;

// Token: 0x02000566 RID: 1382
public class VMOptics : MonoBehaviour
{
	// Token: 0x06002DB9 RID: 11705 RVA: 0x000AC6C0 File Offset: 0x000AA8C0
	private void OnDrawGizmosSelected()
	{
		this.sightOverride.DrawGizmos("sights");
	}

	// Token: 0x040017B5 RID: 6069
	public global::Socket.CameraSpace sightOverride;
}
