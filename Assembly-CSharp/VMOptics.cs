using System;
using UnityEngine;

// Token: 0x020004AB RID: 1195
public class VMOptics : MonoBehaviour
{
	// Token: 0x06002A07 RID: 10759 RVA: 0x000A4928 File Offset: 0x000A2B28
	private void OnDrawGizmosSelected()
	{
		this.sightOverride.DrawGizmos("sights");
	}

	// Token: 0x040015F8 RID: 5624
	public Socket.CameraSpace sightOverride;
}
