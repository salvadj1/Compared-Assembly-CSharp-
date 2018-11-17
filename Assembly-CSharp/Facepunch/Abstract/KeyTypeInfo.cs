using System;

namespace Facepunch.Abstract
{
	// Token: 0x02000209 RID: 521
	internal static class KeyTypeInfo
	{
		// Token: 0x06000E58 RID: 3672 RVA: 0x00037284 File Offset: 0x00035484
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
