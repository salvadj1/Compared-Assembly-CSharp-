using System;
using UnityEngine;

// Token: 0x020004F3 RID: 1267
public class TerrainTextureHelper
{
	// Token: 0x06002B14 RID: 11028 RVA: 0x000AC7CC File Offset: 0x000AA9CC
	public static void EnsureInit()
	{
		if (TerrainTextureHelper.cachedTerrain == Terrain.activeTerrain)
		{
			return;
		}
		TerrainTextureHelper.CacheTextures();
		TerrainTextureHelper.cachedTerrain = Terrain.activeTerrain;
	}

	// Token: 0x06002B15 RID: 11029 RVA: 0x000AC800 File Offset: 0x000AAA00
	public static void CacheTextures()
	{
		Debug.Log("Caching Terrain splatmap lookups, please wait...");
		Terrain activeTerrain = Terrain.activeTerrain;
		TerrainData terrainData = activeTerrain.terrainData;
		Vector3 position = activeTerrain.transform.position;
		float[,,] alphamaps = terrainData.GetAlphamaps(0, 0, terrainData.alphamapWidth, terrainData.alphamapHeight);
		TerrainTextureHelper.textures = new byte[alphamaps.GetUpperBound(0) + 1, alphamaps.GetUpperBound(1) + 1];
		for (int i = 0; i < terrainData.alphamapWidth; i++)
		{
			for (int j = 0; j < terrainData.alphamapHeight; j++)
			{
				float num = 0f;
				int num2 = 0;
				for (int k = 0; k < alphamaps.GetUpperBound(2) + 1; k++)
				{
					if (alphamaps[i, j, k] >= num)
					{
						num2 = k;
						num = alphamaps[i, j, k];
					}
				}
				TerrainTextureHelper.textures[i, j] = (byte)num2;
			}
		}
		GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
	}

	// Token: 0x06002B16 RID: 11030 RVA: 0x000AC900 File Offset: 0x000AAB00
	public static float[] GetTextureAmounts(Vector3 worldPos)
	{
		return TerrainTextureHelper.OLD_GetTextureMix(worldPos);
	}

	// Token: 0x06002B17 RID: 11031 RVA: 0x000AC908 File Offset: 0x000AAB08
	public static int GetTextureIndex(Vector3 worldPos)
	{
		return TerrainTextureHelper.OLD_GetMainTexture(worldPos);
	}

	// Token: 0x06002B18 RID: 11032 RVA: 0x000AC91C File Offset: 0x000AAB1C
	public static float[] OLD_GetTextureMix(Vector3 worldPos)
	{
		Terrain activeTerrain = Terrain.activeTerrain;
		TerrainData terrainData = activeTerrain.terrainData;
		Vector3 position = activeTerrain.transform.position;
		int num = (int)((worldPos.x - position.x) / terrainData.size.x * (float)terrainData.alphamapWidth);
		int num2 = (int)((worldPos.z - position.z) / terrainData.size.z * (float)terrainData.alphamapHeight);
		float[,,] alphamaps = terrainData.GetAlphamaps(num, num2, 1, 1);
		float[] array = new float[alphamaps.GetUpperBound(2) + 1];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = alphamaps[0, 0, i];
		}
		return array;
	}

	// Token: 0x06002B19 RID: 11033 RVA: 0x000AC9E0 File Offset: 0x000AABE0
	public static int OLD_GetMainTexture(Vector3 worldPos)
	{
		float[] array = TerrainTextureHelper.OLD_GetTextureMix(worldPos);
		float num = 0f;
		int result = 0;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] > num)
			{
				result = i;
				num = array[i];
			}
		}
		return result;
	}

	// Token: 0x0400179F RID: 6047
	public static byte[,] textures;

	// Token: 0x040017A0 RID: 6048
	public static Terrain cachedTerrain;
}
