using System;
using UnityEngine;

// Token: 0x020005B0 RID: 1456
public class TerrainTextureHelper
{
	// Token: 0x06002ED4 RID: 11988 RVA: 0x000B4868 File Offset: 0x000B2A68
	public static void EnsureInit()
	{
		if (global::TerrainTextureHelper.cachedTerrain == Terrain.activeTerrain)
		{
			return;
		}
		global::TerrainTextureHelper.CacheTextures();
		global::TerrainTextureHelper.cachedTerrain = Terrain.activeTerrain;
	}

	// Token: 0x06002ED5 RID: 11989 RVA: 0x000B489C File Offset: 0x000B2A9C
	public static void CacheTextures()
	{
		Debug.Log("Caching Terrain splatmap lookups, please wait...");
		Terrain activeTerrain = Terrain.activeTerrain;
		TerrainData terrainData = activeTerrain.terrainData;
		Vector3 position = activeTerrain.transform.position;
		float[,,] alphamaps = terrainData.GetAlphamaps(0, 0, terrainData.alphamapWidth, terrainData.alphamapHeight);
		global::TerrainTextureHelper.textures = new byte[alphamaps.GetUpperBound(0) + 1, alphamaps.GetUpperBound(1) + 1];
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
				global::TerrainTextureHelper.textures[i, j] = (byte)num2;
			}
		}
		GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
	}

	// Token: 0x06002ED6 RID: 11990 RVA: 0x000B499C File Offset: 0x000B2B9C
	public static float[] GetTextureAmounts(Vector3 worldPos)
	{
		return global::TerrainTextureHelper.OLD_GetTextureMix(worldPos);
	}

	// Token: 0x06002ED7 RID: 11991 RVA: 0x000B49A4 File Offset: 0x000B2BA4
	public static int GetTextureIndex(Vector3 worldPos)
	{
		return global::TerrainTextureHelper.OLD_GetMainTexture(worldPos);
	}

	// Token: 0x06002ED8 RID: 11992 RVA: 0x000B49B8 File Offset: 0x000B2BB8
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

	// Token: 0x06002ED9 RID: 11993 RVA: 0x000B4A7C File Offset: 0x000B2C7C
	public static int OLD_GetMainTexture(Vector3 worldPos)
	{
		float[] array = global::TerrainTextureHelper.OLD_GetTextureMix(worldPos);
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

	// Token: 0x0400196B RID: 6507
	public static byte[,] textures;

	// Token: 0x0400196C RID: 6508
	public static Terrain cachedTerrain;
}
