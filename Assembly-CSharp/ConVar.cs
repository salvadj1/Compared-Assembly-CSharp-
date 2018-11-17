using System;
using System.Reflection;

// Token: 0x020001A8 RID: 424
public class ConVar
{
	// Token: 0x06000CB1 RID: 3249 RVA: 0x00030E84 File Offset: 0x0002F084
	public static string GetString(string strName, string strDefault)
	{
		global::ConsoleSystem.Arg arg = new global::ConsoleSystem.Arg(strName);
		if (arg.Invalid)
		{
			return strDefault;
		}
		Type[] array = global::ConsoleSystem.FindTypes(arg.Class);
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

	// Token: 0x06000CB2 RID: 3250 RVA: 0x00030F40 File Offset: 0x0002F140
	public static float GetFloat(string strName, float strDefault)
	{
		string @string = global::ConVar.GetString(strName, string.Empty);
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

	// Token: 0x06000CB3 RID: 3251 RVA: 0x00030F78 File Offset: 0x0002F178
	public static int GetInt(string strName, float strDefault)
	{
		return (int)global::ConVar.GetFloat(strName, strDefault);
	}

	// Token: 0x06000CB4 RID: 3252 RVA: 0x00030F84 File Offset: 0x0002F184
	public static bool GetBool(string strName, bool strDefault)
	{
		string @string = global::ConVar.GetString(strName, (!strDefault) ? bool.FalseString : bool.TrueString);
		bool result;
		try
		{
			result = bool.Parse(@string);
		}
		catch
		{
			result = (global::ConVar.GetInt(strName, (float)((!strDefault) ? 0 : 1)) != 0);
		}
		return result;
	}
}
