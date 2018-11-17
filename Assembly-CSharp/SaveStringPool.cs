using System;
using System.Collections.Generic;

// Token: 0x020000C5 RID: 197
public class SaveStringPool
{
	// Token: 0x06000420 RID: 1056 RVA: 0x0001558C File Offset: 0x0001378C
	public static int GetInt(string strName)
	{
		return SaveStringPool.prefabDictionary[strName];
	}

	// Token: 0x06000421 RID: 1057 RVA: 0x0001559C File Offset: 0x0001379C
	public static string Convert(int iNum)
	{
		foreach (KeyValuePair<string, int> keyValuePair in SaveStringPool.prefabDictionary)
		{
			if (keyValuePair.Value == iNum)
			{
				return keyValuePair.Key;
			}
		}
		return string.Empty;
	}

	// Token: 0x040003A7 RID: 935
	private static Dictionary<string, int> prefabDictionary = new Dictionary<string, int>
	{
		{
			"StructureMasterPrefab",
			1
		}
	};
}
