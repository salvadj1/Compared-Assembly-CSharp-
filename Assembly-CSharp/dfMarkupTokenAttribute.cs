using System;
using System.Collections.Generic;

// Token: 0x020006E2 RID: 1762
public class dfMarkupTokenAttribute
{
	// Token: 0x06003EF4 RID: 16116 RVA: 0x000EEB30 File Offset: 0x000ECD30
	private dfMarkupTokenAttribute()
	{
	}

	// Token: 0x06003EF6 RID: 16118 RVA: 0x000EEB4C File Offset: 0x000ECD4C
	internal static dfMarkupTokenAttribute GetAttribute(int index)
	{
		return dfMarkupTokenAttribute.pool[index];
	}

	// Token: 0x06003EF7 RID: 16119 RVA: 0x000EEB5C File Offset: 0x000ECD5C
	public static void Reset()
	{
		dfMarkupTokenAttribute.poolIndex = 0;
	}

	// Token: 0x06003EF8 RID: 16120 RVA: 0x000EEB64 File Offset: 0x000ECD64
	public static dfMarkupTokenAttribute Obtain(dfMarkupToken key, dfMarkupToken value)
	{
		if (dfMarkupTokenAttribute.poolIndex >= dfMarkupTokenAttribute.pool.Count - 1)
		{
			dfMarkupTokenAttribute.pool.Add(new dfMarkupTokenAttribute());
		}
		dfMarkupTokenAttribute dfMarkupTokenAttribute = dfMarkupTokenAttribute.pool[dfMarkupTokenAttribute.poolIndex];
		dfMarkupTokenAttribute.Index = dfMarkupTokenAttribute.poolIndex;
		dfMarkupTokenAttribute.Key = key;
		dfMarkupTokenAttribute.Value = value;
		dfMarkupTokenAttribute.poolIndex++;
		return dfMarkupTokenAttribute;
	}

	// Token: 0x040021AD RID: 8621
	public int Index;

	// Token: 0x040021AE RID: 8622
	public dfMarkupToken Key;

	// Token: 0x040021AF RID: 8623
	public dfMarkupToken Value;

	// Token: 0x040021B0 RID: 8624
	private static List<dfMarkupTokenAttribute> pool = new List<dfMarkupTokenAttribute>();

	// Token: 0x040021B1 RID: 8625
	private static int poolIndex = 0;
}
