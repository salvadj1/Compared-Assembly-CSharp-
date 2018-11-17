using System;
using UnityEngine;

// Token: 0x02000832 RID: 2098
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public abstract class TOD_PostEffectsBase : MonoBehaviour
{
	// Token: 0x06004A75 RID: 19061
	protected abstract bool CheckResources();

	// Token: 0x06004A76 RID: 19062 RVA: 0x001431A8 File Offset: 0x001413A8
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

	// Token: 0x06004A77 RID: 19063 RVA: 0x00143278 File Offset: 0x00141478
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

	// Token: 0x06004A78 RID: 19064 RVA: 0x00143300 File Offset: 0x00141500
	protected void OnEnable()
	{
		this.isSupported = true;
	}

	// Token: 0x06004A79 RID: 19065 RVA: 0x0014330C File Offset: 0x0014150C
	protected void Start()
	{
		this.CheckResources();
	}

	// Token: 0x06004A7A RID: 19066 RVA: 0x00143318 File Offset: 0x00141518
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

	// Token: 0x06004A7B RID: 19067 RVA: 0x0014337C File Offset: 0x0014157C
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

	// Token: 0x06004A7C RID: 19068 RVA: 0x001433B4 File Offset: 0x001415B4
	protected void ReportAutoDisable()
	{
		Debug.LogWarning("The image effect " + this.ToString() + " has been disabled as it's not supported on the current platform.");
	}

	// Token: 0x06004A7D RID: 19069 RVA: 0x001433D0 File Offset: 0x001415D0
	protected void NotSupported()
	{
		base.enabled = false;
		this.isSupported = false;
	}

	// Token: 0x06004A7E RID: 19070 RVA: 0x001433E0 File Offset: 0x001415E0
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

	// Token: 0x04002B52 RID: 11090
	protected bool isSupported = true;
}
