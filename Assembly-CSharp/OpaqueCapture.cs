using System;
using UnityEngine;

// Token: 0x020004FD RID: 1277
public class OpaqueCapture : PostEffectsBase
{
	// Token: 0x06002B41 RID: 11073 RVA: 0x000AD91C File Offset: 0x000ABB1C
	protected void OnDisable()
	{
		this.CleanupCaptureRT();
	}

	// Token: 0x06002B42 RID: 11074 RVA: 0x000AD924 File Offset: 0x000ABB24
	public override bool CheckResources()
	{
		this.CheckSupport(false);
		if (!this.isSupported)
		{
			this.ReportAutoDisable();
		}
		return this.isSupported;
	}

	// Token: 0x06002B43 RID: 11075 RVA: 0x000AD948 File Offset: 0x000ABB48
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

	// Token: 0x06002B44 RID: 11076 RVA: 0x000AD988 File Offset: 0x000ABB88
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

	// Token: 0x040017BE RID: 6078
	private RenderTexture captureRT;

	// Token: 0x040017BF RID: 6079
	private int w = -1;

	// Token: 0x040017C0 RID: 6080
	private int h = -1;

	// Token: 0x040017C1 RID: 6081
	private int d = -1;

	// Token: 0x040017C2 RID: 6082
	private RenderTextureFormat fmt;
}
