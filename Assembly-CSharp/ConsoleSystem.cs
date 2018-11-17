using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Facepunch.Util;
using Facepunch.Utility;
using UnityEngine;

// Token: 0x0200017D RID: 381
public class ConsoleSystem
{
	// Token: 0x06000B86 RID: 2950 RVA: 0x0002D120 File Offset: 0x0002B320
	public static void RegisterLogCallback(Application.LogCallback Callback, bool CallbackWritesToConsole = false)
	{
		if (ConsoleSystem.RegisteredLogCallback)
		{
			if (Callback != ConsoleSystem.LogCallback)
			{
				if (object.ReferenceEquals(Callback, null))
				{
					Application.RegisterLogCallback(null);
					ConsoleSystem.LogCallbackWritesToConsole = (ConsoleSystem.RegisteredLogCallback = false);
					ConsoleSystem.LogCallback = null;
				}
				else
				{
					Application.RegisterLogCallback(Callback);
					ConsoleSystem.LogCallback = Callback;
					ConsoleSystem.LogCallbackWritesToConsole = CallbackWritesToConsole;
				}
			}
			else
			{
				ConsoleSystem.LogCallbackWritesToConsole = CallbackWritesToConsole;
			}
		}
		else if (!object.ReferenceEquals(Callback, null))
		{
			Application.RegisterLogCallback(Callback);
			ConsoleSystem.RegisteredLogCallback = true;
			ConsoleSystem.LogCallbackWritesToConsole = CallbackWritesToConsole;
			ConsoleSystem.LogCallback = Callback;
		}
	}

	// Token: 0x06000B87 RID: 2951 RVA: 0x0002D1B8 File Offset: 0x0002B3B8
	public static bool UnregisterLogCallback(Application.LogCallback Callback)
	{
		if (ConsoleSystem.RegisteredLogCallback && Callback == ConsoleSystem.LogCallback)
		{
			ConsoleSystem.RegisterLogCallback(null, false);
			return true;
		}
		return false;
	}

	// Token: 0x06000B88 RID: 2952 RVA: 0x0002D1EC File Offset: 0x0002B3EC
	public static void Print(object message, bool toLogFile = false)
	{
		ConsoleSystem.PrintLogType(3, message, toLogFile);
	}

	// Token: 0x06000B89 RID: 2953 RVA: 0x0002D1F8 File Offset: 0x0002B3F8
	public static void PrintWarning(object message, bool toLogFile = false)
	{
		ConsoleSystem.PrintLogType(2, message, toLogFile);
	}

	// Token: 0x06000B8A RID: 2954 RVA: 0x0002D204 File Offset: 0x0002B404
	public static void PrintError(object message, bool toLogFile = false)
	{
		ConsoleSystem.PrintLogType(0, message, toLogFile);
	}

	// Token: 0x06000B8B RID: 2955 RVA: 0x0002D210 File Offset: 0x0002B410
	public static void Log(object message)
	{
		Debug.Log(message);
	}

	// Token: 0x06000B8C RID: 2956 RVA: 0x0002D218 File Offset: 0x0002B418
	public static void Log(object message, Object context)
	{
		Debug.Log(message, context);
	}

	// Token: 0x06000B8D RID: 2957 RVA: 0x0002D224 File Offset: 0x0002B424
	public static void LogWarning(object message)
	{
		Debug.LogWarning(message);
	}

	// Token: 0x06000B8E RID: 2958 RVA: 0x0002D22C File Offset: 0x0002B42C
	public static void LogWarning(object message, Object context)
	{
		Debug.LogWarning(message, context);
	}

	// Token: 0x06000B8F RID: 2959 RVA: 0x0002D238 File Offset: 0x0002B438
	public static void LogError(object message)
	{
		Debug.LogError(message);
	}

	// Token: 0x06000B90 RID: 2960 RVA: 0x0002D240 File Offset: 0x0002B440
	public static void LogError(object message, Object context)
	{
		Debug.LogError(message, context);
	}

	// Token: 0x06000B91 RID: 2961 RVA: 0x0002D24C File Offset: 0x0002B44C
	public static void LogException(Exception exception)
	{
		Debug.LogException(exception);
	}

	// Token: 0x06000B92 RID: 2962 RVA: 0x0002D254 File Offset: 0x0002B454
	public static void LogException(Exception exception, Object context)
	{
		Debug.LogException(exception, context);
	}

	// Token: 0x06000B93 RID: 2963 RVA: 0x0002D260 File Offset: 0x0002B460
	private static void PrintLogType(LogType logType, string message, bool log = false)
	{
		if (global.logprint)
		{
			switch (logType)
			{
			case 0:
				ConsoleSystem.LogError(message);
				return;
			case 2:
				ConsoleSystem.LogWarning(message);
				return;
			case 3:
				ConsoleSystem.Log(message);
				return;
			}
		}
		if (log && !ConsoleSystem.LogCallbackWritesToConsole)
		{
			try
			{
				((logType != 3) ? Console.Error : Console.Out).WriteLine("Print{0}:{1}", logType, message);
			}
			catch (Exception arg)
			{
				Console.Error.WriteLine("PrintLogType Log Exception\n:{0}", arg);
			}
		}
		if (ConsoleSystem.RegisteredLogCallback)
		{
			try
			{
				ConsoleSystem.LogCallback.Invoke(message, string.Empty, logType);
			}
			catch (Exception arg2)
			{
				Console.Error.WriteLine("PrintLogType Exception\n:{0}", arg2);
			}
		}
	}

	// Token: 0x06000B94 RID: 2964 RVA: 0x0002D368 File Offset: 0x0002B568
	private static void PrintLogType(LogType logType, object obj, bool log = false)
	{
		ConsoleSystem.PrintLogType(logType, string.Concat(obj ?? "Null"), log);
	}

	// Token: 0x06000B95 RID: 2965 RVA: 0x0002D384 File Offset: 0x0002B584
	public static string CollectSavedFields(Type type)
	{
		string text = string.Empty;
		FieldInfo[] fields = type.GetFields();
		for (int i = 0; i < fields.Length; i++)
		{
			if (fields[i].IsStatic)
			{
				if (Reflection.HasAttribute(fields[i], typeof(ConsoleSystem.Saved)))
				{
					string text2 = type.Name + ".";
					if (text2 == "global.")
					{
						text2 = string.Empty;
					}
					string text3 = text;
					text = string.Concat(new string[]
					{
						text3,
						text2,
						fields[i].Name,
						" ",
						fields[i].GetValue(null).ToString(),
						"\n"
					});
				}
			}
		}
		return text;
	}

	// Token: 0x06000B96 RID: 2966 RVA: 0x0002D44C File Offset: 0x0002B64C
	public static string CollectSavedProperties(Type type)
	{
		string text = string.Empty;
		PropertyInfo[] properties = type.GetProperties();
		for (int i = 0; i < properties.Length; i++)
		{
			if (properties[i].GetGetMethod().IsStatic)
			{
				if (Reflection.HasAttribute(properties[i], typeof(ConsoleSystem.Saved)))
				{
					string text2 = type.Name + ".";
					if (text2 == "global.")
					{
						text2 = string.Empty;
					}
					string text3 = text;
					text = string.Concat(new string[]
					{
						text3,
						text2,
						properties[i].Name,
						" ",
						properties[i].GetValue(null, null).ToString(),
						"\n"
					});
				}
			}
		}
		return text;
	}

	// Token: 0x06000B97 RID: 2967 RVA: 0x0002D51C File Offset: 0x0002B71C
	public static string CollectSavedFunctions(Type type)
	{
		string text = string.Empty;
		MethodInfo[] methods = type.GetMethods();
		for (int i = 0; i < methods.Length; i++)
		{
			if (methods[i].IsStatic)
			{
				if (Reflection.HasAttribute(methods[i], typeof(ConsoleSystem.Saved)))
				{
					if (methods[i].ReturnType == typeof(string))
					{
						text += methods[i].Invoke(null, null);
					}
				}
			}
		}
		return text;
	}

	// Token: 0x06000B98 RID: 2968 RVA: 0x0002D5A8 File Offset: 0x0002B7A8
	public static string SaveToConfigString()
	{
		string text = string.Empty;
		Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
		for (int i = 0; i < assemblies.Length; i++)
		{
			Type[] types = assemblies[i].GetTypes();
			for (int j = 0; j < types.Length; j++)
			{
				if (types[j].IsSubclassOf(typeof(ConsoleSystem)))
				{
					text += ConsoleSystem.CollectSavedFields(types[j]);
					text += ConsoleSystem.CollectSavedProperties(types[j]);
					text += ConsoleSystem.CollectSavedFunctions(types[j]);
				}
			}
		}
		return text;
	}

	// Token: 0x06000B99 RID: 2969 RVA: 0x0002D64C File Offset: 0x0002B84C
	public static void RunFile(string strFile)
	{
		string[] array = strFile.Split(new char[]
		{
			'\n'
		}, StringSplitOptions.RemoveEmptyEntries);
		foreach (string text in array)
		{
			if (text[0] != '#')
			{
				ConsoleSystem.Run(text, false);
			}
		}
	}

	// Token: 0x06000B9A RID: 2970 RVA: 0x0002D6A4 File Offset: 0x0002B8A4
	public static bool Run(string strCommand, bool bWantsFeedback = false)
	{
		string empty = string.Empty;
		bool result = ConsoleSystem.RunCommand_Clientside(strCommand, out empty, bWantsFeedback);
		if (empty.Length > 0)
		{
			Debug.Log(empty);
		}
		return result;
	}

	// Token: 0x06000B9B RID: 2971 RVA: 0x0002D6D4 File Offset: 0x0002B8D4
	public static bool RunCommand_Clientside(string strCommand, out string StrOutput, bool bWantsFeedback = false)
	{
		StrOutput = string.Empty;
		ConsoleSystem.Arg arg = new ConsoleSystem.Arg(strCommand);
		if (arg.Invalid)
		{
			return false;
		}
		if (!ConsoleSystem.RunCommand(ref arg, bWantsFeedback))
		{
			return false;
		}
		if (arg.Reply != null && arg.Reply.Length > 0)
		{
			StrOutput = arg.Reply;
		}
		return true;
	}

	// Token: 0x06000B9C RID: 2972 RVA: 0x0002D730 File Offset: 0x0002B930
	public static bool RunCommand(ref ConsoleSystem.Arg arg, bool bWantReply = true)
	{
		Type[] array = ConsoleSystem.FindTypes(arg.Class);
		if (array.Length == 0)
		{
			if (bWantReply)
			{
				arg.ReplyWith("Console class not found: " + arg.Class);
			}
			return false;
		}
		if (bWantReply)
		{
			arg.ReplyWith(string.Concat(new string[]
			{
				"command ",
				arg.Class,
				".",
				arg.Function,
				" was executed"
			}));
		}
		Type[] array2 = array;
		int i = 0;
		while (i < array2.Length)
		{
			Type type = array2[i];
			MethodInfo method = type.GetMethod(arg.Function);
			if (method != null && method.IsStatic)
			{
				if (!arg.CheckPermissions(method.GetCustomAttributes(true)))
				{
					if (bWantReply)
					{
						arg.ReplyWith("No permission: " + arg.Class + "." + arg.Function);
					}
					return false;
				}
				object[] array3 = new ConsoleSystem.Arg[]
				{
					arg
				};
				try
				{
					method.Invoke(null, array3);
				}
				catch (Exception ex)
				{
					Debug.LogWarning(string.Concat(new string[]
					{
						"Error: ",
						arg.Class,
						".",
						arg.Function,
						" - ",
						ex.Message
					}));
					arg.ReplyWith(string.Concat(new string[]
					{
						"Error: ",
						arg.Class,
						".",
						arg.Function,
						" - ",
						ex.Message
					}));
					return false;
				}
				arg = (array3[0] as ConsoleSystem.Arg);
				return true;
			}
			else
			{
				FieldInfo field = type.GetField(arg.Function);
				if (field != null && field.IsStatic)
				{
					if (!arg.CheckPermissions(field.GetCustomAttributes(true)))
					{
						if (bWantReply)
						{
							arg.ReplyWith("No permission: " + arg.Class + "." + arg.Function);
						}
						return false;
					}
					Type fieldType = field.FieldType;
					if (arg.HasArgs(1))
					{
						try
						{
							string str = field.GetValue(null).ToString();
							if (fieldType == typeof(float))
							{
								field.SetValue(null, float.Parse(arg.Args[0]));
							}
							if (fieldType == typeof(int))
							{
								field.SetValue(null, int.Parse(arg.Args[0]));
							}
							if (fieldType == typeof(string))
							{
								field.SetValue(null, arg.Args[0]);
							}
							if (fieldType == typeof(bool))
							{
								field.SetValue(null, bool.Parse(arg.Args[0]));
							}
							if (bWantReply)
							{
								arg.ReplyWith(string.Concat(new string[]
								{
									arg.Class,
									".",
									arg.Function,
									": changed ",
									Facepunch.Utility.String.QuoteSafe(str),
									" to ",
									Facepunch.Utility.String.QuoteSafe(field.GetValue(null).ToString()),
									" (",
									fieldType.Name,
									")"
								}));
							}
						}
						catch (Exception)
						{
							if (bWantReply)
							{
								arg.ReplyWith("error setting value: " + arg.Class + "." + arg.Function);
							}
						}
					}
					else if (bWantReply)
					{
						arg.ReplyWith(string.Concat(new string[]
						{
							arg.Class,
							".",
							arg.Function,
							": ",
							Facepunch.Utility.String.QuoteSafe(field.GetValue(null).ToString()),
							" (",
							fieldType.Name,
							")"
						}));
					}
					return true;
				}
				else
				{
					PropertyInfo property = type.GetProperty(arg.Function);
					if (property != null && property.GetGetMethod().IsStatic && property.GetSetMethod().IsStatic)
					{
						if (!arg.CheckPermissions(property.GetCustomAttributes(true)))
						{
							if (bWantReply)
							{
								arg.ReplyWith("No permission: " + arg.Class + "." + arg.Function);
							}
							return false;
						}
						Type propertyType = property.PropertyType;
						if (arg.HasArgs(1))
						{
							try
							{
								string str2 = property.GetValue(null, null).ToString();
								if (propertyType == typeof(float))
								{
									property.SetValue(null, float.Parse(arg.Args[0]), null);
								}
								if (propertyType == typeof(int))
								{
									property.SetValue(null, int.Parse(arg.Args[0]), null);
								}
								if (propertyType == typeof(string))
								{
									property.SetValue(null, arg.Args[0], null);
								}
								if (propertyType == typeof(bool))
								{
									property.SetValue(null, bool.Parse(arg.Args[0]), null);
								}
								if (bWantReply)
								{
									arg.ReplyWith(string.Concat(new string[]
									{
										arg.Class,
										".",
										arg.Function,
										": changed ",
										Facepunch.Utility.String.QuoteSafe(str2),
										" to ",
										Facepunch.Utility.String.QuoteSafe(property.GetValue(null, null).ToString()),
										" (",
										propertyType.Name,
										")"
									}));
								}
							}
							catch (Exception)
							{
								if (bWantReply)
								{
									arg.ReplyWith("error setting value: " + arg.Class + "." + arg.Function);
								}
							}
						}
						else if (bWantReply)
						{
							arg.ReplyWith(string.Concat(new string[]
							{
								arg.Class,
								".",
								arg.Function,
								": ",
								Facepunch.Utility.String.QuoteSafe(property.GetValue(null, null).ToString()),
								" (",
								propertyType.Name,
								")"
							}));
						}
						return true;
					}
					else
					{
						i++;
					}
				}
			}
		}
		if (bWantReply)
		{
			arg.ReplyWith("Command not found: " + arg.Class + "." + arg.Function);
		}
		return false;
	}

	// Token: 0x06000B9D RID: 2973 RVA: 0x0002DE38 File Offset: 0x0002C038
	public static Type[] FindTypes(string className)
	{
		List<Type> list = new List<Type>();
		Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
		for (int i = 0; i < assemblies.Length; i++)
		{
			Type type = assemblies[i].GetType(className);
			if (type != null)
			{
				if (type.IsSubclassOf(typeof(ConsoleSystem)))
				{
					list.Add(type);
				}
			}
		}
		return list.ToArray();
	}

	// Token: 0x0400072D RID: 1837
	private static bool RegisteredLogCallback;

	// Token: 0x0400072E RID: 1838
	private static bool LogCallbackWritesToConsole;

	// Token: 0x0400072F RID: 1839
	private static Application.LogCallback LogCallback;

	// Token: 0x0200017E RID: 382
	public class Arg
	{
		// Token: 0x06000B9E RID: 2974 RVA: 0x0002DEA8 File Offset: 0x0002C0A8
		public Arg(string rconCommand)
		{
			rconCommand = ConsoleSystem.Arg.RemoveInvalidCharacters(rconCommand);
			if (rconCommand.IndexOf('.') <= 0 || rconCommand.IndexOf(' ', 0, rconCommand.IndexOf('.')) != -1)
			{
				rconCommand = "global." + rconCommand;
			}
			if (rconCommand.IndexOf('.') <= 0)
			{
				return;
			}
			this.Class = rconCommand.Substring(0, rconCommand.IndexOf('.'));
			if (this.Class.Length <= 1)
			{
				return;
			}
			this.Class = this.Class.ToLower();
			this.Function = rconCommand.Substring(this.Class.Length + 1);
			if (this.Function.Length <= 1)
			{
				return;
			}
			this.Invalid = false;
			if (this.Function.IndexOf(' ') <= 0)
			{
				return;
			}
			this.ArgsStr = this.Function.Substring(this.Function.IndexOf(' '));
			this.ArgsStr = this.ArgsStr.Trim();
			this.Args = Facepunch.Utility.String.SplitQuotesStrings(this.ArgsStr);
			this.Function = this.Function.Substring(0, this.Function.IndexOf(' '));
			this.Function.ToLower();
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x0002E020 File Offset: 0x0002C220
		private static string RemoveInvalidCharacters(string str)
		{
			if (str == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in str)
			{
				if (char.IsLetterOrDigit(c) || char.IsPunctuation(c) || char.IsSeparator(c) || char.IsSymbol(c))
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0002E094 File Offset: 0x0002C294
		public bool CheckPermissions(object[] attributes)
		{
			foreach (object obj in attributes)
			{
				if (obj is ConsoleSystem.Client)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0002E0CC File Offset: 0x0002C2CC
		public void ReplyWith(string strValue)
		{
			this.Reply = strValue;
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x0002E0D8 File Offset: 0x0002C2D8
		public bool HasArgs(int iMinimum = 1)
		{
			return this.Args != null && this.Args.Length >= iMinimum;
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x0002E0F8 File Offset: 0x0002C2F8
		public string GetString(int iArg, string def = "")
		{
			if (this.HasArgs(iArg + 1))
			{
				return ConsoleSystem.Parse.DefaultString(this.Args[iArg], def);
			}
			return def;
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0002E118 File Offset: 0x0002C318
		public int GetInt(int iArg, int def = 0)
		{
			return ConsoleSystem.Parse.DefaultInt(this.GetString(iArg, null), def);
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0002E128 File Offset: 0x0002C328
		public ulong GetUInt64(int iArg, ulong def = 0UL)
		{
			return ConsoleSystem.Parse.DefaultUInt64(this.GetString(iArg, null), def);
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0002E138 File Offset: 0x0002C338
		public float GetFloat(int iArg, float def = 0f)
		{
			return ConsoleSystem.Parse.DefaultFloat(this.GetString(iArg, null), def);
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0002E148 File Offset: 0x0002C348
		public bool GetBool(int iArg, bool def = false)
		{
			return ConsoleSystem.Parse.DefaultBool(this.GetString(iArg, null), def);
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x0002E158 File Offset: 0x0002C358
		public Enum GetEnum(Type enumType, int iArg, Enum def)
		{
			return ConsoleSystem.Parse.DefaultEnum(enumType, this.GetString(iArg, null), def);
		}

		// Token: 0x04000730 RID: 1840
		public string Class = string.Empty;

		// Token: 0x04000731 RID: 1841
		public string Function = string.Empty;

		// Token: 0x04000732 RID: 1842
		public string ArgsStr = string.Empty;

		// Token: 0x04000733 RID: 1843
		public string[] Args;

		// Token: 0x04000734 RID: 1844
		public bool Invalid = true;

		// Token: 0x04000735 RID: 1845
		public string Reply = string.Empty;
	}

	// Token: 0x0200017F RID: 383
	public static class Parse
	{
		// Token: 0x06000BA9 RID: 2985 RVA: 0x0002E16C File Offset: 0x0002C36C
		public static float Float(string text)
		{
			return float.Parse(text);
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0002E174 File Offset: 0x0002C374
		public static bool AttemptFloat(string text, out float value)
		{
			return float.TryParse(text, out value);
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x0002E180 File Offset: 0x0002C380
		public static float DefaultFloat(string text, float @default)
		{
			float result;
			if (object.ReferenceEquals(text, null) || !ConsoleSystem.Parse.AttemptFloat(text, out result))
			{
				result = @default;
			}
			return result;
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x0002E1AC File Offset: 0x0002C3AC
		public static float DefaultFloat(string text)
		{
			return ConsoleSystem.Parse.DefaultFloat(text, 0f);
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x0002E1BC File Offset: 0x0002C3BC
		public static int Int(string text)
		{
			return int.Parse(text);
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0002E1C4 File Offset: 0x0002C3C4
		public static bool AttemptInt(string text, out int value)
		{
			return int.TryParse(text, out value);
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0002E1D0 File Offset: 0x0002C3D0
		public static int DefaultInt(string text, int @default)
		{
			int result;
			if (object.ReferenceEquals(text, null) || !ConsoleSystem.Parse.AttemptInt(text, out result))
			{
				result = @default;
			}
			return result;
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0002E1FC File Offset: 0x0002C3FC
		public static ulong DefaultUInt64(string text, ulong @default)
		{
			if (text == null)
			{
				return @default;
			}
			ulong result = @default;
			ulong.TryParse(text, out result);
			return result;
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0002E220 File Offset: 0x0002C420
		public static int DefaultInt(string text)
		{
			return ConsoleSystem.Parse.DefaultInt(text, 0);
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0002E22C File Offset: 0x0002C42C
		public static bool AttemptBool(string text, out bool value)
		{
			if (bool.TryParse(text, out value))
			{
				return true;
			}
			if (text.Length != 0)
			{
				decimal d2;
				if (char.IsLetter(text[0]))
				{
					decimal d;
					if (text.Length == 4)
					{
						if (string.Equals(text, "true", StringComparison.InvariantCultureIgnoreCase))
						{
							value = true;
							return true;
						}
					}
					else if (text.Length == 5)
					{
						if (string.Equals(text, "false", StringComparison.InvariantCultureIgnoreCase))
						{
							value = false;
							return true;
						}
					}
					else if (decimal.TryParse(text, out d))
					{
						value = (d != 0m);
						return true;
					}
				}
				else if (decimal.TryParse(text, out d2))
				{
					value = (d2 != 0m);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0002E2F4 File Offset: 0x0002C4F4
		public static bool Bool(string text)
		{
			bool result;
			if (!ConsoleSystem.Parse.AttemptBool(text, out result))
			{
				throw new FormatException("not in the correct format.");
			}
			return result;
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0002E31C File Offset: 0x0002C51C
		public static bool DefaultBool(string text, bool @default)
		{
			bool result;
			if (object.ReferenceEquals(text, null) || !ConsoleSystem.Parse.AttemptBool(text, out result))
			{
				result = @default;
			}
			return result;
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0002E348 File Offset: 0x0002C548
		public static bool DefaultBool(string text)
		{
			return ConsoleSystem.Parse.DefaultBool(text, false);
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0002E354 File Offset: 0x0002C554
		public static TEnum Enum<TEnum>(string text) where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			return ConsoleSystem.Parse.VerifyEnum<TEnum>.Parse(text);
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x0002E35C File Offset: 0x0002C55C
		public static bool AttemptEnum<TEnum>(string text, out TEnum value) where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			return ConsoleSystem.Parse.VerifyEnum<TEnum>.TryParse(text, out value);
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0002E368 File Offset: 0x0002C568
		public static TEnum DefaultEnum<TEnum>(string text, TEnum @default) where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			TEnum result;
			if (object.ReferenceEquals(text, null) || !ConsoleSystem.Parse.AttemptEnum<TEnum>(text, out result))
			{
				result = @default;
			}
			return result;
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x0002E394 File Offset: 0x0002C594
		public static TEnum DefaultEnum<TEnum>(string text) where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			return ConsoleSystem.Parse.DefaultEnum<TEnum>(text, default(TEnum));
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x0002E3B0 File Offset: 0x0002C5B0
		public static Enum Enum(Type enumType, string text)
		{
			Enum result;
			try
			{
				result = (Enum)System.Enum.Parse(enumType, text, true);
			}
			catch (Exception ex)
			{
				try
				{
					result = (Enum)System.Enum.ToObject(enumType, long.Parse(text));
				}
				catch
				{
					throw ex;
				}
			}
			return result;
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x0002E434 File Offset: 0x0002C634
		public static bool AttemptEnum(Type enumType, string text, out Enum value)
		{
			bool result;
			try
			{
				value = (Enum)System.Enum.Parse(enumType, text, true);
				result = true;
			}
			catch
			{
				try
				{
					value = (Enum)System.Enum.ToObject(enumType, long.Parse(text));
					result = true;
				}
				catch
				{
					value = null;
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x0002E4C8 File Offset: 0x0002C6C8
		public static Enum DefaultEnum(Type enumType, string text, Enum @default)
		{
			Enum result;
			if (object.ReferenceEquals(text, null) || !ConsoleSystem.Parse.AttemptEnum(enumType, text, out result))
			{
				result = @default;
			}
			return result;
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x0002E4F4 File Offset: 0x0002C6F4
		public static Enum DefaultEnum(Type enumType, string text)
		{
			return ConsoleSystem.Parse.DefaultEnum(enumType, text, null);
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x0002E500 File Offset: 0x0002C700
		public static string String(string text)
		{
			if (object.ReferenceEquals(text, null))
			{
				throw new ArgumentNullException("text");
			}
			if (text.Length == 1)
			{
				throw new FormatException("Cannot use empty strings.");
			}
			return text;
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0002E534 File Offset: 0x0002C734
		public static bool AttemptString(string text, out string value)
		{
			if (string.IsNullOrEmpty(text))
			{
				value = string.Empty;
				return false;
			}
			value = text;
			return true;
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x0002E550 File Offset: 0x0002C750
		public static string DefaultString(string text, string @default)
		{
			string result;
			if (!ConsoleSystem.Parse.AttemptString(text, out result))
			{
				result = @default;
			}
			return result;
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x0002E570 File Offset: 0x0002C770
		public static string DefaultString(string text)
		{
			return ConsoleSystem.Parse.DefaultString(text, string.Empty);
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x0002E580 File Offset: 0x0002C780
		public static bool IsSupported(Type type)
		{
			if (object.ReferenceEquals(type, null))
			{
				return false;
			}
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Boolean:
				return typeof(bool) == type;
			case TypeCode.SByte:
			case TypeCode.Byte:
			case TypeCode.Int16:
			case TypeCode.UInt16:
			case TypeCode.UInt32:
			case TypeCode.Int64:
			case TypeCode.UInt64:
				return type.IsEnum;
			case TypeCode.Int32:
				return typeof(int) == type || type.IsEnum;
			case TypeCode.Single:
				return typeof(float) == type;
			case TypeCode.String:
				return typeof(string) == type;
			}
			return false;
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0002E63C File Offset: 0x0002C83C
		public static bool IsSupported<T>()
		{
			return ConsoleSystem.Parse.PrecachedSupport<T>.IsSupported;
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x0002E644 File Offset: 0x0002C844
		public static bool AttemptObject(Type type, string value, out object boxed)
		{
			try
			{
				switch (Type.GetTypeCode(type))
				{
				case TypeCode.Boolean:
					if (typeof(bool) == type)
					{
						boxed = ConsoleSystem.Parse.Bool(value);
						return true;
					}
					break;
				case TypeCode.SByte:
				case TypeCode.Byte:
				case TypeCode.Int16:
				case TypeCode.UInt16:
				case TypeCode.UInt32:
				case TypeCode.Int64:
				case TypeCode.UInt64:
					if (type.IsEnum)
					{
						boxed = ConsoleSystem.Parse.Enum(type, value);
						return true;
					}
					break;
				case TypeCode.Int32:
					if (type == typeof(int))
					{
						boxed = ConsoleSystem.Parse.Int(value);
					}
					else
					{
						if (!type.IsEnum)
						{
							break;
						}
						boxed = ConsoleSystem.Parse.Enum(type, value);
					}
					return true;
				case TypeCode.Single:
					if (typeof(float) == type)
					{
						boxed = ConsoleSystem.Parse.Float(value);
						return true;
					}
					break;
				case TypeCode.String:
					if (typeof(string) == type)
					{
						boxed = ConsoleSystem.Parse.String(value);
						return true;
					}
					break;
				}
			}
			catch (Exception ex)
			{
				boxed = ex;
				return false;
			}
			boxed = null;
			return false;
		}

		// Token: 0x04000736 RID: 1846
		private const bool kEnumCaseInsensitive = true;

		// Token: 0x02000180 RID: 384
		private static class VerifyEnum<TEnum> where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			// Token: 0x06000BC5 RID: 3013 RVA: 0x0002E7C0 File Offset: 0x0002C9C0
			static VerifyEnum()
			{
				if (!typeof(TEnum).IsEnum)
				{
					throw new ArgumentException("TEnum", "Is not a enum type");
				}
			}

			// Token: 0x06000BC6 RID: 3014 RVA: 0x0002E7F4 File Offset: 0x0002C9F4
			public static bool TryParse(string text, out TEnum value)
			{
				bool result;
				try
				{
					value = (TEnum)((object)System.Enum.Parse(typeof(TEnum), text, true));
					result = true;
				}
				catch
				{
					try
					{
						value = (TEnum)((object)System.Enum.ToObject(typeof(TEnum), long.Parse(text)));
						result = true;
					}
					catch
					{
						value = default(TEnum);
						result = false;
					}
				}
				return result;
			}

			// Token: 0x06000BC7 RID: 3015 RVA: 0x0002E8AC File Offset: 0x0002CAAC
			public static TEnum Parse(string text)
			{
				TEnum result;
				try
				{
					result = (TEnum)((object)System.Enum.Parse(typeof(TEnum), text, true));
				}
				catch (Exception ex)
				{
					try
					{
						result = (TEnum)((object)System.Enum.ToObject(typeof(TEnum), long.Parse(text)));
					}
					catch
					{
						throw ex;
					}
				}
				return result;
			}
		}

		// Token: 0x02000181 RID: 385
		private static class PrecachedSupport<T>
		{
			// Token: 0x04000737 RID: 1847
			public static readonly bool IsSupported = ConsoleSystem.Parse.IsSupported(typeof(T));
		}
	}

	// Token: 0x02000182 RID: 386
	[AttributeUsage(AttributeTargets.All)]
	public sealed class Admin : Attribute
	{
	}

	// Token: 0x02000183 RID: 387
	[AttributeUsage(AttributeTargets.All)]
	public sealed class User : Attribute
	{
	}

	// Token: 0x02000184 RID: 388
	[AttributeUsage(AttributeTargets.All)]
	public sealed class Client : Attribute
	{
	}

	// Token: 0x02000185 RID: 389
	[AttributeUsage(AttributeTargets.All)]
	public sealed class Saved : Attribute
	{
	}

	// Token: 0x02000186 RID: 390
	[AttributeUsage(AttributeTargets.All)]
	public sealed class Help : Attribute
	{
		// Token: 0x06000BCD RID: 3021 RVA: 0x0002E97C File Offset: 0x0002CB7C
		public Help(string strHelp, string strArgs = "")
		{
			this.helpDescription = strHelp;
			this.argsDescription = strArgs;
		}

		// Token: 0x04000738 RID: 1848
		public string helpDescription;

		// Token: 0x04000739 RID: 1849
		public string argsDescription;
	}
}
