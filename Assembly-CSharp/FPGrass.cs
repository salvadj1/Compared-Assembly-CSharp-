using System;
using System.Collections.Generic;
using Facepunch;
using UnityEngine;

// Token: 0x0200004A RID: 74
[ExecuteInEditMode]
public class FPGrass : MonoBehaviour, global::IFPGrassAsset
{
	// Token: 0x17000070 RID: 112
	// (get) Token: 0x06000286 RID: 646 RVA: 0x0000D378 File Offset: 0x0000B578
	public static bool anyEnabled
	{
		get
		{
			return global::FPGrass.AllEnabledFPGrass.Count > 0;
		}
	}

	// Token: 0x06000287 RID: 647 RVA: 0x0000D388 File Offset: 0x0000B588
	private void Awake()
	{
		if (!this.material)
		{
			this.material = (Material)Object.Instantiate(Facepunch.Bundling.Load("rust/fpgrass/grassmaterial", typeof(Material)));
		}
	}

	// Token: 0x06000288 RID: 648 RVA: 0x0000D3CC File Offset: 0x0000B5CC
	private void Start()
	{
		this.Initialize();
	}

	// Token: 0x06000289 RID: 649 RVA: 0x0000D3D4 File Offset: 0x0000B5D4
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

	// Token: 0x0600028A RID: 650 RVA: 0x0000D454 File Offset: 0x0000B654
	private void OnValidate()
	{
		this.Initialize();
	}

	// Token: 0x0600028B RID: 651 RVA: 0x0000D45C File Offset: 0x0000B65C
	private void Initialize()
	{
		if (!global::FPGrass.Support.Supported)
		{
			return;
		}
		if (!this.grassProbabilities)
		{
			this.grassProbabilities = ScriptableObject.CreateInstance<global::FPGrassProbabilities>();
			this.grassProbabilities.name = "FPGrassProbabilities";
		}
		if (!this.grassAtlas)
		{
			this.grassAtlas = ScriptableObject.CreateInstance<global::FPGrassAtlas>();
			this.grassAtlas.name = "FPGrassAtlas";
		}
		this.settingsDirty = true;
		this.UpdateProbabilities();
		this.UpdateGrassProperties();
	}

	// Token: 0x0600028C RID: 652 RVA: 0x0000D4E0 File Offset: 0x0000B6E0
	private void Update()
	{
		if (!global::FPGrass.Support.Supported)
		{
			return;
		}
		if (!global::grass.on)
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

	// Token: 0x0600028D RID: 653 RVA: 0x0000D578 File Offset: 0x0000B778
	private bool EnterList()
	{
		if (!this.inList)
		{
			global::FPGrass.AllEnabledFPGrass.Add(this);
			this.inList = true;
			return true;
		}
		return false;
	}

	// Token: 0x0600028E RID: 654 RVA: 0x0000D5A8 File Offset: 0x0000B7A8
	private bool ExitList()
	{
		if (this.inList)
		{
			bool result = global::FPGrass.AllEnabledFPGrass.Remove(this);
			this.inList = false;
			return result;
		}
		return false;
	}

	// Token: 0x0600028F RID: 655 RVA: 0x0000D5D8 File Offset: 0x0000B7D8
	private void OnEnable()
	{
		if (!global::FPGrass.Support.Supported)
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

	// Token: 0x06000290 RID: 656 RVA: 0x0000D628 File Offset: 0x0000B828
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

	// Token: 0x06000291 RID: 657 RVA: 0x0000D66C File Offset: 0x0000B86C
	internal static void DrawAllGrass(ref global::FPGrass.RenderArguments renderArgs)
	{
		List<global::FPGrass> allEnabledFPGrass = global::FPGrass.AllEnabledFPGrass;
		global::FPGrass.AllEnabledFPGrassInstancesSwap.AddRange(allEnabledFPGrass);
		global::FPGrass.AllEnabledFPGrass = global::FPGrass.AllEnabledFPGrassInstancesSwap;
		global::FPGrass.AllEnabledFPGrassInstancesSwap = allEnabledFPGrass;
		try
		{
			foreach (global::FPGrass fpgrass in global::FPGrass.AllEnabledFPGrassInstancesSwap)
			{
				fpgrass.Render(ref renderArgs);
			}
		}
		finally
		{
			global::FPGrass.AllEnabledFPGrassInstancesSwap.Clear();
		}
	}

	// Token: 0x06000292 RID: 658 RVA: 0x0000D71C File Offset: 0x0000B91C
	private void Render(ref global::FPGrass.RenderArguments renderArgs)
	{
		if (base.enabled)
		{
			foreach (global::FPGrassLevel fpgrassLevel in this.children)
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

	// Token: 0x06000293 RID: 659 RVA: 0x0000D7C0 File Offset: 0x0000B9C0
	public void UpdateLevels(Vector3 position)
	{
		base.transform.position = Vector3.zero;
		Terrain activeTerrain = Terrain.activeTerrain;
		if (activeTerrain)
		{
			foreach (global::FPGrassLevel fpgrassLevel in this.children)
			{
				fpgrassLevel.UpdateLevel(position, activeTerrain);
			}
		}
	}

	// Token: 0x06000294 RID: 660 RVA: 0x0000D848 File Offset: 0x0000BA48
	public void UpdateProbabilities()
	{
		foreach (global::FPGrassLevel fpgrassLevel in this.children)
		{
			fpgrassLevel.probabilityGenerator.SetDetailProbabilities(this.grassProbabilities.GetTexture());
		}
	}

	// Token: 0x06000295 RID: 661 RVA: 0x0000D8C0 File Offset: 0x0000BAC0
	public void UpdateGrassProperties()
	{
		if (!this.grassAtlas || !this.material)
		{
			return;
		}
		for (int i = 0; i < 16; i++)
		{
			global::FPGrassProperty fpgrassProperty = this.grassAtlas.properties[i];
			this.material.SetColor("_GrassColorsOne" + i, fpgrassProperty.Color1);
			this.material.SetColor("_GrassColorsTwo" + i, fpgrassProperty.Color2);
			this.material.SetVector("_GrassSizes" + i, new Vector4(fpgrassProperty.MinWidth, fpgrassProperty.MaxWidth, fpgrassProperty.MinHeight, fpgrassProperty.MaxHeight));
		}
		for (int j = 0; j < this.children.Count; j++)
		{
			for (int k = 0; k < 16; k++)
			{
				global::FPGrassProperty fpgrassProperty2 = this.grassAtlas.properties[k];
				this.children[j].levelMaterial.SetColor("_GrassColorsOne" + k, fpgrassProperty2.Color1);
				this.children[j].levelMaterial.SetColor("_GrassColorsTwo" + k, fpgrassProperty2.Color2);
				this.children[j].levelMaterial.SetVector("_GrassSizes" + k, new Vector4(fpgrassProperty2.MinWidth, fpgrassProperty2.MaxWidth, fpgrassProperty2.MinHeight, fpgrassProperty2.MaxHeight));
			}
		}
	}

	// Token: 0x17000071 RID: 113
	// (get) Token: 0x06000297 RID: 663 RVA: 0x0000DAC4 File Offset: 0x0000BCC4
	// (set) Token: 0x06000296 RID: 662 RVA: 0x0000DA70 File Offset: 0x0000BC70
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

	// Token: 0x17000072 RID: 114
	// (get) Token: 0x06000299 RID: 665 RVA: 0x0000DB34 File Offset: 0x0000BD34
	// (set) Token: 0x06000298 RID: 664 RVA: 0x0000DACC File Offset: 0x0000BCCC
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

	// Token: 0x17000073 RID: 115
	// (get) Token: 0x0600029A RID: 666 RVA: 0x0000DB3C File Offset: 0x0000BD3C
	// (set) Token: 0x0600029B RID: 667 RVA: 0x0000DB44 File Offset: 0x0000BD44
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

	// Token: 0x17000074 RID: 116
	// (get) Token: 0x0600029C RID: 668 RVA: 0x0000DB54 File Offset: 0x0000BD54
	// (set) Token: 0x0600029D RID: 669 RVA: 0x0000DB5C File Offset: 0x0000BD5C
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

	// Token: 0x17000075 RID: 117
	// (get) Token: 0x0600029E RID: 670 RVA: 0x0000DB68 File Offset: 0x0000BD68
	// (set) Token: 0x0600029F RID: 671 RVA: 0x0000DB70 File Offset: 0x0000BD70
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

	// Token: 0x17000076 RID: 118
	// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000DB80 File Offset: 0x0000BD80
	// (set) Token: 0x060002A1 RID: 673 RVA: 0x0000DB88 File Offset: 0x0000BD88
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

	// Token: 0x060002A2 RID: 674 RVA: 0x0000DB98 File Offset: 0x0000BD98
	public void UpdateWindSettings()
	{
		for (int i = 0; i < this.children.Count; i++)
		{
			this.children[i].levelMaterial.SetVector("_WaveAndDistance", new Vector4(this.windSpeed, this.windSize, this.windBending, 0f));
			this.children[i].levelMaterial.SetColor("_WavingTint", this.windTint);
		}
	}

	// Token: 0x040001A4 RID: 420
	[SerializeField]
	private List<global::FPGrassLevel> children = new List<global::FPGrassLevel>();

	// Token: 0x040001A5 RID: 421
	public Camera parentCamera;

	// Token: 0x040001A6 RID: 422
	public int numberOfLevels = 4;

	// Token: 0x040001A7 RID: 423
	public float baseLevelSize = 20f;

	// Token: 0x040001A8 RID: 424
	public int gridSizePerLevel = 28;

	// Token: 0x040001A9 RID: 425
	[SerializeField]
	private float gridSizeAtFinestLevel;

	// Token: 0x040001AA RID: 426
	public Material material;

	// Token: 0x040001AB RID: 427
	[SerializeField]
	private float scatterAmount = 1f;

	// Token: 0x040001AC RID: 428
	[SerializeField]
	private float normalBias = 0.7f;

	// Token: 0x040001AD RID: 429
	public global::FPGrassProbabilities grassProbabilities;

	// Token: 0x040001AE RID: 430
	public global::FPGrassAtlas grassAtlas;

	// Token: 0x040001AF RID: 431
	public bool followSceneCamera;

	// Token: 0x040001B0 RID: 432
	public bool toggleWireframe;

	// Token: 0x040001B1 RID: 433
	[SerializeField]
	private float windSpeed = 0.1f;

	// Token: 0x040001B2 RID: 434
	[SerializeField]
	private float windSize = 1f;

	// Token: 0x040001B3 RID: 435
	[SerializeField]
	private float windBending = 1f;

	// Token: 0x040001B4 RID: 436
	[SerializeField]
	private Color windTint = Color.white;

	// Token: 0x040001B5 RID: 437
	[SerializeField]
	[HideInInspector]
	private Texture2D heightMap;

	// Token: 0x040001B6 RID: 438
	[SerializeField]
	[HideInInspector]
	private Texture2D normalMap;

	// Token: 0x040001B7 RID: 439
	[HideInInspector]
	[SerializeField]
	private Texture2D splatMap;

	// Token: 0x040001B8 RID: 440
	[NonSerialized]
	private bool settingsDirty;

	// Token: 0x040001B9 RID: 441
	private static List<global::FPGrass> AllEnabledFPGrass = new List<global::FPGrass>();

	// Token: 0x040001BA RID: 442
	private static List<global::FPGrass> AllEnabledFPGrassInstancesSwap = new List<global::FPGrass>();

	// Token: 0x040001BB RID: 443
	[NonSerialized]
	private bool inList;

	// Token: 0x040001BC RID: 444
	public static bool castShadows = false;

	// Token: 0x040001BD RID: 445
	public static bool receiveShadows = true;

	// Token: 0x0200004B RID: 75
	internal struct RenderArguments
	{
		// Token: 0x040001BE RID: 446
		public Plane[] frustum;

		// Token: 0x040001BF RID: 447
		public Camera camera;

		// Token: 0x040001C0 RID: 448
		public Terrain terrain;

		// Token: 0x040001C1 RID: 449
		public Vector3 center;

		// Token: 0x040001C2 RID: 450
		public bool immediate;
	}

	// Token: 0x0200004C RID: 76
	public static class Support
	{
		// Token: 0x060002A3 RID: 675 RVA: 0x0000DC1C File Offset: 0x0000BE1C
		static Support()
		{
			bool flag;
			if (SystemInfo.SupportsRenderTextureFormat(0))
			{
				global::FPGrass.Support.ProbabilityRenderTextureFormat4Channel = 0;
				flag = true;
			}
			else
			{
				global::FPGrass.Support.ProbabilityRenderTextureFormat4Channel = 7;
				flag = SystemInfo.SupportsRenderTextureFormat(7);
			}
			bool flag2;
			if (SystemInfo.SupportsRenderTextureFormat(16))
			{
				global::FPGrass.Support.ProbabilityRenderTextureFormat1Channel = 16;
				flag2 = true;
			}
			else
			{
				flag2 = false;
				global::FPGrass.Support.ProbabilityRenderTextureFormat1Channel = 7;
			}
			if (flag && !flag2)
			{
				global::FPGrass.Support.DisplacementExpensive = true;
				global::FPGrass.Support.ProbabilityRenderTextureFormat1Channel = global::FPGrass.Support.ProbabilityRenderTextureFormat4Channel;
			}
			else
			{
				global::FPGrass.Support.DisplacementExpensive = false;
			}
			global::FPGrass.Support.Supported = (flag2 || flag);
			if (!SystemInfo.supportsComputeShaders)
			{
				global::FPGrass.Support.DetailProbabilityFilterMode = 1;
			}
			else
			{
				global::FPGrass.Support.DetailProbabilityFilterMode = 0;
			}
		}

		// Token: 0x040001C3 RID: 451
		public static readonly bool Supported;

		// Token: 0x040001C4 RID: 452
		public static readonly bool DisplacementExpensive;

		// Token: 0x040001C5 RID: 453
		public static FilterMode DetailProbabilityFilterMode;

		// Token: 0x040001C6 RID: 454
		public static readonly RenderTextureFormat ProbabilityRenderTextureFormat4Channel;

		// Token: 0x040001C7 RID: 455
		public static readonly RenderTextureFormat ProbabilityRenderTextureFormat1Channel;
	}
}
