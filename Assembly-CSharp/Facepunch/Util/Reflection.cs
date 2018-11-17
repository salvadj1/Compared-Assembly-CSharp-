using System;
using System.Reflection;

namespace Facepunch.Util
{
	// Token: 0x020001B4 RID: 436
	public class Reflection
	{
		// Token: 0x06000D03 RID: 3331 RVA: 0x00032924 File Offset: 0x00030B24
		public static bool HasAttribute(MemberInfo method, Type attribute)
		{
			object[] customAttributes = method.GetCustomAttributes(attribute, true);
			return customAttributes.Length > 0;
		}
	}
}
