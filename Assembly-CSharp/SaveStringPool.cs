using System;
using System.Collections.Generic;

// Token: 0x020000D9 RID: 217
public class SaveStringPool
{
	// Token: 0x0600049E RID: 1182 RVA: 0x00016F54 File Offset: 0x00015154
	public static int GetInt(string strName)
	{
		return global::SaveStringPool.prefabDictionary[strName];
	}

	// Token: 0x0600049F RID: 1183 RVA: 0x00016F64 File Offset: 0x00015164
	public static string Convert(int iNum)
	{
		foreach (KeyValuePair<string, int> keyValuePair in global::SaveStringPool.prefabDictionary)
		{
			if (keyValuePair.Value == iNum)
			{
				return keyValuePair.Key;
			}
		}
		return string.Empty;
	}

	// Token: 0x04000416 RID: 1046
	private static Dictionary<string, int> prefabDictionary = new Dictionary<string, int>
	{
		{
			"StructureMasterPrefab",
			1
		}
	};
}
