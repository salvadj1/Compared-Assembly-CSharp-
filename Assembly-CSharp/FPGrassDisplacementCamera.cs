using System;
using UnityEngine;

// Token: 0x02000044 RID: 68
[ExecuteInEditMode]
public class FPGrassDisplacementCamera : MonoBehaviour
{
	// Token: 0x1700006F RID: 111
	// (get) Token: 0x0600026D RID: 621 RVA: 0x0000CF50 File Offset: 0x0000B150
	public static global::FPGrassDisplacementCamera singleton
	{
		get
		{
			return global::FPGrassDisplacementCamera.Global.singleton;
		}
	}

	// Token: 0x0600026E RID: 622 RVA: 0x0000CF58 File Offset: 0x0000B158
	public void Awake()
	{
	}

	// Token: 0x0600026F RID: 623 RVA: 0x0000CF5C File Offset: 0x0000B15C
	public static global::FPGrassDisplacementCamera Get()
	{
		return global::FPGrassDisplacementCamera.singleton;
	}

	// Token: 0x06000270 RID: 624 RVA: 0x0000CF64 File Offset: 0x0000B164
	public static RenderTexture GetRT()
	{
		return global::FPGrassDisplacementCamera.singleton.camera.targetTexture;
	}

	// Token: 0x06000271 RID: 625 RVA: 0x0000CF78 File Offset: 0x0000B178
	public static Material GetBlitMat()
	{
		return global::FPGrassDisplacementCamera.singleton.blitMat;
	}

	// Token: 0x06000272 RID: 626 RVA: 0x0000CF84 File Offset: 0x0000B184
	public void OnDestroy()
	{
		Object.DestroyImmediate(base.camera.targetTexture);
		Object.DestroyImmediate(this.blitMat);
	}

	// Token: 0x0400019D RID: 413
	[NonSerialized]
	public Material blitMat;

	// Token: 0x02000045 RID: 69
	private static class Global
	{
		// Token: 0x06000273 RID: 627 RVA: 0x0000CFA4 File Offset: 0x0000B1A4
		static Global()
		{
			GameObject gameObject = GameObject.FindWithTag("DisplacementCamera");
			if (gameObject)
			{
				global::FPGrassDisplacementCamera.Global.singleton = gameObject.GetComponent<global::FPGrassDisplacementCamera>();
				if (global::FPGrassDisplacementCamera.Global.singleton)
				{
					Object.DestroyImmediate(gameObject);
				}
				global::FPGrassDisplacementCamera.Global.singleton = null;
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
			global::FPGrassDisplacementCamera.Global.singleton = gameObject2.AddComponent<global::FPGrassDisplacementCamera>();
			RenderTexture renderTexture = new RenderTexture(512, 512, 0, global::FPGrass.Support.ProbabilityRenderTextureFormat1Channel)
			{
				hideFlags = 4
			};
			renderTexture.Create();
			renderTexture.name = "FPGrassDisplacement_RT";
			gameObject2.camera.targetTexture = renderTexture;
			global::FPGrassDisplacementCamera.Global.singleton.blitMat = new Material(Shader.Find("Custom/DisplacementBlit"))
			{
				hideFlags = 4
			};
		}

		// Token: 0x0400019E RID: 414
		public static global::FPGrassDisplacementCamera singleton;
	}
}
