using System;
using UnityEngine;

// Token: 0x02000669 RID: 1641
public class SecurityCamScreen : MonoBehaviour
{
	// Token: 0x06003905 RID: 14597 RVA: 0x000D19F8 File Offset: 0x000CFBF8
	private void Awake()
	{
		base.Invoke("UpdateCam", this.renderInterval);
	}

	// Token: 0x06003906 RID: 14598 RVA: 0x000D1A0C File Offset: 0x000CFC0C
	private void UpdateCam()
	{
		if (!this.RenderCamera)
		{
			return;
		}
		PlayerClient localPlayer = PlayerClient.GetLocalPlayer();
		Controllable controllable = (!localPlayer) ? null : localPlayer.controllable;
		if (controllable)
		{
			if (this.firstInit)
			{
				this.RenderCamera.Render();
				this.firstInit = false;
			}
			if (Vector3.Distance(controllable.transform.position, base.transform.position) < 15f)
			{
				this.RenderCamera.Render();
			}
		}
		base.Invoke("UpdateCam", this.renderInterval);
	}

	// Token: 0x04001D2B RID: 7467
	public Camera RenderCamera;

	// Token: 0x04001D2C RID: 7468
	public float renderInterval;

	// Token: 0x04001D2D RID: 7469
	private bool firstInit = true;
}
