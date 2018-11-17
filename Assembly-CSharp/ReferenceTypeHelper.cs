using System;
using System.Collections.Generic;
using System.Reflection;

// Token: 0x02000105 RID: 261
public static class ReferenceTypeHelper
{
	// Token: 0x060006BD RID: 1725 RVA: 0x0001EDB8 File Offset: 0x0001CFB8
	public static bool TreatAsReferenceHolder(Type type)
	{
		bool flag;
		if (!ReferenceTypeHelper.cache.TryGetValue(type, out flag))
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
					if (fieldType.IsByRef || !ReferenceTypeHelper.TreatAsReferenceHolder(fieldType))
					{
						flag = false;
						break;
					}
				}
			}
			ReferenceTypeHelper.cache[type] = flag;
		}
		return flag;
	}

	// Token: 0x0400050F RID: 1295
	private static readonly Dictionary<Type, bool> cache = new Dictionary<Type, bool>();
}
