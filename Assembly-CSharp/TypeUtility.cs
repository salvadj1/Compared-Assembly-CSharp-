using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// Token: 0x020001E8 RID: 488
public static class TypeUtility
{
	// Token: 0x06000D70 RID: 3440 RVA: 0x00034A20 File Offset: 0x00032C20
	private static bool ContainsAQN(string text)
	{
		int num = text.IndexOf(", ");
		if (num != -1)
		{
			for (int i = 0; i < TypeUtility.hintsAQN.Length; i++)
			{
				if (text.IndexOf(TypeUtility.hintsAQN[i], num) != -1)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000D71 RID: 3441 RVA: 0x00034A70 File Offset: 0x00032C70
	private static bool Parse(string text, bool ignoreCase, out Type type)
	{
		type = Type.GetType(text, false, ignoreCase);
		return !object.ReferenceEquals(type, null);
	}

	// Token: 0x06000D72 RID: 3442 RVA: 0x00034A8C File Offset: 0x00032C8C
	private static bool Parse(string text, out Type type)
	{
		if (TypeUtility.Parse(text, false, out type))
		{
			return true;
		}
		if (TypeUtility.ContainsAQN(text))
		{
			string text2 = TypeUtility.g.StrippedName(text);
			return TypeUtility.Parse(text2, false, out type) || TypeUtility.Parse(text, true, out type) || TypeUtility.Parse(text2, true, out type);
		}
		return TypeUtility.Parse(text, true, out type);
	}

	// Token: 0x06000D73 RID: 3443 RVA: 0x00034AE8 File Offset: 0x00032CE8
	private static bool Parse(Type requiredBase, string text, bool ignoreCase, out Type type)
	{
		if (TypeUtility.Parse(text, ignoreCase, out type))
		{
			if (requiredBase.IsAssignableFrom(type))
			{
				return true;
			}
			type = null;
		}
		return false;
	}

	// Token: 0x06000D74 RID: 3444 RVA: 0x00034B18 File Offset: 0x00032D18
	private static bool Parse(Type requiredType, string text, out Type type)
	{
		if (TypeUtility.Parse(requiredType, text, false, out type))
		{
			return true;
		}
		if (TypeUtility.ContainsAQN(text))
		{
			string text2 = TypeUtility.g.StrippedName(text);
			return TypeUtility.Parse(requiredType, text2, false, out type) || TypeUtility.Parse(requiredType, text, true, out type) || TypeUtility.Parse(requiredType, text2, true, out type);
		}
		return TypeUtility.Parse(requiredType, text, true, out type);
	}

	// Token: 0x06000D75 RID: 3445 RVA: 0x00034B7C File Offset: 0x00032D7C
	public static Type Parse(string text)
	{
		if (object.ReferenceEquals(text, null))
		{
			throw new ArgumentNullException("text");
		}
		if (text.Length == 0)
		{
			throw new ArgumentException("text.Length==0", "text");
		}
		Type result;
		if (!TypeUtility.Parse(text, out result))
		{
			throw new ArgumentException("could not get type", text);
		}
		return result;
	}

	// Token: 0x06000D76 RID: 3446 RVA: 0x00034BD8 File Offset: 0x00032DD8
	public static Type Parse<TRequiredBaseClass>(string text) where TRequiredBaseClass : class
	{
		if (object.ReferenceEquals(text, null))
		{
			throw new ArgumentNullException("text");
		}
		if (text.Length == 0)
		{
			throw new ArgumentException("text.Length==0", "text");
		}
		Type result;
		if (!TypeUtility.Parse(typeof(TRequiredBaseClass), text, out result))
		{
			throw new ArgumentException("could not get type that would match base class " + typeof(TRequiredBaseClass), text);
		}
		return result;
	}

	// Token: 0x06000D77 RID: 3447 RVA: 0x00034C4C File Offset: 0x00032E4C
	public static bool TryParse(string text, out Type type)
	{
		if (string.IsNullOrEmpty(text))
		{
			type = null;
			return false;
		}
		return TypeUtility.Parse(text, out type);
	}

	// Token: 0x06000D78 RID: 3448 RVA: 0x00034C68 File Offset: 0x00032E68
	public static bool TryParse<TRequiredBaseClass>(string text, out Type type) where TRequiredBaseClass : class
	{
		if (string.IsNullOrEmpty(text))
		{
			type = null;
			return false;
		}
		return TypeUtility.Parse(typeof(TRequiredBaseClass), text, out type);
	}

	// Token: 0x06000D79 RID: 3449 RVA: 0x00034C8C File Offset: 0x00032E8C
	public static string VersionlessName(this Type type)
	{
		if (object.ReferenceEquals(type, null))
		{
			return null;
		}
		return TypeUtility.g.StrippedName(type);
	}

	// Token: 0x06000D7A RID: 3450 RVA: 0x00034CA4 File Offset: 0x00032EA4
	public static string VersionlessName<T>()
	{
		return typeof(T).VersionlessName();
	}

	// Token: 0x0400082E RID: 2094
	private static bool ginit;

	// Token: 0x0400082F RID: 2095
	private static readonly string[] hintsAQN = new string[]
	{
		", Version=",
		", Culture=",
		", PublicKeyToken="
	};

	// Token: 0x020001E9 RID: 489
	private static class g
	{
		// Token: 0x06000D7B RID: 3451 RVA: 0x00034CB8 File Offset: 0x00032EB8
		static g()
		{
			TypeUtility.ginit = true;
			TypeUtility.g.strippedNames = new Dictionary<Type, string>();
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x00034CCC File Offset: 0x00032ECC
		public static string StrippedName(Type type)
		{
			string result;
			if (!TypeUtility.g.strippedNames.TryGetValue(type, out result))
			{
				result = (TypeUtility.g.strippedNames[type] = TypeUtility.g.expression.replace(type.AssemblyQualifiedName));
			}
			return result;
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x00034D04 File Offset: 0x00032F04
		public static string StrippedName(string assemblyQualifiedName)
		{
			return TypeUtility.g.expression.replace(assemblyQualifiedName);
		}

		// Token: 0x04000830 RID: 2096
		private static readonly Dictionary<Type, string> strippedNames;

		// Token: 0x020001EA RID: 490
		private static class expression
		{
			// Token: 0x06000D7F RID: 3455 RVA: 0x00034D4C File Offset: 0x00032F4C
			public static string replace(string assemblyQualifiedName)
			{
				return TypeUtility.g.expression.version.Replace(assemblyQualifiedName, string.Empty);
			}

			// Token: 0x04000831 RID: 2097
			private const RegexOptions kRegexOptions = RegexOptions.Compiled;

			// Token: 0x04000832 RID: 2098
			public static readonly Regex version = new Regex(", Version=\\d+.\\d+.\\d+.\\d+", RegexOptions.Compiled);

			// Token: 0x04000833 RID: 2099
			public static readonly Regex culture = new Regex(", Culture=\\w+", RegexOptions.Compiled);

			// Token: 0x04000834 RID: 2100
			public static readonly Regex publicKeyToken = new Regex(", PublicKeyToken=\\w+", RegexOptions.Compiled);
		}
	}
}
