using System;
using UnityEngine;

// Token: 0x0200092D RID: 2349
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Time of Day/Camera Sun Shafts")]
[ExecuteInEditMode]
internal class TOD_SunShafts : global::TOD_PostEffectsBase
{
	// Token: 0x06004F6D RID: 20333 RVA: 0x0014FBFC File Offset: 0x0014DDFC
	protected void OnDisable()
	{
		if (this.sunShaftsMaterial)
		{
			Object.DestroyImmediate(this.sunShaftsMaterial);
		}
		if (this.screenClearMaterial)
		{
			Object.DestroyImmediate(this.screenClearMaterial);
		}
	}

	// Token: 0x06004F6E RID: 20334 RVA: 0x0014FC40 File Offset: 0x0014DE40
	protected override bool CheckResources()
	{
		base.CheckSupport(this.UseDepthTexture);
		this.sunShaftsMaterial = base.CheckShaderAndCreateMaterial(this.SunShaftsShader, this.sunShaftsMaterial);
		this.screenClearMaterial = base.CheckShaderAndCreateMaterial(this.ScreenClearShader, this.screenClearMaterial);
		if (!this.isSupported)
		{
			base.ReportAutoDisable();
		}
		return this.isSupported;
	}

	// Token: 0x06004F6F RID: 20335 RVA: 0x0014FCA4 File Offset: 0x0014DEA4
	protected void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.CheckResources() || !this.sky)
		{
			Graphics.Blit(source, destination);
			return;
		}
		this.sky.Components.SunShafts = this;
		if (this.UseDepthTexture)
		{
			base.camera.depthTextureMode |= 1;
		}
		int num;
		int num2;
		if (this.Resolution == TOD_SunShafts.SunShaftsResolution.High)
		{
			num = source.width;
			num2 = source.height;
		}
		else if (this.Resolution == TOD_SunShafts.SunShaftsResolution.Normal)
		{
			num = source.width / 2;
			num2 = source.height / 2;
		}
		else
		{
			num = source.width / 4;
			num2 = source.height / 4;
		}
		Vector3 vector = base.camera.WorldToViewportPoint(this.sky.Components.SunTransform.position);
		this.sunShaftsMaterial.SetVector("_BlurRadius4", new Vector4(1f, 1f, 0f, 0f) * this.SunShaftBlurRadius);
		this.sunShaftsMaterial.SetVector("_SunPosition", new Vector4(vector.x, vector.y, vector.z, this.MaxRadius));
		RenderTexture temporary = RenderTexture.GetTemporary(num, num2, 0);
		RenderTexture temporary2 = RenderTexture.GetTemporary(num, num2, 0);
		if (this.UseDepthTexture)
		{
			Graphics.Blit(source, temporary, this.sunShaftsMaterial, 2);
		}
		else
		{
			Graphics.Blit(source, temporary, this.sunShaftsMaterial, 3);
		}
		base.DrawBorder(temporary, this.screenClearMaterial);
		float num3 = this.SunShaftBlurRadius * 0.00130208337f;
		this.sunShaftsMaterial.SetVector("_BlurRadius4", new Vector4(num3, num3, 0f, 0f));
		this.sunShaftsMaterial.SetVector("_SunPosition", new Vector4(vector.x, vector.y, vector.z, this.MaxRadius));
		for (int i = 0; i < this.RadialBlurIterations; i++)
		{
			Graphics.Blit(temporary, temporary2, this.sunShaftsMaterial, 1);
			num3 = this.SunShaftBlurRadius * (((float)i * 2f + 1f) * 6f) / 768f;
			this.sunShaftsMaterial.SetVector("_BlurRadius4", new Vector4(num3, num3, 0f, 0f));
			Graphics.Blit(temporary2, temporary, this.sunShaftsMaterial, 1);
			num3 = this.SunShaftBlurRadius * (((float)i * 2f + 2f) * 6f) / 768f;
			this.sunShaftsMaterial.SetVector("_BlurRadius4", new Vector4(num3, num3, 0f, 0f));
		}
		Vector4 vector2 = ((double)vector.z < 0.0) ? Vector4.zero : ((1f - this.sky.Atmosphere.Fogginess) * this.SunShaftIntensity * this.sky.SunShaftColor);
		this.sunShaftsMaterial.SetVector("_SunColor", vector2);
		this.sunShaftsMaterial.SetTexture("_ColorBuffer", temporary);
		if (this.BlendMode == TOD_SunShafts.SunShaftsBlendMode.Screen)
		{
			Graphics.Blit(source, destination, this.sunShaftsMaterial, 0);
		}
		else
		{
			Graphics.Blit(source, destination, this.sunShaftsMaterial, 4);
		}
		RenderTexture.ReleaseTemporary(temporary);
		RenderTexture.ReleaseTemporary(temporary2);
	}

	// Token: 0x04002DE4 RID: 11748
	private const int PASS_DEPTH = 2;

	// Token: 0x04002DE5 RID: 11749
	private const int PASS_NODEPTH = 3;

	// Token: 0x04002DE6 RID: 11750
	private const int PASS_RADIAL = 1;

	// Token: 0x04002DE7 RID: 11751
	private const int PASS_SCREEN = 0;

	// Token: 0x04002DE8 RID: 11752
	private const int PASS_ADD = 4;

	// Token: 0x04002DE9 RID: 11753
	public global::TOD_Sky sky;

	// Token: 0x04002DEA RID: 11754
	public TOD_SunShafts.SunShaftsResolution Resolution = TOD_SunShafts.SunShaftsResolution.Normal;

	// Token: 0x04002DEB RID: 11755
	public TOD_SunShafts.SunShaftsBlendMode BlendMode;

	// Token: 0x04002DEC RID: 11756
	public int RadialBlurIterations = 2;

	// Token: 0x04002DED RID: 11757
	public float SunShaftBlurRadius = 2f;

	// Token: 0x04002DEE RID: 11758
	public float SunShaftIntensity = 1f;

	// Token: 0x04002DEF RID: 11759
	public float MaxRadius = 1f;

	// Token: 0x04002DF0 RID: 11760
	public bool UseDepthTexture = true;

	// Token: 0x04002DF1 RID: 11761
	public Shader SunShaftsShader;

	// Token: 0x04002DF2 RID: 11762
	public Shader ScreenClearShader;

	// Token: 0x04002DF3 RID: 11763
	private Material sunShaftsMaterial;

	// Token: 0x04002DF4 RID: 11764
	private Material screenClearMaterial;

	// Token: 0x0200092E RID: 2350
	public enum SunShaftsResolution
	{
		// Token: 0x04002DF6 RID: 11766
		Low,
		// Token: 0x04002DF7 RID: 11767
		Normal,
		// Token: 0x04002DF8 RID: 11768
		High
	}

	// Token: 0x0200092F RID: 2351
	public enum SunShaftsBlendMode
	{
		// Token: 0x04002DFA RID: 11770
		Screen,
		// Token: 0x04002DFB RID: 11771
		Add
	}
}
