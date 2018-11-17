using System;
using System.Reflection;

// Token: 0x0200017C RID: 380
public class ConVar
{
	// Token: 0x06000B81 RID: 2945 RVA: 0x0002CF98 File Offset: 0x0002B198
	public static string GetString(string strName, string strDefault)
	{
		ConsoleSystem.Arg arg = new ConsoleSystem.Arg(strName);
		if (arg.Invalid)
		{
			return strDefault;
		}
		Type[] array = ConsoleSystem.FindTypes(arg.Class);
		if (array.Length == 0)
		{
			return strDefault;
		}
		foreach (Type type in array)
		{
			FieldInfo field = type.GetField(arg.Function);
			if (field != null && field.IsStatic)
			{
				return field.GetValue(null).ToString();
			}
			PropertyInfo property = type.GetProperty(arg.Function);
			if (property != null && property.GetGetMethod().IsStatic)
			{
				return property.GetValue(null, null).ToString();
			}
		}
		return strDefault;
	}

	// Token: 0x06000B82 RID: 2946 RVA: 0x0002D054 File Offset: 0x0002B254
	public static float GetFloat(string strName, float strDefault)
	{
		string @string = ConVar.GetString(strName, string.Empty);
		if (@string.Length == 0)
		{
			return strDefault;
		}
		float result = strDefault;
		if (float.TryParse(@string, out result))
		{
			return result;
		}
		return strDefault;
	}

	// Token: 0x06000B83 RID: 2947 RVA: 0x0002D08C File Offset: 0x0002B28C
	public static int GetInt(string strName, float strDefault)
	{
		return (int)ConVar.GetFloat(strName, strDefault);
	}

	// Token: 0x06000B84 RID: 2948 RVA: 0x0002D098 File Offset: 0x0002B298
	public static bool GetBool(string strName, bool strDefault)
	{
		string @string = ConVar.GetString(strName, (!strDefault) ? bool.FalseString : bool.TrueString);
		bool result;
		try
		{
			result = bool.Parse(@string);
		}
		catch
		{
			result = (ConVar.GetInt(strName, (float)((!strDefault) ? 0 : 1)) != 0);
		}
		return result;
	}
}
