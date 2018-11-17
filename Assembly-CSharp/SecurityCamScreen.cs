using System;
using UnityEngine;

// Token: 0x0200072D RID: 1837
public class SecurityCamScreen : MonoBehaviour
{
	// Token: 0x06003CF9 RID: 15609 RVA: 0x000DA3D8 File Offset: 0x000D85D8
	private void Awake()
	{
		base.Invoke("UpdateCam", this.renderInterval);
	}

	// Token: 0x06003CFA RID: 15610 RVA: 0x000DA3EC File Offset: 0x000D85EC
	private void UpdateCam()
	{
		if (!this.RenderCamera)
		{
			return;
		}
		global::PlayerClient localPlayer = global::PlayerClient.GetLocalPlayer();
		global::Controllable controllable = (!localPlayer) ? null : localPlayer.controllable;
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

	// Token: 0x04001F23 RID: 7971
	public Camera RenderCamera;

	// Token: 0x04001F24 RID: 7972
	public float renderInterval;

	// Token: 0x04001F25 RID: 7973
	private bool firstInit = true;
}
