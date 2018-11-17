using System;
using System.Collections.Generic;
using System.Reflection;

// Token: 0x02000124 RID: 292
public static class ReferenceTypeHelper
{
	// Token: 0x0600078F RID: 1935 RVA: 0x0002198C File Offset: 0x0001FB8C
	public static bool TreatAsReferenceHolder(Type type)
	{
		bool flag;
		if (!global::ReferenceTypeHelper.cache.TryGetValue(type, out flag))
		{
			if (type.IsByRef)
			{
				flag = true;
			}
			else if (type.IsEnum)
			{
				flag = false;
			}
			else
			{
				foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
				{
					Type fieldType = fieldInfo.FieldType;
					if (fieldType.IsByRef || !global::ReferenceTypeHelper.TreatAsReferenceHolder(fieldType))
					{
						flag = false;
						break;
					}
				}
			}
			global::ReferenceTypeHelper.cache[type] = flag;
		}
		return flag;
	}

	// Token: 0x040005DA RID: 1498
	private static readonly Dictionary<Type, bool> cache = new Dictionary<Type, bool>();
}
