using System;
using UnityEngine;

// Token: 0x0200044D RID: 1101
[RequireComponent(typeof(Camera))]
public class CameraFXPre : MonoBehaviour
{
	// Token: 0x06002850 RID: 10320 RVA: 0x0009E064 File Offset: 0x0009C264
	private void OnPreCull()
	{
		if (CameraFXPre.lastRenderFrame != Time.renderedFrameCount)
		{
			CameraFXPre.lastRenderFrame = Time.renderedFrameCount;
			CameraFXPre.didPostRender = false;
			if (CameraFXPre.mountedCamera)
			{
				CameraFXPre.mountedCamera.PreCullBegin();
			}
			if (CameraFXPre.cameraFX)
			{
				CameraFXPre.cameraFX.PrePreCull();
			}
			return;
		}
	}

	// Token: 0x06002851 RID: 10321 RVA: 0x0009E0C8 File Offset: 0x0009C2C8
	private void OnPostRender()
	{
		if (this.allowPostRenderCalls)
		{
			if (Time.renderedFrameCount != CameraFXPre.lastRenderFrame || CameraFXPre.didPostRender)
			{
				return;
			}
			if (CameraFXPre.cameraFX)
			{
				CameraFXPre.cameraFX.PrePostRender();
			}
			CameraFXPre.didPostRender = true;
		}
	}

	// Token: 0x04001425 RID: 5157
	private static int lastRenderFrame = -100;

	// Token: 0x04001426 RID: 5158
	private static bool didPostRender;

	// Token: 0x04001427 RID: 5159
	public static CameraFX cameraFX;

	// Token: 0x04001428 RID: 5160
	public static MountedCamera mountedCamera;

	// Token: 0x04001429 RID: 5161
	public bool allowPostRenderCalls;
}
