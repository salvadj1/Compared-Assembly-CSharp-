using System;
using UnityEngine;

// Token: 0x0200073E RID: 1854
[RequireComponent(typeof(Camera))]
public class SurveillanceCamera : MonoBehaviour
{
	// Token: 0x06003DB6 RID: 15798 RVA: 0x000DDD60 File Offset: 0x000DBF60
	private void Awake()
	{
		this.camera = base.camera;
		this.camera.enabled = false;
		base.enabled = false;
	}

	// Token: 0x06003DB7 RID: 15799 RVA: 0x000DDD84 File Offset: 0x000DBF84
	public RenderTexture Render()
	{
		int frameCount = Time.frameCount;
		if (this.lastFrameRendered == frameCount)
		{
			return this.boundTarget;
		}
		bool flag = this.lastFrameRendered != frameCount - 1;
		this.lastFrameRendered = Time.frameCount;
		if (flag && !this.boundTarget)
		{
			this.boundTarget = RenderTexture.GetTemporary(512, 512, 24, 4);
			base.enabled = true;
			this.camera.targetTexture = this.boundTarget;
			this.camera.ResetAspect();
		}
		this.camera.Render();
		return this.boundTarget;
	}

	// Token: 0x06003DB8 RID: 15800 RVA: 0x000DDE28 File Offset: 0x000DC028
	private void OnDestroy()
	{
		if (this.boundTarget)
		{
			if (this.camera)
			{
				this.camera.targetTexture = null;
			}
			RenderTexture.ReleaseTemporary(this.boundTarget);
			this.boundTarget = null;
		}
	}

	// Token: 0x06003DB9 RID: 15801 RVA: 0x000DDE74 File Offset: 0x000DC074
	private void LateUpdate()
	{
		int num = Mathf.Abs(this.lastFrameRendered - Time.frameCount);
		if (num > 3)
		{
			this.camera.targetTexture = null;
			RenderTexture.ReleaseTemporary(this.boundTarget);
			this.boundTarget = null;
			base.enabled = false;
		}
	}

	// Token: 0x04001F9E RID: 8094
	public const int kWidth = 512;

	// Token: 0x04001F9F RID: 8095
	public const int kHeight = 512;

	// Token: 0x04001FA0 RID: 8096
	public const int kDepth = 24;

	// Token: 0x04001FA1 RID: 8097
	public const RenderTextureFormat kFormat = 4;

	// Token: 0x04001FA2 RID: 8098
	public const float kAspect = 1f;

	// Token: 0x04001FA3 RID: 8099
	private const int kRetireFrameCount = 3;

	// Token: 0x04001FA4 RID: 8100
	public Camera camera;

	// Token: 0x04001FA5 RID: 8101
	private int lastFrameRendered;

	// Token: 0x04001FA6 RID: 8102
	private RenderTexture boundTarget;
}
