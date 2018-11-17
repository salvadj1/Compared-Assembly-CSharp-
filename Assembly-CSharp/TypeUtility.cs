using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// Token: 0x0200021A RID: 538
public static class TypeUtility
{
	// Token: 0x06000EC0 RID: 3776 RVA: 0x00038DC8 File Offset: 0x00036FC8
	private static bool ContainsAQN(string text)
	{
		int num = text.IndexOf(", ");
		if (num != -1)
		{
			for (int i = 0; i < global::TypeUtility.hintsAQN.Length; i++)
			{
				if (text.IndexOf(global::TypeUtility.hintsAQN[i], num) != -1)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000EC1 RID: 3777 RVA: 0x00038E18 File Offset: 0x00037018
	private static bool Parse(string text, bool ignoreCase, out Type type)
	{
		type = Type.GetType(text, false, ignoreCase);
		return !object.ReferenceEquals(type, null);
	}

	// Token: 0x06000EC2 RID: 3778 RVA: 0x00038E34 File Offset: 0x00037034
	private static bool Parse(string text, out Type type)
	{
		if (global::TypeUtility.Parse(text, false, out type))
		{
			return true;
		}
		if (global::TypeUtility.ContainsAQN(text))
		{
			string text2 = global::TypeUtility.g.StrippedName(text);
			return global::TypeUtility.Parse(text2, false, out type) || global::TypeUtility.Parse(text, true, out type) || global::TypeUtility.Parse(text2, true, out type);
		}
		return global::TypeUtility.Parse(text, true, out type);
	}

	// Token: 0x06000EC3 RID: 3779 RVA: 0x00038E90 File Offset: 0x00037090
	private static bool Parse(Type requiredBase, string text, bool ignoreCase, out Type type)
	{
		if (global::TypeUtility.Parse(text, ignoreCase, out type))
		{
			if (requiredBase.IsAssignableFrom(type))
			{
				return true;
			}
			type = null;
		}
		return false;
	}

	// Token: 0x06000EC4 RID: 3780 RVA: 0x00038EC0 File Offset: 0x000370C0
	private static bool Parse(Type requiredType, string text, out Type type)
	{
		if (global::TypeUtility.Parse(requiredType, text, false, out type))
		{
			return true;
		}
		if (global::TypeUtility.ContainsAQN(text))
		{
			string text2 = global::TypeUtility.g.StrippedName(text);
			return global::TypeUtility.Parse(requiredType, text2, false, out type) || global::TypeUtility.Parse(requiredType, text, true, out type) || global::TypeUtility.Parse(requiredType, text2, true, out type);
		}
		return global::TypeUtility.Parse(requiredType, text, true, out type);
	}

	// Token: 0x06000EC5 RID: 3781 RVA: 0x00038F24 File Offset: 0x00037124
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
		if (!global::TypeUtility.Parse(text, out result))
		{
			throw new ArgumentException("could not get type", text);
		}
		return result;
	}

	// Token: 0x06000EC6 RID: 3782 RVA: 0x00038F80 File Offset: 0x00037180
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
		if (!global::TypeUtility.Parse(typeof(TRequiredBaseClass), text, out result))
		{
			throw new ArgumentException("could not get type that would match base class " + typeof(TRequiredBaseClass), text);
		}
		return result;
	}

	// Token: 0x06000EC7 RID: 3783 RVA: 0x00038FF4 File Offset: 0x000371F4
	public static bool TryParse(string text, out Type type)
	{
		if (string.IsNullOrEmpty(text))
		{
			type = null;
			return false;
		}
		return global::TypeUtility.Parse(text, out type);
	}

	// Token: 0x06000EC8 RID: 3784 RVA: 0x00039010 File Offset: 0x00037210
	public static bool TryParse<TRequiredBaseClass>(string text, out Type type) where TRequiredBaseClass : class
	{
		if (string.IsNullOrEmpty(text))
		{
			type = null;
			return false;
		}
		return global::TypeUtility.Parse(typeof(TRequiredBaseClass), text, out type);
	}

	// Token: 0x06000EC9 RID: 3785 RVA: 0x00039034 File Offset: 0x00037234
	public static string VersionlessName(this Type type)
	{
		if (object.ReferenceEquals(type, null))
		{
			return null;
		}
		return global::TypeUtility.g.StrippedName(type);
	}

	// Token: 0x06000ECA RID: 3786 RVA: 0x0003904C File Offset: 0x0003724C
	public static string VersionlessName<T>()
	{
		return typeof(T).VersionlessName();
	}

	// Token: 0x04000951 RID: 2385
	private static bool ginit;

	// Token: 0x04000952 RID: 2386
	private static readonly string[] hintsAQN = new string[]
	{
		", Version=",
		", Culture=",
		", PublicKeyToken="
	};

	// Token: 0x0200021B RID: 539
	private static class g
	{
		// Token: 0x06000ECB RID: 3787 RVA: 0x00039060 File Offset: 0x00037260
		static g()
		{
			global::TypeUtility.ginit = true;
			global::TypeUtility.g.strippedNames = new Dictionary<Type, string>();
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x00039074 File Offset: 0x00037274
		public static string StrippedName(Type type)
		{
			string result;
			if (!global::TypeUtility.g.strippedNames.TryGetValue(type, out result))
			{
				result = (global::TypeUtility.g.strippedNames[type] = global::TypeUtility.g.expression.replace(type.AssemblyQualifiedName));
			}
			return result;
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x000390AC File Offset: 0x000372AC
		public static string StrippedName(string assemblyQualifiedName)
		{
			return global::TypeUtility.g.expression.replace(assemblyQualifiedName);
		}

		// Token: 0x04000953 RID: 2387
		private static readonly Dictionary<Type, string> strippedNames;

		// Token: 0x0200021C RID: 540
		private static class expression
		{
			// Token: 0x06000ECF RID: 3791 RVA: 0x000390F4 File Offset: 0x000372F4
			public static string replace(string assemblyQualifiedName)
			{
				return global::TypeUtility.g.expression.version.Replace(assemblyQualifiedName, string.Empty);
			}

			// Token: 0x04000954 RID: 2388
			private const RegexOptions kRegexOptions = RegexOptions.Compiled;

			// Token: 0x04000955 RID: 2389
			public static readonly Regex version = new Regex(", Version=\\d+.\\d+.\\d+.\\d+", RegexOptions.Compiled);

			// Token: 0x04000956 RID: 2390
			public static readonly Regex culture = new Regex(", Culture=\\w+", RegexOptions.Compiled);

			// Token: 0x04000957 RID: 2391
			public static readonly Regex publicKeyToken = new Regex(", PublicKeyToken=\\w+", RegexOptions.Compiled);
		}
	}
}
