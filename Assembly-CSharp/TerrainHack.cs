using System;
using System.Reflection;
using UnityEngine;

// Token: 0x0200068F RID: 1679
public static class TerrainHack
{
	// Token: 0x06003A1A RID: 14874 RVA: 0x000D84A4 File Offset: 0x000D66A4
	static TerrainHack()
	{
		if (TerrainHack.OnTerrainChanged != null)
		{
			Type type = Type.GetType("UnityEngine.TerrainChangedFlags, UnityEngine", false, false);
			if (type != null)
			{
				object obj;
				try
				{
					obj = Enum.Parse(type, "TreeInstances", false);
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
					try
					{
						obj = Enum.ToObject(type, 2);
					}
					catch (Exception ex2)
					{
						Debug.LogException(ex);
						return;
					}
				}
				TerrainHack.AbleToLocateOnTerrainChanged = true;
				TerrainHack.TriggerTreeChangeValues = new object[]
				{
					obj
				};
			}
			else
			{
				Debug.LogWarning("Couldnt locate enum TerrainChangedFlags.");
			}
		}
		else
		{
			Debug.LogWarning("Couldnt locate method OnTerrainChanged");
		}
	}

	// Token: 0x06003A1B RID: 14875 RVA: 0x000D858C File Offset: 0x000D678C
	public static void RefreshTreeTextures(Terrain terrain)
	{
		if (!terrain)
		{
			throw new NullReferenceException();
		}
		if (!TerrainHack.RanOnce)
		{
			TerrainHack.RanOnce = true;
			if (TerrainHack.AbleToLocateOnTerrainChanged)
			{
				try
				{
					TerrainHack.OnTerrainChanged.Invoke(terrain, TerrainHack.TriggerTreeChangeValues);
					TerrainHack.Working = true;
					return;
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
					TerrainHack.Working = false;
				}
			}
		}
		if (TerrainHack.Working)
		{
			TerrainHack.OnTerrainChanged.Invoke(terrain, TerrainHack.TriggerTreeChangeValues);
		}
		else
		{
			terrain.Flush();
		}
	}

	// Token: 0x04001E3B RID: 7739
	private static readonly bool AbleToLocateOnTerrainChanged;

	// Token: 0x04001E3C RID: 7740
	private static readonly object[] TriggerTreeChangeValues;

	// Token: 0x04001E3D RID: 7741
	private static bool RanOnce;

	// Token: 0x04001E3E RID: 7742
	private static bool Working;

	// Token: 0x04001E3F RID: 7743
	private static MethodInfo OnTerrainChanged = typeof(Terrain).GetMethod("OnTerrainChanged", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
}
