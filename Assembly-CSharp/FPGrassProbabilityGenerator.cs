using System;
using Facepunch;
using UnityEngine;

// Token: 0x02000045 RID: 69
[ExecuteInEditMode]
public class FPGrassProbabilityGenerator : ScriptableObject, IFPGrassAsset
{
	// Token: 0x06000258 RID: 600 RVA: 0x0000D4FC File Offset: 0x0000B6FC
	private void OnEnable()
	{
		this.Initialize();
	}

	// Token: 0x06000259 RID: 601 RVA: 0x0000D504 File Offset: 0x0000B704
	private void OnDisable()
	{
		this.DestroyProbabilityTexture();
	}

	// Token: 0x0600025A RID: 602 RVA: 0x0000D50C File Offset: 0x0000B70C
	private void OnDestroy()
	{
		this.DestroyProbabilityTexture();
	}

	// Token: 0x0600025B RID: 603 RVA: 0x0000D514 File Offset: 0x0000B714
	private void DestroyProbabilityTexture()
	{
		if (this.probabilityTexture)
		{
			Object.DestroyImmediate(this.probabilityTexture, true);
			this.probabilityTexture = null;
		}
	}

	// Token: 0x0600025C RID: 604 RVA: 0x0000D53C File Offset: 0x0000B73C
	public void Initialize()
	{
		if (!this.probabilityTexture && this.gridSize > 0)
		{
			this.CreateRenderTexture();
		}
		if (!this.material)
		{
			this.material = (Material)Object.Instantiate(Bundling.Load("rust/fpgrass/RenderSplatMaterial", typeof(Material)));
			this.material.SetTexture("_Noise", (Texture2D)Bundling.Load("rust/fpgrass/noise", typeof(Texture2D)));
		}
	}

	// Token: 0x0600025D RID: 605 RVA: 0x0000D5C8 File Offset: 0x0000B7C8
	public void SetSplatTexture(Texture2D texture)
	{
		this.material.SetTexture("_Splat1", texture);
	}

	// Token: 0x0600025E RID: 606 RVA: 0x0000D5DC File Offset: 0x0000B7DC
	public void SetDetailProbabilities(Texture2D texture)
	{
		this.material.SetTexture("_DetailProbabilities", texture);
	}

	// Token: 0x0600025F RID: 607 RVA: 0x0000D5F0 File Offset: 0x0000B7F0
	public void SetGridScale(float newScale)
	{
		this.gridScale = newScale;
		this.material.SetFloat("_GridScale", this.gridScale);
	}

	// Token: 0x06000260 RID: 608 RVA: 0x0000D610 File Offset: 0x0000B810
	public void SetGridSize(int newSize)
	{
		this.gridSize = newSize;
		this.material.SetFloat("_GridSize", (float)this.gridSize);
		this.CreateRenderTexture();
	}

	// Token: 0x06000261 RID: 609 RVA: 0x0000D644 File Offset: 0x0000B844
	private void CreateRenderTexture()
	{
		this.DestroyProbabilityTexture();
		int num = Mathf.NextPowerOfTwo(this.gridSize);
		this.probabilityTexture = new RenderTexture(num, num, 0, FPGrass.Support.ProbabilityRenderTextureFormat4Channel)
		{
			hideFlags = 4
		};
		this.probabilityTexture.filterMode = 0;
		this.probabilityTexture.useMipMap = false;
		this.probabilityTexture.anisoLevel = 0;
		this.probabilityTexture.Create();
	}

	// Token: 0x06000262 RID: 610 RVA: 0x0000D6B0 File Offset: 0x0000B8B0
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
		int pass = (FPGrass.Support.DetailProbabilityFilterMode != null) ? 1 : 0;
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

	// Token: 0x06000263 RID: 611 RVA: 0x0000D950 File Offset: 0x0000BB50
	public void DestroyObjects()
	{
		this.DestroyProbabilityTexture();
	}

	// Token: 0x17000061 RID: 97
	// (get) Token: 0x06000264 RID: 612 RVA: 0x0000D958 File Offset: 0x0000BB58
	// (set) Token: 0x06000265 RID: 613 RVA: 0x0000D960 File Offset: 0x0000BB60
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

	// Token: 0x04000196 RID: 406
	[NonSerialized]
	public RenderTexture probabilityTexture;

	// Token: 0x04000197 RID: 407
	[SerializeField]
	private Material material;

	// Token: 0x04000198 RID: 408
	[SerializeField]
	public float gridScale;

	// Token: 0x04000199 RID: 409
	[SerializeField]
	public int gridSize;
}
