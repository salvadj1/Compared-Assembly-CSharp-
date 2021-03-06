﻿using System;
using UnityEngine;

// Token: 0x02000942 RID: 2370
public class TerrainQualitySettings : MonoBehaviour
{
	// Token: 0x06004FA2 RID: 20386 RVA: 0x00151574 File Offset: 0x0014F774
	private void Start()
	{
		this.UpdateQuality();
	}

	// Token: 0x06004FA3 RID: 20387 RVA: 0x0015157C File Offset: 0x0014F77C
	private void UpdateQuality()
	{
		Debug.Log("updating terrain quality");
		switch (QualitySettings.currentLevel)
		{
		case 0:
			Terrain.activeTerrain.treeDistance = 250f;
			Terrain.activeTerrain.treeBillboardDistance = 30f;
			Terrain.activeTerrain.treeCrossFadeLength = 5f;
			Terrain.activeTerrain.treeMaximumFullLODCount = 5;
			Terrain.activeTerrain.detailObjectDistance = 30f;
			Terrain.activeTerrain.heightmapPixelError = 20f;
			Terrain.activeTerrain.heightmapMaximumLOD = 1;
			Terrain.activeTerrain.basemapDistance = 100f;
			break;
		case 1:
			Terrain.activeTerrain.treeDistance = 500f;
			Terrain.activeTerrain.treeBillboardDistance = 50f;
			Terrain.activeTerrain.treeCrossFadeLength = 10f;
			Terrain.activeTerrain.treeMaximumFullLODCount = 10;
			Terrain.activeTerrain.detailObjectDistance = 40f;
			Terrain.activeTerrain.heightmapPixelError = 10f;
			Terrain.activeTerrain.heightmapMaximumLOD = 1;
			Terrain.activeTerrain.basemapDistance = 250f;
			break;
		case 2:
			Terrain.activeTerrain.treeDistance = 650f;
			Terrain.activeTerrain.treeBillboardDistance = 75f;
			Terrain.activeTerrain.treeCrossFadeLength = 25f;
			Terrain.activeTerrain.treeMaximumFullLODCount = 20;
			Terrain.activeTerrain.detailObjectDistance = 60f;
			Terrain.activeTerrain.heightmapPixelError = 8f;
			Terrain.activeTerrain.heightmapMaximumLOD = 0;
			Terrain.activeTerrain.basemapDistance = 500f;
			break;
		case 3:
			Terrain.activeTerrain.treeDistance = 800f;
			Terrain.activeTerrain.treeBillboardDistance = 100f;
			Terrain.activeTerrain.treeCrossFadeLength = 40f;
			Terrain.activeTerrain.treeMaximumFullLODCount = 30;
			Terrain.activeTerrain.detailObjectDistance = 75f;
			Terrain.activeTerrain.heightmapPixelError = 5f;
			Terrain.activeTerrain.heightmapMaximumLOD = 0;
			Terrain.activeTerrain.basemapDistance = 800f;
			break;
		case 4:
			Terrain.activeTerrain.treeDistance = 1000f;
			Terrain.activeTerrain.treeBillboardDistance = 150f;
			Terrain.activeTerrain.treeCrossFadeLength = 50f;
			Terrain.activeTerrain.treeMaximumFullLODCount = 50;
			Terrain.activeTerrain.detailObjectDistance = 100f;
			Terrain.activeTerrain.heightmapPixelError = 5f;
			Terrain.activeTerrain.heightmapMaximumLOD = 0;
			Terrain.activeTerrain.basemapDistance = 1000f;
			break;
		case 5:
			Terrain.activeTerrain.treeDistance = 2000f;
			Terrain.activeTerrain.treeBillboardDistance = 250f;
			Terrain.activeTerrain.treeCrossFadeLength = 50f;
			Terrain.activeTerrain.treeMaximumFullLODCount = 100;
			Terrain.activeTerrain.detailObjectDistance = 200f;
			Terrain.activeTerrain.heightmapPixelError = 5f;
			Terrain.activeTerrain.heightmapMaximumLOD = 0;
			Terrain.activeTerrain.basemapDistance = 1000f;
			break;
		}
	}
}
