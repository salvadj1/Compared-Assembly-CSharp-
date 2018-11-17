using System;
using System.Collections.Generic;

// Token: 0x020001A7 RID: 423
public static class TempList
{
	// Token: 0x06000CAE RID: 3246 RVA: 0x00030E6C File Offset: 0x0002F06C
	public static global::TempList<T> New<T>(IEnumerable<T> enumerable)
	{
		return global::TempList<T>.New(enumerable);
	}

	// Token: 0x06000CAF RID: 3247 RVA: 0x00030E74 File Offset: 0x0002F074
	public static global::TempList<T> New<T>()
	{
		return global::TempList<T>.New();
	}
}
