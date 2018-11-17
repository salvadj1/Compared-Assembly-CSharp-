using System;
using UnityEngine;

// Token: 0x0200067A RID: 1658
[RequireComponent(typeof(Camera))]
public class SurveillanceCamera : MonoBehaviour
{
	// Token: 0x060039C2 RID: 14786 RVA: 0x000D5380 File Offset: 0x000D3580
	private void Awake()
	{
		this.camera = base.camera;
		this.camera.enabled = false;
		base.enabled = false;
	}

	// Token: 0x060039C3 RID: 14787 RVA: 0x000D53A4 File Offset: 0x000D35A4
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

	// Token: 0x060039C4 RID: 14788 RVA: 0x000D5448 File Offset: 0x000D3648
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

	// Token: 0x060039C5 RID: 14789 RVA: 0x000D5494 File Offset: 0x000D3694
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

	// Token: 0x04001DA6 RID: 7590
	public const int kWidth = 512;

	// Token: 0x04001DA7 RID: 7591
	public const int kHeight = 512;

	// Token: 0x04001DA8 RID: 7592
	public const int kDepth = 24;

	// Token: 0x04001DA9 RID: 7593
	public const RenderTextureFormat kFormat = 4;

	// Token: 0x04001DAA RID: 7594
	public const float kAspect = 1f;

	// Token: 0x04001DAB RID: 7595
	private const int kRetireFrameCount = 3;

	// Token: 0x04001DAC RID: 7596
	public Camera camera;

	// Token: 0x04001DAD RID: 7597
	private int lastFrameRendered;

	// Token: 0x04001DAE RID: 7598
	private RenderTexture boundTarget;
}
