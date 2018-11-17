using System;
using System.Reflection;

namespace Facepunch.Util
{
	// Token: 0x02000188 RID: 392
	public class Reflection
	{
		// Token: 0x06000BD3 RID: 3027 RVA: 0x0002EA38 File Offset: 0x0002CC38
		public static bool HasAttribute(MemberInfo method, Type attribute)
		{
			object[] customAttributes = method.GetCustomAttributes(attribute, true);
			return customAttributes.Length > 0;
		}
	}
}
