using System;
using UnityEngine;

// Token: 0x02000503 RID: 1283
[RequireComponent(typeof(Camera))]
public class CameraFXPre : MonoBehaviour
{
	// Token: 0x06002BE0 RID: 11232 RVA: 0x000A3FE4 File Offset: 0x000A21E4
	private void OnPreCull()
	{
		if (global::CameraFXPre.lastRenderFrame != Time.renderedFrameCount)
		{
			global::CameraFXPre.lastRenderFrame = Time.renderedFrameCount;
			global::CameraFXPre.didPostRender = false;
			if (global::CameraFXPre.mountedCamera)
			{
				global::CameraFXPre.mountedCamera.PreCullBegin();
			}
			if (global::CameraFXPre.cameraFX)
			{
				global::CameraFXPre.cameraFX.PrePreCull();
			}
			return;
		}
	}

	// Token: 0x06002BE1 RID: 11233 RVA: 0x000A4048 File Offset: 0x000A2248
	private void OnPostRender()
	{
		if (this.allowPostRenderCalls)
		{
			if (Time.renderedFrameCount != global::CameraFXPre.lastRenderFrame || global::CameraFXPre.didPostRender)
			{
				return;
			}
			if (global::CameraFXPre.cameraFX)
			{
				global::CameraFXPre.cameraFX.PrePostRender();
			}
			global::CameraFXPre.didPostRender = true;
		}
	}

	// Token: 0x040015A8 RID: 5544
	private static int lastRenderFrame = -100;

	// Token: 0x040015A9 RID: 5545
	private static bool didPostRender;

	// Token: 0x040015AA RID: 5546
	public static global::CameraFX cameraFX;

	// Token: 0x040015AB RID: 5547
	public static global::MountedCamera mountedCamera;

	// Token: 0x040015AC RID: 5548
	public bool allowPostRenderCalls;
}
