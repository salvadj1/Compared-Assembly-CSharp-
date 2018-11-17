using System;
using System.Collections.Generic;

// Token: 0x0200017B RID: 379
public static class TempList
{
	// Token: 0x06000B7E RID: 2942 RVA: 0x0002CF80 File Offset: 0x0002B180
	public static TempList<T> New<T>(IEnumerable<T> enumerable)
	{
		return TempList<T>.New(enumerable);
	}

	// Token: 0x06000B7F RID: 2943 RVA: 0x0002CF88 File Offset: 0x0002B188
	public static TempList<T> New<T>()
	{
		return TempList<T>.New();
	}
}
