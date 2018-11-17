using System;
using UnityEngine;

// Token: 0x02000032 RID: 50
[ExecuteInEditMode]
public class FPGrassDisplacementCamera : MonoBehaviour
{
	// Token: 0x17000059 RID: 89
	// (get) Token: 0x060001FB RID: 507 RVA: 0x0000B9A8 File Offset: 0x00009BA8
	public static FPGrassDisplacementCamera singleton
	{
		get
		{
			return FPGrassDisplacementCamera.Global.singleton;
		}
	}

	// Token: 0x060001FC RID: 508 RVA: 0x0000B9B0 File Offset: 0x00009BB0
	public void Awake()
	{
	}

	// Token: 0x060001FD RID: 509 RVA: 0x0000B9B4 File Offset: 0x00009BB4
	public static FPGrassDisplacementCamera Get()
	{
		return FPGrassDisplacementCamera.singleton;
	}

	// Token: 0x060001FE RID: 510 RVA: 0x0000B9BC File Offset: 0x00009BBC
	public static RenderTexture GetRT()
	{
		return FPGrassDisplacementCamera.singleton.camera.targetTexture;
	}

	// Token: 0x060001FF RID: 511 RVA: 0x0000B9D0 File Offset: 0x00009BD0
	public static Material GetBlitMat()
	{
		return FPGrassDisplacementCamera.singleton.blitMat;
	}

	// Token: 0x06000200 RID: 512 RVA: 0x0000B9DC File Offset: 0x00009BDC
	public void OnDestroy()
	{
		Object.DestroyImmediate(base.camera.targetTexture);
		Object.DestroyImmediate(this.blitMat);
	}

	// Token: 0x0400013B RID: 315
	[NonSerialized]
	public Material blitMat;

	// Token: 0x02000033 RID: 51
	private static class Global
	{
		// Token: 0x06000201 RID: 513 RVA: 0x0000B9FC File Offset: 0x00009BFC
		static Global()
		{
			GameObject gameObject = GameObject.FindWithTag("DisplacementCamera");
			if (gameObject)
			{
				FPGrassDisplacementCamera.Global.singleton = gameObject.GetComponent<FPGrassDisplacementCamera>();
				if (FPGrassDisplacementCamera.Global.singleton)
				{
					Object.DestroyImmediate(gameObject);
				}
				FPGrassDisplacementCamera.Global.singleton = null;
			}
			GameObject gameObject2 = new GameObject("FPGrassDisplacementCamera")
			{
				hideFlags = 4
			};
			gameObject2.AddComponent<Camera>();
			gameObject2.transform.rotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
			gameObject2.camera.backgroundColor = new Color(0f, 0f, 0f, 0f);
			gameObject2.camera.clearFlags = 2;
			gameObject2.camera.orthographic = true;
			gameObject2.camera.orthographicSize = 50f;
			gameObject2.camera.nearClipPlane = 0.3f;
			gameObject2.camera.farClipPlane = 1000f;
			gameObject2.camera.renderingPath = 0;
			gameObject2.camera.enabled = false;
			gameObject2.camera.cullingMask = 1 << LayerMask.NameToLayer("GrassDisplacement");
			gameObject2.camera.tag = "DisplacementCamera";
			FPGrassDisplacementCamera.Global.singleton = gameObject2.AddComponent<FPGrassDisplacementCamera>();
			RenderTexture renderTexture = new RenderTexture(512, 512, 0, FPGrass.Support.ProbabilityRenderTextureFormat1Channel)
			{
				hideFlags = 4
			};
			renderTexture.Create();
			renderTexture.name = "FPGrassDisplacement_RT";
			gameObject2.camera.targetTexture = renderTexture;
			FPGrassDisplacementCamera.Global.singleton.blitMat = new Material(Shader.Find("Custom/DisplacementBlit"))
			{
				hideFlags = 4
			};
		}

		// Token: 0x0400013C RID: 316
		public static FPGrassDisplacementCamera singleton;
	}
}
