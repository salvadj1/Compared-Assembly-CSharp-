using System;

namespace Facepunch.Abstract
{
	// Token: 0x020001D8 RID: 472
	internal static class KeyTypeInfo
	{
		// Token: 0x06000D10 RID: 3344 RVA: 0x000331FC File Offset: 0x000313FC
		public static int ForcedDifCompareValue(Type x, Type y)
		{
			int num = x.GetHashCode().CompareTo(y.GetHashCode());
			if (num == 0)
			{
				num = x.AssemblyQualifiedName.CompareTo(y.AssemblyQualifiedName);
				if (num == 0)
				{
					num = x.TypeHandle.Value.ToInt64().CompareTo(y.TypeHandle.Value);
					if (num == 0)
					{
						throw new InvalidProgramException();
					}
				}
			}
			return num;
		}
	}
}
