using System;
using System.Collections.Generic;
using Facepunch;
using UnityEngine;

// Token: 0x02000038 RID: 56
[ExecuteInEditMode]
public class FPGrass : MonoBehaviour, IFPGrassAsset
{
	// Token: 0x1700005A RID: 90
	// (get) Token: 0x06000214 RID: 532 RVA: 0x0000BDD0 File Offset: 0x00009FD0
	public static bool anyEnabled
	{
		get
		{
			return FPGrass.AllEnabledFPGrass.Count > 0;
		}
	}

	// Token: 0x06000215 RID: 533 RVA: 0x0000BDE0 File Offset: 0x00009FE0
	private void Awake()
	{
		if (!this.material)
		{
			this.material = (Material)Object.Instantiate(Bundling.Load("rust/fpgrass/grassmaterial", typeof(Material)));
		}
	}

	// Token: 0x06000216 RID: 534 RVA: 0x0000BE24 File Offset: 0x0000A024
	private void Start()
	{
		this.Initialize();
	}

	// Token: 0x06000217 RID: 535 RVA: 0x0000BE2C File Offset: 0x0000A02C
	private void Reset()
	{
		for (int i = base.transform.childCount - 1; i >= 0; i--)
		{
			Object.DestroyImmediate(base.transform.GetChild(i).gameObject);
		}
		if (this.grassAtlas)
		{
			Object.DestroyImmediate(this.grassAtlas);
		}
		if (this.grassProbabilities)
		{
			Object.DestroyImmediate(this.grassProbabilities);
		}
		this.Initialize();
	}

	// Token: 0x06000218 RID: 536 RVA: 0x0000BEAC File Offset: 0x0000A0AC
	private void OnValidate()
	{
		this.Initialize();
	}

	// Token: 0x06000219 RID: 537 RVA: 0x0000BEB4 File Offset: 0x0000A0B4
	private void Initialize()
	{
		if (!FPGrass.Support.Supported)
		{
			return;
		}
		if (!this.grassProbabilities)
		{
			this.grassProbabilities = ScriptableObject.CreateInstance<FPGrassProbabilities>();
			this.grassProbabilities.name = "FPGrassProbabilities";
		}
		if (!this.grassAtlas)
		{
			this.grassAtlas = ScriptableObject.CreateInstance<FPGrassAtlas>();
			this.grassAtlas.name = "FPGrassAtlas";
		}
		this.settingsDirty = true;
		this.UpdateProbabilities();
		this.UpdateGrassProperties();
	}

	// Token: 0x0600021A RID: 538 RVA: 0x0000BF38 File Offset: 0x0000A138
	private void Update()
	{
		if (!FPGrass.Support.Supported)
		{
			return;
		}
		if (!grass.on)
		{
			this.ExitList();
			return;
		}
		if (this.EnterList())
		{
			this.Initialize();
		}
		else
		{
			this.settingsDirty = true;
			if (this.settingsDirty)
			{
				this.UpdateProbabilities();
				this.UpdateGrassProperties();
				this.settingsDirty = false;
			}
		}
		if (Application.isPlaying && this.parentCamera)
		{
			this.UpdateLevels(this.parentCamera.transform.position);
		}
	}

	// Token: 0x0600021B RID: 539 RVA: 0x0000BFD0 File Offset: 0x0000A1D0
	private bool EnterList()
	{
		if (!this.inList)
		{
			FPGrass.AllEnabledFPGrass.Add(this);
			this.inList = true;
			return true;
		}
		return false;
	}

	// Token: 0x0600021C RID: 540 RVA: 0x0000C000 File Offset: 0x0000A200
	private bool ExitList()
	{
		if (this.inList)
		{
			bool result = FPGrass.AllEnabledFPGrass.Remove(this);
			this.inList = false;
			return result;
		}
		return false;
	}

	// Token: 0x0600021D RID: 541 RVA: 0x0000C030 File Offset: 0x0000A230
	private void OnEnable()
	{
		if (!FPGrass.Support.Supported)
		{
			return;
		}
		this.EnterList();
		if (!Terrain.activeTerrain)
		{
			return;
		}
		Terrain.activeTerrain.detailObjectDistance = 0f;
		Terrain.activeTerrain.detailObjectDensity = 0f;
	}

	// Token: 0x0600021E RID: 542 RVA: 0x0000C080 File Offset: 0x0000A280
	private void OnDisable()
	{
		this.ExitList();
		if (!Terrain.activeTerrain)
		{
			return;
		}
		Terrain.activeTerrain.detailObjectDistance = 134.6f;
		Terrain.activeTerrain.detailObjectDensity = 1f;
	}

	// Token: 0x0600021F RID: 543 RVA: 0x0000C0C4 File Offset: 0x0000A2C4
	internal static void DrawAllGrass(ref FPGrass.RenderArguments renderArgs)
	{
		List<FPGrass> allEnabledFPGrass = FPGrass.AllEnabledFPGrass;
		FPGrass.AllEnabledFPGrassInstancesSwap.AddRange(allEnabledFPGrass);
		FPGrass.AllEnabledFPGrass = FPGrass.AllEnabledFPGrassInstancesSwap;
		FPGrass.AllEnabledFPGrassInstancesSwap = allEnabledFPGrass;
		try
		{
			foreach (FPGrass fpgrass in FPGrass.AllEnabledFPGrassInstancesSwap)
			{
				fpgrass.Render(ref renderArgs);
			}
		}
		finally
		{
			FPGrass.AllEnabledFPGrassInstancesSwap.Clear();
		}
	}

	// Token: 0x06000220 RID: 544 RVA: 0x0000C174 File Offset: 0x0000A374
	private void Render(ref FPGrass.RenderArguments renderArgs)
	{
		if (base.enabled)
		{
			foreach (FPGrassLevel fpgrassLevel in this.children)
			{
				if (fpgrassLevel.enabled)
				{
					if (!renderArgs.immediate)
					{
						fpgrassLevel.UpdateLevel(renderArgs.center, renderArgs.terrain);
					}
					if (fpgrassLevel.enabled)
					{
						fpgrassLevel.Render(ref renderArgs);
					}
				}
			}
		}
	}

	// Token: 0x06000221 RID: 545 RVA: 0x0000C218 File Offset: 0x0000A418
	public void UpdateLevels(Vector3 position)
	{
		base.transform.position = Vector3.zero;
		Terrain activeTerrain = Terrain.activeTerrain;
		if (activeTerrain)
		{
			foreach (FPGrassLevel fpgrassLevel in this.children)
			{
				fpgrassLevel.UpdateLevel(position, activeTerrain);
			}
		}
	}

	// Token: 0x06000222 RID: 546 RVA: 0x0000C2A0 File Offset: 0x0000A4A0
	public void UpdateProbabilities()
	{
		foreach (FPGrassLevel fpgrassLevel in this.children)
		{
			fpgrassLevel.probabilityGenerator.SetDetailProbabilities(this.grassProbabilities.GetTexture());
		}
	}

	// Token: 0x06000223 RID: 547 RVA: 0x0000C318 File Offset: 0x0000A518
	public void UpdateGrassProperties()
	{
		if (!this.grassAtlas || !this.material)
		{
			return;
		}
		for (int i = 0; i < 16; i++)
		{
			FPGrassProperty fpgrassProperty = this.grassAtlas.properties[i];
			this.material.SetColor("_GrassColorsOne" + i, fpgrassProperty.Color1);
			this.material.SetColor("_GrassColorsTwo" + i, fpgrassProperty.Color2);
			this.material.SetVector("_GrassSizes" + i, new Vector4(fpgrassProperty.MinWidth, fpgrassProperty.MaxWidth, fpgrassProperty.MinHeight, fpgrassProperty.MaxHeight));
		}
		for (int j = 0; j < this.children.Count; j++)
		{
			for (int k = 0; k < 16; k++)
			{
				FPGrassProperty fpgrassProperty2 = this.grassAtlas.properties[k];
				this.children[j].levelMaterial.SetColor("_GrassColorsOne" + k, fpgrassProperty2.Color1);
				this.children[j].levelMaterial.SetColor("_GrassColorsTwo" + k, fpgrassProperty2.Color2);
				this.children[j].levelMaterial.SetVector("_GrassSizes" + k, new Vector4(fpgrassProperty2.MinWidth, fpgrassProperty2.MaxWidth, fpgrassProperty2.MinHeight, fpgrassProperty2.MaxHeight));
			}
		}
	}

	// Token: 0x1700005B RID: 91
	// (get) Token: 0x06000225 RID: 549 RVA: 0x0000C51C File Offset: 0x0000A71C
	// (set) Token: 0x06000224 RID: 548 RVA: 0x0000C4C8 File Offset: 0x0000A6C8
	public float ScatterAmount
	{
		get
		{
			return this.scatterAmount;
		}
		set
		{
			this.scatterAmount = value;
			for (int i = 0; i < this.children.Count; i++)
			{
				this.children[i].levelMaterial.SetFloat("_ScatterAmount", this.scatterAmount);
			}
		}
	}

	// Token: 0x1700005C RID: 92
	// (get) Token: 0x06000227 RID: 551 RVA: 0x0000C58C File Offset: 0x0000A78C
	// (set) Token: 0x06000226 RID: 550 RVA: 0x0000C524 File Offset: 0x0000A724
	public float NormalBias
	{
		get
		{
			return this.normalBias;
		}
		set
		{
			this.normalBias = value;
			this.material.SetFloat("_GroundNormalBias", this.normalBias);
			for (int i = 0; i < this.children.Count; i++)
			{
				this.children[i].levelMaterial.SetFloat("_GroundNormalBias", this.normalBias);
			}
		}
	}

	// Token: 0x1700005D RID: 93
	// (get) Token: 0x06000228 RID: 552 RVA: 0x0000C594 File Offset: 0x0000A794
	// (set) Token: 0x06000229 RID: 553 RVA: 0x0000C59C File Offset: 0x0000A79C
	public float WindSpeed
	{
		get
		{
			return this.windSpeed;
		}
		set
		{
			this.windSpeed = value;
			this.UpdateWindSettings();
		}
	}

	// Token: 0x1700005E RID: 94
	// (get) Token: 0x0600022A RID: 554 RVA: 0x0000C5AC File Offset: 0x0000A7AC
	// (set) Token: 0x0600022B RID: 555 RVA: 0x0000C5B4 File Offset: 0x0000A7B4
	public float WindSize
	{
		get
		{
			return this.windSize;
		}
		set
		{
			this.windSize = value;
		}
	}

	// Token: 0x1700005F RID: 95
	// (get) Token: 0x0600022C RID: 556 RVA: 0x0000C5C0 File Offset: 0x0000A7C0
	// (set) Token: 0x0600022D RID: 557 RVA: 0x0000C5C8 File Offset: 0x0000A7C8
	public float WindBending
	{
		get
		{
			return this.windBending;
		}
		set
		{
			this.windBending = value;
			this.UpdateWindSettings();
		}
	}

	// Token: 0x17000060 RID: 96
	// (get) Token: 0x0600022E RID: 558 RVA: 0x0000C5D8 File Offset: 0x0000A7D8
	// (set) Token: 0x0600022F RID: 559 RVA: 0x0000C5E0 File Offset: 0x0000A7E0
	public Color WindTint
	{
		get
		{
			return this.windTint;
		}
		set
		{
			this.windTint = value;
			this.UpdateWindSettings();
		}
	}

	// Token: 0x06000230 RID: 560 RVA: 0x0000C5F0 File Offset: 0x0000A7F0
	public void UpdateWindSettings()
	{
		for (int i = 0; i < this.children.Count; i++)
		{
			this.children[i].levelMaterial.SetVector("_WaveAndDistance", new Vector4(this.windSpeed, this.windSize, this.windBending, 0f));
			this.children[i].levelMaterial.SetColor("_WavingTint", this.windTint);
		}
	}

	// Token: 0x04000142 RID: 322
	[SerializeField]
	private List<FPGrassLevel> children = new List<FPGrassLevel>();

	// Token: 0x04000143 RID: 323
	public Camera parentCamera;

	// Token: 0x04000144 RID: 324
	public int numberOfLevels = 4;

	// Token: 0x04000145 RID: 325
	public float baseLevelSize = 20f;

	// Token: 0x04000146 RID: 326
	public int gridSizePerLevel = 28;

	// Token: 0x04000147 RID: 327
	[SerializeField]
	private float gridSizeAtFinestLevel;

	// Token: 0x04000148 RID: 328
	public Material material;

	// Token: 0x04000149 RID: 329
	[SerializeField]
	private float scatterAmount = 1f;

	// Token: 0x0400014A RID: 330
	[SerializeField]
	private float normalBias = 0.7f;

	// Token: 0x0400014B RID: 331
	public FPGrassProbabilities grassProbabilities;

	// Token: 0x0400014C RID: 332
	public FPGrassAtlas grassAtlas;

	// Token: 0x0400014D RID: 333
	public bool followSceneCamera;

	// Token: 0x0400014E RID: 334
	public bool toggleWireframe;

	// Token: 0x0400014F RID: 335
	[SerializeField]
	private float windSpeed = 0.1f;

	// Token: 0x04000150 RID: 336
	[SerializeField]
	private float windSize = 1f;

	// Token: 0x04000151 RID: 337
	[SerializeField]
	private float windBending = 1f;

	// Token: 0x04000152 RID: 338
	[SerializeField]
	private Color windTint = Color.white;

	// Token: 0x04000153 RID: 339
	[HideInInspector]
	[SerializeField]
	private Texture2D heightMap;

	// Token: 0x04000154 RID: 340
	[HideInInspector]
	[SerializeField]
	private Texture2D normalMap;

	// Token: 0x04000155 RID: 341
	[SerializeField]
	[HideInInspector]
	private Texture2D splatMap;

	// Token: 0x04000156 RID: 342
	[NonSerialized]
	private bool settingsDirty;

	// Token: 0x04000157 RID: 343
	private static List<FPGrass> AllEnabledFPGrass = new List<FPGrass>();

	// Token: 0x04000158 RID: 344
	private static List<FPGrass> AllEnabledFPGrassInstancesSwap = new List<FPGrass>();

	// Token: 0x04000159 RID: 345
	[NonSerialized]
	private bool inList;

	// Token: 0x0400015A RID: 346
	public static bool castShadows = false;

	// Token: 0x0400015B RID: 347
	public static bool receiveShadows = true;

	// Token: 0x02000039 RID: 57
	internal struct RenderArguments
	{
		// Token: 0x0400015C RID: 348
		public Plane[] frustum;

		// Token: 0x0400015D RID: 349
		public Camera camera;

		// Token: 0x0400015E RID: 350
		public Terrain terrain;

		// Token: 0x0400015F RID: 351
		public Vector3 center;

		// Token: 0x04000160 RID: 352
		public bool immediate;
	}

	// Token: 0x0200003A RID: 58
	public static class Support
	{
		// Token: 0x06000231 RID: 561 RVA: 0x0000C674 File Offset: 0x0000A874
		static Support()
		{
			bool flag;
			if (SystemInfo.SupportsRenderTextureFormat(0))
			{
				FPGrass.Support.ProbabilityRenderTextureFormat4Channel = 0;
				flag = true;
			}
			else
			{
				FPGrass.Support.ProbabilityRenderTextureFormat4Channel = 7;
				flag = SystemInfo.SupportsRenderTextureFormat(7);
			}
			bool flag2;
			if (SystemInfo.SupportsRenderTextureFormat(16))
			{
				FPGrass.Support.ProbabilityRenderTextureFormat1Channel = 16;
				flag2 = true;
			}
			else
			{
				flag2 = false;
				FPGrass.Support.ProbabilityRenderTextureFormat1Channel = 7;
			}
			if (flag && !flag2)
			{
				FPGrass.Support.DisplacementExpensive = true;
				FPGrass.Support.ProbabilityRenderTextureFormat1Channel = FPGrass.Support.ProbabilityRenderTextureFormat4Channel;
			}
			else
			{
				FPGrass.Support.DisplacementExpensive = false;
			}
			FPGrass.Support.Supported = (flag2 || flag);
			if (!SystemInfo.supportsComputeShaders)
			{
				FPGrass.Support.DetailProbabilityFilterMode = 1;
			}
			else
			{
				FPGrass.Support.DetailProbabilityFilterMode = 0;
			}
		}

		// Token: 0x04000161 RID: 353
		public static readonly bool Supported;

		// Token: 0x04000162 RID: 354
		public static readonly bool DisplacementExpensive;

		// Token: 0x04000163 RID: 355
		public static FilterMode DetailProbabilityFilterMode;

		// Token: 0x04000164 RID: 356
		public static readonly RenderTextureFormat ProbabilityRenderTextureFormat4Channel;

		// Token: 0x04000165 RID: 357
		public static readonly RenderTextureFormat ProbabilityRenderTextureFormat1Channel;
	}
}
