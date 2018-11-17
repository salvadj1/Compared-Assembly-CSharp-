using System;
using UnityEngine;

// Token: 0x0200044C RID: 1100
[RequireComponent(typeof(Camera))]
public class CameraFXPost : MonoBehaviour
{
	// Token: 0x0600284C RID: 10316 RVA: 0x0009DF78 File Offset: 0x0009C178
	private void OnPreCull()
	{
		if (CameraFXPost.lastRenderFrame != Time.renderedFrameCount)
		{
			CameraFXPost.lastRenderFrame = Time.renderedFrameCount;
			CameraFXPost.didPostRender = false;
			if (CameraFXPost.cameraFX)
			{
				CameraFXPost.cameraFX.PostPreCull();
				if (CameraFXPost.mountedCamera)
				{
					CameraFXPost.mountedCamera.PreCullEnd(true);
				}
			}
			else if (CameraFXPost.mountedCamera)
			{
				CameraFXPost.mountedCamera.PreCullEnd(false);
			}
			return;
		}
	}

	// Token: 0x0600284D RID: 10317 RVA: 0x0009DFFC File Offset: 0x0009C1FC
	private void OnPostRender()
	{
		if (this.allowPostRenderCalls)
		{
			if (Time.renderedFrameCount != CameraFXPost.lastRenderFrame || CameraFXPost.didPostRender)
			{
				return;
			}
			if (CameraFXPost.cameraFX)
			{
				CameraFXPost.cameraFX.PostPostRender();
			}
			CameraFXPost.didPostRender = true;
		}
	}

	// Token: 0x04001420 RID: 5152
	private static int lastRenderFrame = -100;

	// Token: 0x04001421 RID: 5153
	private static bool didPostRender;

	// Token: 0x04001422 RID: 5154
	public static CameraFX cameraFX;

	// Token: 0x04001423 RID: 5155
	public static MountedCamera mountedCamera;

	// Token: 0x04001424 RID: 5156
	public bool allowPostRenderCalls;
}
