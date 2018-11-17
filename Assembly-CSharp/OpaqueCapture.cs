using System;
using UnityEngine;

// Token: 0x020005BA RID: 1466
public class OpaqueCapture : PostEffectsBase
{
	// Token: 0x06002F01 RID: 12033 RVA: 0x000B59B8 File Offset: 0x000B3BB8
	protected void OnDisable()
	{
		this.CleanupCaptureRT();
	}

	// Token: 0x06002F02 RID: 12034 RVA: 0x000B59C0 File Offset: 0x000B3BC0
	public override bool CheckResources()
	{
		this.CheckSupport(false);
		if (!this.isSupported)
		{
			this.ReportAutoDisable();
		}
		return this.isSupported;
	}

	// Token: 0x06002F03 RID: 12035 RVA: 0x000B59E4 File Offset: 0x000B3BE4
	private void CleanupCaptureRT()
	{
		if (this.captureRT)
		{
			Object.DestroyImmediate(this.captureRT);
		}
		this.w = -1;
		this.h = -1;
		this.d = -1;
	}

	// Token: 0x06002F04 RID: 12036 RVA: 0x000B5A24 File Offset: 0x000B3C24
	[ImageEffectOpaque]
	protected void OnRenderImage(RenderTexture src, RenderTexture dst)
	{
		if (!this.CheckResources())
		{
			Graphics.Blit(src, dst);
			return;
		}
		int width = src.width;
		int height = src.height;
		int depth = src.depth;
		RenderTextureFormat format = src.format;
		if (width != this.w || height != this.h || depth != this.d || format != this.fmt)
		{
			this.CleanupCaptureRT();
			this.captureRT = new RenderTexture(width, height, depth, format)
			{
				hideFlags = 4
			};
			if (!this.captureRT.Create() && !this.captureRT.IsCreated())
			{
				Graphics.Blit(src, dst);
				return;
			}
			this.captureRT.SetGlobalShaderProperty("_OpaqueFrame");
			this.w = width;
			this.h = height;
			this.d = depth;
			this.fmt = format;
		}
		Graphics.Blit(src, this.captureRT);
		Graphics.Blit(src, dst);
	}

	// Token: 0x0400198A RID: 6538
	private RenderTexture captureRT;

	// Token: 0x0400198B RID: 6539
	private int w = -1;

	// Token: 0x0400198C RID: 6540
	private int h = -1;

	// Token: 0x0400198D RID: 6541
	private int d = -1;

	// Token: 0x0400198E RID: 6542
	private RenderTextureFormat fmt;
}
