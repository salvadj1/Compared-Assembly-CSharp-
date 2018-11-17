using System;
using UnityEngine;

// Token: 0x02000838 RID: 2104
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
[AddComponentMenu("Time of Day/Camera Sun Shafts")]
internal class TOD_SunShafts : TOD_PostEffectsBase
{
	// Token: 0x06004AB2 RID: 19122 RVA: 0x00145C98 File Offset: 0x00143E98
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

	// Token: 0x06004AB3 RID: 19123 RVA: 0x00145CDC File Offset: 0x00143EDC
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

	// Token: 0x06004AB4 RID: 19124 RVA: 0x00145D40 File Offset: 0x00143F40
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

	// Token: 0x04002B96 RID: 11158
	private const int PASS_DEPTH = 2;

	// Token: 0x04002B97 RID: 11159
	private const int PASS_NODEPTH = 3;

	// Token: 0x04002B98 RID: 11160
	private const int PASS_RADIAL = 1;

	// Token: 0x04002B99 RID: 11161
	private const int PASS_SCREEN = 0;

	// Token: 0x04002B9A RID: 11162
	private const int PASS_ADD = 4;

	// Token: 0x04002B9B RID: 11163
	public TOD_Sky sky;

	// Token: 0x04002B9C RID: 11164
	public TOD_SunShafts.SunShaftsResolution Resolution = TOD_SunShafts.SunShaftsResolution.Normal;

	// Token: 0x04002B9D RID: 11165
	public TOD_SunShafts.SunShaftsBlendMode BlendMode;

	// Token: 0x04002B9E RID: 11166
	public int RadialBlurIterations = 2;

	// Token: 0x04002B9F RID: 11167
	public float SunShaftBlurRadius = 2f;

	// Token: 0x04002BA0 RID: 11168
	public float SunShaftIntensity = 1f;

	// Token: 0x04002BA1 RID: 11169
	public float MaxRadius = 1f;

	// Token: 0x04002BA2 RID: 11170
	public bool UseDepthTexture = true;

	// Token: 0x04002BA3 RID: 11171
	public Shader SunShaftsShader;

	// Token: 0x04002BA4 RID: 11172
	public Shader ScreenClearShader;

	// Token: 0x04002BA5 RID: 11173
	private Material sunShaftsMaterial;

	// Token: 0x04002BA6 RID: 11174
	private Material screenClearMaterial;

	// Token: 0x02000839 RID: 2105
	public enum SunShaftsResolution
	{
		// Token: 0x04002BA8 RID: 11176
		Low,
		// Token: 0x04002BA9 RID: 11177
		Normal,
		// Token: 0x04002BAA RID: 11178
		High
	}

	// Token: 0x0200083A RID: 2106
	public enum SunShaftsBlendMode
	{
		// Token: 0x04002BAC RID: 11180
		Screen,
		// Token: 0x04002BAD RID: 11181
		Add
	}
}
