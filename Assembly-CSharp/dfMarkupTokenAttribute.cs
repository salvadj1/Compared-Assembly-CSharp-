using System;
using System.Collections.Generic;

// Token: 0x020007B4 RID: 1972
public class dfMarkupTokenAttribute
{
	// Token: 0x06004310 RID: 17168 RVA: 0x000F7734 File Offset: 0x000F5934
	private dfMarkupTokenAttribute()
	{
	}

	// Token: 0x06004312 RID: 17170 RVA: 0x000F7750 File Offset: 0x000F5950
	internal static global::dfMarkupTokenAttribute GetAttribute(int index)
	{
		return global::dfMarkupTokenAttribute.pool[index];
	}

	// Token: 0x06004313 RID: 17171 RVA: 0x000F7760 File Offset: 0x000F5960
	public static void Reset()
	{
		global::dfMarkupTokenAttribute.poolIndex = 0;
	}

	// Token: 0x06004314 RID: 17172 RVA: 0x000F7768 File Offset: 0x000F5968
	public static global::dfMarkupTokenAttribute Obtain(global::dfMarkupToken key, global::dfMarkupToken value)
	{
		if (global::dfMarkupTokenAttribute.poolIndex >= global::dfMarkupTokenAttribute.pool.Count - 1)
		{
			global::dfMarkupTokenAttribute.pool.Add(new global::dfMarkupTokenAttribute());
		}
		global::dfMarkupTokenAttribute dfMarkupTokenAttribute = global::dfMarkupTokenAttribute.pool[global::dfMarkupTokenAttribute.poolIndex];
		dfMarkupTokenAttribute.Index = global::dfMarkupTokenAttribute.poolIndex;
		dfMarkupTokenAttribute.Key = key;
		dfMarkupTokenAttribute.Value = value;
		global::dfMarkupTokenAttribute.poolIndex++;
		return dfMarkupTokenAttribute;
	}

	// Token: 0x040023B6 RID: 9142
	public int Index;

	// Token: 0x040023B7 RID: 9143
	public global::dfMarkupToken Key;

	// Token: 0x040023B8 RID: 9144
	public global::dfMarkupToken Value;

	// Token: 0x040023B9 RID: 9145
	private static List<global::dfMarkupTokenAttribute> pool = new List<global::dfMarkupTokenAttribute>();

	// Token: 0x040023BA RID: 9146
	private static int poolIndex = 0;
}
