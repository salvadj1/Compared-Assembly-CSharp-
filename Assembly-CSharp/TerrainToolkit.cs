using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000812 RID: 2066
[AddComponentMenu("Terrain/Terrain Toolkit")]
[ExecuteInEditMode]
public class TerrainToolkit : MonoBehaviour
{
	// Token: 0x06004A14 RID: 18964 RVA: 0x0013CDF8 File Offset: 0x0013AFF8
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
		this.voronoiPresets.Add(new TerrainToolkit.voronoiPresetData("Scattered Peaks", TerrainToolkit.VoronoiType.Linear, 16, 8f, 0.5f, 1f));
		this.voronoiPresets.Add(new TerrainToolkit.voronoiPresetData("Rolling Hills", TerrainToolkit.VoronoiType.Sine, 8, 8f, 0f, 1f));
		this.voronoiPresets.Add(new TerrainToolkit.voronoiPresetData("Jagged Mountains", TerrainToolkit.VoronoiType.Linear, 32, 32f, 0.5f, 1f));
		this.fractalPresets.Add(new TerrainToolkit.fractalPresetData("Rolling Plains", 0.4f, 1f));
		this.fractalPresets.Add(new TerrainToolkit.fractalPresetData("Rough Mountains", 0.5f, 1f));
		this.fractalPresets.Add(new TerrainToolkit.fractalPresetData("Add Noise", 0.75f, 0.05f));
		this.perlinPresets.Add(new TerrainToolkit.perlinPresetData("Rough Plains", 2, 0.5f, 9, 1f));
		this.perlinPresets.Add(new TerrainToolkit.perlinPresetData("Rolling Hills", 5, 0.75f, 3, 1f));
		this.perlinPresets.Add(new TerrainToolkit.perlinPresetData("Rocky Mountains", 4, 1f, 8, 1f));
		this.perlinPresets.Add(new TerrainToolkit.perlinPresetData("Hellish Landscape", 11, 1f, 7, 1f));
		this.perlinPresets.Add(new TerrainToolkit.perlinPresetData("Add Noise", 10, 1f, 8, 0.2f));
		this.thermalErosionPresets.Add(new TerrainToolkit.thermalErosionPresetData("Gradual, Weak Erosion", 25, 7.5f, 0.5f));
		this.thermalErosionPresets.Add(new TerrainToolkit.thermalErosionPresetData("Fast, Harsh Erosion", 25, 2.5f, 0.1f));
		this.thermalErosionPresets.Add(new TerrainToolkit.thermalErosionPresetData("Thermal Erosion Brush", 25, 0.1f, 0f));
		this.fastHydraulicErosionPresets.Add(new TerrainToolkit.fastHydraulicErosionPresetData("Rainswept Earth", 25, 70f, 1f));
		this.fastHydraulicErosionPresets.Add(new TerrainToolkit.fastHydraulicErosionPresetData("Terraced Slopes", 25, 30f, 0.4f));
		this.fastHydraulicErosionPresets.Add(new TerrainToolkit.fastHydraulicErosionPresetData("Hydraulic Erosion Brush", 25, 85f, 1f));
		this.fullHydraulicErosionPresets.Add(new TerrainToolkit.fullHydraulicErosionPresetData("Low Rainfall, Hard Rock", 25, 0.01f, 0.5f, 0.01f, 0.1f));
		this.fullHydraulicErosionPresets.Add(new TerrainToolkit.fullHydraulicErosionPresetData("Low Rainfall, Soft Earth", 25, 0.01f, 0.5f, 0.06f, 0.15f));
		this.fullHydraulicErosionPresets.Add(new TerrainToolkit.fullHydraulicErosionPresetData("Heavy Rainfall, Hard Rock", 25, 0.02f, 0.5f, 0.01f, 0.1f));
		this.fullHydraulicErosionPresets.Add(new TerrainToolkit.fullHydraulicErosionPresetData("Heavy Rainfall, Soft Earth", 25, 0.02f, 0.5f, 0.06f, 0.15f));
		this.velocityHydraulicErosionPresets.Add(new TerrainToolkit.velocityHydraulicErosionPresetData("Low Rainfall, Hard Rock", 25, 0.01f, 0.5f, 0.01f, 0.1f, 1f, 1f, 0.05f, 0.12f));
		this.velocityHydraulicErosionPresets.Add(new TerrainToolkit.velocityHydraulicErosionPresetData("Low Rainfall, Soft Earth", 25, 0.01f, 0.5f, 0.06f, 0.15f, 1.2f, 2.8f, 0.05f, 0.12f));
		this.velocityHydraulicErosionPresets.Add(new TerrainToolkit.velocityHydraulicErosionPresetData("Heavy Rainfall, Hard Rock", 25, 0.02f, 0.5f, 0.01f, 0.1f, 1.1f, 2.2f, 0.05f, 0.12f));
		this.velocityHydraulicErosionPresets.Add(new TerrainToolkit.velocityHydraulicErosionPresetData("Heavy Rainfall, Soft Earth", 25, 0.02f, 0.5f, 0.06f, 0.15f, 1.2f, 2.4f, 0.05f, 0.12f));
		this.velocityHydraulicErosionPresets.Add(new TerrainToolkit.velocityHydraulicErosionPresetData("Carved Stone", 25, 0.01f, 0.5f, 0.01f, 0.1f, 2f, 1.25f, 0.05f, 0.35f));
		this.tidalErosionPresets.Add(new TerrainToolkit.tidalErosionPresetData("Low Tidal Range, Calm Waves", 25, 5f, 65f));
		this.tidalErosionPresets.Add(new TerrainToolkit.tidalErosionPresetData("Low Tidal Range, Strong Waves", 25, 5f, 35f));
		this.tidalErosionPresets.Add(new TerrainToolkit.tidalErosionPresetData("High Tidal Range, Calm Water", 25, 15f, 55f));
		this.tidalErosionPresets.Add(new TerrainToolkit.tidalErosionPresetData("High Tidal Range, Strong Waves", 25, 15f, 25f));
		this.windErosionPresets.Add(new TerrainToolkit.windErosionPresetData("Default (Northerly)", 25, 180f, 0.5f, 0.01f, 0.5f, 0.01f, 0.1f, 0.25f));
		this.windErosionPresets.Add(new TerrainToolkit.windErosionPresetData("Default (Southerly)", 25, 0f, 0.5f, 0.01f, 0.5f, 0.01f, 0.1f, 0.25f));
		this.windErosionPresets.Add(new TerrainToolkit.windErosionPresetData("Default (Easterly)", 25, 270f, 0.5f, 0.01f, 0.5f, 0.01f, 0.1f, 0.25f));
		this.windErosionPresets.Add(new TerrainToolkit.windErosionPresetData("Default (Westerly)", 25, 90f, 0.5f, 0.01f, 0.5f, 0.01f, 0.1f, 0.25f));
	}

	// Token: 0x06004A15 RID: 18965 RVA: 0x0013D424 File Offset: 0x0013B624
	public void setVoronoiPreset(TerrainToolkit.voronoiPresetData preset)
	{
		this.generatorTypeInt = 0;
		this.generatorType = TerrainToolkit.GeneratorType.Voronoi;
		this.voronoiTypeInt = (int)preset.voronoiType;
		this.voronoiType = preset.voronoiType;
		this.voronoiCells = preset.voronoiCells;
		this.voronoiFeatures = preset.voronoiFeatures;
		this.voronoiScale = preset.voronoiScale;
		this.voronoiBlend = preset.voronoiBlend;
	}

	// Token: 0x06004A16 RID: 18966 RVA: 0x0013D488 File Offset: 0x0013B688
	public void setFractalPreset(TerrainToolkit.fractalPresetData preset)
	{
		this.generatorTypeInt = 1;
		this.generatorType = TerrainToolkit.GeneratorType.DiamondSquare;
		this.diamondSquareDelta = preset.diamondSquareDelta;
		this.diamondSquareBlend = preset.diamondSquareBlend;
	}

	// Token: 0x06004A17 RID: 18967 RVA: 0x0013D4BC File Offset: 0x0013B6BC
	public void setPerlinPreset(TerrainToolkit.perlinPresetData preset)
	{
		this.generatorTypeInt = 2;
		this.generatorType = TerrainToolkit.GeneratorType.Perlin;
		this.perlinFrequency = preset.perlinFrequency;
		this.perlinAmplitude = preset.perlinAmplitude;
		this.perlinOctaves = preset.perlinOctaves;
		this.perlinBlend = preset.perlinBlend;
	}

	// Token: 0x06004A18 RID: 18968 RVA: 0x0013D508 File Offset: 0x0013B708
	public void setThermalErosionPreset(TerrainToolkit.thermalErosionPresetData preset)
	{
		this.erosionTypeInt = 0;
		this.erosionType = TerrainToolkit.ErosionType.Thermal;
		this.thermalIterations = preset.thermalIterations;
		this.thermalMinSlope = preset.thermalMinSlope;
		this.thermalFalloff = preset.thermalFalloff;
	}

	// Token: 0x06004A19 RID: 18969 RVA: 0x0013D548 File Offset: 0x0013B748
	public void setFastHydraulicErosionPreset(TerrainToolkit.fastHydraulicErosionPresetData preset)
	{
		this.erosionTypeInt = 1;
		this.erosionType = TerrainToolkit.ErosionType.Hydraulic;
		this.hydraulicTypeInt = 0;
		this.hydraulicType = TerrainToolkit.HydraulicType.Fast;
		this.hydraulicIterations = preset.hydraulicIterations;
		this.hydraulicMaxSlope = preset.hydraulicMaxSlope;
		this.hydraulicFalloff = preset.hydraulicFalloff;
	}

	// Token: 0x06004A1A RID: 18970 RVA: 0x0013D598 File Offset: 0x0013B798
	public void setFullHydraulicErosionPreset(TerrainToolkit.fullHydraulicErosionPresetData preset)
	{
		this.erosionTypeInt = 1;
		this.erosionType = TerrainToolkit.ErosionType.Hydraulic;
		this.hydraulicTypeInt = 1;
		this.hydraulicType = TerrainToolkit.HydraulicType.Full;
		this.hydraulicIterations = preset.hydraulicIterations;
		this.hydraulicRainfall = preset.hydraulicRainfall;
		this.hydraulicEvaporation = preset.hydraulicEvaporation;
		this.hydraulicSedimentSolubility = preset.hydraulicSedimentSolubility;
		this.hydraulicSedimentSaturation = preset.hydraulicSedimentSaturation;
	}

	// Token: 0x06004A1B RID: 18971 RVA: 0x0013D600 File Offset: 0x0013B800
	public void setVelocityHydraulicErosionPreset(TerrainToolkit.velocityHydraulicErosionPresetData preset)
	{
		this.erosionTypeInt = 1;
		this.erosionType = TerrainToolkit.ErosionType.Hydraulic;
		this.hydraulicTypeInt = 2;
		this.hydraulicType = TerrainToolkit.HydraulicType.Velocity;
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

	// Token: 0x06004A1C RID: 18972 RVA: 0x0013D698 File Offset: 0x0013B898
	public void setTidalErosionPreset(TerrainToolkit.tidalErosionPresetData preset)
	{
		this.erosionTypeInt = 2;
		this.erosionType = TerrainToolkit.ErosionType.Tidal;
		this.tidalIterations = preset.tidalIterations;
		this.tidalRangeAmount = preset.tidalRangeAmount;
		this.tidalCliffLimit = preset.tidalCliffLimit;
	}

	// Token: 0x06004A1D RID: 18973 RVA: 0x0013D6D8 File Offset: 0x0013B8D8
	public void setWindErosionPreset(TerrainToolkit.windErosionPresetData preset)
	{
		this.erosionTypeInt = 3;
		this.erosionType = TerrainToolkit.ErosionType.Wind;
		this.windIterations = preset.windIterations;
		this.windDirection = preset.windDirection;
		this.windForce = preset.windForce;
		this.windLift = preset.windLift;
		this.windGravity = preset.windGravity;
		this.windCapacity = preset.windCapacity;
		this.windEntropy = preset.windEntropy;
		this.windSmoothing = preset.windSmoothing;
	}

	// Token: 0x06004A1E RID: 18974 RVA: 0x0013D754 File Offset: 0x0013B954
	public void Update()
	{
		if (this.isBrushOn && (this.toolModeInt != 1 || this.erosionTypeInt > 2 || (this.erosionTypeInt == 1 && this.hydraulicTypeInt > 0)))
		{
			this.isBrushOn = false;
		}
	}

	// Token: 0x06004A1F RID: 18975 RVA: 0x0013D7A4 File Offset: 0x0013B9A4
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

	// Token: 0x06004A20 RID: 18976 RVA: 0x0013DB68 File Offset: 0x0013BD68
	public void paint()
	{
		this.convertIntVarsToEnums();
		this.erodeTerrainWithBrush();
	}

	// Token: 0x06004A21 RID: 18977 RVA: 0x0013DB78 File Offset: 0x0013BD78
	private void erodeTerrainWithBrush()
	{
		this.erosionMode = TerrainToolkit.ErosionMode.Brush;
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
			TerrainToolkit.ErosionProgressDelegate erosionProgressDelegate = new TerrainToolkit.ErosionProgressDelegate(this.dummyErosionProgress);
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

	// Token: 0x06004A22 RID: 18978 RVA: 0x0013DE18 File Offset: 0x0013C018
	public void erodeAllTerrain(TerrainToolkit.ErosionProgressDelegate erosionProgressDelegate)
	{
		this.erosionMode = TerrainToolkit.ErosionMode.Filter;
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
			case TerrainToolkit.ErosionType.Thermal:
			{
				int iterations = this.thermalIterations;
				array = this.fastErosion(array, new Vector2((float)heightmapWidth, (float)heightmapHeight), iterations, erosionProgressDelegate);
				break;
			}
			case TerrainToolkit.ErosionType.Hydraulic:
			{
				int iterations = this.hydraulicIterations;
				switch (this.hydraulicType)
				{
				case TerrainToolkit.HydraulicType.Fast:
					array = this.fastErosion(array, new Vector2((float)heightmapWidth, (float)heightmapHeight), iterations, erosionProgressDelegate);
					break;
				case TerrainToolkit.HydraulicType.Full:
					array = this.fullHydraulicErosion(array, new Vector2((float)heightmapWidth, (float)heightmapHeight), iterations, erosionProgressDelegate);
					break;
				case TerrainToolkit.HydraulicType.Velocity:
					array = this.velocityHydraulicErosion(array, new Vector2((float)heightmapWidth, (float)heightmapHeight), iterations, erosionProgressDelegate);
					break;
				}
				break;
			}
			case TerrainToolkit.ErosionType.Tidal:
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
			case TerrainToolkit.ErosionType.Wind:
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

	// Token: 0x06004A23 RID: 18979 RVA: 0x0013E028 File Offset: 0x0013C228
	private float[,] fastErosion(float[,] heightMap, Vector2 arraySize, int iterations, TerrainToolkit.ErosionProgressDelegate erosionProgressDelegate)
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
		case TerrainToolkit.ErosionType.Thermal:
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
		case TerrainToolkit.ErosionType.Hydraulic:
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
		case TerrainToolkit.ErosionType.Tidal:
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
							if ((m != num19 || l != num16) && (this.neighbourhood == TerrainToolkit.Neighbourhood.Moore || (this.neighbourhood == TerrainToolkit.Neighbourhood.VonNeumann && (m == num19 || l == num16))))
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
					case TerrainToolkit.ErosionType.Thermal:
						if (num28 >= num3)
						{
							flag = true;
						}
						break;
					case TerrainToolkit.ErosionType.Hydraulic:
						if (num28 > 0f && num28 <= num6)
						{
							flag = true;
						}
						break;
					case TerrainToolkit.ErosionType.Tidal:
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
						if (this.erosionType == TerrainToolkit.ErosionType.Tidal)
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
							if (this.erosionType == TerrainToolkit.ErosionType.Thermal)
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
							if (this.erosionMode == TerrainToolkit.ErosionMode.Filter || (this.erosionMode == TerrainToolkit.ErosionMode.Brush && this.useDifferenceMaps))
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
									if ((m != num19 || l != num16) && (this.neighbourhood == TerrainToolkit.Neighbourhood.Moore || (this.neighbourhood == TerrainToolkit.Neighbourhood.VonNeumann && (m == num19 || l == num16))))
									{
										float num40 = heightMap[k + m + num18, j + l + num15];
										float num27 = num36 - num40;
										if (num27 > 0f)
										{
											float num41 = num35 * (num27 / num22);
											if (this.erosionMode == TerrainToolkit.ErosionMode.Filter || (this.erosionMode == TerrainToolkit.ErosionMode.Brush && this.useDifferenceMaps))
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
			if ((this.erosionMode == TerrainToolkit.ErosionMode.Filter || (this.erosionMode == TerrainToolkit.ErosionMode.Brush && this.useDifferenceMaps)) && this.erosionType != TerrainToolkit.ErosionType.Tidal)
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
			if (this.erosionMode == TerrainToolkit.ErosionMode.Filter)
			{
				string titleString = string.Empty;
				string displayString = string.Empty;
				switch (this.erosionType)
				{
				case TerrainToolkit.ErosionType.Thermal:
					titleString = "Applying Thermal Erosion";
					displayString = "Applying thermal erosion.";
					break;
				case TerrainToolkit.ErosionType.Hydraulic:
					titleString = "Applying Hydraulic Erosion";
					displayString = "Applying hydraulic erosion.";
					break;
				case TerrainToolkit.ErosionType.Tidal:
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

	// Token: 0x06004A24 RID: 18980 RVA: 0x0013E920 File Offset: 0x0013CB20
	private float[,] velocityHydraulicErosion(float[,] heightMap, Vector2 arraySize, int iterations, TerrainToolkit.ErosionProgressDelegate erosionProgressDelegate)
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
						if ((l != num9 || k != num6) && (this.neighbourhood == TerrainToolkit.Neighbourhood.Moore || (this.neighbourhood == TerrainToolkit.Neighbourhood.VonNeumann && (l == num9 || k == num6))))
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
							if ((l != num9 || k != num6) && (this.neighbourhood == TerrainToolkit.Neighbourhood.Moore || (this.neighbourhood == TerrainToolkit.Neighbourhood.VonNeumann && (l == num9 || k == num6))))
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
							if ((l != num9 || k != num6) && (this.neighbourhood == TerrainToolkit.Neighbourhood.Moore || (this.neighbourhood == TerrainToolkit.Neighbourhood.VonNeumann && (l == num9 || k == num6))))
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

	// Token: 0x06004A25 RID: 18981 RVA: 0x0013F3BC File Offset: 0x0013D5BC
	private float[,] fullHydraulicErosion(float[,] heightMap, Vector2 arraySize, int iterations, TerrainToolkit.ErosionProgressDelegate erosionProgressDelegate)
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
							if ((m != num13 || l != num10) && (this.neighbourhood == TerrainToolkit.Neighbourhood.Moore || (this.neighbourhood == TerrainToolkit.Neighbourhood.VonNeumann && (m == num13 || l == num10))))
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
							if ((m != num13 || l != num10) && (this.neighbourhood == TerrainToolkit.Neighbourhood.Moore || (this.neighbourhood == TerrainToolkit.Neighbourhood.VonNeumann && (m == num13 || l == num10))))
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

	// Token: 0x06004A26 RID: 18982 RVA: 0x0013FADC File Offset: 0x0013DCDC
	private float[,] windErosion(float[,] heightMap, Vector2 arraySize, int iterations, TerrainToolkit.ErosionProgressDelegate erosionProgressDelegate)
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
							if ((m != num20 || l != num17) && (this.neighbourhood == TerrainToolkit.Neighbourhood.Moore || (this.neighbourhood == TerrainToolkit.Neighbourhood.VonNeumann && (m == num20 || l == num17))))
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
			TerrainToolkit.GeneratorProgressDelegate generatorProgressDelegate = new TerrainToolkit.GeneratorProgressDelegate(this.dummyGeneratorProgress);
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

	// Token: 0x06004A27 RID: 18983 RVA: 0x0014024C File Offset: 0x0013E44C
	public void textureTerrain(TerrainToolkit.TextureProgressDelegate textureProgressDelegate)
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

	// Token: 0x06004A28 RID: 18984 RVA: 0x0014072C File Offset: 0x0013E92C
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

	// Token: 0x06004A29 RID: 18985 RVA: 0x001407C8 File Offset: 0x0013E9C8
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

	// Token: 0x06004A2A RID: 18986 RVA: 0x00140850 File Offset: 0x0013EA50
	public void deleteAllSplatPrototypes()
	{
		SplatPrototype[] array = new SplatPrototype[0];
		this.splatPrototypes = array;
	}

	// Token: 0x06004A2B RID: 18987 RVA: 0x0014086C File Offset: 0x0013EA6C
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

	// Token: 0x06004A2C RID: 18988 RVA: 0x001408E4 File Offset: 0x0013EAE4
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

	// Token: 0x06004A2D RID: 18989 RVA: 0x00140944 File Offset: 0x0013EB44
	public void deleteAllBlendPoints()
	{
		this.heightBlendPoints = new List<float>();
	}

	// Token: 0x06004A2E RID: 18990 RVA: 0x00140954 File Offset: 0x0013EB54
	public void generateTerrain(TerrainToolkit.GeneratorProgressDelegate generatorProgressDelegate)
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
		case TerrainToolkit.GeneratorType.Voronoi:
			array = this.generateVoronoi(array, new Vector2((float)heightmapWidth, (float)heightmapHeight), generatorProgressDelegate);
			break;
		case TerrainToolkit.GeneratorType.DiamondSquare:
			array = this.generateDiamondSquare(array, new Vector2((float)heightmapWidth, (float)heightmapHeight), generatorProgressDelegate);
			break;
		case TerrainToolkit.GeneratorType.Perlin:
			array = this.generatePerlin(array, new Vector2((float)heightmapWidth, (float)heightmapHeight), generatorProgressDelegate);
			break;
		case TerrainToolkit.GeneratorType.Smooth:
			array = this.smooth(array, new Vector2((float)heightmapWidth, (float)heightmapHeight), generatorProgressDelegate);
			break;
		case TerrainToolkit.GeneratorType.Normalise:
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
				case TerrainToolkit.GeneratorType.Voronoi:
					num3 = num2 * this.voronoiBlend + num * (1f - this.voronoiBlend);
					break;
				case TerrainToolkit.GeneratorType.DiamondSquare:
					num3 = num2 * this.diamondSquareBlend + num * (1f - this.diamondSquareBlend);
					break;
				case TerrainToolkit.GeneratorType.Perlin:
					num3 = num2 * this.perlinBlend + num * (1f - this.perlinBlend);
					break;
				case TerrainToolkit.GeneratorType.Smooth:
					num3 = num2 * this.smoothBlend + num * (1f - this.smoothBlend);
					break;
				case TerrainToolkit.GeneratorType.Normalise:
					num3 = num2 * this.normaliseBlend + num * (1f - this.normaliseBlend);
					break;
				}
				heights[j, i] = num3;
			}
		}
		terrainData.SetHeights(0, 0, heights);
	}

	// Token: 0x06004A2F RID: 18991 RVA: 0x00140B8C File Offset: 0x0013ED8C
	private float[,] generateVoronoi(float[,] heightMap, Vector2 arraySize, TerrainToolkit.GeneratorProgressDelegate generatorProgressDelegate)
	{
		int num = (int)arraySize.x;
		int num2 = (int)arraySize.y;
		ArrayList arrayList = new ArrayList();
		for (int i = 0; i < this.voronoiCells; i++)
		{
			TerrainToolkit.Peak peak = default(TerrainToolkit.Peak);
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
					Vector2 peakPoint = ((TerrainToolkit.Peak)arrayList[i]).peakPoint;
					float dist = Vector2.Distance(peakPoint, new Vector2((float)k, (float)j));
					arrayList2.Add(new TerrainToolkit.PeakDistance
					{
						id = i,
						dist = dist
					});
				}
				arrayList2.Sort();
				TerrainToolkit.PeakDistance peakDistance = (TerrainToolkit.PeakDistance)arrayList2[0];
				TerrainToolkit.PeakDistance peakDistance2 = (TerrainToolkit.PeakDistance)arrayList2[1];
				int id = peakDistance.id;
				float dist2 = peakDistance.dist;
				float dist3 = peakDistance2.dist;
				float num6 = Mathf.Abs(dist2 - dist3) / ((float)(num + num2) / Mathf.Sqrt((float)this.voronoiCells));
				float num7 = ((TerrainToolkit.Peak)arrayList[id]).peakHeight;
				float num8 = num7 - Mathf.Abs(dist2 / dist3) * num7;
				switch (this.voronoiType)
				{
				case TerrainToolkit.VoronoiType.Sine:
				{
					float num9 = num8 * 3.14159274f - 1.57079637f;
					num8 = 0.5f + Mathf.Sin(num9) / 2f;
					break;
				}
				case TerrainToolkit.VoronoiType.Tangent:
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

	// Token: 0x06004A30 RID: 18992 RVA: 0x00140EB0 File Offset: 0x0013F0B0
	private float[,] generateDiamondSquare(float[,] heightMap, Vector2 arraySize, TerrainToolkit.GeneratorProgressDelegate generatorProgressDelegate)
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

	// Token: 0x06004A31 RID: 18993 RVA: 0x0014116C File Offset: 0x0013F36C
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

	// Token: 0x06004A32 RID: 18994 RVA: 0x00141334 File Offset: 0x0013F534
	private float[,] generatePerlin(float[,] heightMap, Vector2 arraySize, TerrainToolkit.GeneratorProgressDelegate generatorProgressDelegate)
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
		TerrainToolkit.PerlinNoise2D[] array = new TerrainToolkit.PerlinNoise2D[this.perlinOctaves];
		int num3 = this.perlinFrequency;
		float num4 = 1f;
		for (int k = 0; k < this.perlinOctaves; k++)
		{
			array[k] = new TerrainToolkit.PerlinNoise2D(num3, num4);
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
		TerrainToolkit.GeneratorProgressDelegate generatorProgressDelegate2 = new TerrainToolkit.GeneratorProgressDelegate(this.dummyGeneratorProgress);
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

	// Token: 0x06004A33 RID: 18995 RVA: 0x0014159C File Offset: 0x0013F79C
	private float[,] smooth(float[,] heightMap, Vector2 arraySize, TerrainToolkit.GeneratorProgressDelegate generatorProgressDelegate)
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
							if (this.neighbourhood == TerrainToolkit.Neighbourhood.Moore || (this.neighbourhood == TerrainToolkit.Neighbourhood.VonNeumann && (m == num8 || l == num5)))
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

	// Token: 0x06004A34 RID: 18996 RVA: 0x00141730 File Offset: 0x0013F930
	private float[,] normalise(float[,] heightMap, Vector2 arraySize, TerrainToolkit.GeneratorProgressDelegate generatorProgressDelegate)
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

	// Token: 0x06004A35 RID: 18997 RVA: 0x00141848 File Offset: 0x0013FA48
	public void FastThermalErosion(int iterations, float minSlope, float blendAmount)
	{
		this.erosionTypeInt = 0;
		this.erosionType = TerrainToolkit.ErosionType.Thermal;
		this.thermalIterations = iterations;
		this.thermalMinSlope = minSlope;
		this.thermalFalloff = blendAmount;
		this.neighbourhood = TerrainToolkit.Neighbourhood.Moore;
		TerrainToolkit.ErosionProgressDelegate erosionProgressDelegate = new TerrainToolkit.ErosionProgressDelegate(this.dummyErosionProgress);
		this.erodeAllTerrain(erosionProgressDelegate);
	}

	// Token: 0x06004A36 RID: 18998 RVA: 0x00141894 File Offset: 0x0013FA94
	public void FastHydraulicErosion(int iterations, float maxSlope, float blendAmount)
	{
		this.erosionTypeInt = 1;
		this.erosionType = TerrainToolkit.ErosionType.Hydraulic;
		this.hydraulicTypeInt = 0;
		this.hydraulicType = TerrainToolkit.HydraulicType.Fast;
		this.hydraulicIterations = iterations;
		this.hydraulicMaxSlope = maxSlope;
		this.hydraulicFalloff = blendAmount;
		this.neighbourhood = TerrainToolkit.Neighbourhood.Moore;
		TerrainToolkit.ErosionProgressDelegate erosionProgressDelegate = new TerrainToolkit.ErosionProgressDelegate(this.dummyErosionProgress);
		this.erodeAllTerrain(erosionProgressDelegate);
	}

	// Token: 0x06004A37 RID: 18999 RVA: 0x001418F0 File Offset: 0x0013FAF0
	public void FullHydraulicErosion(int iterations, float rainfall, float evaporation, float solubility, float saturation)
	{
		this.erosionTypeInt = 1;
		this.erosionType = TerrainToolkit.ErosionType.Hydraulic;
		this.hydraulicTypeInt = 1;
		this.hydraulicType = TerrainToolkit.HydraulicType.Full;
		this.hydraulicIterations = iterations;
		this.hydraulicRainfall = rainfall;
		this.hydraulicEvaporation = evaporation;
		this.hydraulicSedimentSolubility = solubility;
		this.hydraulicSedimentSaturation = saturation;
		this.neighbourhood = TerrainToolkit.Neighbourhood.Moore;
		TerrainToolkit.ErosionProgressDelegate erosionProgressDelegate = new TerrainToolkit.ErosionProgressDelegate(this.dummyErosionProgress);
		this.erodeAllTerrain(erosionProgressDelegate);
	}

	// Token: 0x06004A38 RID: 19000 RVA: 0x0014195C File Offset: 0x0013FB5C
	public void VelocityHydraulicErosion(int iterations, float rainfall, float evaporation, float solubility, float saturation, float velocity, float momentum, float entropy, float downcutting)
	{
		this.erosionTypeInt = 1;
		this.erosionType = TerrainToolkit.ErosionType.Hydraulic;
		this.hydraulicTypeInt = 2;
		this.hydraulicType = TerrainToolkit.HydraulicType.Velocity;
		this.hydraulicIterations = iterations;
		this.hydraulicVelocityRainfall = rainfall;
		this.hydraulicVelocityEvaporation = evaporation;
		this.hydraulicVelocitySedimentSolubility = solubility;
		this.hydraulicVelocitySedimentSaturation = saturation;
		this.hydraulicVelocity = velocity;
		this.hydraulicMomentum = momentum;
		this.hydraulicEntropy = entropy;
		this.hydraulicDowncutting = downcutting;
		this.neighbourhood = TerrainToolkit.Neighbourhood.Moore;
		TerrainToolkit.ErosionProgressDelegate erosionProgressDelegate = new TerrainToolkit.ErosionProgressDelegate(this.dummyErosionProgress);
		this.erodeAllTerrain(erosionProgressDelegate);
	}

	// Token: 0x06004A39 RID: 19001 RVA: 0x001419E8 File Offset: 0x0013FBE8
	public void TidalErosion(int iterations, float seaLevel, float tidalRange, float cliffLimit)
	{
		this.erosionTypeInt = 2;
		this.erosionType = TerrainToolkit.ErosionType.Tidal;
		this.tidalIterations = iterations;
		this.tidalSeaLevel = seaLevel;
		this.tidalRangeAmount = tidalRange;
		this.tidalCliffLimit = cliffLimit;
		this.neighbourhood = TerrainToolkit.Neighbourhood.Moore;
		TerrainToolkit.ErosionProgressDelegate erosionProgressDelegate = new TerrainToolkit.ErosionProgressDelegate(this.dummyErosionProgress);
		this.erodeAllTerrain(erosionProgressDelegate);
	}

	// Token: 0x06004A3A RID: 19002 RVA: 0x00141A3C File Offset: 0x0013FC3C
	public void WindErosion(int iterations, float direction, float force, float lift, float gravity, float capacity, float entropy, float smoothing)
	{
		this.erosionTypeInt = 3;
		this.erosionType = TerrainToolkit.ErosionType.Wind;
		this.windIterations = iterations;
		this.windDirection = direction;
		this.windForce = force;
		this.windLift = lift;
		this.windGravity = gravity;
		this.windCapacity = capacity;
		this.windEntropy = entropy;
		this.windSmoothing = smoothing;
		this.neighbourhood = TerrainToolkit.Neighbourhood.Moore;
		TerrainToolkit.ErosionProgressDelegate erosionProgressDelegate = new TerrainToolkit.ErosionProgressDelegate(this.dummyErosionProgress);
		this.erodeAllTerrain(erosionProgressDelegate);
	}

	// Token: 0x06004A3B RID: 19003 RVA: 0x00141AB0 File Offset: 0x0013FCB0
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
		TerrainToolkit.TextureProgressDelegate textureProgressDelegate = new TerrainToolkit.TextureProgressDelegate(this.dummyTextureProgress);
		this.textureTerrain(textureProgressDelegate);
	}

	// Token: 0x06004A3C RID: 19004 RVA: 0x00141C7C File Offset: 0x0013FE7C
	public void VoronoiGenerator(TerrainToolkit.FeatureType featureType, int cells, float features, float scale, float blend)
	{
		this.generatorTypeInt = 0;
		this.generatorType = TerrainToolkit.GeneratorType.Voronoi;
		switch (featureType)
		{
		case TerrainToolkit.FeatureType.Mountains:
			this.voronoiTypeInt = 0;
			this.voronoiType = TerrainToolkit.VoronoiType.Linear;
			break;
		case TerrainToolkit.FeatureType.Hills:
			this.voronoiTypeInt = 1;
			this.voronoiType = TerrainToolkit.VoronoiType.Sine;
			break;
		case TerrainToolkit.FeatureType.Plateaus:
			this.voronoiTypeInt = 2;
			this.voronoiType = TerrainToolkit.VoronoiType.Tangent;
			break;
		}
		this.voronoiCells = cells;
		this.voronoiFeatures = features;
		this.voronoiScale = scale;
		this.voronoiBlend = blend;
		TerrainToolkit.GeneratorProgressDelegate generatorProgressDelegate = new TerrainToolkit.GeneratorProgressDelegate(this.dummyGeneratorProgress);
		this.generateTerrain(generatorProgressDelegate);
	}

	// Token: 0x06004A3D RID: 19005 RVA: 0x00141D1C File Offset: 0x0013FF1C
	public void FractalGenerator(float fractalDelta, float blend)
	{
		this.generatorTypeInt = 1;
		this.generatorType = TerrainToolkit.GeneratorType.DiamondSquare;
		this.diamondSquareDelta = fractalDelta;
		this.diamondSquareBlend = blend;
		TerrainToolkit.GeneratorProgressDelegate generatorProgressDelegate = new TerrainToolkit.GeneratorProgressDelegate(this.dummyGeneratorProgress);
		this.generateTerrain(generatorProgressDelegate);
	}

	// Token: 0x06004A3E RID: 19006 RVA: 0x00141D5C File Offset: 0x0013FF5C
	public void PerlinGenerator(int frequency, float amplitude, int octaves, float blend)
	{
		this.generatorTypeInt = 2;
		this.generatorType = TerrainToolkit.GeneratorType.Perlin;
		this.perlinFrequency = frequency;
		this.perlinAmplitude = amplitude;
		this.perlinOctaves = octaves;
		this.perlinBlend = blend;
		TerrainToolkit.GeneratorProgressDelegate generatorProgressDelegate = new TerrainToolkit.GeneratorProgressDelegate(this.dummyGeneratorProgress);
		this.generateTerrain(generatorProgressDelegate);
	}

	// Token: 0x06004A3F RID: 19007 RVA: 0x00141DA8 File Offset: 0x0013FFA8
	public void SmoothTerrain(int iterations, float blend)
	{
		this.generatorTypeInt = 3;
		this.generatorType = TerrainToolkit.GeneratorType.Smooth;
		this.smoothIterations = iterations;
		this.smoothBlend = blend;
		TerrainToolkit.GeneratorProgressDelegate generatorProgressDelegate = new TerrainToolkit.GeneratorProgressDelegate(this.dummyGeneratorProgress);
		this.generateTerrain(generatorProgressDelegate);
	}

	// Token: 0x06004A40 RID: 19008 RVA: 0x00141DE8 File Offset: 0x0013FFE8
	public void NormaliseTerrain(float minHeight, float maxHeight, float blend)
	{
		this.generatorTypeInt = 4;
		this.generatorType = TerrainToolkit.GeneratorType.Normalise;
		this.normaliseMin = minHeight;
		this.normaliseMax = maxHeight;
		this.normaliseBlend = blend;
		TerrainToolkit.GeneratorProgressDelegate generatorProgressDelegate = new TerrainToolkit.GeneratorProgressDelegate(this.dummyGeneratorProgress);
		this.generateTerrain(generatorProgressDelegate);
	}

	// Token: 0x06004A41 RID: 19009 RVA: 0x00141E2C File Offset: 0x0014002C
	public void NormalizeTerrain(float minHeight, float maxHeight, float blend)
	{
		this.NormaliseTerrain(minHeight, maxHeight, blend);
	}

	// Token: 0x06004A42 RID: 19010 RVA: 0x00141E38 File Offset: 0x00140038
	private void convertIntVarsToEnums()
	{
		switch (this.erosionTypeInt)
		{
		case 0:
			this.erosionType = TerrainToolkit.ErosionType.Thermal;
			break;
		case 1:
			this.erosionType = TerrainToolkit.ErosionType.Hydraulic;
			break;
		case 2:
			this.erosionType = TerrainToolkit.ErosionType.Tidal;
			break;
		case 3:
			this.erosionType = TerrainToolkit.ErosionType.Wind;
			break;
		case 4:
			this.erosionType = TerrainToolkit.ErosionType.Glacial;
			break;
		}
		switch (this.hydraulicTypeInt)
		{
		case 0:
			this.hydraulicType = TerrainToolkit.HydraulicType.Fast;
			break;
		case 1:
			this.hydraulicType = TerrainToolkit.HydraulicType.Full;
			break;
		case 2:
			this.hydraulicType = TerrainToolkit.HydraulicType.Velocity;
			break;
		}
		switch (this.generatorTypeInt)
		{
		case 0:
			this.generatorType = TerrainToolkit.GeneratorType.Voronoi;
			break;
		case 1:
			this.generatorType = TerrainToolkit.GeneratorType.DiamondSquare;
			break;
		case 2:
			this.generatorType = TerrainToolkit.GeneratorType.Perlin;
			break;
		case 3:
			this.generatorType = TerrainToolkit.GeneratorType.Smooth;
			break;
		case 4:
			this.generatorType = TerrainToolkit.GeneratorType.Normalise;
			break;
		}
		switch (this.voronoiTypeInt)
		{
		case 0:
			this.voronoiType = TerrainToolkit.VoronoiType.Linear;
			break;
		case 1:
			this.voronoiType = TerrainToolkit.VoronoiType.Sine;
			break;
		case 2:
			this.voronoiType = TerrainToolkit.VoronoiType.Tangent;
			break;
		}
		int num = this.neighbourhoodInt;
		if (num != 0)
		{
			if (num == 1)
			{
				this.neighbourhood = TerrainToolkit.Neighbourhood.VonNeumann;
			}
		}
		else
		{
			this.neighbourhood = TerrainToolkit.Neighbourhood.Moore;
		}
	}

	// Token: 0x06004A43 RID: 19011 RVA: 0x00141FC0 File Offset: 0x001401C0
	public void dummyErosionProgress(string titleString, string displayString, int iteration, int nIterations, float percentComplete)
	{
	}

	// Token: 0x06004A44 RID: 19012 RVA: 0x00141FC4 File Offset: 0x001401C4
	public void dummyTextureProgress(string titleString, string displayString, float percentComplete)
	{
	}

	// Token: 0x06004A45 RID: 19013 RVA: 0x00141FC8 File Offset: 0x001401C8
	public void dummyGeneratorProgress(string titleString, string displayString, float percentComplete)
	{
	}

	// Token: 0x04002A2B RID: 10795
	public GUISkin guiSkin;

	// Token: 0x04002A2C RID: 10796
	public Texture2D createIcon;

	// Token: 0x04002A2D RID: 10797
	public Texture2D erodeIcon;

	// Token: 0x04002A2E RID: 10798
	public Texture2D textureIcon;

	// Token: 0x04002A2F RID: 10799
	public Texture2D mooreIcon;

	// Token: 0x04002A30 RID: 10800
	public Texture2D vonNeumannIcon;

	// Token: 0x04002A31 RID: 10801
	public Texture2D mountainsIcon;

	// Token: 0x04002A32 RID: 10802
	public Texture2D hillsIcon;

	// Token: 0x04002A33 RID: 10803
	public Texture2D plateausIcon;

	// Token: 0x04002A34 RID: 10804
	public Texture2D defaultTexture;

	// Token: 0x04002A35 RID: 10805
	public int toolModeInt;

	// Token: 0x04002A36 RID: 10806
	private TerrainToolkit.ErosionMode erosionMode;

	// Token: 0x04002A37 RID: 10807
	private TerrainToolkit.ErosionType erosionType;

	// Token: 0x04002A38 RID: 10808
	public int erosionTypeInt;

	// Token: 0x04002A39 RID: 10809
	private TerrainToolkit.GeneratorType generatorType;

	// Token: 0x04002A3A RID: 10810
	public int generatorTypeInt;

	// Token: 0x04002A3B RID: 10811
	public bool isBrushOn;

	// Token: 0x04002A3C RID: 10812
	public bool isBrushHidden;

	// Token: 0x04002A3D RID: 10813
	public bool isBrushPainting;

	// Token: 0x04002A3E RID: 10814
	public Vector3 brushPosition;

	// Token: 0x04002A3F RID: 10815
	public float brushSize = 50f;

	// Token: 0x04002A40 RID: 10816
	public float brushOpacity = 1f;

	// Token: 0x04002A41 RID: 10817
	public float brushSoftness = 0.5f;

	// Token: 0x04002A42 RID: 10818
	public int neighbourhoodInt;

	// Token: 0x04002A43 RID: 10819
	private TerrainToolkit.Neighbourhood neighbourhood;

	// Token: 0x04002A44 RID: 10820
	public bool useDifferenceMaps = true;

	// Token: 0x04002A45 RID: 10821
	public int thermalIterations = 25;

	// Token: 0x04002A46 RID: 10822
	public float thermalMinSlope = 1f;

	// Token: 0x04002A47 RID: 10823
	public float thermalFalloff = 0.5f;

	// Token: 0x04002A48 RID: 10824
	public int hydraulicTypeInt;

	// Token: 0x04002A49 RID: 10825
	public TerrainToolkit.HydraulicType hydraulicType;

	// Token: 0x04002A4A RID: 10826
	public int hydraulicIterations = 25;

	// Token: 0x04002A4B RID: 10827
	public float hydraulicMaxSlope = 60f;

	// Token: 0x04002A4C RID: 10828
	public float hydraulicFalloff = 0.5f;

	// Token: 0x04002A4D RID: 10829
	public float hydraulicRainfall = 0.01f;

	// Token: 0x04002A4E RID: 10830
	public float hydraulicEvaporation = 0.5f;

	// Token: 0x04002A4F RID: 10831
	public float hydraulicSedimentSolubility = 0.01f;

	// Token: 0x04002A50 RID: 10832
	public float hydraulicSedimentSaturation = 0.1f;

	// Token: 0x04002A51 RID: 10833
	public float hydraulicVelocityRainfall = 0.01f;

	// Token: 0x04002A52 RID: 10834
	public float hydraulicVelocityEvaporation = 0.5f;

	// Token: 0x04002A53 RID: 10835
	public float hydraulicVelocitySedimentSolubility = 0.01f;

	// Token: 0x04002A54 RID: 10836
	public float hydraulicVelocitySedimentSaturation = 0.1f;

	// Token: 0x04002A55 RID: 10837
	public float hydraulicVelocity = 20f;

	// Token: 0x04002A56 RID: 10838
	public float hydraulicMomentum = 1f;

	// Token: 0x04002A57 RID: 10839
	public float hydraulicEntropy;

	// Token: 0x04002A58 RID: 10840
	public float hydraulicDowncutting = 0.1f;

	// Token: 0x04002A59 RID: 10841
	public int tidalIterations = 25;

	// Token: 0x04002A5A RID: 10842
	public float tidalSeaLevel = 50f;

	// Token: 0x04002A5B RID: 10843
	public float tidalRangeAmount = 5f;

	// Token: 0x04002A5C RID: 10844
	public float tidalCliffLimit = 60f;

	// Token: 0x04002A5D RID: 10845
	public int windIterations = 25;

	// Token: 0x04002A5E RID: 10846
	public float windDirection;

	// Token: 0x04002A5F RID: 10847
	public float windForce = 0.5f;

	// Token: 0x04002A60 RID: 10848
	public float windLift = 0.01f;

	// Token: 0x04002A61 RID: 10849
	public float windGravity = 0.5f;

	// Token: 0x04002A62 RID: 10850
	public float windCapacity = 0.01f;

	// Token: 0x04002A63 RID: 10851
	public float windEntropy = 0.1f;

	// Token: 0x04002A64 RID: 10852
	public float windSmoothing = 0.25f;

	// Token: 0x04002A65 RID: 10853
	public SplatPrototype[] splatPrototypes;

	// Token: 0x04002A66 RID: 10854
	public Texture2D tempTexture;

	// Token: 0x04002A67 RID: 10855
	public float slopeBlendMinAngle = 60f;

	// Token: 0x04002A68 RID: 10856
	public float slopeBlendMaxAngle = 75f;

	// Token: 0x04002A69 RID: 10857
	public List<float> heightBlendPoints;

	// Token: 0x04002A6A RID: 10858
	public string[] gradientStyles;

	// Token: 0x04002A6B RID: 10859
	public int voronoiTypeInt;

	// Token: 0x04002A6C RID: 10860
	public TerrainToolkit.VoronoiType voronoiType;

	// Token: 0x04002A6D RID: 10861
	public int voronoiCells = 16;

	// Token: 0x04002A6E RID: 10862
	public float voronoiFeatures = 1f;

	// Token: 0x04002A6F RID: 10863
	public float voronoiScale = 1f;

	// Token: 0x04002A70 RID: 10864
	public float voronoiBlend = 1f;

	// Token: 0x04002A71 RID: 10865
	public float diamondSquareDelta = 0.5f;

	// Token: 0x04002A72 RID: 10866
	public float diamondSquareBlend = 1f;

	// Token: 0x04002A73 RID: 10867
	public int perlinFrequency = 4;

	// Token: 0x04002A74 RID: 10868
	public float perlinAmplitude = 1f;

	// Token: 0x04002A75 RID: 10869
	public int perlinOctaves = 8;

	// Token: 0x04002A76 RID: 10870
	public float perlinBlend = 1f;

	// Token: 0x04002A77 RID: 10871
	public float smoothBlend = 1f;

	// Token: 0x04002A78 RID: 10872
	public int smoothIterations;

	// Token: 0x04002A79 RID: 10873
	public float normaliseMin;

	// Token: 0x04002A7A RID: 10874
	public float normaliseMax = 1f;

	// Token: 0x04002A7B RID: 10875
	public float normaliseBlend = 1f;

	// Token: 0x04002A7C RID: 10876
	[NonSerialized]
	public bool presetsInitialised;

	// Token: 0x04002A7D RID: 10877
	[NonSerialized]
	public int voronoiPresetId;

	// Token: 0x04002A7E RID: 10878
	[NonSerialized]
	public int fractalPresetId;

	// Token: 0x04002A7F RID: 10879
	[NonSerialized]
	public int perlinPresetId;

	// Token: 0x04002A80 RID: 10880
	[NonSerialized]
	public int thermalErosionPresetId;

	// Token: 0x04002A81 RID: 10881
	[NonSerialized]
	public int fastHydraulicErosionPresetId;

	// Token: 0x04002A82 RID: 10882
	[NonSerialized]
	public int fullHydraulicErosionPresetId;

	// Token: 0x04002A83 RID: 10883
	[NonSerialized]
	public int velocityHydraulicErosionPresetId;

	// Token: 0x04002A84 RID: 10884
	[NonSerialized]
	public int tidalErosionPresetId;

	// Token: 0x04002A85 RID: 10885
	[NonSerialized]
	public int windErosionPresetId;

	// Token: 0x04002A86 RID: 10886
	public ArrayList voronoiPresets = new ArrayList();

	// Token: 0x04002A87 RID: 10887
	public ArrayList fractalPresets = new ArrayList();

	// Token: 0x04002A88 RID: 10888
	public ArrayList perlinPresets = new ArrayList();

	// Token: 0x04002A89 RID: 10889
	public ArrayList thermalErosionPresets = new ArrayList();

	// Token: 0x04002A8A RID: 10890
	public ArrayList fastHydraulicErosionPresets = new ArrayList();

	// Token: 0x04002A8B RID: 10891
	public ArrayList fullHydraulicErosionPresets = new ArrayList();

	// Token: 0x04002A8C RID: 10892
	public ArrayList velocityHydraulicErosionPresets = new ArrayList();

	// Token: 0x04002A8D RID: 10893
	public ArrayList tidalErosionPresets = new ArrayList();

	// Token: 0x04002A8E RID: 10894
	public ArrayList windErosionPresets = new ArrayList();

	// Token: 0x02000813 RID: 2067
	public class PeakDistance : IComparable
	{
		// Token: 0x06004A47 RID: 19015 RVA: 0x00141FD4 File Offset: 0x001401D4
		public int CompareTo(object obj)
		{
			TerrainToolkit.PeakDistance peakDistance = (TerrainToolkit.PeakDistance)obj;
			int num = this.dist.CompareTo(peakDistance.dist);
			if (num == 0)
			{
				num = this.dist.CompareTo(peakDistance.dist);
			}
			return num;
		}

		// Token: 0x04002A8F RID: 10895
		public int id;

		// Token: 0x04002A90 RID: 10896
		public float dist;
	}

	// Token: 0x02000814 RID: 2068
	public struct Peak
	{
		// Token: 0x04002A91 RID: 10897
		public Vector2 peakPoint;

		// Token: 0x04002A92 RID: 10898
		public float peakHeight;
	}

	// Token: 0x02000815 RID: 2069
	public class voronoiPresetData
	{
		// Token: 0x06004A48 RID: 19016 RVA: 0x00142014 File Offset: 0x00140214
		public voronoiPresetData(string pn, TerrainToolkit.VoronoiType vt, int c, float vf, float vs, float vb)
		{
			this.presetName = pn;
			this.voronoiType = vt;
			this.voronoiCells = c;
			this.voronoiFeatures = vf;
			this.voronoiScale = vs;
			this.voronoiBlend = vb;
		}

		// Token: 0x04002A93 RID: 10899
		public string presetName;

		// Token: 0x04002A94 RID: 10900
		public TerrainToolkit.VoronoiType voronoiType;

		// Token: 0x04002A95 RID: 10901
		public int voronoiCells;

		// Token: 0x04002A96 RID: 10902
		public float voronoiFeatures;

		// Token: 0x04002A97 RID: 10903
		public float voronoiScale;

		// Token: 0x04002A98 RID: 10904
		public float voronoiBlend;
	}

	// Token: 0x02000816 RID: 2070
	public class fractalPresetData
	{
		// Token: 0x06004A49 RID: 19017 RVA: 0x0014204C File Offset: 0x0014024C
		public fractalPresetData(string pn, float dsd, float dsb)
		{
			this.presetName = pn;
			this.diamondSquareDelta = dsd;
			this.diamondSquareBlend = dsb;
		}

		// Token: 0x04002A99 RID: 10905
		public string presetName;

		// Token: 0x04002A9A RID: 10906
		public float diamondSquareDelta;

		// Token: 0x04002A9B RID: 10907
		public float diamondSquareBlend;
	}

	// Token: 0x02000817 RID: 2071
	public class perlinPresetData
	{
		// Token: 0x06004A4A RID: 19018 RVA: 0x0014206C File Offset: 0x0014026C
		public perlinPresetData(string pn, int pf, float pa, int po, float pb)
		{
			this.presetName = pn;
			this.perlinFrequency = pf;
			this.perlinAmplitude = pa;
			this.perlinOctaves = po;
			this.perlinBlend = pb;
		}

		// Token: 0x04002A9C RID: 10908
		public string presetName;

		// Token: 0x04002A9D RID: 10909
		public int perlinFrequency;

		// Token: 0x04002A9E RID: 10910
		public float perlinAmplitude;

		// Token: 0x04002A9F RID: 10911
		public int perlinOctaves;

		// Token: 0x04002AA0 RID: 10912
		public float perlinBlend;
	}

	// Token: 0x02000818 RID: 2072
	public class thermalErosionPresetData
	{
		// Token: 0x06004A4B RID: 19019 RVA: 0x0014209C File Offset: 0x0014029C
		public thermalErosionPresetData(string pn, int ti, float tms, float tba)
		{
			this.presetName = pn;
			this.thermalIterations = ti;
			this.thermalMinSlope = tms;
			this.thermalFalloff = tba;
		}

		// Token: 0x04002AA1 RID: 10913
		public string presetName;

		// Token: 0x04002AA2 RID: 10914
		public int thermalIterations;

		// Token: 0x04002AA3 RID: 10915
		public float thermalMinSlope;

		// Token: 0x04002AA4 RID: 10916
		public float thermalFalloff;
	}

	// Token: 0x02000819 RID: 2073
	public class fastHydraulicErosionPresetData
	{
		// Token: 0x06004A4C RID: 19020 RVA: 0x001420C4 File Offset: 0x001402C4
		public fastHydraulicErosionPresetData(string pn, int hi, float hms, float hba)
		{
			this.presetName = pn;
			this.hydraulicIterations = hi;
			this.hydraulicMaxSlope = hms;
			this.hydraulicFalloff = hba;
		}

		// Token: 0x04002AA5 RID: 10917
		public string presetName;

		// Token: 0x04002AA6 RID: 10918
		public int hydraulicIterations;

		// Token: 0x04002AA7 RID: 10919
		public float hydraulicMaxSlope;

		// Token: 0x04002AA8 RID: 10920
		public float hydraulicFalloff;
	}

	// Token: 0x0200081A RID: 2074
	public class fullHydraulicErosionPresetData
	{
		// Token: 0x06004A4D RID: 19021 RVA: 0x001420EC File Offset: 0x001402EC
		public fullHydraulicErosionPresetData(string pn, int hi, float hr, float he, float hso, float hsa)
		{
			this.presetName = pn;
			this.hydraulicIterations = hi;
			this.hydraulicRainfall = hr;
			this.hydraulicEvaporation = he;
			this.hydraulicSedimentSolubility = hso;
			this.hydraulicSedimentSaturation = hsa;
		}

		// Token: 0x04002AA9 RID: 10921
		public string presetName;

		// Token: 0x04002AAA RID: 10922
		public int hydraulicIterations;

		// Token: 0x04002AAB RID: 10923
		public float hydraulicRainfall;

		// Token: 0x04002AAC RID: 10924
		public float hydraulicEvaporation;

		// Token: 0x04002AAD RID: 10925
		public float hydraulicSedimentSolubility;

		// Token: 0x04002AAE RID: 10926
		public float hydraulicSedimentSaturation;
	}

	// Token: 0x0200081B RID: 2075
	public class velocityHydraulicErosionPresetData
	{
		// Token: 0x06004A4E RID: 19022 RVA: 0x00142124 File Offset: 0x00140324
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

		// Token: 0x04002AAF RID: 10927
		public string presetName;

		// Token: 0x04002AB0 RID: 10928
		public int hydraulicIterations;

		// Token: 0x04002AB1 RID: 10929
		public float hydraulicVelocityRainfall;

		// Token: 0x04002AB2 RID: 10930
		public float hydraulicVelocityEvaporation;

		// Token: 0x04002AB3 RID: 10931
		public float hydraulicVelocitySedimentSolubility;

		// Token: 0x04002AB4 RID: 10932
		public float hydraulicVelocitySedimentSaturation;

		// Token: 0x04002AB5 RID: 10933
		public float hydraulicVelocity;

		// Token: 0x04002AB6 RID: 10934
		public float hydraulicMomentum;

		// Token: 0x04002AB7 RID: 10935
		public float hydraulicEntropy;

		// Token: 0x04002AB8 RID: 10936
		public float hydraulicDowncutting;
	}

	// Token: 0x0200081C RID: 2076
	public class tidalErosionPresetData
	{
		// Token: 0x06004A4F RID: 19023 RVA: 0x00142184 File Offset: 0x00140384
		public tidalErosionPresetData(string pn, int ti, float tra, float tcl)
		{
			this.presetName = pn;
			this.tidalIterations = ti;
			this.tidalRangeAmount = tra;
			this.tidalCliffLimit = tcl;
		}

		// Token: 0x04002AB9 RID: 10937
		public string presetName;

		// Token: 0x04002ABA RID: 10938
		public int tidalIterations;

		// Token: 0x04002ABB RID: 10939
		public float tidalRangeAmount;

		// Token: 0x04002ABC RID: 10940
		public float tidalCliffLimit;
	}

	// Token: 0x0200081D RID: 2077
	public class windErosionPresetData
	{
		// Token: 0x06004A50 RID: 19024 RVA: 0x001421AC File Offset: 0x001403AC
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

		// Token: 0x04002ABD RID: 10941
		public string presetName;

		// Token: 0x04002ABE RID: 10942
		public int windIterations;

		// Token: 0x04002ABF RID: 10943
		public float windDirection;

		// Token: 0x04002AC0 RID: 10944
		public float windForce;

		// Token: 0x04002AC1 RID: 10945
		public float windLift;

		// Token: 0x04002AC2 RID: 10946
		public float windGravity;

		// Token: 0x04002AC3 RID: 10947
		public float windCapacity;

		// Token: 0x04002AC4 RID: 10948
		public float windEntropy;

		// Token: 0x04002AC5 RID: 10949
		public float windSmoothing;
	}

	// Token: 0x0200081E RID: 2078
	public enum ToolMode
	{
		// Token: 0x04002AC7 RID: 10951
		Create,
		// Token: 0x04002AC8 RID: 10952
		Erode,
		// Token: 0x04002AC9 RID: 10953
		Texture
	}

	// Token: 0x0200081F RID: 2079
	public enum ErosionMode
	{
		// Token: 0x04002ACB RID: 10955
		Filter,
		// Token: 0x04002ACC RID: 10956
		Brush
	}

	// Token: 0x02000820 RID: 2080
	public enum ErosionType
	{
		// Token: 0x04002ACE RID: 10958
		Thermal,
		// Token: 0x04002ACF RID: 10959
		Hydraulic,
		// Token: 0x04002AD0 RID: 10960
		Tidal,
		// Token: 0x04002AD1 RID: 10961
		Wind,
		// Token: 0x04002AD2 RID: 10962
		Glacial
	}

	// Token: 0x02000821 RID: 2081
	public enum HydraulicType
	{
		// Token: 0x04002AD4 RID: 10964
		Fast,
		// Token: 0x04002AD5 RID: 10965
		Full,
		// Token: 0x04002AD6 RID: 10966
		Velocity
	}

	// Token: 0x02000822 RID: 2082
	public enum Neighbourhood
	{
		// Token: 0x04002AD8 RID: 10968
		Moore,
		// Token: 0x04002AD9 RID: 10969
		VonNeumann
	}

	// Token: 0x02000823 RID: 2083
	public enum GeneratorType
	{
		// Token: 0x04002ADB RID: 10971
		Voronoi,
		// Token: 0x04002ADC RID: 10972
		DiamondSquare,
		// Token: 0x04002ADD RID: 10973
		Perlin,
		// Token: 0x04002ADE RID: 10974
		Smooth,
		// Token: 0x04002ADF RID: 10975
		Normalise
	}

	// Token: 0x02000824 RID: 2084
	public enum VoronoiType
	{
		// Token: 0x04002AE1 RID: 10977
		Linear,
		// Token: 0x04002AE2 RID: 10978
		Sine,
		// Token: 0x04002AE3 RID: 10979
		Tangent
	}

	// Token: 0x02000825 RID: 2085
	public enum FeatureType
	{
		// Token: 0x04002AE5 RID: 10981
		Mountains,
		// Token: 0x04002AE6 RID: 10982
		Hills,
		// Token: 0x04002AE7 RID: 10983
		Plateaus
	}

	// Token: 0x02000826 RID: 2086
	public class PerlinNoise2D
	{
		// Token: 0x06004A51 RID: 19025 RVA: 0x00142204 File Offset: 0x00140404
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

		// Token: 0x06004A52 RID: 19026 RVA: 0x00142288 File Offset: 0x00140488
		public double getInterpolatedPoint(int _xa, int _xb, int _ya, int _yb, double Px, double Py)
		{
			double pa = this.interpolate(this.noiseValues[_xa % this.Frequency, _ya % this.frequency], this.noiseValues[_xb % this.Frequency, _ya % this.frequency], Px);
			double pb = this.interpolate(this.noiseValues[_xa % this.Frequency, _yb % this.frequency], this.noiseValues[_xb % this.Frequency, _yb % this.frequency], Px);
			return this.interpolate(pa, pb, Py);
		}

		// Token: 0x06004A53 RID: 19027 RVA: 0x00142320 File Offset: 0x00140520
		private double interpolate(double Pa, double Pb, double Px)
		{
			double num = Px * 3.1415927410125732;
			double num2 = (double)(1f - Mathf.Cos((float)num)) * 0.5;
			return Pa * (1.0 - num2) + Pb * num2;
		}

		// Token: 0x17000E88 RID: 3720
		// (get) Token: 0x06004A54 RID: 19028 RVA: 0x00142364 File Offset: 0x00140564
		public float Amplitude
		{
			get
			{
				return this.amplitude;
			}
		}

		// Token: 0x17000E89 RID: 3721
		// (get) Token: 0x06004A55 RID: 19029 RVA: 0x0014236C File Offset: 0x0014056C
		public int Frequency
		{
			get
			{
				return this.frequency;
			}
		}

		// Token: 0x04002AE8 RID: 10984
		private double[,] noiseValues;

		// Token: 0x04002AE9 RID: 10985
		private float amplitude = 1f;

		// Token: 0x04002AEA RID: 10986
		private int frequency = 1;
	}

	// Token: 0x020008EF RID: 2287
	// (Invoke) Token: 0x06004D94 RID: 19860
	public delegate void ErosionProgressDelegate(string titleString, string displayString, int iteration, int nIterations, float percentComplete);

	// Token: 0x020008F0 RID: 2288
	// (Invoke) Token: 0x06004D98 RID: 19864
	public delegate void TextureProgressDelegate(string titleString, string displayString, float percentComplete);

	// Token: 0x020008F1 RID: 2289
	// (Invoke) Token: 0x06004D9C RID: 19868
	public delegate void GeneratorProgressDelegate(string titleString, string displayString, float percentComplete);
}
