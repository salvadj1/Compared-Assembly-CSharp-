using System;
using Facepunch;
using UnityEngine;

// Token: 0x02000057 RID: 87
[ExecuteInEditMode]
public class FPGrassProbabilityGenerator : ScriptableObject, global::IFPGrassAsset
{
	// Token: 0x060002CA RID: 714 RVA: 0x0000EAA4 File Offset: 0x0000CCA4
	private void OnEnable()
	{
		this.Initialize();
	}

	// Token: 0x060002CB RID: 715 RVA: 0x0000EAAC File Offset: 0x0000CCAC
	private void OnDisable()
	{
		this.DestroyProbabilityTexture();
	}

	// Token: 0x060002CC RID: 716 RVA: 0x0000EAB4 File Offset: 0x0000CCB4
	private void OnDestroy()
	{
		this.DestroyProbabilityTexture();
	}

	// Token: 0x060002CD RID: 717 RVA: 0x0000EABC File Offset: 0x0000CCBC
	private void DestroyProbabilityTexture()
	{
		if (this.probabilityTexture)
		{
			Object.DestroyImmediate(this.probabilityTexture, true);
			this.probabilityTexture = null;
		}
	}

	// Token: 0x060002CE RID: 718 RVA: 0x0000EAE4 File Offset: 0x0000CCE4
	public void Initialize()
	{
		if (!this.probabilityTexture && this.gridSize > 0)
		{
			this.CreateRenderTexture();
		}
		if (!this.material)
		{
			this.material = (Material)Object.Instantiate(Facepunch.Bundling.Load("rust/fpgrass/RenderSplatMaterial", typeof(Material)));
			this.material.SetTexture("_Noise", (Texture2D)Facepunch.Bundling.Load("rust/fpgrass/noise", typeof(Texture2D)));
		}
	}

	// Token: 0x060002CF RID: 719 RVA: 0x0000EB70 File Offset: 0x0000CD70
	public void SetSplatTexture(Texture2D texture)
	{
		this.material.SetTexture("_Splat1", texture);
	}

	// Token: 0x060002D0 RID: 720 RVA: 0x0000EB84 File Offset: 0x0000CD84
	public void SetDetailProbabilities(Texture2D texture)
	{
		this.material.SetTexture("_DetailProbabilities", texture);
	}

	// Token: 0x060002D1 RID: 721 RVA: 0x0000EB98 File Offset: 0x0000CD98
	public void SetGridScale(float newScale)
	{
		this.gridScale = newScale;
		this.material.SetFloat("_GridScale", this.gridScale);
	}

	// Token: 0x060002D2 RID: 722 RVA: 0x0000EBB8 File Offset: 0x0000CDB8
	public void SetGridSize(int newSize)
	{
		this.gridSize = newSize;
		this.material.SetFloat("_GridSize", (float)this.gridSize);
		this.CreateRenderTexture();
	}

	// Token: 0x060002D3 RID: 723 RVA: 0x0000EBEC File Offset: 0x0000CDEC
	private void CreateRenderTexture()
	{
		this.DestroyProbabilityTexture();
		int num = Mathf.NextPowerOfTwo(this.gridSize);
		this.probabilityTexture = new RenderTexture(num, num, 0, global::FPGrass.Support.ProbabilityRenderTextureFormat4Channel)
		{
			hideFlags = 4
		};
		this.probabilityTexture.filterMode = 0;
		this.probabilityTexture.useMipMap = false;
		this.probabilityTexture.anisoLevel = 0;
		this.probabilityTexture.Create();
	}

	// Token: 0x060002D4 RID: 724 RVA: 0x0000EC58 File Offset: 0x0000CE58
	public void UpdateMap(Vector3 newPosition)
	{
		if (!this.material)
		{
			Debug.Log("No Material to render splat!");
			return;
		}
		if (!this.probabilityTexture)
		{
			this.CreateRenderTexture();
		}
		float num = 1f - (float)this.gridSize / (float)this.probabilityTexture.height * 2f;
		float num2 = -1f;
		float num3 = num2 + (float)this.gridSize / (float)this.probabilityTexture.width * 2f;
		float num4 = 1f;
		if (SystemInfo.graphicsDeviceVersion.StartsWith("OpenGL", StringComparison.InvariantCultureIgnoreCase))
		{
			num4 = -1f;
			num = -1f + (float)this.gridSize / (float)this.probabilityTexture.height * 2f;
		}
		float num5 = newPosition.z - (float)Mathf.FloorToInt((float)this.gridSize * 0.5f * this.gridScale);
		float num6 = newPosition.x - (float)Mathf.FloorToInt((float)this.gridSize * 0.5f * this.gridScale);
		float num7 = num5 + (float)this.gridSize * this.gridScale;
		float num8 = num6 + (float)this.gridSize * this.gridScale;
		this.material.SetFloat("_TerrainSize", Terrain.activeTerrain.terrainData.size.x);
		this.material.SetVector("_Position", new Vector4(newPosition.x, newPosition.y, newPosition.z, 1f));
		int pass = (global::FPGrass.Support.DetailProbabilityFilterMode != null) ? 1 : 0;
		RenderTexture active = RenderTexture.active;
		try
		{
			GL.PushMatrix();
			RenderTexture.active = this.probabilityTexture;
			GL.LoadPixelMatrix(0f, (float)this.probabilityTexture.width, 0f, (float)this.probabilityTexture.height);
			this.material.SetPass(pass);
			GL.Begin(5);
			GL.TexCoord(new Vector3(num8, num5, 0f));
			GL.Vertex3(num3, num4, 0f);
			GL.TexCoord(new Vector3(num6, num5, 0f));
			GL.Vertex3(num2, num4, 0f);
			GL.TexCoord(new Vector3(num8, num7, 0f));
			GL.Vertex3(num3, num, 0f);
			GL.TexCoord(new Vector3(num6, num7, 0f));
			GL.Vertex3(num2, num, 0f);
			GL.End();
			GL.PopMatrix();
		}
		finally
		{
			RenderTexture.active = active;
		}
	}

	// Token: 0x060002D5 RID: 725 RVA: 0x0000EEF8 File Offset: 0x0000D0F8
	public void DestroyObjects()
	{
		this.DestroyProbabilityTexture();
	}

	// Token: 0x17000077 RID: 119
	// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000EF00 File Offset: 0x0000D100
	// (set) Token: 0x060002D7 RID: 727 RVA: 0x0000EF08 File Offset: 0x0000D108
	public string name
	{
		get
		{
			return base.name;
		}
		set
		{
			base.name = value;
			this.material.name = value + "(" + this.material.name.Replace("(Clone)", string.Empty) + ")";
		}
	}

	// Token: 0x040001F8 RID: 504
	[NonSerialized]
	public RenderTexture probabilityTexture;

	// Token: 0x040001F9 RID: 505
	[SerializeField]
	private Material material;

	// Token: 0x040001FA RID: 506
	[SerializeField]
	public float gridScale;

	// Token: 0x040001FB RID: 507
	[SerializeField]
	public int gridSize;
}
