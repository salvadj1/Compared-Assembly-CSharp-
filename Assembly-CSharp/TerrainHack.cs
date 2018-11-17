using System;
using System.Reflection;
using UnityEngine;

// Token: 0x02000754 RID: 1876
public static class TerrainHack
{
	// Token: 0x06003E12 RID: 15890 RVA: 0x000E0E84 File Offset: 0x000DF084
	static TerrainHack()
	{
		if (global::TerrainHack.OnTerrainChanged != null)
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
				global::TerrainHack.AbleToLocateOnTerrainChanged = true;
				global::TerrainHack.TriggerTreeChangeValues = new object[]
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

	// Token: 0x06003E13 RID: 15891 RVA: 0x000E0F6C File Offset: 0x000DF16C
	public static void RefreshTreeTextures(Terrain terrain)
	{
		if (!terrain)
		{
			throw new NullReferenceException();
		}
		if (!global::TerrainHack.RanOnce)
		{
			global::TerrainHack.RanOnce = true;
			if (global::TerrainHack.AbleToLocateOnTerrainChanged)
			{
				try
				{
					global::TerrainHack.OnTerrainChanged.Invoke(terrain, global::TerrainHack.TriggerTreeChangeValues);
					global::TerrainHack.Working = true;
					return;
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
					global::TerrainHack.Working = false;
				}
			}
		}
		if (global::TerrainHack.Working)
		{
			global::TerrainHack.OnTerrainChanged.Invoke(terrain, global::TerrainHack.TriggerTreeChangeValues);
		}
		else
		{
			terrain.Flush();
		}
	}

	// Token: 0x04002033 RID: 8243
	private static readonly bool AbleToLocateOnTerrainChanged;

	// Token: 0x04002034 RID: 8244
	private static readonly object[] TriggerTreeChangeValues;

	// Token: 0x04002035 RID: 8245
	private static bool RanOnce;

	// Token: 0x04002036 RID: 8246
	private static bool Working;

	// Token: 0x04002037 RID: 8247
	private static MethodInfo OnTerrainChanged = typeof(Terrain).GetMethod("OnTerrainChanged", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
}
