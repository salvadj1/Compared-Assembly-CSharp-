using System;
using System.Collections.Generic;

namespace JSON
{
	// Token: 0x02000852 RID: 2130
	public static class Extensions
	{
		// Token: 0x06004AF4 RID: 19188 RVA: 0x00148008 File Offset: 0x00146208
		public static T Pop<T>(this List<T> list)
		{
			T result = list[list.Count - 1];
			list.RemoveAt(list.Count - 1);
			return result;
		}
	}
}
