using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000904 RID: 2308
[AddComponentMenu("Terrain/Terrain Toolkit")]
[ExecuteInEditMode]
public class TerrainToolkit : MonoBehaviour
{
	// Token: 0x06004EC3 RID: 20163 RVA: 0x00146D5C File Offset: 0x00144F5C
	public void addPresets()
	{
		this.presetsInitialised = true;
		this.voronoiPresets = new ArrayList();
		this.fractalPresets = new ArrayList();
		this.perlinPresets = new ArrayList();
		this.thermalErosionPresets = new ArrayList();
		this.fastHydraulicErosionPresets = new ArrayList();
		this.fullHydraulicErosionPresets = new ArrayList();
		this.velocityHydraulicErosionPresets = new ArrayList();
		this.tidalErosionPresets = new ArrayList();
		this.windErosionPresets = new ArrayList();
		this.voronoiPresets.Add(new global::TerrainToolkit.voronoiPresetData("Scattered Peaks", global::TerrainToolkit.VoronoiType.Linear, 16, 8f, 0.5f, 1f));
		this.voronoiPresets.Add(new global::TerrainToolkit.voronoiPresetData("Rolling Hills", global::TerrainToolkit.VoronoiType.Sine, 8, 8f, 0f, 1f));
		this.voronoiPresets.Add(new global::TerrainToolkit.voronoiPresetData("Jagged Mountains", global::TerrainToolkit.VoronoiType.Linear, 32, 32f, 0.5f, 1f));
		this.fractalPresets.Add(new global::TerrainToolkit.fractalPresetData("Rolling Plains", 0.4f, 1f));
		this.fractalPresets.Add(new global::TerrainToolkit.fractalPresetData("Rough Mountains", 0.5f, 1f));
		this.fractalPresets.Add(new global::TerrainToolkit.fractalPresetData("Add Noise", 0.75f, 0.05f));
		this.perlinPresets.Add(new global::TerrainToolkit.perlinPresetData("Rough Plains", 2, 0.5f, 9, 1f));
		this.perlinPresets.Add(new global::TerrainToolkit.perlinPresetData("Rolling Hills", 5, 0.75f, 3, 1f));
		this.perlinPresets.Add(new global::TerrainToolkit.perlinPresetData("Rocky Mountains", 4, 1f, 8, 1f));
		this.perlinPresets.Add(new global::TerrainToolkit.perlinPresetData("Hellish Landscape", 11, 1f, 7, 1f));
		this.perlinPresets.Add(new global::TerrainToolkit.perlinPresetData("Add Noise", 10, 1f, 8, 0.2f));
		this.thermalErosionPresets.Add(new global::TerrainToolkit.thermalErosionPresetData("Gradual, Weak Erosion", 25, 7.5f, 0.5f));
		this.thermalErosionPresets.Add(new global::TerrainToolkit.thermalErosionPresetData("Fast, Harsh Erosion", 25, 2.5f, 0.1f));
		this.thermalErosionPresets.Add(new global::TerrainToolkit.thermalErosionPresetData("Thermal Erosion Brush", 25, 0.1f, 0f));
		this.fastHydraulicErosionPresets.Add(new global::TerrainToolkit.fastHydraulicErosionPresetData("Rainswept Earth", 25, 70f, 1f));
		this.fastHydraulicErosionPresets.Add(new global::TerrainToolkit.fastHydraulicErosionPresetData("Terraced Slopes", 25, 30f, 0.4f));
		this.fastHydraulicErosionPresets.Add(new global::TerrainToolkit.fastHydraulicErosionPresetData("Hydraulic Erosion Brush", 25, 85f, 1f));
		this.fullHydraulicErosionPresets.Add(new global::TerrainToolkit.fullHydraulicErosionPresetData("Low Rainfall, Hard Rock", 25, 0.01f, 0.5f, 0.01f, 0.1f));
		this.fullHydraulicErosionPresets.Add(new global::TerrainToolkit.fullHydraulicErosionPresetData("Low Rainfall, Soft Earth", 25, 0.01f, 0.5f, 0.06f, 0.15f));
		this.fullHydraulicErosionPresets.Add(new global::TerrainToolkit.fullHydraulicErosionPresetData("Heavy Rainfall, Hard Rock", 25, 0.02f, 0.5f, 0.01f, 0.1f));
		this.fullHydraulicErosionPresets.Add(new global::TerrainToolkit.fullHydraulicErosionPresetData("Heavy Rainfall, Soft Earth", 25, 0.02f, 0.5f, 0.06f, 0.15f));
		this.velocityHydraulicErosionPresets.Add(new global::TerrainToolkit.velocityHydraulicErosionPresetData("Low Rainfall, Hard Rock", 25, 0.01f, 0.5f, 0.01f, 0.1f, 1f, 1f, 0.05f, 0.12f));
		this.velocityHydraulicErosionPresets.Add(new global::TerrainToolkit.velocityHydraulicErosionPresetData("Low Rainfall, Soft Earth", 25, 0.01f, 0.5f, 0.06f, 0.15f, 1.2f, 2.8f, 0.05f, 0.12f));
		this.velocityHydraulicErosionPresets.Add(new global::TerrainToolkit.velocityHydraulicErosionPresetData("Heavy Rainfall, Hard Rock", 25, 0.02f, 0.5f, 0.01f, 0.1f, 1.1f, 2.2f, 0.05f, 0.12f));
		this.velocityHydraulicErosionPresets.Add(new global::TerrainToolkit.velocityHydraulicErosionPresetData("Heavy Rainfall, Soft Earth", 25, 0.02f, 0.5f, 0.06f, 0.15f, 1.2f, 2.4f, 0.05f, 0.12f));
		this.velocityHydraulicErosionPresets.Add(new global::TerrainToolkit.velocityHydraulicErosionPresetData("Carved Stone", 25, 0.01f, 0.5f, 0.01f, 0.1f, 2f, 1.25f, 0.05f, 0.35f));
		this.tidalErosionPresets.Add(new global::TerrainToolkit.tidalErosionPresetData("Low Tidal Range, Calm Waves", 25, 5f, 65f));
		this.tidalErosionPresets.Add(new global::TerrainToolkit.tidalErosionPresetData("Low Tidal Range, Strong Waves", 25, 5f, 35f));
		this.tidalErosionPresets.Add(new global::TerrainToolkit.tidalErosionPresetData("High Tidal Range, Calm Water", 25, 15f, 55f));
		this.tidalErosionPresets.Add(new global::TerrainToolkit.tidalErosionPresetData("High Tidal Range, Strong Waves", 25, 15f, 25f));
		this.windErosionPresets.Add(new global::TerrainToolkit.windErosionPresetData("Default (Northerly)", 25, 180f, 0.5f, 0.01f, 0.5f, 0.01f, 0.1f, 0.25f));
		this.windErosionPresets.Add(new global::TerrainToolkit.windErosionPresetData("Default (Southerly)", 25, 0f, 0.5f, 0.01f, 0.5f, 0.01f, 0.1f, 0.25f));
		this.windErosionPresets.Add(new global::TerrainToolkit.windErosionPresetData("Default (Easterly)", 25, 270f, 0.5f, 0.01f, 0.5f, 0.01f, 0.1f, 0.25f));
		this.windErosionPresets.Add(new global::TerrainToolkit.windErosionPresetData("Default (Westerly)", 25, 90f, 0.5f, 0.01f, 0.5f, 0.01f, 0.1f, 0.25f));
	}

	// Token: 0x06004EC4 RID: 20164 RVA: 0x00147388 File Offset: 0x00145588
	public void setVoronoiPreset(global::TerrainToolkit.voronoiPresetData preset)
	{
		this.generatorTypeInt = 0;
		this.generatorType = global::TerrainToolkit.GeneratorType.Voronoi;
		this.voronoiTypeInt = (int)preset.voronoiType;
		this.voronoiType = preset.voronoiType;
		this.voronoiCells = preset.voronoiCells;
		this.voronoiFeatures = preset.voronoiFeatures;
		this.voronoiScale = preset.voronoiScale;
		this.voronoiBlend = preset.voronoiBlend;
	}

	// Token: 0x06004EC5 RID: 20165 RVA: 0x001473EC File Offset: 0x001455EC
	public void setFractalPreset(global::TerrainToolkit.fractalPresetData preset)
	{
		this.generatorTypeInt = 1;
		this.generatorType = global::TerrainToolkit.GeneratorType.DiamondSquare;
		this.diamondSquareDelta = preset.diamondSquareDelta;
		this.diamondSquareBlend = preset.diamondSquareBlend;
	}

	// Token: 0x06004EC6 RID: 20166 RVA: 0x00147420 File Offset: 0x00145620
	public void setPerlinPreset(global::TerrainToolkit.perlinPresetData preset)
	{
		this.generatorTypeInt = 2;
		this.generatorType = global::TerrainToolkit.GeneratorType.Perlin;
		this.perlinFrequency = preset.perlinFrequency;
		this.perlinAmplitude = preset.perlinAmplitude;
		this.perlinOctaves = preset.perlinOctaves;
		this.perlinBlend = preset.perlinBlend;
	}

	// Token: 0x06004EC7 RID: 20167 RVA: 0x0014746C File Offset: 0x0014566C
	public void setThermalErosionPreset(global::TerrainToolkit.thermalErosionPresetData preset)
	{
		this.erosionTypeInt = 0;
		this.erosionType = global::TerrainToolkit.ErosionType.Thermal;
		this.thermalIterations = preset.thermalIterations;
		this.thermalMinSlope = preset.thermalMinSlope;
		this.thermalFalloff = preset.thermalFalloff;
	}

	// Token: 0x06004EC8 RID: 20168 RVA: 0x001474AC File Offset: 0x001456AC
	public void setFastHydraulicErosionPreset(global::TerrainToolkit.fastHydraulicErosionPresetData preset)
	{
		this.erosionTypeInt = 1;
		this.erosionType = global::TerrainToolkit.ErosionType.Hydraulic;
		this.hydraulicTypeInt = 0;
		this.hydraulicType = global::TerrainToolkit.HydraulicType.Fast;
		this.hydraulicIterations = preset.hydraulicIterations;
		this.hydraulicMaxSlope = preset.hydraulicMaxSlope;
		this.hydraulicFalloff = preset.hydraulicFalloff;
	}

	// Token: 0x06004EC9 RID: 20169 RVA: 0x001474FC File Offset: 0x001456FC
	public void setFullHydraulicErosionPreset(global::TerrainToolkit.fullHydraulicErosionPresetData preset)
	{
		this.erosionTypeInt = 1;
		this.erosionType = global::TerrainToolkit.ErosionType.Hydraulic;
		this.hydraulicTypeInt = 1;
		this.hydraulicType = global::TerrainToolkit.HydraulicType.Full;
		this.hydraulicIterations = preset.hydraulicIterations;
		this.hydraulicRainfall = preset.hydraulicRainfall;
		this.hydraulicEvaporation = preset.hydraulicEvaporation;
		this.hydraulicSedimentSolubility = preset.hydraulicSedimentSolubility;
		this.hydraulicSedimentSaturation = preset.hydraulicSedimentSaturation;
	}

	// Token: 0x06004ECA RID: 20170 RVA: 0x00147564 File Offset: 0x00145764
	public void setVelocityHydraulicErosionPreset(global::TerrainToolkit.velocityHydraulicErosionPresetData preset)
	{
		this.erosionTypeInt = 1;
		this.erosionType = global::TerrainToolkit.ErosionType.Hydraulic;
		this.hydraulicTypeInt = 2;
		this.hydraulicType = global::TerrainToolkit.HydraulicType.Velocity;
		this.hydraulicIterations = preset.hydraulicIterations;
		this.hydraulicVelocityRainfall = preset.hydraulicVelocityRainfall;
		this.hydraulicVelocityEvaporation = preset.hydraulicVelocityEvaporation;
		this.hydraulicVelocitySedimentSolubility = preset.hydraulicVelocitySedimentSolubility;
		this.hydraulicVelocitySedimentSaturation = preset.hydraulicVelocitySedimentSaturation;
		this.hydraulicVelocity = preset.hydraulicVelocity;
		this.hydraulicMomentum = preset.hydraulicMomentum;
		this.hydraulicEntropy = preset.hydraulicEntropy;
		this.hydraulicDowncutting = preset.hydraulicDowncutting;
	}

	// Token: 0x06004ECB RID: 20171 RVA: 0x001475FC File Offset: 0x001457FC
	public void setTidalErosionPreset(global::TerrainToolkit.tidalErosionPresetData preset)
	{
		this.erosionTypeInt = 2;
		this.erosionType = global::TerrainToolkit.ErosionType.Tidal;
		this.tidalIterations = preset.tidalIterations;
		this.tidalRangeAmount = preset.tidalRangeAmount;
		this.tidalCliffLimit = preset.tidalCliffLimit;
	}

	// Token: 0x06004ECC RID: 20172 RVA: 0x0014763C File Offset: 0x0014583C
	public void setWindErosionPreset(global::TerrainToolkit.windErosionPresetData preset)
	{
		this.erosionTypeInt = 3;
		this.erosionType = global::TerrainToolkit.ErosionType.Wind;
		this.windIterations = preset.windIterations;
		this.windDirection = preset.windDirection;
		this.windForce = preset.windForce;
		this.windLift = preset.windLift;
		this.windGravity = preset.windGravity;
		this.windCapacity = preset.windCapacity;
		this.windEntropy = preset.windEntropy;
		this.windSmoothing = preset.windSmoothing;
	}

	// Token: 0x06004ECD RID: 20173 RVA: 0x001476B8 File Offset: 0x001458B8
	public void Update()
	{
		if (this.isBrushOn && (this.toolModeInt != 1 || this.erosionTypeInt > 2 || (this.erosionTypeInt == 1 && this.hydraulicTypeInt > 0)))
		{
			this.isBrushOn = false;
		}
	}

	// Token: 0x06004ECE RID: 20174 RVA: 0x00147708 File Offset: 0x00145908
	public void OnDrawGizmos()
	{
		Terrain terrain = (Terrain)base.GetComponent(typeof(Terrain));
		if (terrain == null)
		{
			return;
		}
		if (this.isBrushOn && !this.isBrushHidden)
		{
			if (this.isBrushPainting)
			{
				Gizmos.color = Color.red;
			}
			else
			{
				Gizmos.color = Color.white;
			}
			float num = this.brushSize / 4f;
			Gizmos.DrawLine(this.brushPosition + new Vector3(-num, 0f, 0f), this.brushPosition + new Vector3(num, 0f, 0f));
			Gizmos.DrawLine(this.brushPosition + new Vector3(0f, -num, 0f), this.brushPosition + new Vector3(0f, num, 0f));
			Gizmos.DrawLine(this.brushPosition + new Vector3(0f, 0f, -num), this.brushPosition + new Vector3(0f, 0f, num));
			Gizmos.DrawWireCube(this.brushPosition, new Vector3(this.brushSize, 0f, this.brushSize));
			Gizmos.DrawWireSphere(this.brushPosition, this.brushSize / 2f);
		}
		TerrainData terrainData = terrain.terrainData;
		Vector3 size = terrainData.size;
		if (this.toolModeInt == 1 && this.erosionTypeInt == 2)
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawWireCube(new Vector3(base.transform.position.x + size.x / 2f, this.tidalSeaLevel, base.transform.position.z + size.z / 2f), new Vector3(size.x, 0f, size.z));
			Gizmos.color = Color.white;
			Gizmos.DrawWireCube(new Vector3(base.transform.position.x + size.x / 2f, this.tidalSeaLevel, base.transform.position.z + size.z / 2f), new Vector3(size.x, this.tidalRangeAmount * 2f, size.z));
		}
		if (this.toolModeInt == 1 && this.erosionTypeInt == 3)
		{
			Gizmos.color = Color.blue;
			Quaternion quaternion = Quaternion.Euler(0f, this.windDirection, 0f);
			Vector3 vector = quaternion * Vector3.forward;
			Vector3 vector2;
			vector2..ctor(base.transform.position.x + size.x / 2f, base.transform.position.y + size.y, base.transform.position.z + size.z / 2f);
			Vector3 vector3 = vector2 + vector * (size.x / 4f);
			Vector3 vector4 = vector2 + vector * (size.x / 6f);
			Gizmos.DrawLine(vector2, vector3);
			Gizmos.DrawLine(vector3, vector4 + new Vector3(0f, size.x / 16f, 0f));
			Gizmos.DrawLine(vector3, vector4 - new Vector3(0f, size.x / 16f, 0f));
		}
	}

	// Token: 0x06004ECF RID: 20175 RVA: 0x00147ACC File Offset: 0x00145CCC
	public void paint()
	{
		this.convertIntVarsToEnums();
		this.erodeTerrainWithBrush();
	}

	// Token: 0x06004ED0 RID: 20176 RVA: 0x00147ADC File Offset: 0x00145CDC
	private void erodeTerrainWithBrush()
	{
		this.erosionMode = global::TerrainToolkit.ErosionMode.Brush;
		Terrain terrain = (Terrain)base.GetComponent(typeof(Terrain));
		if (terrain == null)
		{
			return;
		}
		try
		{
			TerrainData terrainData = terrain.terrainData;
			int heightmapWidth = terrainData.heightmapWidth;
			int heightmapHeight = terrainData.heightmapHeight;
			Vector3 size = terrainData.size;
			int num = (int)Mathf.Floor((float)heightmapWidth / size.x * this.brushSize);
			int num2 = (int)Mathf.Floor((float)heightmapHeight / size.z * this.brushSize);
			Vector3 vector = base.transform.InverseTransformPoint(this.brushPosition);
			int num3 = (int)Mathf.Round(vector.x / size.x * (float)heightmapWidth - (float)(num / 2));
			int num4 = (int)Mathf.Round(vector.z / size.z * (float)heightmapHeight - (float)(num2 / 2));
			if (num3 < 0)
			{
				num += num3;
				num3 = 0;
			}
			if (num4 < 0)
			{
				num2 += num4;
				num4 = 0;
			}
			if (num3 + num > heightmapWidth)
			{
				num = heightmapWidth - num3;
			}
			if (num4 + num2 > heightmapHeight)
			{
				num2 = heightmapHeight - num4;
			}
			float[,] heights = terrainData.GetHeights(num3, num4, num, num2);
			num = heights.GetLength(1);
			num2 = heights.GetLength(0);
			float[,] array = (float[,])heights.Clone();
			global::TerrainToolkit.ErosionProgressDelegate erosionProgressDelegate = new global::TerrainToolkit.ErosionProgressDelegate(this.dummyErosionProgress);
			array = this.fastErosion(array, new Vector2((float)num, (float)num2), 1, erosionProgressDelegate);
			float num5 = (float)num / 2f;
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < num2; j++)
				{
					float num6 = heights[j, i];
					float num7 = array[j, i];
					float num8 = Vector2.Distance(new Vector2((float)j, (float)i), new Vector2(num5, num5));
					float num9 = 1f - (num8 - (num5 - num5 * this.brushSoftness)) / (num5 * this.brushSoftness);
					if (num9 < 0f)
					{
						num9 = 0f;
					}
					else if (num9 > 1f)
					{
						num9 = 1f;
					}
					num9 *= this.brushOpacity;
					float num10 = num7 * num9 + num6 * (1f - num9);
					heights[j, i] = num10;
				}
			}
			terrainData.SetHeights(num3, num4, heights);
		}
		catch (Exception arg)
		{
			Debug.LogError("A brush error occurred: " + arg);
		}
	}

	// Token: 0x06004ED1 RID: 20177 RVA: 0x00147D7C File Offset: 0x00145F7C
	public void erodeAllTerrain(global::TerrainToolkit.ErosionProgressDelegate erosionProgressDelegate)
	{
		this.erosionMode = global::TerrainToolkit.ErosionMode.Filter;
		this.convertIntVarsToEnums();
		Terrain terrain = (Terrain)base.GetComponent(typeof(Terrain));
		if (terrain == null)
		{
			return;
		}
		try
		{
			TerrainData terrainData = terrain.terrainData;
			int heightmapWidth = terrainData.heightmapWidth;
			int heightmapHeight = terrainData.heightmapHeight;
			float[,] array = terrainData.GetHeights(0, 0, heightmapWidth, heightmapHeight);
			switch (this.erosionType)
			{
			case global::TerrainToolkit.ErosionType.Thermal:
			{
				int iterations = this.thermalIterations;
				array = this.fastErosion(array, new Vector2((float)heightmapWidth, (float)heightmapHeight), iterations, erosionProgressDelegate);
				break;
			}
			case global::TerrainToolkit.ErosionType.Hydraulic:
			{
				int iterations = this.hydraulicIterations;
				switch (this.hydraulicType)
				{
				case global::TerrainToolkit.HydraulicType.Fast:
					array = this.fastErosion(array, new Vector2((float)heightmapWidth, (float)heightmapHeight), iterations, erosionProgressDelegate);
					break;
				case global::TerrainToolkit.HydraulicType.Full:
					array = this.fullHydraulicErosion(array, new Vector2((float)heightmapWidth, (float)heightmapHeight), iterations, erosionProgressDelegate);
					break;
				case global::TerrainToolkit.HydraulicType.Velocity:
					array = this.velocityHydraulicErosion(array, new Vector2((float)heightmapWidth, (float)heightmapHeight), iterations, erosionProgressDelegate);
					break;
				}
				break;
			}
			case global::TerrainToolkit.ErosionType.Tidal:
			{
				Vector3 size = terrainData.size;
				if (this.tidalSeaLevel >= base.transform.position.y && this.tidalSeaLevel <= base.transform.position.y + size.y)
				{
					int iterations = this.tidalIterations;
					array = this.fastErosion(array, new Vector2((float)heightmapWidth, (float)heightmapHeight), iterations, erosionProgressDelegate);
				}
				else
				{
					Debug.LogError("Sea level does not intersect terrain object. Erosion operation failed.");
				}
				break;
			}
			case global::TerrainToolkit.ErosionType.Wind:
			{
				int iterations = this.windIterations;
				array = this.windErosion(array, new Vector2((float)heightmapWidth, (float)heightmapHeight), iterations, erosionProgressDelegate);
				break;
			}
			default:
				return;
			}
			terrainData.SetHeights(0, 0, array);
		}
		catch (Exception arg)
		{
			Debug.LogError("An error occurred: " + arg);
		}
	}

	// Token: 0x06004ED2 RID: 20178 RVA: 0x00147F8C File Offset: 0x0014618C
	private float[,] fastErosion(float[,] heightMap, Vector2 arraySize, int iterations, global::TerrainToolkit.ErosionProgressDelegate erosionProgressDelegate)
	{
		int num = (int)arraySize.y;
		int num2 = (int)arraySize.x;
		float[,] array = new float[num, num2];
		Terrain terrain = (Terrain)base.GetComponent(typeof(Terrain));
		TerrainData terrainData = terrain.terrainData;
		Vector3 size = terrainData.size;
		float num3 = 0f;
		float num4 = 0f;
		float num5 = 0f;
		float num6 = 0f;
		float num7 = 0f;
		float num8 = 0f;
		float num9 = 0f;
		float num10 = 0f;
		float num11 = 0f;
		switch (this.erosionType)
		{
		case global::TerrainToolkit.ErosionType.Thermal:
		{
			num3 = size.x / (float)num * Mathf.Tan(this.thermalMinSlope * 0.0174532924f) / size.y;
			if (num3 > 1f)
			{
				num3 = 1f;
			}
			if (this.thermalFalloff == 1f)
			{
				this.thermalFalloff = 0.999f;
			}
			float num12 = this.thermalMinSlope + (90f - this.thermalMinSlope) * this.thermalFalloff;
			num4 = size.x / (float)num * Mathf.Tan(num12 * 0.0174532924f) / size.y;
			if (num4 > 1f)
			{
				num4 = 1f;
			}
			break;
		}
		case global::TerrainToolkit.ErosionType.Hydraulic:
		{
			num6 = size.x / (float)num * Mathf.Tan(this.hydraulicMaxSlope * 0.0174532924f) / size.y;
			if (this.hydraulicFalloff == 0f)
			{
				this.hydraulicFalloff = 0.001f;
			}
			float num13 = this.hydraulicMaxSlope * (1f - this.hydraulicFalloff);
			num5 = size.x / (float)num * Mathf.Tan(num13 * 0.0174532924f) / size.y;
			break;
		}
		case global::TerrainToolkit.ErosionType.Tidal:
			num7 = (this.tidalSeaLevel - base.transform.position.y) / (base.transform.position.y + size.y);
			num8 = (this.tidalSeaLevel - base.transform.position.y - this.tidalRangeAmount) / (base.transform.position.y + size.y);
			num9 = (this.tidalSeaLevel - base.transform.position.y + this.tidalRangeAmount) / (base.transform.position.y + size.y);
			num10 = num9 - num7;
			num11 = size.x / (float)num * Mathf.Tan(this.tidalCliffLimit * 0.0174532924f) / size.y;
			break;
		default:
			return heightMap;
		}
		for (int i = 0; i < iterations; i++)
		{
			for (int j = 0; j < num2; j++)
			{
				int num14;
				int num15;
				int num16;
				if (j == 0)
				{
					num14 = 2;
					num15 = 0;
					num16 = 0;
				}
				else if (j == num2 - 1)
				{
					num14 = 2;
					num15 = -1;
					num16 = 1;
				}
				else
				{
					num14 = 3;
					num15 = -1;
					num16 = 1;
				}
				for (int k = 0; k < num; k++)
				{
					int num17;
					int num18;
					int num19;
					if (k == 0)
					{
						num17 = 2;
						num18 = 0;
						num19 = 0;
					}
					else if (k == num - 1)
					{
						num17 = 2;
						num18 = -1;
						num19 = 1;
					}
					else
					{
						num17 = 3;
						num18 = -1;
						num19 = 1;
					}
					float num20 = 1f;
					float num21 = 0f;
					float num22 = 0f;
					float num23 = heightMap[k + num19 + num18, j + num16 + num15];
					float num24 = num23;
					int num25 = 0;
					for (int l = 0; l < num14; l++)
					{
						for (int m = 0; m < num17; m++)
						{
							if ((m != num19 || l != num16) && (this.neighbourhood == global::TerrainToolkit.Neighbourhood.Moore || (this.neighbourhood == global::TerrainToolkit.Neighbourhood.VonNeumann && (m == num19 || l == num16))))
							{
								float num26 = heightMap[k + m + num18, j + l + num15];
								num24 += num26;
								float num27 = num23 - num26;
								if (num27 > 0f)
								{
									num22 += num27;
									if (num27 < num20)
									{
										num20 = num27;
									}
									if (num27 > num21)
									{
										num21 = num27;
									}
								}
								num25++;
							}
						}
					}
					float num28 = num22 / (float)num25;
					bool flag = false;
					switch (this.erosionType)
					{
					case global::TerrainToolkit.ErosionType.Thermal:
						if (num28 >= num3)
						{
							flag = true;
						}
						break;
					case global::TerrainToolkit.ErosionType.Hydraulic:
						if (num28 > 0f && num28 <= num6)
						{
							flag = true;
						}
						break;
					case global::TerrainToolkit.ErosionType.Tidal:
						if (num28 > 0f && num28 <= num11 && num23 < num9 && num23 > num8)
						{
							flag = true;
						}
						break;
					default:
						return heightMap;
					}
					if (flag)
					{
						if (this.erosionType == global::TerrainToolkit.ErosionType.Tidal)
						{
							float num29 = num24 / (float)(num25 + 1);
							float num30 = Mathf.Abs(num7 - num23);
							float num31 = num30 / num10;
							float num32 = num23 * num31 + num29 * (1f - num31);
							float num33 = Mathf.Pow(num30, 3f);
							heightMap[k + num19 + num18, j + num16 + num15] = num7 * num33 + num32 * (1f - num33);
						}
						else
						{
							float num31;
							if (this.erosionType == global::TerrainToolkit.ErosionType.Thermal)
							{
								if (num28 > num4)
								{
									num31 = 1f;
								}
								else
								{
									float num34 = num4 - num3;
									num31 = (num28 - num3) / num34;
								}
							}
							else if (num28 < num5)
							{
								num31 = 1f;
							}
							else
							{
								float num34 = num6 - num5;
								num31 = 1f - (num28 - num5) / num34;
							}
							float num35 = num20 / 2f * num31;
							float num36 = heightMap[k + num19 + num18, j + num16 + num15];
							if (this.erosionMode == global::TerrainToolkit.ErosionMode.Filter || (this.erosionMode == global::TerrainToolkit.ErosionMode.Brush && this.useDifferenceMaps))
							{
								float num37 = array[k + num19 + num18, j + num16 + num15];
								float num38 = num37 - num35;
								array[k + num19 + num18, j + num16 + num15] = num38;
							}
							else
							{
								float num39 = num36 - num35;
								if (num39 < 0f)
								{
									num39 = 0f;
								}
								heightMap[k + num19 + num18, j + num16 + num15] = num39;
							}
							for (int l = 0; l < num14; l++)
							{
								for (int m = 0; m < num17; m++)
								{
									if ((m != num19 || l != num16) && (this.neighbourhood == global::TerrainToolkit.Neighbourhood.Moore || (this.neighbourhood == global::TerrainToolkit.Neighbourhood.VonNeumann && (m == num19 || l == num16))))
									{
										float num40 = heightMap[k + m + num18, j + l + num15];
										float num27 = num36 - num40;
										if (num27 > 0f)
										{
											float num41 = num35 * (num27 / num22);
											if (this.erosionMode == global::TerrainToolkit.ErosionMode.Filter || (this.erosionMode == global::TerrainToolkit.ErosionMode.Brush && this.useDifferenceMaps))
											{
												float num42 = array[k + m + num18, j + l + num15];
												float num43 = num42 + num41;
												array[k + m + num18, j + l + num15] = num43;
											}
											else
											{
												num40 += num41;
												if (num40 < 0f)
												{
													num40 = 0f;
												}
												heightMap[k + m + num18, j + l + num15] = num40;
											}
										}
									}
								}
							}
						}
					}
				}
			}
			if ((this.erosionMode == global::TerrainToolkit.ErosionMode.Filter || (this.erosionMode == global::TerrainToolkit.ErosionMode.Brush && this.useDifferenceMaps)) && this.erosionType != global::TerrainToolkit.ErosionType.Tidal)
			{
				for (int j = 0; j < num2; j++)
				{
					for (int k = 0; k < num; k++)
					{
						float num44 = heightMap[k, j] + array[k, j];
						if (num44 > 1f)
						{
							num44 = 1f;
						}
						else if (num44 < 0f)
						{
							num44 = 0f;
						}
						heightMap[k, j] = num44;
						array[k, j] = 0f;
					}
				}
			}
			if (this.erosionMode == global::TerrainToolkit.ErosionMode.Filter)
			{
				string titleString = string.Empty;
				string displayString = string.Empty;
				switch (this.erosionType)
				{
				case global::TerrainToolkit.ErosionType.Thermal:
					titleString = "Applying Thermal Erosion";
					displayString = "Applying thermal erosion.";
					break;
				case global::TerrainToolkit.ErosionType.Hydraulic:
					titleString = "Applying Hydraulic Erosion";
					displayString = "Applying hydraulic erosion.";
					break;
				case global::TerrainToolkit.ErosionType.Tidal:
					titleString = "Applying Tidal Erosion";
					displayString = "Applying tidal erosion.";
					break;
				default:
					return heightMap;
				}
				float percentComplete = (float)i / (float)iterations;
				erosionProgressDelegate(titleString, displayString, i, iterations, percentComplete);
			}
		}
		return heightMap;
	}

	// Token: 0x06004ED3 RID: 20179 RVA: 0x00148884 File Offset: 0x00146A84
	private float[,] velocityHydraulicErosion(float[,] heightMap, Vector2 arraySize, int iterations, global::TerrainToolkit.ErosionProgressDelegate erosionProgressDelegate)
	{
		int num = (int)arraySize.x;
		int num2 = (int)arraySize.y;
		float[,] array = new float[num, num2];
		float[,] array2 = new float[num, num2];
		float[,] array3 = new float[num, num2];
		float[,] array4 = new float[num, num2];
		float[,] array5 = new float[num, num2];
		float[,] array6 = new float[num, num2];
		float[,] array7 = new float[num, num2];
		float[,] array8 = new float[num, num2];
		for (int i = 0; i < num2; i++)
		{
			for (int j = 0; j < num; j++)
			{
				array3[j, i] = 0f;
				array4[j, i] = 0f;
				array5[j, i] = 0f;
				array6[j, i] = 0f;
				array7[j, i] = 0f;
				array8[j, i] = 0f;
			}
		}
		for (int i = 0; i < num2; i++)
		{
			for (int j = 0; j < num; j++)
			{
				float num3 = heightMap[j, i];
				array[j, i] = num3;
			}
		}
		for (int i = 0; i < num2; i++)
		{
			int num4;
			int num5;
			int num6;
			if (i == 0)
			{
				num4 = 2;
				num5 = 0;
				num6 = 0;
			}
			else if (i == num2 - 1)
			{
				num4 = 2;
				num5 = -1;
				num6 = 1;
			}
			else
			{
				num4 = 3;
				num5 = -1;
				num6 = 1;
			}
			for (int j = 0; j < num; j++)
			{
				int num7;
				int num8;
				int num9;
				if (j == 0)
				{
					num7 = 2;
					num8 = 0;
					num9 = 0;
				}
				else if (j == num - 1)
				{
					num7 = 2;
					num8 = -1;
					num9 = 1;
				}
				else
				{
					num7 = 3;
					num8 = -1;
					num9 = 1;
				}
				float num10 = 0f;
				float num11 = heightMap[j + num9 + num8, i + num6 + num5];
				int num12 = 0;
				for (int k = 0; k < num4; k++)
				{
					for (int l = 0; l < num7; l++)
					{
						if ((l != num9 || k != num6) && (this.neighbourhood == global::TerrainToolkit.Neighbourhood.Moore || (this.neighbourhood == global::TerrainToolkit.Neighbourhood.VonNeumann && (l == num9 || k == num6))))
						{
							float num13 = heightMap[j + l + num8, i + k + num5];
							float num14 = Mathf.Abs(num11 - num13);
							num10 += num14;
							num12++;
						}
					}
				}
				float num15 = num10 / (float)num12;
				array2[j + num9 + num8, i + num6 + num5] = num15;
			}
		}
		for (int m = 0; m < iterations; m++)
		{
			for (int i = 0; i < num2; i++)
			{
				for (int j = 0; j < num; j++)
				{
					float num16 = array3[j, i] + array[j, i] * this.hydraulicVelocityRainfall;
					if (num16 > 1f)
					{
						num16 = 1f;
					}
					array3[j, i] = num16;
				}
			}
			for (int i = 0; i < num2; i++)
			{
				for (int j = 0; j < num; j++)
				{
					float num17 = array7[j, i];
					float num18 = array3[j, i] * this.hydraulicVelocitySedimentSaturation;
					if (num17 < num18)
					{
						float num19 = array3[j, i] * array5[j, i] * this.hydraulicVelocitySedimentSolubility;
						if (num17 + num19 > num18)
						{
							num19 = num18 - num17;
						}
						float num11 = heightMap[j, i];
						if (num19 > num11)
						{
							num19 = num11;
						}
						array7[j, i] = num17 + num19;
						heightMap[j, i] = num11 - num19;
					}
				}
			}
			for (int i = 0; i < num2; i++)
			{
				int num4;
				int num5;
				int num6;
				if (i == 0)
				{
					num4 = 2;
					num5 = 0;
					num6 = 0;
				}
				else if (i == num2 - 1)
				{
					num4 = 2;
					num5 = -1;
					num6 = 1;
				}
				else
				{
					num4 = 3;
					num5 = -1;
					num6 = 1;
				}
				for (int j = 0; j < num; j++)
				{
					int num7;
					int num8;
					int num9;
					if (j == 0)
					{
						num7 = 2;
						num8 = 0;
						num9 = 0;
					}
					else if (j == num - 1)
					{
						num7 = 2;
						num8 = -1;
						num9 = 1;
					}
					else
					{
						num7 = 3;
						num8 = -1;
						num9 = 1;
					}
					float num10 = 0f;
					float num11 = heightMap[j, i];
					float num20 = num11;
					float num21 = array3[j, i];
					int num12 = 0;
					for (int k = 0; k < num4; k++)
					{
						for (int l = 0; l < num7; l++)
						{
							if ((l != num9 || k != num6) && (this.neighbourhood == global::TerrainToolkit.Neighbourhood.Moore || (this.neighbourhood == global::TerrainToolkit.Neighbourhood.VonNeumann && (l == num9 || k == num6))))
							{
								float num13 = heightMap[j + l + num8, i + k + num5];
								float num22 = array3[j + l + num8, i + k + num5];
								float num14 = num11 + num21 - (num13 + num22);
								if (num14 > 0f)
								{
									num10 += num14;
									num20 += num11 + num21;
									num12++;
								}
							}
						}
					}
					float num23 = array5[j, i];
					float num24 = array2[j, i];
					float num25 = array7[j, i];
					float num26 = num23 + this.hydraulicVelocity * num24;
					float num27 = num20 / (float)(num12 + 1);
					float num28 = num11 + num21 - num27;
					float num29 = Mathf.Min(num21, num28 * (1f + num23));
					float num30 = array4[j, i];
					float num31 = num30 - num29;
					array4[j, i] = num31;
					float num32 = num26 * (num29 / num21);
					float num33 = num25 * (num29 / num21);
					for (int k = 0; k < num4; k++)
					{
						for (int l = 0; l < num7; l++)
						{
							if ((l != num9 || k != num6) && (this.neighbourhood == global::TerrainToolkit.Neighbourhood.Moore || (this.neighbourhood == global::TerrainToolkit.Neighbourhood.VonNeumann && (l == num9 || k == num6))))
							{
								float num13 = heightMap[j + l + num8, i + k + num5];
								float num22 = array3[j + l + num8, i + k + num5];
								float num14 = num11 + num21 - (num13 + num22);
								if (num14 > 0f)
								{
									float num34 = array4[j + l + num8, i + k + num5];
									float num35 = num29 * (num14 / num10);
									float num36 = num34 + num35;
									array4[j + l + num8, i + k + num5] = num36;
									float num37 = array6[j + l + num8, i + k + num5];
									float num38 = num32 * this.hydraulicMomentum * (num14 / num10);
									float num39 = num37 + num38;
									array6[j + l + num8, i + k + num5] = num39;
									float num40 = array8[j + l + num8, i + k + num5];
									float num41 = num33 * this.hydraulicMomentum * (num14 / num10);
									float num42 = num40 + num41;
									array8[j + l + num8, i + k + num5] = num42;
								}
							}
						}
					}
					float num43 = array6[j, i];
					array6[j, i] = num43 - num32;
				}
			}
			for (int i = 0; i < num2; i++)
			{
				for (int j = 0; j < num; j++)
				{
					float num44 = array5[j, i] + array6[j, i];
					num44 *= 1f - this.hydraulicEntropy;
					if (num44 > 1f)
					{
						num44 = 1f;
					}
					else if (num44 < 0f)
					{
						num44 = 0f;
					}
					array5[j, i] = num44;
					array6[j, i] = 0f;
				}
			}
			for (int i = 0; i < num2; i++)
			{
				for (int j = 0; j < num; j++)
				{
					float num45 = array3[j, i] + array4[j, i];
					float num46 = num45 * this.hydraulicVelocityEvaporation;
					num45 -= num46;
					if (num45 > 1f)
					{
						num45 = 1f;
					}
					else if (num45 < 0f)
					{
						num45 = 0f;
					}
					array3[j, i] = num45;
					array4[j, i] = 0f;
				}
			}
			for (int i = 0; i < num2; i++)
			{
				for (int j = 0; j < num; j++)
				{
					float num47 = array7[j, i] + array8[j, i];
					if (num47 > 1f)
					{
						num47 = 1f;
					}
					else if (num47 < 0f)
					{
						num47 = 0f;
					}
					array7[j, i] = num47;
					array8[j, i] = 0f;
				}
			}
			for (int i = 0; i < num2; i++)
			{
				for (int j = 0; j < num; j++)
				{
					float num18 = array3[j, i] * this.hydraulicVelocitySedimentSaturation;
					float num47 = array7[j, i];
					if (num47 > num18)
					{
						float num48 = num47 - num18;
						array7[j, i] = num18;
						float num49 = heightMap[j, i];
						heightMap[j, i] = num49 + num48;
					}
				}
			}
			for (int i = 0; i < num2; i++)
			{
				for (int j = 0; j < num; j++)
				{
					float num44 = array3[j, i];
					float num49 = heightMap[j, i];
					float num50 = 1f - Mathf.Abs(0.5f - num49) * 2f;
					float num51 = this.hydraulicDowncutting * num44 * num50;
					num49 -= num51;
					heightMap[j, i] = num49;
				}
			}
			float percentComplete = (float)m / (float)iterations;
			erosionProgressDelegate("Applying Hydraulic Erosion", "Applying hydraulic erosion.", m, iterations, percentComplete);
		}
		return heightMap;
	}

	// Token: 0x06004ED4 RID: 20180 RVA: 0x00149320 File Offset: 0x00147520
	private float[,] fullHydraulicErosion(float[,] heightMap, Vector2 arraySize, int iterations, global::TerrainToolkit.ErosionProgressDelegate erosionProgressDelegate)
	{
		int num = (int)arraySize.x;
		int num2 = (int)arraySize.y;
		float[,] array = new float[num, num2];
		float[,] array2 = new float[num, num2];
		float[,] array3 = new float[num, num2];
		float[,] array4 = new float[num, num2];
		for (int i = 0; i < num2; i++)
		{
			for (int j = 0; j < num; j++)
			{
				array[j, i] = 0f;
				array2[j, i] = 0f;
				array3[j, i] = 0f;
				array4[j, i] = 0f;
			}
		}
		for (int k = 0; k < iterations; k++)
		{
			for (int i = 0; i < num2; i++)
			{
				for (int j = 0; j < num; j++)
				{
					float num3 = array[j, i] + this.hydraulicRainfall;
					if (num3 > 1f)
					{
						num3 = 1f;
					}
					array[j, i] = num3;
				}
			}
			for (int i = 0; i < num2; i++)
			{
				for (int j = 0; j < num; j++)
				{
					float num4 = array3[j, i];
					float num5 = array[j, i] * this.hydraulicSedimentSaturation;
					if (num4 < num5)
					{
						float num6 = array[j, i] * this.hydraulicSedimentSolubility;
						if (num4 + num6 > num5)
						{
							num6 = num5 - num4;
						}
						float num7 = heightMap[j, i];
						if (num6 > num7)
						{
							num6 = num7;
						}
						array3[j, i] = num4 + num6;
						heightMap[j, i] = num7 - num6;
					}
				}
			}
			for (int i = 0; i < num2; i++)
			{
				int num8;
				int num9;
				int num10;
				if (i == 0)
				{
					num8 = 2;
					num9 = 0;
					num10 = 0;
				}
				else if (i == num2 - 1)
				{
					num8 = 2;
					num9 = -1;
					num10 = 1;
				}
				else
				{
					num8 = 3;
					num9 = -1;
					num10 = 1;
				}
				for (int j = 0; j < num; j++)
				{
					int num11;
					int num12;
					int num13;
					if (j == 0)
					{
						num11 = 2;
						num12 = 0;
						num13 = 0;
					}
					else if (j == num - 1)
					{
						num11 = 2;
						num12 = -1;
						num13 = 1;
					}
					else
					{
						num11 = 3;
						num12 = -1;
						num13 = 1;
					}
					float num14 = 0f;
					float num15 = 0f;
					float num7 = heightMap[j + num13 + num12, i + num10 + num9];
					float num16 = array[j + num13 + num12, i + num10 + num9];
					float num17 = num7;
					int num18 = 0;
					for (int l = 0; l < num8; l++)
					{
						for (int m = 0; m < num11; m++)
						{
							if ((m != num13 || l != num10) && (this.neighbourhood == global::TerrainToolkit.Neighbourhood.Moore || (this.neighbourhood == global::TerrainToolkit.Neighbourhood.VonNeumann && (m == num13 || l == num10))))
							{
								float num19 = heightMap[j + m + num12, i + l + num9];
								float num20 = array[j + m + num12, i + l + num9];
								float num21 = num7 + num16 - (num19 + num20);
								if (num21 > 0f)
								{
									num14 += num21;
									num17 += num19 + num20;
									num18++;
									if (num21 > num15)
									{
									}
								}
							}
						}
					}
					float num22 = num17 / (float)(num18 + 1);
					float num23 = num7 + num16 - num22;
					float num24 = Mathf.Min(num16, num23);
					float num25 = array2[j + num13 + num12, i + num10 + num9];
					float num26 = num25 - num24;
					array2[j + num13 + num12, i + num10 + num9] = num26;
					float num27 = array3[j + num13 + num12, i + num10 + num9];
					float num28 = num27 * (num24 / num16);
					float num29 = array4[j + num13 + num12, i + num10 + num9];
					float num30 = num29 - num28;
					array4[j + num13 + num12, i + num10 + num9] = num30;
					for (int l = 0; l < num8; l++)
					{
						for (int m = 0; m < num11; m++)
						{
							if ((m != num13 || l != num10) && (this.neighbourhood == global::TerrainToolkit.Neighbourhood.Moore || (this.neighbourhood == global::TerrainToolkit.Neighbourhood.VonNeumann && (m == num13 || l == num10))))
							{
								float num19 = heightMap[j + m + num12, i + l + num9];
								float num20 = array[j + m + num12, i + l + num9];
								float num21 = num7 + num16 - (num19 + num20);
								if (num21 > 0f)
								{
									float num31 = array2[j + m + num12, i + l + num9];
									float num32 = num24 * (num21 / num14);
									float num33 = num31 + num32;
									array2[j + m + num12, i + l + num9] = num33;
									float num34 = array4[j + m + num12, i + l + num9];
									float num35 = num28 * (num21 / num14);
									float num36 = num34 + num35;
									array4[j + m + num12, i + l + num9] = num36;
								}
							}
						}
					}
				}
			}
			for (int i = 0; i < num2; i++)
			{
				for (int j = 0; j < num; j++)
				{
					float num37 = array[j, i] + array2[j, i];
					float num38 = num37 * this.hydraulicEvaporation;
					num37 -= num38;
					if (num37 < 0f)
					{
						num37 = 0f;
					}
					array[j, i] = num37;
					array2[j, i] = 0f;
				}
			}
			for (int i = 0; i < num2; i++)
			{
				for (int j = 0; j < num; j++)
				{
					float num39 = array3[j, i] + array4[j, i];
					if (num39 > 1f)
					{
						num39 = 1f;
					}
					else if (num39 < 0f)
					{
						num39 = 0f;
					}
					array3[j, i] = num39;
					array4[j, i] = 0f;
				}
			}
			for (int i = 0; i < num2; i++)
			{
				for (int j = 0; j < num; j++)
				{
					float num5 = array[j, i] * this.hydraulicSedimentSaturation;
					float num39 = array3[j, i];
					if (num39 > num5)
					{
						float num40 = num39 - num5;
						array3[j, i] = num5;
						float num41 = heightMap[j, i];
						heightMap[j, i] = num41 + num40;
					}
				}
			}
			float percentComplete = (float)k / (float)iterations;
			erosionProgressDelegate("Applying Hydraulic Erosion", "Applying hydraulic erosion.", k, iterations, percentComplete);
		}
		return heightMap;
	}

	// Token: 0x06004ED5 RID: 20181 RVA: 0x00149A40 File Offset: 0x00147C40
	private float[,] windErosion(float[,] heightMap, Vector2 arraySize, int iterations, global::TerrainToolkit.ErosionProgressDelegate erosionProgressDelegate)
	{
		Terrain terrain = (Terrain)base.GetComponent(typeof(Terrain));
		TerrainData terrainData = terrain.terrainData;
		Quaternion quaternion = Quaternion.Euler(0f, this.windDirection + 180f, 0f);
		Vector3 vector = quaternion * Vector3.forward;
		int num = (int)arraySize.x;
		int num2 = (int)arraySize.y;
		float[,] array = new float[num, num2];
		float[,] array2 = new float[num, num2];
		float[,] array3 = new float[num, num2];
		float[,] array4 = new float[num, num2];
		float[,] array5 = new float[num, num2];
		float[,] array6 = new float[num, num2];
		float[,] array7 = new float[num, num2];
		for (int i = 0; i < num2; i++)
		{
			for (int j = 0; j < num; j++)
			{
				array[j, i] = 0f;
				array2[j, i] = 0f;
				array3[j, i] = 0f;
				array4[j, i] = 0f;
				array5[j, i] = 0f;
				array6[j, i] = 0f;
				array7[j, i] = 0f;
			}
		}
		for (int k = 0; k < iterations; k++)
		{
			for (int i = 0; i < num2; i++)
			{
				for (int j = 0; j < num; j++)
				{
					float num3 = array3[j, i];
					float num4 = heightMap[j, i];
					float num5 = array5[j, i];
					float num6 = num5 * this.windGravity;
					array5[j, i] = num5 - num6;
					heightMap[j, i] = num4 + num6;
				}
			}
			for (int i = 0; i < num2; i++)
			{
				for (int j = 0; j < num; j++)
				{
					float num7 = heightMap[j, i];
					Vector3 interpolatedNormal = terrainData.GetInterpolatedNormal((float)j / (float)num, (float)i / (float)num2);
					float num8 = (Vector3.Angle(interpolatedNormal, vector) - 90f) / 90f;
					if (num8 < 0f)
					{
						num8 = 0f;
					}
					array[j, i] = num8 * num7;
					float num9 = 1f - Mathf.Abs(Vector3.Angle(interpolatedNormal, vector) - 90f) / 90f;
					array2[j, i] = num9 * num7;
					float num10 = num9 * num7 * this.windForce;
					float num11 = array3[j, i];
					float num12 = num11 + num10;
					array3[j, i] = num12;
					float num13 = array5[j, i];
					float num14 = this.windLift * num12;
					if (num13 + num14 > this.windCapacity)
					{
						num14 = this.windCapacity - num13;
					}
					array5[j, i] = num13 + num14;
					heightMap[j, i] = num7 - num14;
				}
			}
			for (int i = 0; i < num2; i++)
			{
				int num15;
				int num16;
				int num17;
				if (i == 0)
				{
					num15 = 2;
					num16 = 0;
					num17 = 0;
				}
				else if (i == num2 - 1)
				{
					num15 = 2;
					num16 = -1;
					num17 = 1;
				}
				else
				{
					num15 = 3;
					num16 = -1;
					num17 = 1;
				}
				for (int j = 0; j < num; j++)
				{
					int num18;
					int num19;
					int num20;
					if (j == 0)
					{
						num18 = 2;
						num19 = 0;
						num20 = 0;
					}
					else if (j == num - 1)
					{
						num18 = 2;
						num19 = -1;
						num20 = 1;
					}
					else
					{
						num18 = 3;
						num19 = -1;
						num20 = 1;
					}
					float num21 = array2[j, i];
					float num22 = array[j, i];
					float num13 = array5[j, i];
					for (int l = 0; l < num15; l++)
					{
						for (int m = 0; m < num18; m++)
						{
							if ((m != num20 || l != num17) && (this.neighbourhood == global::TerrainToolkit.Neighbourhood.Moore || (this.neighbourhood == global::TerrainToolkit.Neighbourhood.VonNeumann && (m == num20 || l == num17))))
							{
								Vector3 vector2;
								vector2..ctor((float)(m + num19), 0f, (float)(-1 * (l + num16)));
								float num23 = (90f - Vector3.Angle(vector2, vector)) / 90f;
								if (num23 < 0f)
								{
									num23 = 0f;
								}
								float num24 = array4[j + m + num19, i + l + num16];
								float num25 = num23 * (num21 - num22) * 0.1f;
								if (num25 < 0f)
								{
									num25 = 0f;
								}
								float num26 = num24 + num25;
								array4[j + m + num19, i + l + num16] = num26;
								float num27 = array4[j, i];
								float num28 = num27 - num25;
								array4[j, i] = num28;
								float num29 = array6[j + m + num19, i + l + num16];
								float num30 = num13 * num25;
								float num31 = num29 + num30;
								array6[j + m + num19, i + l + num16] = num31;
								float num32 = array6[j, i];
								float num33 = num32 - num30;
								array6[j, i] = num33;
							}
						}
					}
				}
			}
			for (int i = 0; i < num2; i++)
			{
				for (int j = 0; j < num; j++)
				{
					float num34 = array5[j, i] + array6[j, i];
					if (num34 > 1f)
					{
						num34 = 1f;
					}
					else if (num34 < 0f)
					{
						num34 = 0f;
					}
					array5[j, i] = num34;
					array6[j, i] = 0f;
				}
			}
			for (int i = 0; i < num2; i++)
			{
				for (int j = 0; j < num; j++)
				{
					float num3 = array3[j, i] + array4[j, i];
					num3 *= 1f - this.windEntropy;
					if (num3 > 1f)
					{
						num3 = 1f;
					}
					else if (num3 < 0f)
					{
						num3 = 0f;
					}
					array3[j, i] = num3;
					array4[j, i] = 0f;
				}
			}
			this.smoothIterations = 1;
			this.smoothBlend = 0.25f;
			float[,] array8 = (float[,])heightMap.Clone();
			global::TerrainToolkit.GeneratorProgressDelegate generatorProgressDelegate = new global::TerrainToolkit.GeneratorProgressDelegate(this.dummyGeneratorProgress);
			array8 = this.smooth(array8, arraySize, generatorProgressDelegate);
			for (int i = 0; i < num2; i++)
			{
				for (int j = 0; j < num; j++)
				{
					float num35 = heightMap[j, i];
					float num36 = array8[j, i];
					float num37 = array[j, i] * this.windSmoothing;
					float num38 = num36 * num37 + num35 * (1f - num37);
					heightMap[j, i] = num38;
				}
			}
			float percentComplete = (float)k / (float)iterations;
			erosionProgressDelegate("Applying Wind Erosion", "Applying wind erosion.", k, iterations, percentComplete);
		}
		return heightMap;
	}

	// Token: 0x06004ED6 RID: 20182 RVA: 0x0014A1B0 File Offset: 0x001483B0
	public void textureTerrain(global::TerrainToolkit.TextureProgressDelegate textureProgressDelegate)
	{
		Terrain terrain = (Terrain)base.GetComponent(typeof(Terrain));
		if (terrain == null)
		{
			return;
		}
		TerrainData terrainData = terrain.terrainData;
		this.splatPrototypes = terrainData.splatPrototypes;
		int num = this.splatPrototypes.Length;
		if (num < 2)
		{
			Debug.LogError("Error: You must assign at least 2 textures.");
			return;
		}
		textureProgressDelegate("Procedural Terrain Texture", "Generating height and slope maps. Please wait.", 0.1f);
		int num2 = terrainData.heightmapWidth - 1;
		int num3 = terrainData.heightmapHeight - 1;
		float[,] array = new float[num2, num3];
		float[,] array2 = new float[num2, num3];
		terrainData.alphamapResolution = num2;
		float[,,] alphamaps = terrainData.GetAlphamaps(0, 0, num2, num2);
		Vector3 size = terrainData.size;
		float num4 = size.x / (float)num2 * Mathf.Tan(this.slopeBlendMinAngle * 0.0174532924f) / size.y;
		float num5 = size.x / (float)num2 * Mathf.Tan(this.slopeBlendMaxAngle * 0.0174532924f) / size.y;
		try
		{
			float num6 = 0f;
			float[,] heights = terrainData.GetHeights(0, 0, num2, num3);
			for (int i = 0; i < num3; i++)
			{
				int num7;
				int num8;
				int num9;
				if (i == 0)
				{
					num7 = 2;
					num8 = 0;
					num9 = 0;
				}
				else if (i == num3 - 1)
				{
					num7 = 2;
					num8 = -1;
					num9 = 1;
				}
				else
				{
					num7 = 3;
					num8 = -1;
					num9 = 1;
				}
				for (int j = 0; j < num2; j++)
				{
					int num10;
					int num11;
					int num12;
					if (j == 0)
					{
						num10 = 2;
						num11 = 0;
						num12 = 0;
					}
					else if (j == num2 - 1)
					{
						num10 = 2;
						num11 = -1;
						num12 = 1;
					}
					else
					{
						num10 = 3;
						num11 = -1;
						num12 = 1;
					}
					float num13 = heights[j + num12 + num11, i + num9 + num8];
					if (num13 > num6)
					{
						num6 = num13;
					}
					array[j, i] = num13;
					float num14 = 0f;
					float num15 = (float)(num10 * num7 - 1);
					for (int k = 0; k < num7; k++)
					{
						for (int l = 0; l < num10; l++)
						{
							if (l != num12 || k != num9)
							{
								float num16 = Mathf.Abs(num13 - heights[j + l + num11, i + k + num8]);
								num14 += num16;
							}
						}
					}
					float num17 = num14 / num15;
					array2[j, i] = num17;
				}
			}
			for (int m = 0; m < num3; m++)
			{
				for (int n = 0; n < num2; n++)
				{
					float num18 = array2[n, m];
					if (num18 < num4)
					{
						num18 = 0f;
					}
					else if (num18 < num5)
					{
						num18 = (num18 - num4) / (num5 - num4);
					}
					else if (num18 > num5)
					{
						num18 = 1f;
					}
					array2[n, m] = num18;
					alphamaps[n, m, 0] = num18;
				}
			}
			for (int num19 = 1; num19 < num; num19++)
			{
				for (int m = 0; m < num3; m++)
				{
					for (int n = 0; n < num2; n++)
					{
						float num20 = 0f;
						float num21 = 0f;
						float num22 = 1f;
						float num23 = 1f;
						float num24 = 0f;
						if (num19 > 1)
						{
							num20 = this.heightBlendPoints[num19 * 2 - 4];
							num21 = this.heightBlendPoints[num19 * 2 - 3];
						}
						if (num19 < num - 1)
						{
							num22 = this.heightBlendPoints[num19 * 2 - 2];
							num23 = this.heightBlendPoints[num19 * 2 - 1];
						}
						float num25 = array[n, m];
						if (num25 >= num21 && num25 <= num22)
						{
							num24 = 1f;
						}
						else if (num25 >= num20 && num25 < num21)
						{
							num24 = (num25 - num20) / (num21 - num20);
						}
						else if (num25 > num22 && num25 <= num23)
						{
							num24 = 1f - (num25 - num22) / (num23 - num22);
						}
						float num26 = array2[n, m];
						num24 -= num26;
						if (num24 < 0f)
						{
							num24 = 0f;
						}
						alphamaps[n, m, num19] = num24;
					}
				}
			}
			textureProgressDelegate("Procedural Terrain Texture", "Generating splat map. Please wait.", 0.9f);
			terrainData.SetAlphamaps(0, 0, alphamaps);
		}
		catch (Exception arg)
		{
			Debug.LogError("An error occurred: " + arg);
		}
	}

	// Token: 0x06004ED7 RID: 20183 RVA: 0x0014A690 File Offset: 0x00148890
	public void addSplatPrototype(Texture2D tex, int index)
	{
		SplatPrototype[] array = new SplatPrototype[index + 1];
		for (int i = 0; i <= index; i++)
		{
			array[i] = new SplatPrototype();
			if (i == index)
			{
				array[i].texture = tex;
				array[i].tileSize = new Vector2(15f, 15f);
			}
			else
			{
				array[i].texture = this.splatPrototypes[i].texture;
				array[i].tileSize = this.splatPrototypes[i].tileSize;
			}
		}
		this.splatPrototypes = array;
		if (index + 1 > 2)
		{
			this.addBlendPoints();
		}
	}

	// Token: 0x06004ED8 RID: 20184 RVA: 0x0014A72C File Offset: 0x0014892C
	public void deleteSplatPrototype(Texture2D tex, int index)
	{
		int num = this.splatPrototypes.Length;
		SplatPrototype[] array = new SplatPrototype[num - 1];
		int num2 = 0;
		for (int i = 0; i < num; i++)
		{
			if (i != index)
			{
				array[num2] = new SplatPrototype();
				array[num2].texture = this.splatPrototypes[i].texture;
				array[num2].tileSize = this.splatPrototypes[i].tileSize;
				num2++;
			}
		}
		this.splatPrototypes = array;
		if (num - 1 > 1)
		{
			this.deleteBlendPoints();
		}
	}

	// Token: 0x06004ED9 RID: 20185 RVA: 0x0014A7B4 File Offset: 0x001489B4
	public void deleteAllSplatPrototypes()
	{
		SplatPrototype[] array = new SplatPrototype[0];
		this.splatPrototypes = array;
	}

	// Token: 0x06004EDA RID: 20186 RVA: 0x0014A7D0 File Offset: 0x001489D0
	public void addBlendPoints()
	{
		float num = 0f;
		if (this.heightBlendPoints.Count > 0)
		{
			num = this.heightBlendPoints[this.heightBlendPoints.Count - 1];
		}
		float item = num + (1f - num) * 0.33f;
		this.heightBlendPoints.Add(item);
		item = num + (1f - num) * 0.66f;
		this.heightBlendPoints.Add(item);
	}

	// Token: 0x06004EDB RID: 20187 RVA: 0x0014A848 File Offset: 0x00148A48
	public void deleteBlendPoints()
	{
		if (this.heightBlendPoints.Count > 0)
		{
			this.heightBlendPoints.RemoveAt(this.heightBlendPoints.Count - 1);
		}
		if (this.heightBlendPoints.Count > 0)
		{
			this.heightBlendPoints.RemoveAt(this.heightBlendPoints.Count - 1);
		}
	}

	// Token: 0x06004EDC RID: 20188 RVA: 0x0014A8A8 File Offset: 0x00148AA8
	public void deleteAllBlendPoints()
	{
		this.heightBlendPoints = new List<float>();
	}

	// Token: 0x06004EDD RID: 20189 RVA: 0x0014A8B8 File Offset: 0x00148AB8
	public void generateTerrain(global::TerrainToolkit.GeneratorProgressDelegate generatorProgressDelegate)
	{
		this.convertIntVarsToEnums();
		Terrain terrain = (Terrain)base.GetComponent(typeof(Terrain));
		if (terrain == null)
		{
			return;
		}
		TerrainData terrainData = terrain.terrainData;
		int heightmapWidth = terrainData.heightmapWidth;
		int heightmapHeight = terrainData.heightmapHeight;
		float[,] heights = terrainData.GetHeights(0, 0, heightmapWidth, heightmapHeight);
		float[,] array = (float[,])heights.Clone();
		switch (this.generatorType)
		{
		case global::TerrainToolkit.GeneratorType.Voronoi:
			array = this.generateVoronoi(array, new Vector2((float)heightmapWidth, (float)heightmapHeight), generatorProgressDelegate);
			break;
		case global::TerrainToolkit.GeneratorType.DiamondSquare:
			array = this.generateDiamondSquare(array, new Vector2((float)heightmapWidth, (float)heightmapHeight), generatorProgressDelegate);
			break;
		case global::TerrainToolkit.GeneratorType.Perlin:
			array = this.generatePerlin(array, new Vector2((float)heightmapWidth, (float)heightmapHeight), generatorProgressDelegate);
			break;
		case global::TerrainToolkit.GeneratorType.Smooth:
			array = this.smooth(array, new Vector2((float)heightmapWidth, (float)heightmapHeight), generatorProgressDelegate);
			break;
		case global::TerrainToolkit.GeneratorType.Normalise:
			array = this.normalise(array, new Vector2((float)heightmapWidth, (float)heightmapHeight), generatorProgressDelegate);
			break;
		default:
			return;
		}
		for (int i = 0; i < heightmapHeight; i++)
		{
			for (int j = 0; j < heightmapWidth; j++)
			{
				float num = heights[j, i];
				float num2 = array[j, i];
				float num3 = 0f;
				switch (this.generatorType)
				{
				case global::TerrainToolkit.GeneratorType.Voronoi:
					num3 = num2 * this.voronoiBlend + num * (1f - this.voronoiBlend);
					break;
				case global::TerrainToolkit.GeneratorType.DiamondSquare:
					num3 = num2 * this.diamondSquareBlend + num * (1f - this.diamondSquareBlend);
					break;
				case global::TerrainToolkit.GeneratorType.Perlin:
					num3 = num2 * this.perlinBlend + num * (1f - this.perlinBlend);
					break;
				case global::TerrainToolkit.GeneratorType.Smooth:
					num3 = num2 * this.smoothBlend + num * (1f - this.smoothBlend);
					break;
				case global::TerrainToolkit.GeneratorType.Normalise:
					num3 = num2 * this.normaliseBlend + num * (1f - this.normaliseBlend);
					break;
				}
				heights[j, i] = num3;
			}
		}
		terrainData.SetHeights(0, 0, heights);
	}

	// Token: 0x06004EDE RID: 20190 RVA: 0x0014AAF0 File Offset: 0x00148CF0
	private float[,] generateVoronoi(float[,] heightMap, Vector2 arraySize, global::TerrainToolkit.GeneratorProgressDelegate generatorProgressDelegate)
	{
		int num = (int)arraySize.x;
		int num2 = (int)arraySize.y;
		ArrayList arrayList = new ArrayList();
		for (int i = 0; i < this.voronoiCells; i++)
		{
			global::TerrainToolkit.Peak peak = default(global::TerrainToolkit.Peak);
			int num3 = (int)Mathf.Floor(Random.value * (float)num);
			int num4 = (int)Mathf.Floor(Random.value * (float)num2);
			float peakHeight = Random.value;
			if (Random.value > this.voronoiFeatures)
			{
				peakHeight = 0f;
			}
			peak.peakPoint = new Vector2((float)num3, (float)num4);
			peak.peakHeight = peakHeight;
			arrayList.Add(peak);
		}
		float num5 = 0f;
		for (int j = 0; j < num2; j++)
		{
			for (int k = 0; k < num; k++)
			{
				ArrayList arrayList2 = new ArrayList();
				for (int i = 0; i < this.voronoiCells; i++)
				{
					Vector2 peakPoint = ((global::TerrainToolkit.Peak)arrayList[i]).peakPoint;
					float dist = Vector2.Distance(peakPoint, new Vector2((float)k, (float)j));
					arrayList2.Add(new global::TerrainToolkit.PeakDistance
					{
						id = i,
						dist = dist
					});
				}
				arrayList2.Sort();
				global::TerrainToolkit.PeakDistance peakDistance = (global::TerrainToolkit.PeakDistance)arrayList2[0];
				global::TerrainToolkit.PeakDistance peakDistance2 = (global::TerrainToolkit.PeakDistance)arrayList2[1];
				int id = peakDistance.id;
				float dist2 = peakDistance.dist;
				float dist3 = peakDistance2.dist;
				float num6 = Mathf.Abs(dist2 - dist3) / ((float)(num + num2) / Mathf.Sqrt((float)this.voronoiCells));
				float num7 = ((global::TerrainToolkit.Peak)arrayList[id]).peakHeight;
				float num8 = num7 - Mathf.Abs(dist2 / dist3) * num7;
				switch (this.voronoiType)
				{
				case global::TerrainToolkit.VoronoiType.Sine:
				{
					float num9 = num8 * 3.14159274f - 1.57079637f;
					num8 = 0.5f + Mathf.Sin(num9) / 2f;
					break;
				}
				case global::TerrainToolkit.VoronoiType.Tangent:
				{
					float num9 = num8 * 3.14159274f / 2f;
					num8 = 0.5f + Mathf.Tan(num9) / 2f;
					break;
				}
				}
				num8 = num8 * num6 * this.voronoiScale + num8 * (1f - this.voronoiScale);
				if (num8 < 0f)
				{
					num8 = 0f;
				}
				else if (num8 > 1f)
				{
					num8 = 1f;
				}
				heightMap[k, j] = num8;
				if (num8 > num5)
				{
					num5 = num8;
				}
			}
			float num10 = (float)(j * num2);
			float num11 = (float)(num * num2);
			float percentComplete = num10 / num11;
			generatorProgressDelegate("Voronoi Generator", "Generating height map. Please wait.", percentComplete);
		}
		for (int j = 0; j < num2; j++)
		{
			for (int k = 0; k < num; k++)
			{
				float num12 = heightMap[k, j] * (1f / num5);
				heightMap[k, j] = num12;
			}
		}
		return heightMap;
	}

	// Token: 0x06004EDF RID: 20191 RVA: 0x0014AE14 File Offset: 0x00149014
	private float[,] generateDiamondSquare(float[,] heightMap, Vector2 arraySize, global::TerrainToolkit.GeneratorProgressDelegate generatorProgressDelegate)
	{
		int num = (int)arraySize.x;
		int num2 = (int)arraySize.y;
		float num3 = 1f;
		int i = num - 1;
		heightMap[0, 0] = 0.5f;
		heightMap[num - 1, 0] = 0.5f;
		heightMap[0, num2 - 1] = 0.5f;
		heightMap[num - 1, num2 - 1] = 0.5f;
		generatorProgressDelegate("Fractal Generator", "Generating height map. Please wait.", 0f);
		while (i > 1)
		{
			for (int j = 0; j < num - 1; j += i)
			{
				for (int k = 0; k < num2 - 1; k += i)
				{
					int tx = j + (i >> 1);
					int ty = k + (i >> 1);
					Vector2[] points = new Vector2[]
					{
						new Vector2((float)j, (float)k),
						new Vector2((float)(j + i), (float)k),
						new Vector2((float)j, (float)(k + i)),
						new Vector2((float)(j + i), (float)(k + i))
					};
					this.dsCalculateHeight(heightMap, arraySize, tx, ty, points, num3);
				}
			}
			for (int l = 0; l < num - 1; l += i)
			{
				for (int m = 0; m < num2 - 1; m += i)
				{
					int num4 = i >> 1;
					int num5 = l + num4;
					int num6 = m;
					int num7 = l;
					int num8 = m + num4;
					Vector2[] points2 = new Vector2[]
					{
						new Vector2((float)(num5 - num4), (float)num6),
						new Vector2((float)num5, (float)(num6 - num4)),
						new Vector2((float)(num5 + num4), (float)num6),
						new Vector2((float)num5, (float)(num6 + num4))
					};
					Vector2[] points3 = new Vector2[]
					{
						new Vector2((float)(num7 - num4), (float)num8),
						new Vector2((float)num7, (float)(num8 - num4)),
						new Vector2((float)(num7 + num4), (float)num8),
						new Vector2((float)num7, (float)(num8 + num4))
					};
					this.dsCalculateHeight(heightMap, arraySize, num5, num6, points2, num3);
					this.dsCalculateHeight(heightMap, arraySize, num7, num8, points3, num3);
				}
			}
			num3 *= this.diamondSquareDelta;
			i >>= 1;
		}
		generatorProgressDelegate("Fractal Generator", "Generating height map. Please wait.", 1f);
		return heightMap;
	}

	// Token: 0x06004EE0 RID: 20192 RVA: 0x0014B0D0 File Offset: 0x001492D0
	private void dsCalculateHeight(float[,] heightMap, Vector2 arraySize, int Tx, int Ty, Vector2[] points, float heightRange)
	{
		int num = (int)arraySize.x;
		int num2 = (int)arraySize.y;
		float num3 = 0f;
		for (int i = 0; i < 4; i++)
		{
			if (points[i].x < 0f)
			{
				int num4 = i;
				points[num4].x = points[num4].x + (float)(num - 1);
			}
			else if (points[i].x > (float)num)
			{
				int num5 = i;
				points[num5].x = points[num5].x - (float)(num - 1);
			}
			else if (points[i].y < 0f)
			{
				int num6 = i;
				points[num6].y = points[num6].y + (float)(num2 - 1);
			}
			else if (points[i].y > (float)num2)
			{
				int num7 = i;
				points[num7].y = points[num7].y - (float)(num2 - 1);
			}
			num3 += heightMap[(int)points[i].x, (int)points[i].y] / 4f;
		}
		num3 += Random.value * heightRange - heightRange / 2f;
		if (num3 < 0f)
		{
			num3 = 0f;
		}
		else if (num3 > 1f)
		{
			num3 = 1f;
		}
		heightMap[Tx, Ty] = num3;
		if (Tx == 0)
		{
			heightMap[num - 1, Ty] = num3;
		}
		else if (Tx == num - 1)
		{
			heightMap[0, Ty] = num3;
		}
		else if (Ty == 0)
		{
			heightMap[Tx, num2 - 1] = num3;
		}
		else if (Ty == num2 - 1)
		{
			heightMap[Tx, 0] = num3;
		}
	}

	// Token: 0x06004EE1 RID: 20193 RVA: 0x0014B298 File Offset: 0x00149498
	private float[,] generatePerlin(float[,] heightMap, Vector2 arraySize, global::TerrainToolkit.GeneratorProgressDelegate generatorProgressDelegate)
	{
		int num = (int)arraySize.x;
		int num2 = (int)arraySize.y;
		for (int i = 0; i < num2; i++)
		{
			for (int j = 0; j < num; j++)
			{
				heightMap[j, i] = 0f;
			}
		}
		global::TerrainToolkit.PerlinNoise2D[] array = new global::TerrainToolkit.PerlinNoise2D[this.perlinOctaves];
		int num3 = this.perlinFrequency;
		float num4 = 1f;
		for (int k = 0; k < this.perlinOctaves; k++)
		{
			array[k] = new global::TerrainToolkit.PerlinNoise2D(num3, num4);
			num3 *= 2;
			num4 /= 2f;
		}
		for (int k = 0; k < this.perlinOctaves; k++)
		{
			double num5 = (double)((float)num / (float)array[k].Frequency);
			double num6 = (double)((float)num2 / (float)array[k].Frequency);
			for (int l = 0; l < num; l++)
			{
				for (int m = 0; m < num2; m++)
				{
					int num7 = (int)((double)l / num5);
					int xb = num7 + 1;
					int num8 = (int)((double)m / num6);
					int yb = num8 + 1;
					double interpolatedPoint = array[k].getInterpolatedPoint(num7, xb, num8, yb, (double)l / num5 - (double)num7, (double)m / num6 - (double)num8);
					heightMap[l, m] += (float)(interpolatedPoint * (double)array[k].Amplitude);
				}
			}
			float percentComplete = (float)((k + 1) / this.perlinOctaves);
			generatorProgressDelegate("Perlin Generator", "Generating height map. Please wait.", percentComplete);
		}
		global::TerrainToolkit.GeneratorProgressDelegate generatorProgressDelegate2 = new global::TerrainToolkit.GeneratorProgressDelegate(this.dummyGeneratorProgress);
		float num9 = this.normaliseMin;
		float num10 = this.normaliseMax;
		float num11 = this.normaliseBlend;
		this.normaliseMin = 0f;
		this.normaliseMax = 1f;
		this.normaliseBlend = 1f;
		heightMap = this.normalise(heightMap, arraySize, generatorProgressDelegate2);
		this.normaliseMin = num9;
		this.normaliseMax = num10;
		this.normaliseBlend = num11;
		for (int n = 0; n < num; n++)
		{
			for (int num12 = 0; num12 < num2; num12++)
			{
				heightMap[n, num12] *= this.perlinAmplitude;
			}
		}
		for (int k = 0; k < this.perlinOctaves; k++)
		{
			array[k] = null;
		}
		return heightMap;
	}

	// Token: 0x06004EE2 RID: 20194 RVA: 0x0014B500 File Offset: 0x00149700
	private float[,] smooth(float[,] heightMap, Vector2 arraySize, global::TerrainToolkit.GeneratorProgressDelegate generatorProgressDelegate)
	{
		int num = (int)arraySize.x;
		int num2 = (int)arraySize.y;
		for (int i = 0; i < this.smoothIterations; i++)
		{
			for (int j = 0; j < num2; j++)
			{
				int num3;
				int num4;
				int num5;
				if (j == 0)
				{
					num3 = 2;
					num4 = 0;
					num5 = 0;
				}
				else if (j == num2 - 1)
				{
					num3 = 2;
					num4 = -1;
					num5 = 1;
				}
				else
				{
					num3 = 3;
					num4 = -1;
					num5 = 1;
				}
				for (int k = 0; k < num; k++)
				{
					int num6;
					int num7;
					int num8;
					if (k == 0)
					{
						num6 = 2;
						num7 = 0;
						num8 = 0;
					}
					else if (k == num - 1)
					{
						num6 = 2;
						num7 = -1;
						num8 = 1;
					}
					else
					{
						num6 = 3;
						num7 = -1;
						num8 = 1;
					}
					float num9 = 0f;
					int num10 = 0;
					for (int l = 0; l < num3; l++)
					{
						for (int m = 0; m < num6; m++)
						{
							if (this.neighbourhood == global::TerrainToolkit.Neighbourhood.Moore || (this.neighbourhood == global::TerrainToolkit.Neighbourhood.VonNeumann && (m == num8 || l == num5)))
							{
								float num11 = heightMap[k + m + num7, j + l + num4];
								num9 += num11;
								num10++;
							}
						}
					}
					float num12 = num9 / (float)num10;
					heightMap[k + num8 + num7, j + num5 + num4] = num12;
				}
			}
			float percentComplete = (float)((i + 1) / this.smoothIterations);
			generatorProgressDelegate("Smoothing Filter", "Smoothing height map. Please wait.", percentComplete);
		}
		return heightMap;
	}

	// Token: 0x06004EE3 RID: 20195 RVA: 0x0014B694 File Offset: 0x00149894
	private float[,] normalise(float[,] heightMap, Vector2 arraySize, global::TerrainToolkit.GeneratorProgressDelegate generatorProgressDelegate)
	{
		int num = (int)arraySize.x;
		int num2 = (int)arraySize.y;
		float num3 = 0f;
		float num4 = 1f;
		generatorProgressDelegate("Normalise Filter", "Normalising height map. Please wait.", 0f);
		for (int i = 0; i < num2; i++)
		{
			for (int j = 0; j < num; j++)
			{
				float num5 = heightMap[j, i];
				if (num5 < num4)
				{
					num4 = num5;
				}
				else if (num5 > num3)
				{
					num3 = num5;
				}
			}
		}
		generatorProgressDelegate("Normalise Filter", "Normalising height map. Please wait.", 0.5f);
		float num6 = num3 - num4;
		float num7 = this.normaliseMax - this.normaliseMin;
		for (int i = 0; i < num2; i++)
		{
			for (int j = 0; j < num; j++)
			{
				float num8 = (heightMap[j, i] - num4) / num6 * num7;
				heightMap[j, i] = this.normaliseMin + num8;
			}
		}
		generatorProgressDelegate("Normalise Filter", "Normalising height map. Please wait.", 1f);
		return heightMap;
	}

	// Token: 0x06004EE4 RID: 20196 RVA: 0x0014B7AC File Offset: 0x001499AC
	public void FastThermalErosion(int iterations, float minSlope, float blendAmount)
	{
		this.erosionTypeInt = 0;
		this.erosionType = global::TerrainToolkit.ErosionType.Thermal;
		this.thermalIterations = iterations;
		this.thermalMinSlope = minSlope;
		this.thermalFalloff = blendAmount;
		this.neighbourhood = global::TerrainToolkit.Neighbourhood.Moore;
		global::TerrainToolkit.ErosionProgressDelegate erosionProgressDelegate = new global::TerrainToolkit.ErosionProgressDelegate(this.dummyErosionProgress);
		this.erodeAllTerrain(erosionProgressDelegate);
	}

	// Token: 0x06004EE5 RID: 20197 RVA: 0x0014B7F8 File Offset: 0x001499F8
	public void FastHydraulicErosion(int iterations, float maxSlope, float blendAmount)
	{
		this.erosionTypeInt = 1;
		this.erosionType = global::TerrainToolkit.ErosionType.Hydraulic;
		this.hydraulicTypeInt = 0;
		this.hydraulicType = global::TerrainToolkit.HydraulicType.Fast;
		this.hydraulicIterations = iterations;
		this.hydraulicMaxSlope = maxSlope;
		this.hydraulicFalloff = blendAmount;
		this.neighbourhood = global::TerrainToolkit.Neighbourhood.Moore;
		global::TerrainToolkit.ErosionProgressDelegate erosionProgressDelegate = new global::TerrainToolkit.ErosionProgressDelegate(this.dummyErosionProgress);
		this.erodeAllTerrain(erosionProgressDelegate);
	}

	// Token: 0x06004EE6 RID: 20198 RVA: 0x0014B854 File Offset: 0x00149A54
	public void FullHydraulicErosion(int iterations, float rainfall, float evaporation, float solubility, float saturation)
	{
		this.erosionTypeInt = 1;
		this.erosionType = global::TerrainToolkit.ErosionType.Hydraulic;
		this.hydraulicTypeInt = 1;
		this.hydraulicType = global::TerrainToolkit.HydraulicType.Full;
		this.hydraulicIterations = iterations;
		this.hydraulicRainfall = rainfall;
		this.hydraulicEvaporation = evaporation;
		this.hydraulicSedimentSolubility = solubility;
		this.hydraulicSedimentSaturation = saturation;
		this.neighbourhood = global::TerrainToolkit.Neighbourhood.Moore;
		global::TerrainToolkit.ErosionProgressDelegate erosionProgressDelegate = new global::TerrainToolkit.ErosionProgressDelegate(this.dummyErosionProgress);
		this.erodeAllTerrain(erosionProgressDelegate);
	}

	// Token: 0x06004EE7 RID: 20199 RVA: 0x0014B8C0 File Offset: 0x00149AC0
	public void VelocityHydraulicErosion(int iterations, float rainfall, float evaporation, float solubility, float saturation, float velocity, float momentum, float entropy, float downcutting)
	{
		this.erosionTypeInt = 1;
		this.erosionType = global::TerrainToolkit.ErosionType.Hydraulic;
		this.hydraulicTypeInt = 2;
		this.hydraulicType = global::TerrainToolkit.HydraulicType.Velocity;
		this.hydraulicIterations = iterations;
		this.hydraulicVelocityRainfall = rainfall;
		this.hydraulicVelocityEvaporation = evaporation;
		this.hydraulicVelocitySedimentSolubility = solubility;
		this.hydraulicVelocitySedimentSaturation = saturation;
		this.hydraulicVelocity = velocity;
		this.hydraulicMomentum = momentum;
		this.hydraulicEntropy = entropy;
		this.hydraulicDowncutting = downcutting;
		this.neighbourhood = global::TerrainToolkit.Neighbourhood.Moore;
		global::TerrainToolkit.ErosionProgressDelegate erosionProgressDelegate = new global::TerrainToolkit.ErosionProgressDelegate(this.dummyErosionProgress);
		this.erodeAllTerrain(erosionProgressDelegate);
	}

	// Token: 0x06004EE8 RID: 20200 RVA: 0x0014B94C File Offset: 0x00149B4C
	public void TidalErosion(int iterations, float seaLevel, float tidalRange, float cliffLimit)
	{
		this.erosionTypeInt = 2;
		this.erosionType = global::TerrainToolkit.ErosionType.Tidal;
		this.tidalIterations = iterations;
		this.tidalSeaLevel = seaLevel;
		this.tidalRangeAmount = tidalRange;
		this.tidalCliffLimit = cliffLimit;
		this.neighbourhood = global::TerrainToolkit.Neighbourhood.Moore;
		global::TerrainToolkit.ErosionProgressDelegate erosionProgressDelegate = new global::TerrainToolkit.ErosionProgressDelegate(this.dummyErosionProgress);
		this.erodeAllTerrain(erosionProgressDelegate);
	}

	// Token: 0x06004EE9 RID: 20201 RVA: 0x0014B9A0 File Offset: 0x00149BA0
	public void WindErosion(int iterations, float direction, float force, float lift, float gravity, float capacity, float entropy, float smoothing)
	{
		this.erosionTypeInt = 3;
		this.erosionType = global::TerrainToolkit.ErosionType.Wind;
		this.windIterations = iterations;
		this.windDirection = direction;
		this.windForce = force;
		this.windLift = lift;
		this.windGravity = gravity;
		this.windCapacity = capacity;
		this.windEntropy = entropy;
		this.windSmoothing = smoothing;
		this.neighbourhood = global::TerrainToolkit.Neighbourhood.Moore;
		global::TerrainToolkit.ErosionProgressDelegate erosionProgressDelegate = new global::TerrainToolkit.ErosionProgressDelegate(this.dummyErosionProgress);
		this.erodeAllTerrain(erosionProgressDelegate);
	}

	// Token: 0x06004EEA RID: 20202 RVA: 0x0014BA14 File Offset: 0x00149C14
	public void TextureTerrain(float[] slopeStops, float[] heightStops, Texture2D[] textures)
	{
		if (slopeStops.Length != 2)
		{
			Debug.LogError("Error: slopeStops must have 2 values");
			return;
		}
		if (heightStops.Length > 8)
		{
			Debug.LogError("Error: heightStops must have no more than 8 values");
			return;
		}
		if (heightStops.Length % 2 != 0)
		{
			Debug.LogError("Error: heightStops must have an even number of values");
			return;
		}
		int num = textures.Length;
		int num2 = heightStops.Length / 2 + 2;
		if (num != num2)
		{
			Debug.LogError("Error: heightStops contains an incorrect number of values");
			return;
		}
		foreach (float num3 in slopeStops)
		{
			if (num3 < 0f || num3 > 90f)
			{
				Debug.LogError("Error: The value of all slopeStops must be in the range 0.0 to 90.0");
				return;
			}
		}
		foreach (float num4 in heightStops)
		{
			if (num4 < 0f || num4 > 1f)
			{
				Debug.LogError("Error: The value of all heightStops must be in the range 0.0 to 1.0");
				return;
			}
		}
		Terrain terrain = (Terrain)base.GetComponent(typeof(Terrain));
		TerrainData terrainData = terrain.terrainData;
		this.splatPrototypes = terrainData.splatPrototypes;
		this.deleteAllSplatPrototypes();
		int num5 = 0;
		foreach (Texture2D tex in textures)
		{
			this.addSplatPrototype(tex, num5);
			num5++;
		}
		this.slopeBlendMinAngle = slopeStops[0];
		this.slopeBlendMaxAngle = slopeStops[1];
		num5 = 0;
		foreach (float value in heightStops)
		{
			this.heightBlendPoints[num5] = value;
			num5++;
		}
		terrainData.splatPrototypes = this.splatPrototypes;
		global::TerrainToolkit.TextureProgressDelegate textureProgressDelegate = new global::TerrainToolkit.TextureProgressDelegate(this.dummyTextureProgress);
		this.textureTerrain(textureProgressDelegate);
	}

	// Token: 0x06004EEB RID: 20203 RVA: 0x0014BBE0 File Offset: 0x00149DE0
	public void VoronoiGenerator(global::TerrainToolkit.FeatureType featureType, int cells, float features, float scale, float blend)
	{
		this.generatorTypeInt = 0;
		this.generatorType = global::TerrainToolkit.GeneratorType.Voronoi;
		switch (featureType)
		{
		case global::TerrainToolkit.FeatureType.Mountains:
			this.voronoiTypeInt = 0;
			this.voronoiType = global::TerrainToolkit.VoronoiType.Linear;
			break;
		case global::TerrainToolkit.FeatureType.Hills:
			this.voronoiTypeInt = 1;
			this.voronoiType = global::TerrainToolkit.VoronoiType.Sine;
			break;
		case global::TerrainToolkit.FeatureType.Plateaus:
			this.voronoiTypeInt = 2;
			this.voronoiType = global::TerrainToolkit.VoronoiType.Tangent;
			break;
		}
		this.voronoiCells = cells;
		this.voronoiFeatures = features;
		this.voronoiScale = scale;
		this.voronoiBlend = blend;
		global::TerrainToolkit.GeneratorProgressDelegate generatorProgressDelegate = new global::TerrainToolkit.GeneratorProgressDelegate(this.dummyGeneratorProgress);
		this.generateTerrain(generatorProgressDelegate);
	}

	// Token: 0x06004EEC RID: 20204 RVA: 0x0014BC80 File Offset: 0x00149E80
	public void FractalGenerator(float fractalDelta, float blend)
	{
		this.generatorTypeInt = 1;
		this.generatorType = global::TerrainToolkit.GeneratorType.DiamondSquare;
		this.diamondSquareDelta = fractalDelta;
		this.diamondSquareBlend = blend;
		global::TerrainToolkit.GeneratorProgressDelegate generatorProgressDelegate = new global::TerrainToolkit.GeneratorProgressDelegate(this.dummyGeneratorProgress);
		this.generateTerrain(generatorProgressDelegate);
	}

	// Token: 0x06004EED RID: 20205 RVA: 0x0014BCC0 File Offset: 0x00149EC0
	public void PerlinGenerator(int frequency, float amplitude, int octaves, float blend)
	{
		this.generatorTypeInt = 2;
		this.generatorType = global::TerrainToolkit.GeneratorType.Perlin;
		this.perlinFrequency = frequency;
		this.perlinAmplitude = amplitude;
		this.perlinOctaves = octaves;
		this.perlinBlend = blend;
		global::TerrainToolkit.GeneratorProgressDelegate generatorProgressDelegate = new global::TerrainToolkit.GeneratorProgressDelegate(this.dummyGeneratorProgress);
		this.generateTerrain(generatorProgressDelegate);
	}

	// Token: 0x06004EEE RID: 20206 RVA: 0x0014BD0C File Offset: 0x00149F0C
	public void SmoothTerrain(int iterations, float blend)
	{
		this.generatorTypeInt = 3;
		this.generatorType = global::TerrainToolkit.GeneratorType.Smooth;
		this.smoothIterations = iterations;
		this.smoothBlend = blend;
		global::TerrainToolkit.GeneratorProgressDelegate generatorProgressDelegate = new global::TerrainToolkit.GeneratorProgressDelegate(this.dummyGeneratorProgress);
		this.generateTerrain(generatorProgressDelegate);
	}

	// Token: 0x06004EEF RID: 20207 RVA: 0x0014BD4C File Offset: 0x00149F4C
	public void NormaliseTerrain(float minHeight, float maxHeight, float blend)
	{
		this.generatorTypeInt = 4;
		this.generatorType = global::TerrainToolkit.GeneratorType.Normalise;
		this.normaliseMin = minHeight;
		this.normaliseMax = maxHeight;
		this.normaliseBlend = blend;
		global::TerrainToolkit.GeneratorProgressDelegate generatorProgressDelegate = new global::TerrainToolkit.GeneratorProgressDelegate(this.dummyGeneratorProgress);
		this.generateTerrain(generatorProgressDelegate);
	}

	// Token: 0x06004EF0 RID: 20208 RVA: 0x0014BD90 File Offset: 0x00149F90
	public void NormalizeTerrain(float minHeight, float maxHeight, float blend)
	{
		this.NormaliseTerrain(minHeight, maxHeight, blend);
	}

	// Token: 0x06004EF1 RID: 20209 RVA: 0x0014BD9C File Offset: 0x00149F9C
	private void convertIntVarsToEnums()
	{
		switch (this.erosionTypeInt)
		{
		case 0:
			this.erosionType = global::TerrainToolkit.ErosionType.Thermal;
			break;
		case 1:
			this.erosionType = global::TerrainToolkit.ErosionType.Hydraulic;
			break;
		case 2:
			this.erosionType = global::TerrainToolkit.ErosionType.Tidal;
			break;
		case 3:
			this.erosionType = global::TerrainToolkit.ErosionType.Wind;
			break;
		case 4:
			this.erosionType = global::TerrainToolkit.ErosionType.Glacial;
			break;
		}
		switch (this.hydraulicTypeInt)
		{
		case 0:
			this.hydraulicType = global::TerrainToolkit.HydraulicType.Fast;
			break;
		case 1:
			this.hydraulicType = global::TerrainToolkit.HydraulicType.Full;
			break;
		case 2:
			this.hydraulicType = global::TerrainToolkit.HydraulicType.Velocity;
			break;
		}
		switch (this.generatorTypeInt)
		{
		case 0:
			this.generatorType = global::TerrainToolkit.GeneratorType.Voronoi;
			break;
		case 1:
			this.generatorType = global::TerrainToolkit.GeneratorType.DiamondSquare;
			break;
		case 2:
			this.generatorType = global::TerrainToolkit.GeneratorType.Perlin;
			break;
		case 3:
			this.generatorType = global::TerrainToolkit.GeneratorType.Smooth;
			break;
		case 4:
			this.generatorType = global::TerrainToolkit.GeneratorType.Normalise;
			break;
		}
		switch (this.voronoiTypeInt)
		{
		case 0:
			this.voronoiType = global::TerrainToolkit.VoronoiType.Linear;
			break;
		case 1:
			this.voronoiType = global::TerrainToolkit.VoronoiType.Sine;
			break;
		case 2:
			this.voronoiType = global::TerrainToolkit.VoronoiType.Tangent;
			break;
		}
		int num = this.neighbourhoodInt;
		if (num != 0)
		{
			if (num == 1)
			{
				this.neighbourhood = global::TerrainToolkit.Neighbourhood.VonNeumann;
			}
		}
		else
		{
			this.neighbourhood = global::TerrainToolkit.Neighbourhood.Moore;
		}
	}

	// Token: 0x06004EF2 RID: 20210 RVA: 0x0014BF24 File Offset: 0x0014A124
	public void dummyErosionProgress(string titleString, string displayString, int iteration, int nIterations, float percentComplete)
	{
	}

	// Token: 0x06004EF3 RID: 20211 RVA: 0x0014BF28 File Offset: 0x0014A128
	public void dummyTextureProgress(string titleString, string displayString, float percentComplete)
	{
	}

	// Token: 0x06004EF4 RID: 20212 RVA: 0x0014BF2C File Offset: 0x0014A12C
	public void dummyGeneratorProgress(string titleString, string displayString, float percentComplete)
	{
	}

	// Token: 0x04002C79 RID: 11385
	public GUISkin guiSkin;

	// Token: 0x04002C7A RID: 11386
	public Texture2D createIcon;

	// Token: 0x04002C7B RID: 11387
	public Texture2D erodeIcon;

	// Token: 0x04002C7C RID: 11388
	public Texture2D textureIcon;

	// Token: 0x04002C7D RID: 11389
	public Texture2D mooreIcon;

	// Token: 0x04002C7E RID: 11390
	public Texture2D vonNeumannIcon;

	// Token: 0x04002C7F RID: 11391
	public Texture2D mountainsIcon;

	// Token: 0x04002C80 RID: 11392
	public Texture2D hillsIcon;

	// Token: 0x04002C81 RID: 11393
	public Texture2D plateausIcon;

	// Token: 0x04002C82 RID: 11394
	public Texture2D defaultTexture;

	// Token: 0x04002C83 RID: 11395
	public int toolModeInt;

	// Token: 0x04002C84 RID: 11396
	private global::TerrainToolkit.ErosionMode erosionMode;

	// Token: 0x04002C85 RID: 11397
	private global::TerrainToolkit.ErosionType erosionType;

	// Token: 0x04002C86 RID: 11398
	public int erosionTypeInt;

	// Token: 0x04002C87 RID: 11399
	private global::TerrainToolkit.GeneratorType generatorType;

	// Token: 0x04002C88 RID: 11400
	public int generatorTypeInt;

	// Token: 0x04002C89 RID: 11401
	public bool isBrushOn;

	// Token: 0x04002C8A RID: 11402
	public bool isBrushHidden;

	// Token: 0x04002C8B RID: 11403
	public bool isBrushPainting;

	// Token: 0x04002C8C RID: 11404
	public Vector3 brushPosition;

	// Token: 0x04002C8D RID: 11405
	public float brushSize = 50f;

	// Token: 0x04002C8E RID: 11406
	public float brushOpacity = 1f;

	// Token: 0x04002C8F RID: 11407
	public float brushSoftness = 0.5f;

	// Token: 0x04002C90 RID: 11408
	public int neighbourhoodInt;

	// Token: 0x04002C91 RID: 11409
	private global::TerrainToolkit.Neighbourhood neighbourhood;

	// Token: 0x04002C92 RID: 11410
	public bool useDifferenceMaps = true;

	// Token: 0x04002C93 RID: 11411
	public int thermalIterations = 25;

	// Token: 0x04002C94 RID: 11412
	public float thermalMinSlope = 1f;

	// Token: 0x04002C95 RID: 11413
	public float thermalFalloff = 0.5f;

	// Token: 0x04002C96 RID: 11414
	public int hydraulicTypeInt;

	// Token: 0x04002C97 RID: 11415
	public global::TerrainToolkit.HydraulicType hydraulicType;

	// Token: 0x04002C98 RID: 11416
	public int hydraulicIterations = 25;

	// Token: 0x04002C99 RID: 11417
	public float hydraulicMaxSlope = 60f;

	// Token: 0x04002C9A RID: 11418
	public float hydraulicFalloff = 0.5f;

	// Token: 0x04002C9B RID: 11419
	public float hydraulicRainfall = 0.01f;

	// Token: 0x04002C9C RID: 11420
	public float hydraulicEvaporation = 0.5f;

	// Token: 0x04002C9D RID: 11421
	public float hydraulicSedimentSolubility = 0.01f;

	// Token: 0x04002C9E RID: 11422
	public float hydraulicSedimentSaturation = 0.1f;

	// Token: 0x04002C9F RID: 11423
	public float hydraulicVelocityRainfall = 0.01f;

	// Token: 0x04002CA0 RID: 11424
	public float hydraulicVelocityEvaporation = 0.5f;

	// Token: 0x04002CA1 RID: 11425
	public float hydraulicVelocitySedimentSolubility = 0.01f;

	// Token: 0x04002CA2 RID: 11426
	public float hydraulicVelocitySedimentSaturation = 0.1f;

	// Token: 0x04002CA3 RID: 11427
	public float hydraulicVelocity = 20f;

	// Token: 0x04002CA4 RID: 11428
	public float hydraulicMomentum = 1f;

	// Token: 0x04002CA5 RID: 11429
	public float hydraulicEntropy;

	// Token: 0x04002CA6 RID: 11430
	public float hydraulicDowncutting = 0.1f;

	// Token: 0x04002CA7 RID: 11431
	public int tidalIterations = 25;

	// Token: 0x04002CA8 RID: 11432
	public float tidalSeaLevel = 50f;

	// Token: 0x04002CA9 RID: 11433
	public float tidalRangeAmount = 5f;

	// Token: 0x04002CAA RID: 11434
	public float tidalCliffLimit = 60f;

	// Token: 0x04002CAB RID: 11435
	public int windIterations = 25;

	// Token: 0x04002CAC RID: 11436
	public float windDirection;

	// Token: 0x04002CAD RID: 11437
	public float windForce = 0.5f;

	// Token: 0x04002CAE RID: 11438
	public float windLift = 0.01f;

	// Token: 0x04002CAF RID: 11439
	public float windGravity = 0.5f;

	// Token: 0x04002CB0 RID: 11440
	public float windCapacity = 0.01f;

	// Token: 0x04002CB1 RID: 11441
	public float windEntropy = 0.1f;

	// Token: 0x04002CB2 RID: 11442
	public float windSmoothing = 0.25f;

	// Token: 0x04002CB3 RID: 11443
	public SplatPrototype[] splatPrototypes;

	// Token: 0x04002CB4 RID: 11444
	public Texture2D tempTexture;

	// Token: 0x04002CB5 RID: 11445
	public float slopeBlendMinAngle = 60f;

	// Token: 0x04002CB6 RID: 11446
	public float slopeBlendMaxAngle = 75f;

	// Token: 0x04002CB7 RID: 11447
	public List<float> heightBlendPoints;

	// Token: 0x04002CB8 RID: 11448
	public string[] gradientStyles;

	// Token: 0x04002CB9 RID: 11449
	public int voronoiTypeInt;

	// Token: 0x04002CBA RID: 11450
	public global::TerrainToolkit.VoronoiType voronoiType;

	// Token: 0x04002CBB RID: 11451
	public int voronoiCells = 16;

	// Token: 0x04002CBC RID: 11452
	public float voronoiFeatures = 1f;

	// Token: 0x04002CBD RID: 11453
	public float voronoiScale = 1f;

	// Token: 0x04002CBE RID: 11454
	public float voronoiBlend = 1f;

	// Token: 0x04002CBF RID: 11455
	public float diamondSquareDelta = 0.5f;

	// Token: 0x04002CC0 RID: 11456
	public float diamondSquareBlend = 1f;

	// Token: 0x04002CC1 RID: 11457
	public int perlinFrequency = 4;

	// Token: 0x04002CC2 RID: 11458
	public float perlinAmplitude = 1f;

	// Token: 0x04002CC3 RID: 11459
	public int perlinOctaves = 8;

	// Token: 0x04002CC4 RID: 11460
	public float perlinBlend = 1f;

	// Token: 0x04002CC5 RID: 11461
	public float smoothBlend = 1f;

	// Token: 0x04002CC6 RID: 11462
	public int smoothIterations;

	// Token: 0x04002CC7 RID: 11463
	public float normaliseMin;

	// Token: 0x04002CC8 RID: 11464
	public float normaliseMax = 1f;

	// Token: 0x04002CC9 RID: 11465
	public float normaliseBlend = 1f;

	// Token: 0x04002CCA RID: 11466
	[NonSerialized]
	public bool presetsInitialised;

	// Token: 0x04002CCB RID: 11467
	[NonSerialized]
	public int voronoiPresetId;

	// Token: 0x04002CCC RID: 11468
	[NonSerialized]
	public int fractalPresetId;

	// Token: 0x04002CCD RID: 11469
	[NonSerialized]
	public int perlinPresetId;

	// Token: 0x04002CCE RID: 11470
	[NonSerialized]
	public int thermalErosionPresetId;

	// Token: 0x04002CCF RID: 11471
	[NonSerialized]
	public int fastHydraulicErosionPresetId;

	// Token: 0x04002CD0 RID: 11472
	[NonSerialized]
	public int fullHydraulicErosionPresetId;

	// Token: 0x04002CD1 RID: 11473
	[NonSerialized]
	public int velocityHydraulicErosionPresetId;

	// Token: 0x04002CD2 RID: 11474
	[NonSerialized]
	public int tidalErosionPresetId;

	// Token: 0x04002CD3 RID: 11475
	[NonSerialized]
	public int windErosionPresetId;

	// Token: 0x04002CD4 RID: 11476
	public ArrayList voronoiPresets = new ArrayList();

	// Token: 0x04002CD5 RID: 11477
	public ArrayList fractalPresets = new ArrayList();

	// Token: 0x04002CD6 RID: 11478
	public ArrayList perlinPresets = new ArrayList();

	// Token: 0x04002CD7 RID: 11479
	public ArrayList thermalErosionPresets = new ArrayList();

	// Token: 0x04002CD8 RID: 11480
	public ArrayList fastHydraulicErosionPresets = new ArrayList();

	// Token: 0x04002CD9 RID: 11481
	public ArrayList fullHydraulicErosionPresets = new ArrayList();

	// Token: 0x04002CDA RID: 11482
	public ArrayList velocityHydraulicErosionPresets = new ArrayList();

	// Token: 0x04002CDB RID: 11483
	public ArrayList tidalErosionPresets = new ArrayList();

	// Token: 0x04002CDC RID: 11484
	public ArrayList windErosionPresets = new ArrayList();

	// Token: 0x02000905 RID: 2309
	public class PeakDistance : IComparable
	{
		// Token: 0x06004EF6 RID: 20214 RVA: 0x0014BF38 File Offset: 0x0014A138
		public int CompareTo(object obj)
		{
			global::TerrainToolkit.PeakDistance peakDistance = (global::TerrainToolkit.PeakDistance)obj;
			int num = this.dist.CompareTo(peakDistance.dist);
			if (num == 0)
			{
				num = this.dist.CompareTo(peakDistance.dist);
			}
			return num;
		}

		// Token: 0x04002CDD RID: 11485
		public int id;

		// Token: 0x04002CDE RID: 11486
		public float dist;
	}

	// Token: 0x02000906 RID: 2310
	public struct Peak
	{
		// Token: 0x04002CDF RID: 11487
		public Vector2 peakPoint;

		// Token: 0x04002CE0 RID: 11488
		public float peakHeight;
	}

	// Token: 0x02000907 RID: 2311
	public class voronoiPresetData
	{
		// Token: 0x06004EF7 RID: 20215 RVA: 0x0014BF78 File Offset: 0x0014A178
		public voronoiPresetData(string pn, global::TerrainToolkit.VoronoiType vt, int c, float vf, float vs, float vb)
		{
			this.presetName = pn;
			this.voronoiType = vt;
			this.voronoiCells = c;
			this.voronoiFeatures = vf;
			this.voronoiScale = vs;
			this.voronoiBlend = vb;
		}

		// Token: 0x04002CE1 RID: 11489
		public string presetName;

		// Token: 0x04002CE2 RID: 11490
		public global::TerrainToolkit.VoronoiType voronoiType;

		// Token: 0x04002CE3 RID: 11491
		public int voronoiCells;

		// Token: 0x04002CE4 RID: 11492
		public float voronoiFeatures;

		// Token: 0x04002CE5 RID: 11493
		public float voronoiScale;

		// Token: 0x04002CE6 RID: 11494
		public float voronoiBlend;
	}

	// Token: 0x02000908 RID: 2312
	public class fractalPresetData
	{
		// Token: 0x06004EF8 RID: 20216 RVA: 0x0014BFB0 File Offset: 0x0014A1B0
		public fractalPresetData(string pn, float dsd, float dsb)
		{
			this.presetName = pn;
			this.diamondSquareDelta = dsd;
			this.diamondSquareBlend = dsb;
		}

		// Token: 0x04002CE7 RID: 11495
		public string presetName;

		// Token: 0x04002CE8 RID: 11496
		public float diamondSquareDelta;

		// Token: 0x04002CE9 RID: 11497
		public float diamondSquareBlend;
	}

	// Token: 0x02000909 RID: 2313
	public class perlinPresetData
	{
		// Token: 0x06004EF9 RID: 20217 RVA: 0x0014BFD0 File Offset: 0x0014A1D0
		public perlinPresetData(string pn, int pf, float pa, int po, float pb)
		{
			this.presetName = pn;
			this.perlinFrequency = pf;
			this.perlinAmplitude = pa;
			this.perlinOctaves = po;
			this.perlinBlend = pb;
		}

		// Token: 0x04002CEA RID: 11498
		public string presetName;

		// Token: 0x04002CEB RID: 11499
		public int perlinFrequency;

		// Token: 0x04002CEC RID: 11500
		public float perlinAmplitude;

		// Token: 0x04002CED RID: 11501
		public int perlinOctaves;

		// Token: 0x04002CEE RID: 11502
		public float perlinBlend;
	}

	// Token: 0x0200090A RID: 2314
	public class thermalErosionPresetData
	{
		// Token: 0x06004EFA RID: 20218 RVA: 0x0014C000 File Offset: 0x0014A200
		public thermalErosionPresetData(string pn, int ti, float tms, float tba)
		{
			this.presetName = pn;
			this.thermalIterations = ti;
			this.thermalMinSlope = tms;
			this.thermalFalloff = tba;
		}

		// Token: 0x04002CEF RID: 11503
		public string presetName;

		// Token: 0x04002CF0 RID: 11504
		public int thermalIterations;

		// Token: 0x04002CF1 RID: 11505
		public float thermalMinSlope;

		// Token: 0x04002CF2 RID: 11506
		public float thermalFalloff;
	}

	// Token: 0x0200090B RID: 2315
	public class fastHydraulicErosionPresetData
	{
		// Token: 0x06004EFB RID: 20219 RVA: 0x0014C028 File Offset: 0x0014A228
		public fastHydraulicErosionPresetData(string pn, int hi, float hms, float hba)
		{
			this.presetName = pn;
			this.hydraulicIterations = hi;
			this.hydraulicMaxSlope = hms;
			this.hydraulicFalloff = hba;
		}

		// Token: 0x04002CF3 RID: 11507
		public string presetName;

		// Token: 0x04002CF4 RID: 11508
		public int hydraulicIterations;

		// Token: 0x04002CF5 RID: 11509
		public float hydraulicMaxSlope;

		// Token: 0x04002CF6 RID: 11510
		public float hydraulicFalloff;
	}

	// Token: 0x0200090C RID: 2316
	public class fullHydraulicErosionPresetData
	{
		// Token: 0x06004EFC RID: 20220 RVA: 0x0014C050 File Offset: 0x0014A250
		public fullHydraulicErosionPresetData(string pn, int hi, float hr, float he, float hso, float hsa)
		{
			this.presetName = pn;
			this.hydraulicIterations = hi;
			this.hydraulicRainfall = hr;
			this.hydraulicEvaporation = he;
			this.hydraulicSedimentSolubility = hso;
			this.hydraulicSedimentSaturation = hsa;
		}

		// Token: 0x04002CF7 RID: 11511
		public string presetName;

		// Token: 0x04002CF8 RID: 11512
		public int hydraulicIterations;

		// Token: 0x04002CF9 RID: 11513
		public float hydraulicRainfall;

		// Token: 0x04002CFA RID: 11514
		public float hydraulicEvaporation;

		// Token: 0x04002CFB RID: 11515
		public float hydraulicSedimentSolubility;

		// Token: 0x04002CFC RID: 11516
		public float hydraulicSedimentSaturation;
	}

	// Token: 0x0200090D RID: 2317
	public class velocityHydraulicErosionPresetData
	{
		// Token: 0x06004EFD RID: 20221 RVA: 0x0014C088 File Offset: 0x0014A288
		public velocityHydraulicErosionPresetData(string pn, int hi, float hvr, float hve, float hso, float hsa, float hv, float hm, float he, float hd)
		{
			this.presetName = pn;
			this.hydraulicIterations = hi;
			this.hydraulicVelocityRainfall = hvr;
			this.hydraulicVelocityEvaporation = hve;
			this.hydraulicVelocitySedimentSolubility = hso;
			this.hydraulicVelocitySedimentSaturation = hsa;
			this.hydraulicVelocity = hv;
			this.hydraulicMomentum = hm;
			this.hydraulicEntropy = he;
			this.hydraulicDowncutting = hd;
		}

		// Token: 0x04002CFD RID: 11517
		public string presetName;

		// Token: 0x04002CFE RID: 11518
		public int hydraulicIterations;

		// Token: 0x04002CFF RID: 11519
		public float hydraulicVelocityRainfall;

		// Token: 0x04002D00 RID: 11520
		public float hydraulicVelocityEvaporation;

		// Token: 0x04002D01 RID: 11521
		public float hydraulicVelocitySedimentSolubility;

		// Token: 0x04002D02 RID: 11522
		public float hydraulicVelocitySedimentSaturation;

		// Token: 0x04002D03 RID: 11523
		public float hydraulicVelocity;

		// Token: 0x04002D04 RID: 11524
		public float hydraulicMomentum;

		// Token: 0x04002D05 RID: 11525
		public float hydraulicEntropy;

		// Token: 0x04002D06 RID: 11526
		public float hydraulicDowncutting;
	}

	// Token: 0x0200090E RID: 2318
	public class tidalErosionPresetData
	{
		// Token: 0x06004EFE RID: 20222 RVA: 0x0014C0E8 File Offset: 0x0014A2E8
		public tidalErosionPresetData(string pn, int ti, float tra, float tcl)
		{
			this.presetName = pn;
			this.tidalIterations = ti;
			this.tidalRangeAmount = tra;
			this.tidalCliffLimit = tcl;
		}

		// Token: 0x04002D07 RID: 11527
		public string presetName;

		// Token: 0x04002D08 RID: 11528
		public int tidalIterations;

		// Token: 0x04002D09 RID: 11529
		public float tidalRangeAmount;

		// Token: 0x04002D0A RID: 11530
		public float tidalCliffLimit;
	}

	// Token: 0x0200090F RID: 2319
	public class windErosionPresetData
	{
		// Token: 0x06004EFF RID: 20223 RVA: 0x0014C110 File Offset: 0x0014A310
		public windErosionPresetData(string pn, int wi, float wd, float wf, float wl, float wg, float wc, float we, float ws)
		{
			this.presetName = pn;
			this.windIterations = wi;
			this.windDirection = wd;
			this.windForce = wf;
			this.windLift = wl;
			this.windGravity = wg;
			this.windCapacity = wc;
			this.windEntropy = we;
			this.windSmoothing = ws;
		}

		// Token: 0x04002D0B RID: 11531
		public string presetName;

		// Token: 0x04002D0C RID: 11532
		public int windIterations;

		// Token: 0x04002D0D RID: 11533
		public float windDirection;

		// Token: 0x04002D0E RID: 11534
		public float windForce;

		// Token: 0x04002D0F RID: 11535
		public float windLift;

		// Token: 0x04002D10 RID: 11536
		public float windGravity;

		// Token: 0x04002D11 RID: 11537
		public float windCapacity;

		// Token: 0x04002D12 RID: 11538
		public float windEntropy;

		// Token: 0x04002D13 RID: 11539
		public float windSmoothing;
	}

	// Token: 0x02000910 RID: 2320
	public enum ToolMode
	{
		// Token: 0x04002D15 RID: 11541
		Create,
		// Token: 0x04002D16 RID: 11542
		Erode,
		// Token: 0x04002D17 RID: 11543
		Texture
	}

	// Token: 0x02000911 RID: 2321
	public enum ErosionMode
	{
		// Token: 0x04002D19 RID: 11545
		Filter,
		// Token: 0x04002D1A RID: 11546
		Brush
	}

	// Token: 0x02000912 RID: 2322
	public enum ErosionType
	{
		// Token: 0x04002D1C RID: 11548
		Thermal,
		// Token: 0x04002D1D RID: 11549
		Hydraulic,
		// Token: 0x04002D1E RID: 11550
		Tidal,
		// Token: 0x04002D1F RID: 11551
		Wind,
		// Token: 0x04002D20 RID: 11552
		Glacial
	}

	// Token: 0x02000913 RID: 2323
	public enum HydraulicType
	{
		// Token: 0x04002D22 RID: 11554
		Fast,
		// Token: 0x04002D23 RID: 11555
		Full,
		// Token: 0x04002D24 RID: 11556
		Velocity
	}

	// Token: 0x02000914 RID: 2324
	public enum Neighbourhood
	{
		// Token: 0x04002D26 RID: 11558
		Moore,
		// Token: 0x04002D27 RID: 11559
		VonNeumann
	}

	// Token: 0x02000915 RID: 2325
	public enum GeneratorType
	{
		// Token: 0x04002D29 RID: 11561
		Voronoi,
		// Token: 0x04002D2A RID: 11562
		DiamondSquare,
		// Token: 0x04002D2B RID: 11563
		Perlin,
		// Token: 0x04002D2C RID: 11564
		Smooth,
		// Token: 0x04002D2D RID: 11565
		Normalise
	}

	// Token: 0x02000916 RID: 2326
	public enum VoronoiType
	{
		// Token: 0x04002D2F RID: 11567
		Linear,
		// Token: 0x04002D30 RID: 11568
		Sine,
		// Token: 0x04002D31 RID: 11569
		Tangent
	}

	// Token: 0x02000917 RID: 2327
	public enum FeatureType
	{
		// Token: 0x04002D33 RID: 11571
		Mountains,
		// Token: 0x04002D34 RID: 11572
		Hills,
		// Token: 0x04002D35 RID: 11573
		Plateaus
	}

	// Token: 0x02000918 RID: 2328
	public class PerlinNoise2D
	{
		// Token: 0x06004F00 RID: 20224 RVA: 0x0014C168 File Offset: 0x0014A368
		public PerlinNoise2D(int freq, float _amp)
		{
			Random random = new Random(Environment.TickCount);
			this.noiseValues = new double[freq, freq];
			this.amplitude = _amp;
			this.frequency = freq;
			for (int i = 0; i < freq; i++)
			{
				for (int j = 0; j < freq; j++)
				{
					this.noiseValues[i, j] = random.NextDouble();
				}
			}
		}

		// Token: 0x06004F01 RID: 20225 RVA: 0x0014C1EC File Offset: 0x0014A3EC
		public double getInterpolatedPoint(int _xa, int _xb, int _ya, int _yb, double Px, double Py)
		{
			double pa = this.interpolate(this.noiseValues[_xa % this.Frequency, _ya % this.frequency], this.noiseValues[_xb % this.Frequency, _ya % this.frequency], Px);
			double pb = this.interpolate(this.noiseValues[_xa % this.Frequency, _yb % this.frequency], this.noiseValues[_xb % this.Frequency, _yb % this.frequency], Px);
			return this.interpolate(pa, pb, Py);
		}

		// Token: 0x06004F02 RID: 20226 RVA: 0x0014C284 File Offset: 0x0014A484
		private double interpolate(double Pa, double Pb, double Px)
		{
			double num = Px * 3.1415927410125732;
			double num2 = (double)(1f - Mathf.Cos((float)num)) * 0.5;
			return Pa * (1.0 - num2) + Pb * num2;
		}

		// Token: 0x17000F22 RID: 3874
		// (get) Token: 0x06004F03 RID: 20227 RVA: 0x0014C2C8 File Offset: 0x0014A4C8
		public float Amplitude
		{
			get
			{
				return this.amplitude;
			}
		}

		// Token: 0x17000F23 RID: 3875
		// (get) Token: 0x06004F04 RID: 20228 RVA: 0x0014C2D0 File Offset: 0x0014A4D0
		public int Frequency
		{
			get
			{
				return this.frequency;
			}
		}

		// Token: 0x04002D36 RID: 11574
		private double[,] noiseValues;

		// Token: 0x04002D37 RID: 11575
		private float amplitude = 1f;

		// Token: 0x04002D38 RID: 11576
		private int frequency = 1;
	}

	// Token: 0x02000919 RID: 2329
	// (Invoke) Token: 0x06004F06 RID: 20230
	public delegate void ErosionProgressDelegate(string titleString, string displayString, int iteration, int nIterations, float percentComplete);

	// Token: 0x0200091A RID: 2330
	// (Invoke) Token: 0x06004F0A RID: 20234
	public delegate void TextureProgressDelegate(string titleString, string displayString, float percentComplete);

	// Token: 0x0200091B RID: 2331
	// (Invoke) Token: 0x06004F0E RID: 20238
	public delegate void GeneratorProgressDelegate(string titleString, string displayString, float percentComplete);
}
