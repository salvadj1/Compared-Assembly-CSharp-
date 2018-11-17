using System;
using UnityEngine;

// Token: 0x02000502 RID: 1282
[RequireComponent(typeof(Camera))]
public class CameraFXPost : MonoBehaviour
{
	// Token: 0x06002BDC RID: 11228 RVA: 0x000A3EF8 File Offset: 0x000A20F8
	private void OnPreCull()
	{
		if (global::CameraFXPost.lastRenderFrame != Time.renderedFrameCount)
		{
			global::CameraFXPost.lastRenderFrame = Time.renderedFrameCount;
			global::CameraFXPost.didPostRender = false;
			if (global::CameraFXPost.cameraFX)
			{
				global::CameraFXPost.cameraFX.PostPreCull();
				if (global::CameraFXPost.mountedCamera)
				{
					global::CameraFXPost.mountedCamera.PreCullEnd(true);
				}
			}
			else if (global::CameraFXPost.mountedCamera)
			{
				global::CameraFXPost.mountedCamera.PreCullEnd(false);
			}
			return;
		}
	}

	// Token: 0x06002BDD RID: 11229 RVA: 0x000A3F7C File Offset: 0x000A217C
	private void OnPostRender()
	{
		if (this.allowPostRenderCalls)
		{
			if (Time.renderedFrameCount != global::CameraFXPost.lastRenderFrame || global::CameraFXPost.didPostRender)
			{
				return;
			}
			if (global::CameraFXPost.cameraFX)
			{
				global::CameraFXPost.cameraFX.PostPostRender();
			}
			global::CameraFXPost.didPostRender = true;
		}
	}

	// Token: 0x040015A3 RID: 5539
	private static int lastRenderFrame = -100;

	// Token: 0x040015A4 RID: 5540
	private static bool didPostRender;

	// Token: 0x040015A5 RID: 5541
	public static global::CameraFX cameraFX;

	// Token: 0x040015A6 RID: 5542
	public static global::MountedCamera mountedCamera;

	// Token: 0x040015A7 RID: 5543
	public bool allowPostRenderCalls;
}
