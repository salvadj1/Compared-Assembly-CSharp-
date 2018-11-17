using System;
using UnityEngine;

// Token: 0x02000927 RID: 2343
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public abstract class TOD_PostEffectsBase : MonoBehaviour
{
	// Token: 0x06004F30 RID: 20272
	protected abstract bool CheckResources();

	// Token: 0x06004F31 RID: 20273 RVA: 0x0014D10C File Offset: 0x0014B30C
	protected Material CheckShaderAndCreateMaterial(Shader shader, Material material)
	{
		if (!shader)
		{
			Debug.Log("Missing shader in " + this.ToString());
			base.enabled = false;
			return null;
		}
		if (shader.isSupported && material && material.shader == shader)
		{
			return material;
		}
		if (!shader.isSupported)
		{
			this.NotSupported();
			Debug.LogError(string.Concat(new string[]
			{
				"The shader ",
				shader.ToString(),
				" on effect ",
				this.ToString(),
				" is not supported on this platform!"
			}));
			return null;
		}
		material = new Material(shader);
		material.hideFlags = 4;
		return (!material) ? null : material;
	}

	// Token: 0x06004F32 RID: 20274 RVA: 0x0014D1DC File Offset: 0x0014B3DC
	protected Material CreateMaterial(Shader shader, Material material)
	{
		if (!shader)
		{
			Debug.Log("Missing shader in " + this.ToString());
			return null;
		}
		if (material && material.shader == shader && shader.isSupported)
		{
			return material;
		}
		if (!shader.isSupported)
		{
			return null;
		}
		material = new Material(shader);
		material.hideFlags = 4;
		return (!material) ? null : material;
	}

	// Token: 0x06004F33 RID: 20275 RVA: 0x0014D264 File Offset: 0x0014B464
	protected void OnEnable()
	{
		this.isSupported = true;
	}

	// Token: 0x06004F34 RID: 20276 RVA: 0x0014D270 File Offset: 0x0014B470
	protected void Start()
	{
		this.CheckResources();
	}

	// Token: 0x06004F35 RID: 20277 RVA: 0x0014D27C File Offset: 0x0014B47C
	protected bool CheckSupport(bool needDepth)
	{
		this.isSupported = true;
		if (!SystemInfo.supportsImageEffects || !SystemInfo.supportsRenderTextures)
		{
			this.NotSupported();
			return false;
		}
		if (needDepth && !SystemInfo.SupportsRenderTextureFormat(1))
		{
			this.NotSupported();
			return false;
		}
		if (needDepth)
		{
			base.camera.depthTextureMode |= 1;
		}
		return true;
	}

	// Token: 0x06004F36 RID: 20278 RVA: 0x0014D2E0 File Offset: 0x0014B4E0
	protected bool CheckSupport(bool needDepth, bool needHdr)
	{
		if (!this.CheckSupport(needDepth))
		{
			return false;
		}
		if (needHdr && !SystemInfo.SupportsRenderTextureFormat(2))
		{
			this.NotSupported();
			return false;
		}
		return true;
	}

	// Token: 0x06004F37 RID: 20279 RVA: 0x0014D318 File Offset: 0x0014B518
	protected void ReportAutoDisable()
	{
		Debug.LogWarning("The image effect " + this.ToString() + " has been disabled as it's not supported on the current platform.");
	}

	// Token: 0x06004F38 RID: 20280 RVA: 0x0014D334 File Offset: 0x0014B534
	protected void NotSupported()
	{
		base.enabled = false;
		this.isSupported = false;
	}

	// Token: 0x06004F39 RID: 20281 RVA: 0x0014D344 File Offset: 0x0014B544
	protected void DrawBorder(RenderTexture dest, Material material)
	{
		RenderTexture.active = dest;
		bool flag = true;
		GL.PushMatrix();
		GL.LoadOrtho();
		for (int i = 0; i < material.passCount; i++)
		{
			material.SetPass(i);
			float num;
			float num2;
			if (flag)
			{
				num = 1f;
				num2 = 0f;
			}
			else
			{
				num = 0f;
				num2 = 1f;
			}
			float num3 = 0f;
			float num4 = 0f + 1f / ((float)dest.width * 1f);
			float num5 = 0f;
			float num6 = 1f;
			GL.Begin(7);
			GL.TexCoord2(0f, num);
			GL.Vertex3(num3, num5, 0.1f);
			GL.TexCoord2(1f, num);
			GL.Vertex3(num4, num5, 0.1f);
			GL.TexCoord2(1f, num2);
			GL.Vertex3(num4, num6, 0.1f);
			GL.TexCoord2(0f, num2);
			GL.Vertex3(num3, num6, 0.1f);
			num3 = 1f - 1f / ((float)dest.width * 1f);
			num4 = 1f;
			num5 = 0f;
			num6 = 1f;
			GL.TexCoord2(0f, num);
			GL.Vertex3(num3, num5, 0.1f);
			GL.TexCoord2(1f, num);
			GL.Vertex3(num4, num5, 0.1f);
			GL.TexCoord2(1f, num2);
			GL.Vertex3(num4, num6, 0.1f);
			GL.TexCoord2(0f, num2);
			GL.Vertex3(num3, num6, 0.1f);
			num3 = 0f;
			num4 = 1f;
			num5 = 0f;
			num6 = 0f + 1f / ((float)dest.height * 1f);
			GL.TexCoord2(0f, num);
			GL.Vertex3(num3, num5, 0.1f);
			GL.TexCoord2(1f, num);
			GL.Vertex3(num4, num5, 0.1f);
			GL.TexCoord2(1f, num2);
			GL.Vertex3(num4, num6, 0.1f);
			GL.TexCoord2(0f, num2);
			GL.Vertex3(num3, num6, 0.1f);
			num3 = 0f;
			num4 = 1f;
			num5 = 1f - 1f / ((float)dest.height * 1f);
			num6 = 1f;
			GL.TexCoord2(0f, num);
			GL.Vertex3(num3, num5, 0.1f);
			GL.TexCoord2(1f, num);
			GL.Vertex3(num4, num5, 0.1f);
			GL.TexCoord2(1f, num2);
			GL.Vertex3(num4, num6, 0.1f);
			GL.TexCoord2(0f, num2);
			GL.Vertex3(num3, num6, 0.1f);
			GL.End();
		}
		GL.PopMatrix();
	}

	// Token: 0x04002DA0 RID: 11680
	protected bool isSupported = true;
}
